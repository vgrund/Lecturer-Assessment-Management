using System;
using System.Windows.Forms;
using LectureAssessmentManager.Models;
using LectureAssessmentManager.Managers;

namespace LectureAssessmentManager.Forms
{
    public partial class RubricForm : Form
    {
        private readonly RubricManager _rubricManager;
        private Rubric _currentRubric;
        private string _assignmentId;

        public RubricForm(string assignmentId, RubricManager rubricManager)
        {
            InitializeComponent();
            _rubricManager = rubricManager;
            _assignmentId = assignmentId;
            InitializeForm();
            LoadRubric();
        }

        private void InitializeForm()
        {
            // Set up DataGridView for criteria
            dgvCriteria.AutoGenerateColumns = false;
            dgvCriteria.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Title",
                HeaderText = "Title",
                DataPropertyName = "Title"
            });
            dgvCriteria.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Weight",
                HeaderText = "Weight",
                DataPropertyName = "Weight"
            });
            dgvCriteria.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Description",
                HeaderText = "Description",
                DataPropertyName = "Description"
            });

            // Add event handlers
            btnAddCriterion.Click += BtnAddCriterion_Click;
            btnEditCriterion.Click += BtnEditCriterion_Click;
            btnDeleteCriterion.Click += BtnDeleteCriterion_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void LoadRubric()
        {
            var rubrics = _rubricManager.GetRubricsByAssignment(_assignmentId);
            if (rubrics.Count > 0)
            {
                _currentRubric = rubrics[0];
                txtTitle.Text = _currentRubric.Title;
                txtDescription.Text = _currentRubric.Description;
                RefreshCriteriaGrid();
            }
        }

        private void BtnAddCriterion_Click(object sender, EventArgs e)
        {
            if (_currentRubric == null)
            {
                MessageBox.Show("Please save the rubric first before adding criteria.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var criterionForm = new RubricCriterionForm(null, _currentRubric.RubricId, _rubricManager))
            {
                if (criterionForm.ShowDialog() == DialogResult.OK)
                {
                    RefreshCriteriaGrid();
                }
            }
        }

        private void BtnEditCriterion_Click(object sender, EventArgs e)
        {
            if (dgvCriteria.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a criterion to edit.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var criterion = (RubricCriterion)dgvCriteria.SelectedRows[0].DataBoundItem;
            using (var criterionForm = new RubricCriterionForm(criterion, _currentRubric.RubricId, _rubricManager))
            {
                if (criterionForm.ShowDialog() == DialogResult.OK)
                {
                    RefreshCriteriaGrid();
                }
            }
        }

        private void BtnDeleteCriterion_Click(object sender, EventArgs e)
        {
            if (dgvCriteria.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a criterion to delete.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var criterion = (RubricCriterion)dgvCriteria.SelectedRows[0].DataBoundItem;
            if (MessageBox.Show("Are you sure you want to delete this criterion?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _rubricManager.DeleteCriterion(criterion.CriterionId);
                RefreshCriteriaGrid();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Please enter a title for the rubric.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_currentRubric == null)
            {
                _currentRubric = _rubricManager.CreateRubric(_assignmentId, txtTitle.Text, txtDescription.Text);
            }
            else
            {
                _currentRubric.Title = txtTitle.Text;
                _currentRubric.Description = txtDescription.Text;
                _rubricManager.UpdateRubric(_currentRubric);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void RefreshCriteriaGrid()
        {
            if (_currentRubric != null)
            {
                dgvCriteria.DataSource = null;
                dgvCriteria.DataSource = _rubricManager.GetCriteriaByRubric(_currentRubric.RubricId);
                lblTotalWeight.Text = $"Total Weight: {_currentRubric.TotalWeight}";
            }
        }
    }
}