using System;
using System.Data;
using System.Windows.Forms;
using LectureAssessmentManager.Data;
using LectureAssessmentManager.Models;

namespace LectureAssessmentManager.Forms
{
    public partial class AssessmentForm : Form
    {
        private DataTable _assessmentsTable;
        private bool _isEditing = false;
        private int _currentAssessmentId = 0;

        // Designer variables
        private DataGridView dgvAssessments;
        private ComboBox cboAssignment;
        private ComboBox cboStudent;
        private NumericUpDown nudScore;
        private TextBox txtComments;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnSave;
        private Button btnCancel;
        private Label lblAssignment;
        private Label lblStudent;
        private Label lblScore;
        private Label lblComments;

        public AssessmentForm()
        {
            InitializeComponent();
            DatabaseHelper.InitializeDatabase();
            LoadAssignments();
            LoadStudents();
            LoadAssessments();
        }

        private void LoadAssessments()
        {
            try
            {
                string query = @"SELECT a.AssessmentId, a.AssignmentId, a.StudentId, 
                                      a.Score, a.Comments, asn.Title as AssignmentTitle,
                                      s.FirstName + ' ' + s.LastName as StudentName
                               FROM Assessments a 
                               LEFT JOIN Assignments asn ON a.AssignmentId = asn.AssignmentId
                               LEFT JOIN Students s ON a.StudentId = s.StudentId";
                _assessmentsTable = DatabaseHelper.ExecuteQuery(query);
                dgvAssessments.DataSource = null;
                dgvAssessments.DataSource = _assessmentsTable;

                if (dgvAssessments.Columns.Count > 0)
                {
                    dgvAssessments.Columns["AssessmentId"].Visible = false;
                    dgvAssessments.Columns["AssignmentId"].Visible = false;
                    dgvAssessments.Columns["StudentId"].Visible = false;
                    dgvAssessments.Columns["AssignmentTitle"].HeaderText = "Assignment";
                    dgvAssessments.Columns["StudentName"].HeaderText = "Student";
                    dgvAssessments.Columns["Score"].HeaderText = "Score";
                    dgvAssessments.Columns["Comments"].HeaderText = "Comments";
                }

                dgvAssessments.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading assessments: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAssignments()
        {
            try
            {
                string query = "SELECT AssignmentId, Title FROM Assignments";
                var assignments = DatabaseHelper.ExecuteQuery(query);
                cboAssignment.DisplayMember = "Title";
                cboAssignment.ValueMember = "AssignmentId";
                cboAssignment.DataSource = assignments;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading assignments: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStudents()
        {
            try
            {
                string query = "SELECT StudentId, FirstName + ' ' + LastName as FullName FROM Students";
                var students = DatabaseHelper.ExecuteQuery(query);
                cboStudent.DisplayMember = "FullName";
                cboStudent.ValueMember = "StudentId";
                cboStudent.DataSource = students;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading students: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveAssessment()
        {
            try
            {
                string query;
                System.Data.OleDb.OleDbParameter[] parameters;

                if (_isEditing)
                {
                    query = @"UPDATE Assessments 
                             SET AssignmentId = ?, StudentId = ?, Score = ?, Comments = ?
                             WHERE AssessmentId = ?";

                    parameters = new[]
                    {
                        new System.Data.OleDb.OleDbParameter("AssignmentId", Convert.ToInt32(cboAssignment.SelectedValue)),
                        new System.Data.OleDb.OleDbParameter("StudentId", Convert.ToInt32(cboStudent.SelectedValue)),
                        new System.Data.OleDb.OleDbParameter("Score", (int)nudScore.Value),
                        new System.Data.OleDb.OleDbParameter("Comments", txtComments.Text.Trim()),
                        new System.Data.OleDb.OleDbParameter("AssessmentId", _currentAssessmentId)
                    };

                    int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
                    if (rowsAffected == 0)
                    {
                        throw new Exception("No rows were updated. Please check if the assessment exists.");
                    }
                }
                else
                {
                    query = @"INSERT INTO Assessments (AssignmentId, StudentId, Score, Comments)
                             VALUES (?, ?, ?, ?)";

                    parameters = new[]
                    {
                        new System.Data.OleDb.OleDbParameter("AssignmentId", Convert.ToInt32(cboAssignment.SelectedValue)),
                        new System.Data.OleDb.OleDbParameter("StudentId", Convert.ToInt32(cboStudent.SelectedValue)),
                        new System.Data.OleDb.OleDbParameter("Score", (int)nudScore.Value),
                        new System.Data.OleDb.OleDbParameter("Comments", txtComments.Text.Trim())
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                }

                MessageBox.Show(_isEditing ? "Assessment updated successfully." : "Assessment added successfully.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAssessments();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving assessment: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            if (cboAssignment.Items.Count > 0)
                cboAssignment.SelectedIndex = 0;
            if (cboStudent.Items.Count > 0)
                cboStudent.SelectedIndex = 0;
            nudScore.Value = nudScore.Minimum;
            txtComments.Text = string.Empty;
            _currentAssessmentId = 0;
        }

        private void SetInputState(bool enabled)
        {
            cboAssignment.Enabled = enabled;
            cboStudent.Enabled = enabled;
            nudScore.Enabled = enabled;
            txtComments.Enabled = enabled;
            btnSave.Enabled = enabled;
            btnCancel.Enabled = enabled;
            btnAdd.Enabled = !enabled;
            btnEdit.Enabled = !enabled && dgvAssessments.SelectedRows.Count > 0;
            btnDelete.Enabled = !enabled && dgvAssessments.SelectedRows.Count > 0;
            dgvAssessments.Enabled = !enabled;
        }

        private bool ValidateInputs()
        {
            if (cboAssignment.SelectedValue == null)
            {
                MessageBox.Show("Please select an assignment.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cboStudent.SelectedValue == null)
            {
                MessageBox.Show("Please select a student.", "Validation Error",
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
            cboAssignment.Focus();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvAssessments.SelectedRows.Count > 0)
            {
                var row = dgvAssessments.SelectedRows[0];
                _currentAssessmentId = Convert.ToInt32(row.Cells["AssessmentId"].Value);
                cboAssignment.SelectedValue = Convert.ToInt32(row.Cells["AssignmentId"].Value);
                cboStudent.SelectedValue = Convert.ToInt32(row.Cells["StudentId"].Value);
                nudScore.Value = Convert.ToDecimal(row.Cells["Score"].Value);
                txtComments.Text = row.Cells["Comments"].Value?.ToString() ?? string.Empty;

                _isEditing = true;
                SetInputState(true);
                cboAssignment.Focus();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvAssessments.SelectedRows.Count > 0)
            {
                var assessmentId = Convert.ToInt32(dgvAssessments.SelectedRows[0].Cells["AssessmentId"].Value);
                var studentName = dgvAssessments.SelectedRows[0].Cells["StudentName"].Value.ToString();
                var assignmentTitle = dgvAssessments.SelectedRows[0].Cells["AssignmentTitle"].Value.ToString();

                if (MessageBox.Show($"Are you sure you want to delete the assessment for {studentName} on {assignmentTitle}?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string query = "DELETE FROM Assessments WHERE AssessmentId = ?";
                        var parameter = new System.Data.OleDb.OleDbParameter("AssessmentId", assessmentId);
                        DatabaseHelper.ExecuteNonQuery(query, parameter);

                        MessageBox.Show("Assessment deleted successfully.", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAssessments();
                        ClearInputs();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting assessment: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                SaveAssessment();
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

        private void DgvAssessments_SelectionChanged(object sender, EventArgs e)
        {
            btnEdit.Enabled = !_isEditing && dgvAssessments.SelectedRows.Count > 0;
            btnDelete.Enabled = !_isEditing && dgvAssessments.SelectedRows.Count > 0;
        }
    }
}