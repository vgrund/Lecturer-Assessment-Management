using System;
using System.Data;
using System.Windows.Forms;
using LectureAssessmentManager.Business;
using LectureAssessmentManager.Models;

namespace LectureAssessmentManager.Forms
{
    public partial class GradeAssignmentForm : Form
    {
        private readonly string _assignmentId;
        private string _currentStudentId;
        private Assignment _assignment;

        public GradeAssignmentForm(string assignmentId)
        {
            InitializeComponent();
            _assignmentId = assignmentId;
            SetupEventHandlers();
            LoadAssignmentInfo();
            LoadStudents();
        }

        private void SetupEventHandlers()
        {
            dgvStudents.SelectionChanged += DgvStudents_SelectionChanged;
            btnSave.Click += BtnSave_Click;
            btnClose.Click += BtnClose_Click;
        }

        private void LoadAssignmentInfo()
        {
            try
            {
                _assignment = AssignmentManager.GetAssignmentById(_assignmentId);
                if (_assignment != null)
                {
                    lblAssignmentInfo.Text = $"Assignment: {_assignment.Title} (Max Score: {_assignment.MaxScore})";
                    nudScore.Maximum = _assignment.MaxScore;
                }
                else
                {
                    MessageBox.Show("Assignment not found.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading assignment: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void LoadStudents()
        {
            try
            {
                dgvStudents.DataSource = AssignmentManager.GetStudentsForGrading(_assignmentId);
                if (dgvStudents.Columns.Contains("StudentId"))
                    dgvStudents.Columns["StudentId"].Visible = false;

                if (dgvStudents.Columns.Contains("Score"))
                    dgvStudents.Columns["Score"].DefaultCellStyle.Format = "N1";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading students: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStudentGrade()
        {
            if (string.IsNullOrEmpty(_currentStudentId))
            {
                ClearGradeInputs();
                return;
            }

            try
            {
                var grade = AssignmentManager.GetStudentGrade(_assignmentId, _currentStudentId);
                if (grade != null)
                {
                    nudScore.Value = grade.Score;
                    txtFeedback.Text = grade.Feedback;
                }
                else
                {
                    ClearGradeInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading student grade: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearGradeInputs()
        {
            nudScore.Value = 0;
            txtFeedback.Text = string.Empty;
        }

        private void DgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                var row = dgvStudents.SelectedRows[0];
                _currentStudentId = row.Cells["StudentId"].Value.ToString();
                txtStudent.Text = row.Cells["Name"].Value.ToString();
                LoadStudentGrade();
            }
            else
            {
                _currentStudentId = string.Empty;
                txtStudent.Text = string.Empty;
                ClearGradeInputs();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentStudentId))
            {
                MessageBox.Show("Please select a student.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var grade = new Grade
                {
                    AssignmentId = _assignmentId,
                    StudentId = _currentStudentId,
                    Score = (int)nudScore.Value,
                    Feedback = txtFeedback.Text.Trim()
                };

                if (AssignmentManager.SaveGrade(grade))
                {
                    MessageBox.Show("Grade saved successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStudents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving grade: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}