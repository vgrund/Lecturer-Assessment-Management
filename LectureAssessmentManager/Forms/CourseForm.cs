using System;
using System.Data;
using System.Windows.Forms;
using LectureAssessmentManager.Business;
using LectureAssessmentManager.Models;
using System.Drawing;
using LectureAssessmentManager.Data;

namespace LectureAssessmentManager.Forms
{
    public partial class CourseForm : Form
    {
        private DataTable _coursesTable;
        private bool _isEditing = false;
        private string _currentCourseId = string.Empty;

        // Designer variables
        private DataGridView dgvCourses;
        private TextBox txtCourseId;
        private TextBox txtName;
        private TextBox txtDescription;
        private ComboBox cboLecturer;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnNew;
        private Button btnSave;
        private Button btnCancel;
        private Label lblCourseId;
        private Label lblName;
        private Label lblDescription;
        private Label lblLecturer;

        public CourseForm()
        {
            InitializeComponent();
            DatabaseHelper.InitializeDatabase(); // Ensure database is initialized
            LoadLecturers(); // Load lecturers first since courses depend on them
            LoadCourses();
        }

        private void LoadCourses()
        {
            try
            {
                string query = "SELECT c.CourseId, c.Name, c.Description, c.LecturerId, l.Name as LecturerName " +
                             "FROM Courses c LEFT JOIN Lecturers l ON c.LecturerId = l.LecturerId";
                _coursesTable = DatabaseHelper.ExecuteQuery(query);

                // Clear existing binding and rebind
                dgvCourses.DataSource = null;
                dgvCourses.DataSource = _coursesTable;

                // Configure grid columns
                if (dgvCourses.Columns.Count > 0)
                {
                    dgvCourses.Columns["CourseId"].HeaderText = "Course ID";
                    dgvCourses.Columns["Name"].HeaderText = "Course Name";
                    dgvCourses.Columns["Description"].HeaderText = "Description";
                    dgvCourses.Columns["LecturerName"].HeaderText = "Lecturer";
                    dgvCourses.Columns["LecturerId"].Visible = false;
                }

                // Refresh the grid
                dgvCourses.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading courses: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLecturers()
        {
            try
            {
                string query = "SELECT LecturerId, Name FROM Lecturers";
                var lecturers = DatabaseHelper.ExecuteQuery(query);
                cboLecturer.DisplayMember = "Name";
                cboLecturer.ValueMember = "LecturerId";
                cboLecturer.DataSource = lecturers;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading lecturers: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvCourses_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count > 0 && !_isEditing)
            {
                var row = dgvCourses.SelectedRows[0];
                _currentCourseId = row.Cells["CourseId"].Value.ToString();
                txtCourseId.Text = _currentCourseId;
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtDescription.Text = row.Cells["Description"].Value.ToString();
                if (row.Cells["LecturerId"].Value != null)
                {
                    cboLecturer.SelectedValue = row.Cells["LecturerId"].Value;
                }
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
            else if (!_isEditing)
            {
                ClearInputs();
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            _isEditing = false;
            ClearInputs();
            SetInputState(true);
            txtCourseId.Focus();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                SaveCourse();
                LoadCourses();
                SetInputState(false);
                ClearInputs();
                _isEditing = false;  // Reset editing state
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            SetInputState(false);
            ClearInputs();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtCourseId.Text))
            {
                MessageBox.Show("Please enter a Course ID.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cboLecturer.SelectedValue == null)
            {
                MessageBox.Show("Please select a lecturer.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void SaveCourse()
        {
            try
            {
                var course = new Course
                {
                    CourseId = _isEditing ? _currentCourseId : txtCourseId.Text.Trim(),
                    Name = txtName.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    LecturerId = Convert.ToInt32(cboLecturer.SelectedValue)
                };

                string query;
                if (_isEditing)
                {
                    // Debug information
                    Console.WriteLine($"Updating course: ID={course.CourseId}, Name={course.Name}, Description={course.Description}, LecturerId={course.LecturerId}");

                    query = @"UPDATE Courses 
                             SET [Name] = ?, [Description] = ?, [LecturerId] = ? 
                             WHERE [CourseId] = ?";

                    var parameters = new[]
                    {
                        new System.Data.OleDb.OleDbParameter("Name", course.Name),
                        new System.Data.OleDb.OleDbParameter("Description", course.Description),
                        new System.Data.OleDb.OleDbParameter("LecturerId", course.LecturerId),
                        new System.Data.OleDb.OleDbParameter("CourseId", course.CourseId)
                    };

                    int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
                    if (rowsAffected == 0)
                    {
                        throw new Exception("No rows were updated. Please check if the course ID exists.");
                    }
                }
                else
                {
                    query = @"INSERT INTO Courses ([CourseId], [Name], [Description], [LecturerId]) 
                             VALUES (?, ?, ?, ?)";

                    var parameters = new[]
                    {
                        new System.Data.OleDb.OleDbParameter("CourseId", course.CourseId),
                        new System.Data.OleDb.OleDbParameter("Name", course.Name),
                        new System.Data.OleDb.OleDbParameter("Description", course.Description),
                        new System.Data.OleDb.OleDbParameter("LecturerId", course.LecturerId)
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                }

                MessageBox.Show(_isEditing ? "Course updated successfully." : "Course added successfully.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving course: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void SetInputState(bool enabled)
        {
            txtCourseId.Enabled = enabled && !_isEditing;
            txtName.Enabled = enabled;
            txtDescription.Enabled = enabled;
            cboLecturer.Enabled = enabled;
            btnSave.Enabled = enabled;
            btnCancel.Enabled = enabled;
            btnNew.Enabled = !enabled;
            btnEdit.Enabled = !enabled && dgvCourses.SelectedRows.Count > 0;
            btnDelete.Enabled = !enabled && dgvCourses.SelectedRows.Count > 0;
            dgvCourses.Enabled = !enabled;
        }

        private void ClearInputs()
        {
            txtCourseId.Text = string.Empty;
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            if (cboLecturer.Items.Count > 0)
                cboLecturer.SelectedIndex = 0;
            _currentCourseId = string.Empty;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            _isEditing = false;
            ClearInputs();
            SetInputState(true);
            txtCourseId.Focus();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count > 0)
            {
                var row = dgvCourses.SelectedRows[0];
                _currentCourseId = row.Cells["CourseId"].Value.ToString();
                txtCourseId.Text = _currentCourseId;
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtDescription.Text = row.Cells["Description"].Value.ToString();
                if (row.Cells["LecturerId"].Value != null)
                {
                    cboLecturer.SelectedValue = row.Cells["LecturerId"].Value;
                }

                _isEditing = true;
                SetInputState(true);
                txtName.Focus();
            }
            else
            {
                MessageBox.Show("Please select a course to edit.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count > 0)
            {
                var courseId = dgvCourses.SelectedRows[0].Cells["CourseId"].Value.ToString();
                var name = dgvCourses.SelectedRows[0].Cells["Name"].Value.ToString();

                if (MessageBox.Show($"Are you sure you want to delete course '{courseId} - {name}'?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string query = "DELETE FROM Courses WHERE CourseId = @CourseId";
                        var parameter = new System.Data.OleDb.OleDbParameter("@CourseId", courseId);
                        DatabaseHelper.ExecuteNonQuery(query, parameter);

                        MessageBox.Show("Course deleted successfully.", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCourses();
                        ClearInputs();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting course: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a course to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}