using System;
using System.Data;
using System.Windows.Forms;

namespace LectureAssessmentManager.Forms
{
    public partial class CourseSelectionForm : Form
    {
        public string SelectedCourseId { get; private set; }

        public CourseSelectionForm(DataTable availableCourses)
        {
            InitializeComponent();

            this.Text = "Select Course";
            this.Size = new System.Drawing.Size(600, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            dgvCourses.DataSource = availableCourses;
            dgvCourses.CellDoubleClick += DgvCourses_CellDoubleClick;
            btnSelect.Click += BtnSelect_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void DgvCourses_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SelectCourse();
            }
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            SelectCourse();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void SelectCourse()
        {
            if (dgvCourses.SelectedRows.Count > 0)
            {
                SelectedCourseId = dgvCourses.SelectedRows[0].Cells["CourseId"].Value.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a course.", "Selection Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}