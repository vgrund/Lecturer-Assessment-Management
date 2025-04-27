using System;
using System.Data;
using System.Windows.Forms;
using LectureAssessmentManager.Data;
using LectureAssessmentManager.Models;

namespace LectureAssessmentManager.Forms
{
    public partial class AssignmentForm : Form
    {
        private DataTable _assignmentsTable;
        private bool _isEditing = false;
        private int _currentAssignmentId = 0;

        // Designer variables
        private DataGridView dgvAssignments;
        private TextBox txtTitle;
        private TextBox txtDescription;
        private DateTimePicker dtpDueDate;
        private ComboBox cboCourse;
        private NumericUpDown nudTotalPoints;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnSave;
        private Button btnCancel;
        private Label lblTitle;
        private Label lblDescription;
        private Label lblDueDate;
        private Label lblCourse;
        private Label lblTotalPoints;

        public AssignmentForm()
        {
            InitializeComponent();
            DatabaseHelper.InitializeDatabase();
            LoadCourses();
            LoadAssignments();
        }

        private void LoadAssignments()
        {
            try
            {
                string query = @"SELECT a.AssignmentId, a.Title, a.Description, a.DueDate, 
                                      a.TotalPoints, a.CourseId, c.Name as CourseName
                               FROM Assignments a 
                               LEFT JOIN Courses c ON a.CourseId = c.CourseId";
                _assignmentsTable = DatabaseHelper.ExecuteQuery(query);
                dgvAssignments.DataSource = null;
                dgvAssignments.DataSource = _assignmentsTable;

                if (dgvAssignments.Columns.Count > 0)
                {
                    dgvAssignments.Columns["AssignmentId"].Visible = false;
                    dgvAssignments.Columns["CourseId"].Visible = false;
                    dgvAssignments.Columns["Title"].HeaderText = "Title";
                    dgvAssignments.Columns["Description"].HeaderText = "Description";
                    dgvAssignments.Columns["DueDate"].HeaderText = "Due Date";
                    dgvAssignments.Columns["TotalPoints"].HeaderText = "Total Points";
                    dgvAssignments.Columns["CourseName"].HeaderText = "Course";
                }

                dgvAssignments.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading assignments: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCourses()
        {
            try
            {
                string query = "SELECT CourseId, Name FROM Courses";
                var courses = DatabaseHelper.ExecuteQuery(query);
                cboCourse.DisplayMember = "Name";
                cboCourse.ValueMember = "CourseId";
                cboCourse.DataSource = courses;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading courses: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveAssignment()
        {
            try
            {
                string query;
                System.Data.OleDb.OleDbParameter[] parameters;

                if (_isEditing)
                {
                    query = @"UPDATE Assignments 
                             SET Title = ?, Description = ?, DueDate = ?, 
                                 TotalPoints = ?, CourseId = ?
                             WHERE AssignmentId = ?";

                    parameters = new[]
                    {
                        new System.Data.OleDb.OleDbParameter("Title", txtTitle.Text.Trim()),
                        new System.Data.OleDb.OleDbParameter("Description", txtDescription.Text.Trim()),
                        new System.Data.OleDb.OleDbParameter("DueDate", dtpDueDate.Value.Date),
                        new System.Data.OleDb.OleDbParameter("TotalPoints", (int)nudTotalPoints.Value),
                        new System.Data.OleDb.OleDbParameter("CourseId", cboCourse.SelectedValue.ToString()),
                        new System.Data.OleDb.OleDbParameter("AssignmentId", _currentAssignmentId)
                    };

                    int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
                    if (rowsAffected == 0)
                    {
                        throw new Exception("No rows were updated. Please check if the assignment exists.");
                    }
                }
                else
                {
                    query = @"INSERT INTO Assignments (Title, Description, DueDate, TotalPoints, CourseId)
                             VALUES (?, ?, ?, ?, ?)";

                    parameters = new[]
                    {
                        new System.Data.OleDb.OleDbParameter("Title", txtTitle.Text.Trim()),
                        new System.Data.OleDb.OleDbParameter("Description", txtDescription.Text.Trim()),
                        new System.Data.OleDb.OleDbParameter("DueDate", dtpDueDate.Value.Date),
                        new System.Data.OleDb.OleDbParameter("TotalPoints", (int)nudTotalPoints.Value),
                        new System.Data.OleDb.OleDbParameter("CourseId", cboCourse.SelectedValue.ToString())
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                }

                MessageBox.Show(_isEditing ? "Assignment updated successfully." : "Assignment added successfully.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAssignments();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving assignment: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtTitle.Text = string.Empty;
            txtDescription.Text = string.Empty;
            dtpDueDate.Value = DateTime.Now;
            nudTotalPoints.Value = nudTotalPoints.Minimum;
            if (cboCourse.Items.Count > 0)
                cboCourse.SelectedIndex = 0;
            _currentAssignmentId = 0;
        }

        private void SetInputState(bool enabled)
        {
            txtTitle.Enabled = enabled;
            txtDescription.Enabled = enabled;
            dtpDueDate.Enabled = enabled;
            cboCourse.Enabled = enabled;
            nudTotalPoints.Enabled = enabled;
            btnSave.Enabled = enabled;
            btnCancel.Enabled = enabled;
            btnAdd.Enabled = !enabled;
            btnEdit.Enabled = !enabled && dgvAssignments.SelectedRows.Count > 0;
            btnDelete.Enabled = !enabled && dgvAssignments.SelectedRows.Count > 0;
            dgvAssignments.Enabled = !enabled;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Please enter a title.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (nudTotalPoints.Value <= 0)
            {
                MessageBox.Show("Total points must be greater than 0.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cboCourse.SelectedValue == null)
            {
                MessageBox.Show("Please select a course.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            _isEditing = false;
            ClearInputs();
            SetInputState(true);
            txtTitle.Focus();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvAssignments.SelectedRows.Count > 0)
            {
                var row = dgvAssignments.SelectedRows[0];
                _currentAssignmentId = Convert.ToInt32(row.Cells["AssignmentId"].Value);
                txtTitle.Text = row.Cells["Title"].Value.ToString();
                txtDescription.Text = row.Cells["Description"].Value.ToString();
                dtpDueDate.Value = Convert.ToDateTime(row.Cells["DueDate"].Value);
                nudTotalPoints.Value = Convert.ToDecimal(row.Cells["TotalPoints"].Value);
                cboCourse.SelectedValue = row.Cells["CourseId"].Value.ToString();

                _isEditing = true;
                SetInputState(true);
                txtTitle.Focus();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvAssignments.SelectedRows.Count > 0)
            {
                var assignmentId = Convert.ToInt32(dgvAssignments.SelectedRows[0].Cells["AssignmentId"].Value);
                var title = dgvAssignments.SelectedRows[0].Cells["Title"].Value.ToString();

                if (MessageBox.Show($"Are you sure you want to delete assignment '{title}'?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string query = "DELETE FROM Assignments WHERE AssignmentId = ?";
                        var parameter = new System.Data.OleDb.OleDbParameter("AssignmentId", assignmentId);
                        DatabaseHelper.ExecuteNonQuery(query, parameter);

                        MessageBox.Show("Assignment deleted successfully.", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAssignments();
                        ClearInputs();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting assignment: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                SaveAssignment();
                SetInputState(false);
                ClearInputs();
                _isEditing = false;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            SetInputState(false);
            ClearInputs();
            _isEditing = false;
        }

        private void DgvAssignments_SelectionChanged(object sender, EventArgs e)
        {
            btnEdit.Enabled = !_isEditing && dgvAssignments.SelectedRows.Count > 0;
            btnDelete.Enabled = !_isEditing && dgvAssignments.SelectedRows.Count > 0;
        }
    }
}