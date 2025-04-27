using System;
using System.Data;
using System.Windows.Forms;
using LectureAssessmentManager.Business;
using LectureAssessmentManager.Models;

namespace LectureAssessmentManager.Forms
{
    public partial class StudentForm : Form
    {
        private DataTable _studentsTable;
        private bool _isEditing = false;
        private string _currentStudentId = string.Empty;

        public StudentForm()
        {
            InitializeComponent();
            SetupEventHandlers();
            LoadStudents();
            SetInputState(false);
            SetEnrollmentState(false);
        }

        private void SetupEventHandlers()
        {
            dgvStudents.SelectionChanged += DgvStudents_SelectionChanged;
            btnNew.Click += BtnNew_Click;
            btnSave.Click += BtnSave_Click;
            btnDelete.Click += BtnDelete_Click;
            btnCancel.Click += BtnCancel_Click;
            btnEnroll.Click += BtnEnroll_Click;
            btnUnenroll.Click += BtnUnenroll_Click;
        }

        private void LoadStudents()
        {
            try
            {
                _studentsTable = StudentManager.GetAllStudents();
                dgvStudents.DataSource = _studentsTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading students: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStudentCourses()
        {
            if (string.IsNullOrEmpty(_currentStudentId))
            {
                dgvCourses.DataSource = null;
                return;
            }

            try
            {
                var coursesTable = StudentManager.GetStudentCourses(_currentStudentId);
                dgvCourses.DataSource = null;
                dgvCourses.DataSource = coursesTable;

                if (dgvCourses.Columns.Count > 0)
                {
                    dgvCourses.Columns["CourseId"].HeaderText = "Course ID";
                    dgvCourses.Columns["CourseName"].HeaderText = "Course Name";
                    dgvCourses.Columns["LecturerName"].HeaderText = "Lecturer";
                }

                dgvCourses.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading courses: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetInputState(bool enabled)
        {
            txtStudentId.Enabled = enabled && !_isEditing;
            txtFirstName.Enabled = enabled;
            txtLastName.Enabled = enabled;
            txtEmail.Enabled = enabled;
            btnSave.Enabled = enabled;
            btnCancel.Enabled = enabled;
            btnNew.Enabled = !enabled;
            btnDelete.Enabled = !enabled && dgvStudents.SelectedRows.Count > 0;
            dgvStudents.Enabled = !enabled;
        }

        private void SetEnrollmentState(bool enabled)
        {
            dgvCourses.Enabled = enabled;
            btnEnroll.Enabled = enabled;
            btnUnenroll.Enabled = enabled;
        }

        private void ClearInputs()
        {
            txtStudentId.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            _currentStudentId = string.Empty;
            LoadStudentCourses();
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            _isEditing = false;
            ClearInputs();
            SetInputState(true);
            SetEnrollmentState(false);
            txtStudentId.Focus();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStudentId.Text))
            {
                MessageBox.Show("Please enter a Student ID.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Please enter both first and last names.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please enter an email.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var student = new Student
                {
                    StudentId = txtStudentId.Text.Trim(),
                    Name = $"{txtFirstName.Text.Trim()} {txtLastName.Text.Trim()}",
                    Email = txtEmail.Text.Trim()
                };

                if (_isEditing)
                {
                    if (StudentManager.UpdateStudent(student))
                        MessageBox.Show("Student updated successfully.", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (StudentManager.AddStudent(student) > 0)
                        MessageBox.Show("Student added successfully.", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadStudents();
                SetInputState(false);
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving student: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0) return;

            var studentId = dgvStudents.SelectedRows[0].Cells["StudentId"].Value.ToString();
            var name = dgvStudents.SelectedRows[0].Cells["Name"].Value.ToString();

            if (MessageBox.Show($"Are you sure you want to delete student '{studentId} - {name}'?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (StudentManager.DeleteStudent(studentId))
                    {
                        MessageBox.Show("Student deleted successfully.", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadStudents();
                        ClearInputs();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting student: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            SetInputState(false);
            ClearInputs();
        }

        private void DgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0 && !_isEditing)
            {
                var row = dgvStudents.SelectedRows[0];
                _currentStudentId = row.Cells["StudentId"].Value.ToString();
                txtStudentId.Text = _currentStudentId;

                string fullName = row.Cells["Name"].Value.ToString();
                string[] nameParts = fullName.Split(new[] { ' ' }, 2);
                txtFirstName.Text = nameParts[0];
                txtLastName.Text = nameParts.Length > 1 ? nameParts[1] : string.Empty;

                txtEmail.Text = row.Cells["Email"].Value.ToString();
                btnDelete.Enabled = true;
                SetEnrollmentState(true);
                LoadStudentCourses();
            }
            else
            {
                btnDelete.Enabled = false;
                SetEnrollmentState(false);
            }
        }

        private void BtnEnroll_Click(object sender, EventArgs e)
        {
            try
            {
                var availableCourses = StudentManager.GetAvailableCourses(_currentStudentId);
                if (availableCourses.Rows.Count == 0)
                {
                    MessageBox.Show("No available courses to enroll in.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var courseForm = new CourseSelectionForm(availableCourses))
                {
                    if (courseForm.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(courseForm.SelectedCourseId))
                    {
                        StudentManager.EnrollStudentInCourse(_currentStudentId, courseForm.SelectedCourseId);
                        LoadStudentCourses();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error enrolling student in course: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUnenroll_Click(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count == 0) return;

            try
            {
                var courseId = dgvCourses.SelectedRows[0].Cells["CourseId"].Value.ToString();
                var courseName = dgvCourses.SelectedRows[0].Cells["CourseName"].Value.ToString();

                if (MessageBox.Show($"Are you sure you want to unenroll from '{courseId} - {courseName}'?",
                    "Confirm Unenroll", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    StudentManager.RemoveStudentFromCourse(_currentStudentId, courseId);
                    LoadStudentCourses();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing student from course: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {
            LoadStudents();
        }
    }
}