using System;
using System.Data;
using System.Data.OleDb;
using LectureAssessmentManager.Data;
using LectureAssessmentManager.Models;

namespace LectureAssessmentManager.Business
{
    public class CourseManager
    {
        public static DataTable GetAllCourses()
        {
            string query = @"SELECT C.*, L.Name AS LecturerName 
                           FROM Courses C 
                           INNER JOIN Lecturers L ON C.LecturerId = L.LecturerId 
                           ORDER BY C.CourseId";
            return DatabaseHelper.ExecuteQuery(query);
        }

        public static Course GetCourseById(string courseId)
        {
            string query = "SELECT * FROM Courses WHERE CourseId = @CourseId";
            var parameter = new OleDbParameter("@CourseId", courseId);

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameter);
            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            return new Course
            {
                CourseId = row["CourseId"].ToString(),
                Name = row["Name"].ToString(),
                Description = row["Description"].ToString(),
                LecturerId = Convert.ToInt32(row["LecturerId"])
            };
        }

        public static bool IsCourseIdUnique(string courseId)
        {
            string query = "SELECT COUNT(*) FROM Courses WHERE CourseId = @CourseId";
            var parameter = new OleDbParameter("@CourseId", courseId);

            var result = DatabaseHelper.ExecuteQuery(query, parameter);
            return Convert.ToInt32(result.Rows[0][0]) == 0;
        }

        public static int AddCourse(Course course)
        {
            if (string.IsNullOrWhiteSpace(course.CourseId))
                throw new ArgumentException("Course ID is required.");

            if (string.IsNullOrWhiteSpace(course.Name))
                throw new ArgumentException("Course name is required.");

            if (course.LecturerId <= 0)
                throw new ArgumentException("Valid lecturer assignment is required.");

            if (!IsCourseIdUnique(course.CourseId))
                throw new ArgumentException("Course ID must be unique.");

            string query = @"INSERT INTO Courses (CourseId, Name, Description, LecturerId) 
                           VALUES (@CourseId, @Name, @Description, @LecturerId)";

            var parameters = new[]
            {
                new OleDbParameter("@CourseId", course.CourseId),
                new OleDbParameter("@Name", course.Name),
                new OleDbParameter("@Description", course.Description ?? (object)DBNull.Value),
                new OleDbParameter("@LecturerId", course.LecturerId)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public static bool UpdateCourse(Course course)
        {
            if (string.IsNullOrWhiteSpace(course.CourseId))
                throw new ArgumentException("Course ID is required.");

            if (string.IsNullOrWhiteSpace(course.Name))
                throw new ArgumentException("Course name is required.");

            if (course.LecturerId <= 0)
                throw new ArgumentException("Valid lecturer assignment is required.");

            string query = @"UPDATE Courses 
                           SET Name = @Name, 
                               Description = @Description, 
                               LecturerId = @LecturerId 
                           WHERE CourseId = @CourseId";

            var parameters = new[]
            {
                new OleDbParameter("@Name", course.Name),
                new OleDbParameter("@Description", course.Description ?? (object)DBNull.Value),
                new OleDbParameter("@LecturerId", course.LecturerId),
                new OleDbParameter("@CourseId", course.CourseId)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        public static bool DeleteCourse(string courseId)
        {
            // First check if the course has any assignments
            string checkQuery = "SELECT COUNT(*) FROM Assignments WHERE CourseId = @CourseId";
            var checkParam = new OleDbParameter("@CourseId", courseId);

            var result = DatabaseHelper.ExecuteQuery(checkQuery, checkParam);
            if (Convert.ToInt32(result.Rows[0][0]) > 0)
                throw new InvalidOperationException("Cannot delete course with existing assignments.");

            string query = "DELETE FROM Courses WHERE CourseId = @CourseId";
            var parameter = new OleDbParameter("@CourseId", courseId);

            return DatabaseHelper.ExecuteNonQuery(query, parameter) > 0;
        }

        public static DataTable GetCoursesForLecturer(int lecturerId)
        {
            string query = "SELECT * FROM Courses WHERE LecturerId = @LecturerId ORDER BY CourseId";
            var parameter = new OleDbParameter("@LecturerId", lecturerId);
            return DatabaseHelper.ExecuteQuery(query, parameter);
        }
    }
}