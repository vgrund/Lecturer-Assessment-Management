using System;
using System.Data;
using System.Windows.Forms;
using LectureAssessmentManager.Business;
using LectureAssessmentManager.Models;
using LectureAssessmentManager.Data;

namespace LectureAssessmentManager.Forms
{
    public partial class LecturerForm : Form
    {
        private DataTable _lecturersTable;
        private bool _isEditing = false;
        private int _currentLecturerId = 0;

        public LecturerForm()
        {
            InitializeComponent();
            DatabaseHelper.InitializeDatabase(); // Ensure database is initialized
            LoadLecturers();
            SetInputState(false);
        }

        private void LoadLecturers()
        {
            try
            {
                string query = "SELECT LecturerId, Name, Department, Email FROM Lecturers";
                _lecturersTable = DatabaseHelper.ExecuteQuery(query);
                dgvLecturers.DataSource = _lecturersTable;

                if (dgvLecturers.Columns.Count > 0)
                {
                    dgvLecturers.Columns["LecturerId"].Visible = false;
                    dgvLecturers.Columns["Name"].HeaderText = "Name";
                    dgvLecturers.Columns["Department"].HeaderText = "Department";
                    dgvLecturers.Columns["Email"].HeaderText = "Email";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading lecturers: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetInputState(bool enabled)
        {
            txtName.Enabled = enabled;
            txtDepartment.Enabled = enabled;
            txtEmail.Enabled = enabled;
            btnSave.Enabled = enabled;
            btnCancel.Enabled = enabled;
            btnNew.Enabled = !enabled;
            btnEdit.Enabled = !enabled && dgvLecturers.SelectedRows.Count > 0;
            btnDelete.Enabled = !enabled && dgvLecturers.SelectedRows.Count > 0;
            dgvLecturers.Enabled = !enabled;
        }

        private void ClearInputs()
        {
            txtName.Text = string.Empty;
            txtDepartment.Text = string.Empty;
            txtEmail.Text = string.Empty;
            _currentLecturerId = 0;
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            _isEditing = false;
            ClearInputs();
            SetInputState(true);
            txtName.Focus();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a name.", "Validation Error",
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
                string query;
                var parameters = new[]
                {
                    new System.Data.OleDb.OleDbParameter("@Name", txtName.Text.Trim()),
                    new System.Data.OleDb.OleDbParameter("@Department", txtDepartment.Text.Trim()),
                    new System.Data.OleDb.OleDbParameter("@Email", txtEmail.Text.Trim())
                };

                if (_isEditing)
                {
                    query = "UPDATE Lecturers SET Name = @Name, Department = @Department, Email = @Email WHERE LecturerId = @LecturerId";
                    Array.Resize(ref parameters, 4);
                    parameters[3] = new System.Data.OleDb.OleDbParameter("@LecturerId", _currentLecturerId);
                }
                else
                {
                    query = "INSERT INTO Lecturers (Name, Department, Email) VALUES (@Name, @Department, @Email)";
                }

                DatabaseHelper.ExecuteNonQuery(query, parameters);
                MessageBox.Show(_isEditing ? "Lecturer updated successfully." : "Lecturer added successfully.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadLecturers();
                SetInputState(false);
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving lecturer: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvLecturers.SelectedRows.Count > 0)
            {
                var lecturerId = Convert.ToInt32(dgvLecturers.SelectedRows[0].Cells["LecturerId"].Value);
                var name = dgvLecturers.SelectedRows[0].Cells["Name"].Value.ToString();

                if (MessageBox.Show($"Are you sure you want to delete lecturer '{name}'?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string query = "DELETE FROM Lecturers WHERE LecturerId = @LecturerId";
                        var parameter = new System.Data.OleDb.OleDbParameter("@LecturerId", lecturerId);
                        DatabaseHelper.ExecuteNonQuery(query, parameter);

                        MessageBox.Show("Lecturer deleted successfully.", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadLecturers();
                        ClearInputs();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting lecturer: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            SetInputState(false);
            ClearInputs();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvLecturers.SelectedRows.Count > 0)
            {
                var row = dgvLecturers.SelectedRows[0];
                _currentLecturerId = Convert.ToInt32(row.Cells["LecturerId"].Value);
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtDepartment.Text = row.Cells["Department"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();

                _isEditing = true;
                SetInputState(true);
                txtName.Focus();
            }
            else
            {
                MessageBox.Show("Please select a lecturer to edit.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DgvLecturers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLecturers.SelectedRows.Count > 0 && !_isEditing)
            {
                var row = dgvLecturers.SelectedRows[0];
                _currentLecturerId = Convert.ToInt32(row.Cells["LecturerId"].Value);
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtDepartment.Text = row.Cells["Department"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
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
    }
}