using System;
using System.Data;
using System.Data.OleDb;
using LectureAssessmentManager.Data;
using LectureAssessmentManager.Models;

namespace LectureAssessmentManager.Business
{
    public class AssignmentManager
    {
        public static DataTable GetAssignmentsByCourse(string courseId)
        {
            string query = @"SELECT AssignmentId, Title, Description, DueDate, MaxScore 
                           FROM Assignments 
                           WHERE CourseId = @CourseId 
                           ORDER BY DueDate";

            var parameter = new OleDbParameter("@CourseId", courseId);
            return DatabaseHelper.ExecuteQuery(query, parameter);
        }

        public static Assignment GetAssignmentById(string assignmentId)
        {
            string query = @"SELECT * FROM Assignments WHERE AssignmentId = @AssignmentId";
            var parameter = new OleDbParameter("@AssignmentId", assignmentId);

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameter);
            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            return new Assignment
            {
                AssignmentId = row["AssignmentId"].ToString(),
                CourseId = row["CourseId"].ToString(),
                Title = row["Title"].ToString(),
                Description = row["Description"].ToString(),
                DueDate = Convert.ToDateTime(row["DueDate"]),
                MaxScore = Convert.ToInt32(row["MaxScore"])
            };
        }

        public static bool IsAssignmentIdUnique(string assignmentId)
        {
            string query = "SELECT COUNT(*) FROM Assignments WHERE AssignmentId = @AssignmentId";
            var parameter = new OleDbParameter("@AssignmentId", assignmentId);

            var result = DatabaseHelper.ExecuteQuery(query, parameter);
            return Convert.ToInt32(result.Rows[0][0]) == 0;
        }

        public static int AddAssignment(Assignment assignment)
        {
            if (string.IsNullOrWhiteSpace(assignment.AssignmentId))
                throw new ArgumentException("Assignment ID is required.");

            if (string.IsNullOrWhiteSpace(assignment.CourseId))
                throw new ArgumentException("Course ID is required.");

            if (string.IsNullOrWhiteSpace(assignment.Title))
                throw new ArgumentException("Title is required.");

            if (!IsAssignmentIdUnique(assignment.AssignmentId))
                throw new ArgumentException("Assignment ID must be unique.");

            string query = @"INSERT INTO Assignments (AssignmentId, CourseId, Title, Description, DueDate, MaxScore) 
                           VALUES (@AssignmentId, @CourseId, @Title, @Description, @DueDate, @MaxScore)";

            var parameters = new[]
            {
                new OleDbParameter("@AssignmentId", assignment.AssignmentId),
                new OleDbParameter("@CourseId", assignment.CourseId),
                new OleDbParameter("@Title", assignment.Title),
                new OleDbParameter("@Description", assignment.Description ?? (object)DBNull.Value),
                new OleDbParameter("@DueDate", assignment.DueDate),
                new OleDbParameter("@MaxScore", assignment.MaxScore)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public static bool UpdateAssignment(Assignment assignment)
        {
            if (string.IsNullOrWhiteSpace(assignment.AssignmentId))
                throw new ArgumentException("Assignment ID is required.");

            if (string.IsNullOrWhiteSpace(assignment.Title))
                throw new ArgumentException("Title is required.");

            string query = @"UPDATE Assignments 
                           SET Title = @Title, 
                               Description = @Description, 
                               DueDate = @DueDate, 
                               MaxScore = @MaxScore 
                           WHERE AssignmentId = @AssignmentId";

            var parameters = new[]
            {
                new OleDbParameter("@Title", assignment.Title),
                new OleDbParameter("@Description", assignment.Description ?? (object)DBNull.Value),
                new OleDbParameter("@DueDate", assignment.DueDate),
                new OleDbParameter("@MaxScore", assignment.MaxScore),
                new OleDbParameter("@AssignmentId", assignment.AssignmentId)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        public static bool DeleteAssignment(string assignmentId)
        {
            // First check if the assignment has any grades
            string checkQuery = "SELECT COUNT(*) FROM StudentAssessments WHERE AssignmentId = @AssignmentId";
            var checkParam = new OleDbParameter("@AssignmentId", assignmentId);

            var result = DatabaseHelper.ExecuteQuery(checkQuery, checkParam);
            if (Convert.ToInt32(result.Rows[0][0]) > 0)
                throw new InvalidOperationException("Cannot delete assignment with existing grades.");

            string query = "DELETE FROM Assignments WHERE AssignmentId = @AssignmentId";
            var parameter = new OleDbParameter("@AssignmentId", assignmentId);

            return DatabaseHelper.ExecuteNonQuery(query, parameter) > 0;
        }

        public static DataTable GetStudentsForGrading(string assignmentId)
        {
            string query = @"SELECT s.StudentId, s.Name, 
                                  ISNULL(sa.Score, 0) AS Score,
                                  CASE WHEN sa.AssignmentId IS NULL THEN 'Not Graded' ELSE 'Graded' END AS Status
                           FROM Students s
                           INNER JOIN StudentCourses sc ON s.StudentId = sc.StudentId
                           INNER JOIN Assignments a ON sc.CourseId = a.CourseId
                           LEFT JOIN StudentAssessments sa ON s.StudentId = sa.StudentId 
                                AND sa.AssignmentId = a.AssignmentId
                           WHERE a.AssignmentId = @AssignmentId
                           ORDER BY s.Name";

            var parameter = new OleDbParameter("@AssignmentId", assignmentId);
            return DatabaseHelper.ExecuteQuery(query, parameter);
        }

        public static Grade GetStudentGrade(string assignmentId, string studentId)
        {
            string query = @"SELECT * FROM StudentAssessments 
                           WHERE AssignmentId = @AssignmentId AND StudentId = @StudentId";

            var parameters = new[]
            {
                new OleDbParameter("@AssignmentId", assignmentId),
                new OleDbParameter("@StudentId", studentId)
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            return new Grade
            {
                AssignmentId = assignmentId,
                StudentId = studentId,
                Score = Convert.ToInt32(row["Score"]),
                Feedback = row["Feedback"] as string
            };
        }

        public static bool SaveGrade(Grade grade)
        {
            // Check if grade already exists
            var existingGrade = GetStudentGrade(grade.AssignmentId, grade.StudentId);
            string query;

            if (existingGrade == null)
            {
                query = @"INSERT INTO StudentAssessments (AssignmentId, StudentId, Score, Feedback) 
                         VALUES (@AssignmentId, @StudentId, @Score, @Feedback)";
            }
            else
            {
                query = @"UPDATE StudentAssessments 
                         SET Score = @Score, Feedback = @Feedback 
                         WHERE AssignmentId = @AssignmentId AND StudentId = @StudentId";
            }

            var parameters = new[]
            {
                new OleDbParameter("@AssignmentId", grade.AssignmentId),
                new OleDbParameter("@StudentId", grade.StudentId),
                new OleDbParameter("@Score", grade.Score),
                new OleDbParameter("@Feedback", grade.Feedback ?? (object)DBNull.Value)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}