using System;
using System.Drawing;
using System.Windows.Forms;
using LectureAssessmentManager.Models;
using LectureAssessmentManager.Managers;

namespace LectureAssessmentManager.Forms
{
    public partial class StudentMarkingForm : Form
    {
        private readonly MarkingManager _markingManager;
        private readonly RubricManager _rubricManager;
        private readonly string _studentId;
        private readonly string _assignmentId;
        private readonly string _rubricId;
        private StudentMark _currentMark;

        public StudentMarkingForm(string studentId, string assignmentId, string rubricId,
            MarkingManager markingManager, RubricManager rubricManager)
        {
            InitializeComponent();
            _studentId = studentId;
            _assignmentId = assignmentId;
            _rubricId = rubricId;
            _markingManager = markingManager;
            _rubricManager = rubricManager;
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Load or create mark
            _currentMark = _markingManager.GetMarkByStudent(_studentId, _assignmentId);
            if (_currentMark == null)
            {
                _currentMark = _markingManager.CreateMark(_studentId, _assignmentId, _rubricId);
            }

            // Load rubric
            var rubric = _rubricManager.GetRubricById(_rubricId);
            if (rubric == null)
            {
                MessageBox.Show("Could not load rubric.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            // Set up the marking panel
            int yPosition = 10;
            foreach (var criterion in rubric.Criteria)
            {
                var criterionMark = _currentMark.CriterionMarks.Find(cm => cm.CriterionId == criterion.CriterionId);

                // Create criterion group
                var grpCriterion = new GroupBox
                {
                    Text = $"{criterion.Title} (Weight: {criterion.Weight}%)",
                    Location = new Point(10, yPosition),
                    Size = new Size(pnlMarking.Width - 30, 120),
                    Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
                };

                // Add description label
                var lblDescription = new Label
                {
                    Text = criterion.Description,
                    Location = new Point(10, 20),
                    Size = new Size(grpCriterion.Width - 20, 40),
                    AutoSize = false
                };
                grpCriterion.Controls.Add(lblDescription);

                // Add score input
                var lblScore = new Label
                {
                    Text = "Score (0-100):",
                    Location = new Point(10, 70),
                    AutoSize = true
                };
                grpCriterion.Controls.Add(lblScore);

                var numScore = new NumericUpDown
                {
                    Location = new Point(100, 68),
                    Size = new Size(60, 20),
                    Minimum = 0,
                    Maximum = 100,
                    Value = (decimal)(criterionMark?.Score ?? 0),
                    Tag = criterion.CriterionId
                };
                numScore.ValueChanged += NumScore_ValueChanged;
                grpCriterion.Controls.Add(numScore);

                // Add comments input
                var lblComments = new Label
                {
                    Text = "Comments:",
                    Location = new Point(180, 70),
                    AutoSize = true
                };
                grpCriterion.Controls.Add(lblComments);

                var txtComments = new TextBox
                {
                    Location = new Point(250, 67),
                    Size = new Size(grpCriterion.Width - 270, 20),
                    Text = criterionMark?.Comments ?? string.Empty,
                    Tag = criterion.CriterionId,
                    Anchor = AnchorStyles.Left | AnchorStyles.Right
                };
                txtComments.TextChanged += TxtComments_TextChanged;
                grpCriterion.Controls.Add(txtComments);

                pnlMarking.Controls.Add(grpCriterion);
                yPosition += 130;
            }

            // Add overall comments
            var lblOverallComments = new Label
            {
                Text = "Overall Comments:",
                Location = new Point(10, yPosition),
                AutoSize = true
            };
            pnlMarking.Controls.Add(lblOverallComments);

            txtOverallComments.Location = new Point(10, yPosition + 20);
            txtOverallComments.Size = new Size(pnlMarking.Width - 30, 60);
            txtOverallComments.Multiline = true;
            txtOverallComments.Text = _currentMark.Comments;
            txtOverallComments.TextChanged += TxtOverallComments_TextChanged;
            pnlMarking.Controls.Add(txtOverallComments);

            UpdateTotalScore();
        }

        private void NumScore_ValueChanged(object sender, EventArgs e)
        {
            if (sender is NumericUpDown numScore)
            {
                var criterionId = (string)numScore.Tag;
                var criterionMark = _currentMark.CriterionMarks.Find(cm => cm.CriterionId == criterionId);
                if (criterionMark != null)
                {
                    criterionMark.Score = numScore.Value;
                    UpdateTotalScore();
                }
            }
        }

        private void TxtComments_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox txtComments)
            {
                var criterionId = (string)txtComments.Tag;
                var criterionMark = _currentMark.CriterionMarks.Find(cm => cm.CriterionId == criterionId);
                if (criterionMark != null)
                {
                    criterionMark.Comments = txtComments.Text;
                }
            }
        }

        private void TxtOverallComments_TextChanged(object sender, EventArgs e)
        {
            _currentMark.Comments = txtOverallComments.Text;
        }

        private void UpdateTotalScore()
        {
            _markingManager.CalculateFinalScore(_currentMark);
            lblTotalScore.Text = $"Total Score: {_currentMark.FinalScore:F1}";
            lblGrade.Text = $"Grade: {_currentMark.FinalGrade}";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (_markingManager.ValidateMarks(_currentMark))
            {
                _markingManager.UpdateMark(_currentMark);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Please ensure all scores are between 0 and 100.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}