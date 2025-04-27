using System;
using System.Windows.Forms;
using LectureAssessmentManager.Data;
using LectureAssessmentManager.Forms;

namespace LectureAssessmentManager
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                DatabaseHelper.InitializeDatabase();
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while starting the application: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}