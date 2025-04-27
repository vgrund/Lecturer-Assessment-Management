using System;
using System.Windows.Forms;
using LectureAssessmentManager.Models;
using LectureAssessmentManager.Managers;

namespace LectureAssessmentManager.Forms
{
    public partial class RubricCriterionForm : Form
    {
        private readonly RubricManager _rubricManager;
        private readonly RubricCriterion _criterion;
        private readonly string _rubricId;

        public RubricCriterionForm(RubricCriterion criterion, string rubricId, RubricManager rubricManager)
        {
            InitializeComponent();
            _criterion = criterion;
            _rubricId = rubricId;
            _rubricManager = rubricManager;
            InitializeGradingScale();
            LoadCriterion();
        }

        private void InitializeGradingScale()
        {
            // Set up the grading scale in the combo box
            cboGradeScale.Items.Add(new GradeLevel("Excellent", 70, 100));
            cboGradeScale.Items.Add(new GradeLevel("Distinction", 60, 69));
            cboGradeScale.Items.Add(new GradeLevel("Merit", 50, 59));
            cboGradeScale.Items.Add(new GradeLevel("Pass", 40, 49));
            cboGradeScale.Items.Add(new GradeLevel("Fail", 0, 39));
            cboGradeScale.SelectedIndex = 0;
        }

        private void LoadCriterion()
        {
            if (_criterion != null)
            {
                txtTitle.Text = _criterion.Title;
                txtDescription.Text = _criterion.Description;
                numWeight.Value = _criterion.Weight;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                if (_criterion == null)
                {
                    var totalWeight = _rubricManager.GetTotalWeightForRubric(_rubricId);
                    if (totalWeight + (int)numWeight.Value > 100)
                    {
                        MessageBox.Show($"The total weight of all criteria cannot exceed 100%. Current total: {totalWeight}%",
                            "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    _rubricManager.AddCriterion(
                        _rubricId,
                        txtTitle.Text,
                        txtDescription.Text,
                        (int)numWeight.Value
                    );
                }
                else
                {
                    var totalWeight = _rubricManager.GetTotalWeightForRubric(_rubricId) - _criterion.Weight;
                    if (totalWeight + (int)numWeight.Value > 100)
                    {
                        MessageBox.Show($"The total weight of all criteria cannot exceed 100%. Current total: {totalWeight}%",
                            "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    _criterion.Title = txtTitle.Text;
                    _criterion.Description = txtDescription.Text;
                    _criterion.Weight = (int)numWeight.Value;

                    _rubricManager.UpdateCriterion(_criterion);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Please enter a title for the criterion.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (numWeight.Value <= 0)
            {
                MessageBox.Show("Weight must be greater than 0.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }

    public class GradeLevel
    {
        public string Name { get; }
        public int MinScore { get; }
        public int MaxScore { get; }

        public GradeLevel(string name, int minScore, int maxScore)
        {
            Name = name;
            MinScore = minScore;
            MaxScore = maxScore;
        }

        public override string ToString()
        {
            return $"{Name} ({MinScore}-{MaxScore})";
        }
    }
}