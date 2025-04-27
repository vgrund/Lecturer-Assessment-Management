namespace LectureAssessmentManager.Forms
{
    partial class GradeAssignmentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblAssignmentInfo = new System.Windows.Forms.Label();
            this.dgvStudents = new System.Windows.Forms.DataGridView();
            this.pnlGrade = new System.Windows.Forms.Panel();
            this.lblStudent = new System.Windows.Forms.Label();
            this.txtStudent = new System.Windows.Forms.TextBox();
            this.lblScore = new System.Windows.Forms.Label();
            this.nudScore = new System.Windows.Forms.NumericUpDown();
            this.lblFeedback = new System.Windows.Forms.Label();
            this.txtFeedback = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScore)).BeginInit();
            this.pnlGrade.SuspendLayout();
            this.SuspendLayout();

            // lblAssignmentInfo
            this.lblAssignmentInfo.AutoSize = true;
            this.lblAssignmentInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblAssignmentInfo.Location = new System.Drawing.Point(12, 9);
            this.lblAssignmentInfo.Name = "lblAssignmentInfo";
            this.lblAssignmentInfo.Size = new System.Drawing.Size(200, 16);
            this.lblAssignmentInfo.Text = "Assignment Information";

            // dgvStudents
            this.dgvStudents.AllowUserToAddRows = false;
            this.dgvStudents.AllowUserToDeleteRows = false;
            this.dgvStudents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStudents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStudents.Location = new System.Drawing.Point(12, 35);
            this.dgvStudents.MultiSelect = false;
            this.dgvStudents.Name = "dgvStudents";
            this.dgvStudents.ReadOnly = true;
            this.dgvStudents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStudents.Size = new System.Drawing.Size(560, 200);
            this.dgvStudents.TabIndex = 0;

            // pnlGrade
            this.pnlGrade.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGrade.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGrade.Location = new System.Drawing.Point(12, 241);
            this.pnlGrade.Name = "pnlGrade";
            this.pnlGrade.Size = new System.Drawing.Size(560, 200);
            this.pnlGrade.TabIndex = 1;
            this.pnlGrade.Padding = new System.Windows.Forms.Padding(10);

            // lblStudent
            this.lblStudent.AutoSize = true;
            this.lblStudent.Location = new System.Drawing.Point(13, 13);
            this.lblStudent.Name = "lblStudent";
            this.lblStudent.Size = new System.Drawing.Size(47, 13);
            this.lblStudent.Text = "Student:";

            // txtStudent
            this.txtStudent.Location = new System.Drawing.Point(80, 10);
            this.txtStudent.Name = "txtStudent";
            this.txtStudent.ReadOnly = true;
            this.txtStudent.Size = new System.Drawing.Size(250, 20);
            this.txtStudent.TabIndex = 0;

            // lblScore
            this.lblScore.AutoSize = true;
            this.lblScore.Location = new System.Drawing.Point(13, 39);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(38, 13);
            this.lblScore.Text = "Score:";

            // nudScore
            this.nudScore.Location = new System.Drawing.Point(80, 37);
            this.nudScore.Name = "nudScore";
            this.nudScore.Size = new System.Drawing.Size(80, 20);
            this.nudScore.TabIndex = 1;
            this.nudScore.Minimum = 0;
            this.nudScore.Maximum = 100;

            // lblFeedback
            this.lblFeedback.AutoSize = true;
            this.lblFeedback.Location = new System.Drawing.Point(13, 65);
            this.lblFeedback.Name = "lblFeedback";
            this.lblFeedback.Size = new System.Drawing.Size(58, 13);
            this.lblFeedback.Text = "Feedback:";

            // txtFeedback
            this.txtFeedback.Location = new System.Drawing.Point(80, 62);
            this.txtFeedback.Multiline = true;
            this.txtFeedback.Name = "txtFeedback";
            this.txtFeedback.Size = new System.Drawing.Size(450, 80);
            this.txtFeedback.TabIndex = 2;

            // btnSave
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(374, 154);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;

            // btnClose
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(455, 154);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;

            // GradeAssignmentForm
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.Controls.Add(this.lblAssignmentInfo);
            this.Controls.Add(this.dgvStudents);
            this.Controls.Add(this.pnlGrade);
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "GradeAssignmentForm";
            this.Text = "Grade Assignment";

            // Add controls to grade panel
            this.pnlGrade.Controls.Add(this.lblStudent);
            this.pnlGrade.Controls.Add(this.txtStudent);
            this.pnlGrade.Controls.Add(this.lblScore);
            this.pnlGrade.Controls.Add(this.nudScore);
            this.pnlGrade.Controls.Add(this.lblFeedback);
            this.pnlGrade.Controls.Add(this.txtFeedback);
            this.pnlGrade.Controls.Add(this.btnSave);
            this.pnlGrade.Controls.Add(this.btnClose);

            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScore)).EndInit();
            this.pnlGrade.ResumeLayout(false);
            this.pnlGrade.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblAssignmentInfo;
        private System.Windows.Forms.DataGridView dgvStudents;
        private System.Windows.Forms.Panel pnlGrade;
        private System.Windows.Forms.Label lblStudent;
        private System.Windows.Forms.TextBox txtStudent;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.NumericUpDown nudScore;
        private System.Windows.Forms.Label lblFeedback;
        private System.Windows.Forms.TextBox txtFeedback;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
    }
}