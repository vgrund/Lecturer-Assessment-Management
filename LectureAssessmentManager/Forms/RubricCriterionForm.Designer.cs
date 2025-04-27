namespace LectureAssessmentManager.Forms
{
    partial class RubricCriterionForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblWeight = new System.Windows.Forms.Label();
            this.numWeight = new System.Windows.Forms.NumericUpDown();
            this.grpGrading = new System.Windows.Forms.GroupBox();
            this.lblGradeScale = new System.Windows.Forms.Label();
            this.cboGradeScale = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.numWeight)).BeginInit();
            this.grpGrading.SuspendLayout();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(12, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(35, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Title:";

            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(89, 12);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(483, 20);
            this.txtTitle.TabIndex = 1;

            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(12, 41);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(63, 13);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description:";

            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(89, 38);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(483, 60);
            this.txtDescription.TabIndex = 3;

            // 
            // lblWeight
            // 
            this.lblWeight.AutoSize = true;
            this.lblWeight.Location = new System.Drawing.Point(12, 107);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(71, 13);
            this.lblWeight.TabIndex = 4;
            this.lblWeight.Text = "Weight (%):";

            // 
            // numWeight
            // 
            this.numWeight.Location = new System.Drawing.Point(89, 105);
            this.numWeight.Name = "numWeight";
            this.numWeight.Size = new System.Drawing.Size(60, 20);
            this.numWeight.TabIndex = 5;
            this.numWeight.Minimum = 1;
            this.numWeight.Maximum = 100;
            this.numWeight.Value = 1;

            // 
            // grpGrading
            // 
            this.grpGrading.Controls.Add(this.lblGradeScale);
            this.grpGrading.Controls.Add(this.cboGradeScale);
            this.grpGrading.Location = new System.Drawing.Point(12, 131);
            this.grpGrading.Name = "grpGrading";
            this.grpGrading.Size = new System.Drawing.Size(560, 100);
            this.grpGrading.TabIndex = 6;
            this.grpGrading.Text = "Grading Scale";

            // 
            // lblGradeScale
            // 
            this.lblGradeScale.AutoSize = true;
            this.lblGradeScale.Location = new System.Drawing.Point(6, 22);
            this.lblGradeScale.Name = "lblGradeScale";
            this.lblGradeScale.Size = new System.Drawing.Size(77, 13);
            this.lblGradeScale.TabIndex = 0;
            this.lblGradeScale.Text = "Grade Scale:";

            // 
            // cboGradeScale
            // 
            this.cboGradeScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGradeScale.FormattingEnabled = true;
            this.cboGradeScale.Location = new System.Drawing.Point(89, 19);
            this.cboGradeScale.Name = "cboGradeScale";
            this.cboGradeScale.Size = new System.Drawing.Size(200, 21);
            this.cboGradeScale.TabIndex = 1;

            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(416, 237);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);

            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(497, 237);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);

            // 
            // RubricCriterionForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(584, 272);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpGrading);
            this.Controls.Add(this.numWeight);
            this.Controls.Add(this.lblWeight);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblTitle);
            this.MinimumSize = new System.Drawing.Size(600, 311);
            this.Name = "RubricCriterionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rubric Criterion";
            ((System.ComponentModel.ISupportInitialize)(this.numWeight)).EndInit();
            this.grpGrading.ResumeLayout(false);
            this.grpGrading.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.NumericUpDown numWeight;
        private System.Windows.Forms.GroupBox grpGrading;
        private System.Windows.Forms.Label lblGradeScale;
        private System.Windows.Forms.ComboBox cboGradeScale;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}