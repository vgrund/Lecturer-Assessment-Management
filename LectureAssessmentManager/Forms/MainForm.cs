using System;
using System.Windows.Forms;

namespace LectureAssessmentManager.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Text = "Lecture Assessment Manager";
            this.Size = new System.Drawing.Size(1024, 768);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Create menu strip
            MenuStrip menuStrip = new MenuStrip();
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);

            // File menu
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("File");
            menuStrip.Items.Add(fileMenu);
            fileMenu.DropDownItems.Add("Exit", null, (s, e) => Application.Exit());

            // Management menus
            ToolStripMenuItem lecturersMenu = new ToolStripMenuItem("Lecturers");
            ToolStripMenuItem coursesMenu = new ToolStripMenuItem("Courses");
            ToolStripMenuItem studentsMenu = new ToolStripMenuItem("Students");
            ToolStripMenuItem assignmentsMenu = new ToolStripMenuItem("Assignments");
            ToolStripMenuItem assessmentMenu = new ToolStripMenuItem("Assessment");

            menuStrip.Items.AddRange(new ToolStripItem[] {
                lecturersMenu,
                coursesMenu,
                studentsMenu,
                assignmentsMenu,
                assessmentMenu
            });

            // Add menu items
            lecturersMenu.DropDownItems.Add("Manage Lecturers", null, OnManageLecturers);
            coursesMenu.DropDownItems.Add("Manage Courses", null, OnManageCourses);
            studentsMenu.DropDownItems.Add("Manage Students", null, OnManageStudents);
            assignmentsMenu.DropDownItems.Add("Manage Assignments", null, OnManageAssignments);
            assessmentMenu.DropDownItems.Add("Mark Students", null, OnMarkStudents);

            // Create welcome label
            Label welcomeLabel = new Label
            {
                Text = "Welcome to Lecture Assessment Manager",
                Font = new System.Drawing.Font("Arial", 20, System.Drawing.FontStyle.Bold),
                AutoSize = true
            };

            // Center the welcome label
            welcomeLabel.Location = new System.Drawing.Point(
                (this.ClientSize.Width - welcomeLabel.Width) / 2,
                (this.ClientSize.Height - welcomeLabel.Height) / 2
            );

            this.Controls.Add(welcomeLabel);
        }

        private void OnManageLecturers(object sender, EventArgs e)
        {
            using (var lecturerForm = new LecturerForm())
            {
                lecturerForm.ShowDialog(this);
            }
        }

        private void OnManageCourses(object sender, EventArgs e)
        {
            using (var courseForm = new CourseForm())
            {
                courseForm.ShowDialog(this);
            }
        }

        private void OnManageStudents(object sender, EventArgs e)
        {
            using (var studentForm = new StudentForm())
            {
                studentForm.ShowDialog(this);
            }
        }

        private void OnManageAssignments(object sender, EventArgs e)
        {
            using (var assignmentForm = new AssignmentForm())
            {
                assignmentForm.ShowDialog(this);
            }
        }

        private void OnMarkStudents(object sender, EventArgs e)
        {
            using (var assessmentForm = new AssessmentForm())
            {
                assessmentForm.ShowDialog(this);
            }
        }
    }
}