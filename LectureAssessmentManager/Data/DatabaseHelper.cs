using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using ADOX;

namespace LectureAssessmentManager.Data
{
    public class DatabaseHelper
    {
        private static string _connectionString;

        public static void InitializeDatabase()
        {
            string dbPath = Path.Combine(Application.StartupPath, "AssessmentDB.accdb");
            _connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};";

            if (!File.Exists(dbPath))
            {
                CreateDatabase();
            }
        }

        private static void CreateDatabase()
        {
            // Create tables for all entities
            string[] createTableQueries = new string[]
            {
                @"CREATE TABLE Lecturers (
                    LecturerId COUNTER PRIMARY KEY,
                    Name TEXT(100) NOT NULL,
                    Department TEXT(100),
                    Email TEXT(100)
                )",
                @"CREATE TABLE Courses (
                    CourseId TEXT(20) PRIMARY KEY,
                    Name TEXT(100) NOT NULL,
                    Description TEXT(255),
                    LecturerId LONG NOT NULL,
                    FOREIGN KEY (LecturerId) REFERENCES Lecturers(LecturerId)
                )",
                @"CREATE TABLE Students (
                    StudentId TEXT(20) PRIMARY KEY,
                    Name TEXT(100) NOT NULL,
                    Email TEXT(100)
                )",
                @"CREATE TABLE StudentCourses (
                    StudentId TEXT(20),
                    CourseId TEXT(20),
                    PRIMARY KEY (StudentId, CourseId),
                    FOREIGN KEY (StudentId) REFERENCES Students(StudentId),
                    FOREIGN KEY (CourseId) REFERENCES Courses(CourseId)
                )",
                @"CREATE TABLE Assignments (
                    AssignmentId COUNTER PRIMARY KEY,
                    CourseId TEXT(20) NOT NULL,
                    Title TEXT(100) NOT NULL,
                    Description TEXT(255),
                    Deadline DATE,
                    FOREIGN KEY (CourseId) REFERENCES Courses(CourseId)
                )",
                @"CREATE TABLE RubricCategories (
                    CategoryId COUNTER PRIMARY KEY,
                    AssignmentId LONG NOT NULL,
                    Name TEXT(100) NOT NULL,
                    WeightPercentage DECIMAL(5,2) NOT NULL,
                    FOREIGN KEY (AssignmentId) REFERENCES Assignments(AssignmentId)
                )",
                @"CREATE TABLE StudentAssessments (
                    AssessmentId COUNTER PRIMARY KEY,
                    StudentId TEXT(20) NOT NULL,
                    AssignmentId LONG NOT NULL,
                    CategoryId LONG NOT NULL,
                    Score DECIMAL(5,2) NOT NULL,
                    Grade TEXT(20),
                    AssessmentDate DATE,
                    FOREIGN KEY (StudentId) REFERENCES Students(StudentId),
                    FOREIGN KEY (AssignmentId) REFERENCES Assignments(AssignmentId),
                    FOREIGN KEY (CategoryId) REFERENCES RubricCategories(CategoryId)
                )"
            };

            // Create the database and tables
            var catalog = new ADOX.Catalog();
            try
            {
                catalog.Create(_connectionString);

                using (OleDbConnection conn = new OleDbConnection(_connectionString))
                {
                    conn.Open();
                    foreach (string query in createTableQueries)
                    {
                        using (OleDbCommand cmd = new OleDbCommand(query, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating database: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public static OleDbConnection GetConnection()
        {
            return new OleDbConnection(_connectionString);
        }

        public static DataTable ExecuteQuery(string query, params OleDbParameter[] parameters)
        {
            using (OleDbConnection conn = GetConnection())
            {
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    DataTable dt = new DataTable();
                    conn.Open();
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                    return dt;
                }
            }
        }

        public static int ExecuteNonQuery(string query, params OleDbParameter[] parameters)
        {
            using (OleDbConnection conn = GetConnection())
            {
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}