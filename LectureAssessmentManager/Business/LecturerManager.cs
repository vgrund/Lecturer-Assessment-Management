using System;
using System.Data;
using System.Data.OleDb;
using LectureAssessmentManager.Data;
using LectureAssessmentManager.Models;

namespace LectureAssessmentManager.Business
{
    public class LecturerManager
    {
        public static DataTable GetAllLecturers()
        {
            string query = "SELECT * FROM Lecturers ORDER BY Name";
            return DatabaseHelper.ExecuteQuery(query);
        }

        public static Lecturer GetLecturerById(int lecturerId)
        {
            string query = "SELECT * FROM Lecturers WHERE LecturerId = @LecturerId";
            var parameter = new OleDbParameter("@LecturerId", lecturerId);

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameter);
            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            return new Lecturer
            {
                LecturerId = Convert.ToInt32(row["LecturerId"]),
                Name = row["Name"].ToString(),
                Department = row["Department"].ToString(),
                Email = row["Email"].ToString()
            };
        }

        public static int AddLecturer(Lecturer lecturer)
        {
            if (string.IsNullOrWhiteSpace(lecturer.Name))
                throw new ArgumentException("Lecturer name is required.");

            if (string.IsNullOrWhiteSpace(lecturer.Email))
                throw new ArgumentException("Email is required.");

            string query = @"INSERT INTO Lecturers (Name, Department, Email) 
                           VALUES (@Name, @Department, @Email)";

            var parameters = new[]
            {
                new OleDbParameter("@Name", lecturer.Name),
                new OleDbParameter("@Department", lecturer.Department ?? (object)DBNull.Value),
                new OleDbParameter("@Email", lecturer.Email)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public static bool UpdateLecturer(Lecturer lecturer)
        {
            if (lecturer.LecturerId <= 0)
                throw new ArgumentException("Invalid Lecturer ID.");

            if (string.IsNullOrWhiteSpace(lecturer.Name))
                throw new ArgumentException("Lecturer name is required.");

            if (string.IsNullOrWhiteSpace(lecturer.Email))
                throw new ArgumentException("Email is required.");

            string query = @"UPDATE Lecturers 
                           SET Name = @Name, 
                               Department = @Department, 
                               Email = @Email 
                           WHERE LecturerId = @LecturerId";

            var parameters = new[]
            {
                new OleDbParameter("@Name", lecturer.Name),
                new OleDbParameter("@Department", lecturer.Department ?? (object)DBNull.Value),
                new OleDbParameter("@Email", lecturer.Email),
                new OleDbParameter("@LecturerId", lecturer.LecturerId)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        public static bool DeleteLecturer(int lecturerId)
        {
            // First check if the lecturer has any courses
            string checkQuery = "SELECT COUNT(*) FROM Courses WHERE LecturerId = @LecturerId";
            var checkParam = new OleDbParameter("@LecturerId", lecturerId);

            var result = DatabaseHelper.ExecuteQuery(checkQuery, checkParam);
            if (Convert.ToInt32(result.Rows[0][0]) > 0)
                throw new InvalidOperationException("Cannot delete lecturer with assigned courses.");

            string query = "DELETE FROM Lecturers WHERE LecturerId = @LecturerId";
            var parameter = new OleDbParameter("@LecturerId", lecturerId);

            return DatabaseHelper.ExecuteNonQuery(query, parameter) > 0;
        }
    }
}