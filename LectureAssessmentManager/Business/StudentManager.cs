using System;
using System.Data;
using System.Data.OleDb;
using LectureAssessmentManager.Data;
using LectureAssessmentManager.Models;

namespace LectureAssessmentManager.Business
{
    public class StudentManager
    {
        public static DataTable GetAllStudents()
        {
            string query = "SELECT * FROM Students ORDER BY Name";
            return DatabaseHelper.ExecuteQuery(query);
        }

        public static Student GetStudentById(string studentId)
        {
            string query = "SELECT * FROM Students WHERE StudentId = @StudentId";
            var parameter = new OleDbParameter("@StudentId", studentId);

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameter);
            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            return new Student
            {
                StudentId = row["StudentId"].ToString(),
                Name = row["Name"].ToString(),
                Email = row["Email"].ToString()
            };
        }

        public static bool IsStudentIdUnique(string studentId)
        {
            string query = "SELECT COUNT(*) FROM Students WHERE StudentId = @StudentId";
            var parameter = new OleDbParameter("@StudentId", studentId);

            var result = DatabaseHelper.ExecuteQuery(query, parameter);
            return Convert.ToInt32(result.Rows[0][0]) == 0;
        }

        public static int AddStudent(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.StudentId))
                throw new ArgumentException("Student ID is required.");

            if (string.IsNullOrWhiteSpace(student.Name))
                throw new ArgumentException("Student name is required.");

            if (string.IsNullOrWhiteSpace(student.Email))
                throw new ArgumentException("Email is required.");

            if (!IsStudentIdUnique(student.StudentId))
                throw new ArgumentException("Student ID must be unique.");

            string query = @"INSERT INTO Students (StudentId, Name, Email) 
                           VALUES (@StudentId, @Name, @Email)";

            var parameters = new[]
            {
                new OleDbParameter("@StudentId", student.StudentId),
                new OleDbParameter("@Name", student.Name),
                new OleDbParameter("@Email", student.Email)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public static bool UpdateStudent(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.StudentId))
                throw new ArgumentException("Student ID is required.");

            if (string.IsNullOrWhiteSpace(student.Name))
                throw new ArgumentException("Student name is required.");

            if (string.IsNullOrWhiteSpace(student.Email))
                throw new ArgumentException("Email is required.");

            string query = @"UPDATE Students 
                           SET Name = @Name, 
                               Email = @Email 
                           WHERE StudentId = @StudentId";

            var parameters = new[]
            {
                new OleDbParameter("@Name", student.Name),
                new OleDbParameter("@Email", student.Email),
                new OleDbParameter("@StudentId", student.StudentId)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        public static bool DeleteStudent(string studentId)
        {
            // First check if the student has any assessments
            string checkQuery = "SELECT COUNT(*) FROM StudentAssessments WHERE StudentId = @StudentId";
            var checkParam = new OleDbParameter("@StudentId", studentId);

            var result = DatabaseHelper.ExecuteQuery(checkQuery, checkParam);
            if (Convert.ToInt32(result.Rows[0][0]) > 0)
                throw new InvalidOperationException("Cannot delete student with existing assessments.");

            string query = "DELETE FROM Students WHERE StudentId = @StudentId";
            var parameter = new OleDbParameter("@StudentId", studentId);

            return DatabaseHelper.ExecuteNonQuery(query, parameter) > 0;
        }

        // Methods for managing student enrollments in courses
        public static DataTable GetStudentCourses(string studentId)
        {
            string query = @"SELECT C.CourseId, C.Name AS CourseName, L.Name AS LecturerName
                           FROM Courses C
                           INNER JOIN Lecturers L ON C.LecturerId = L.LecturerId
                           WHERE C.CourseId IN (
                               SELECT CourseId 
                               FROM StudentCourses 
                               WHERE StudentId = @StudentId
                           )
                           ORDER BY C.CourseId";

            var parameter = new OleDbParameter("@StudentId", studentId);
            return DatabaseHelper.ExecuteQuery(query, parameter);
        }

        public static DataTable GetAvailableCourses(string studentId)
        {
            string query = @"SELECT C.CourseId, C.Name AS CourseName, L.Name AS LecturerName
                           FROM Courses C
                           INNER JOIN Lecturers L ON C.LecturerId = L.LecturerId
                           WHERE C.CourseId NOT IN (
                               SELECT CourseId 
                               FROM StudentCourses 
                               WHERE StudentId = @StudentId
                           )
                           ORDER BY C.CourseId";

            var parameter = new OleDbParameter("@StudentId", studentId);
            return DatabaseHelper.ExecuteQuery(query, parameter);
        }

        public static void EnrollStudentInCourse(string studentId, string courseId)
        {
            string query = @"INSERT INTO StudentCourses (StudentId, CourseId) 
                           VALUES (@StudentId, @CourseId)";

            var parameters = new[]
            {
                new OleDbParameter("@StudentId", studentId),
                new OleDbParameter("@CourseId", courseId)
            };

            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public static void RemoveStudentFromCourse(string studentId, string courseId)
        {
            // First check if the student has any assessments in this course
            string checkQuery = @"SELECT COUNT(*) 
                                FROM StudentAssessments SA
                                INNER JOIN Assignments A ON SA.AssignmentId = A.AssignmentId
                                WHERE SA.StudentId = @StudentId AND A.CourseId = @CourseId";

            var checkParams = new[]
            {
                new OleDbParameter("@StudentId", studentId),
                new OleDbParameter("@CourseId", courseId)
            };

            var result = DatabaseHelper.ExecuteQuery(checkQuery, checkParams);
            if (Convert.ToInt32(result.Rows[0][0]) > 0)
                throw new InvalidOperationException("Cannot remove student from course with existing assessments.");

            string query = @"DELETE FROM StudentCourses 
                           WHERE StudentId = @StudentId AND CourseId = @CourseId";

            DatabaseHelper.ExecuteNonQuery(query, checkParams);
        }
    }
}