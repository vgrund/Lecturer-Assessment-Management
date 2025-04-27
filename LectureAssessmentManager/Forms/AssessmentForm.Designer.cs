namespace LectureAssessmentManager.Forms
{
    partial class AssessmentForm
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
            this.components = new System.ComponentModel.Container();
            this.dgvAssessments = new System.Windows.Forms.DataGridView();
            this.cboAssignment = new System.Windows.Forms.ComboBox();
            this.cboStudent = new System.Windows.Forms.ComboBox();
            this.nudScore = new System.Windows.Forms.NumericUpDown();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblAssignment = new System.Windows.Forms.Label();
            this.lblStudent = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblComments = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvAssessments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScore)).BeginInit();
            this.SuspendLayout();

            // dgvAssessments
            this.dgvAssessments.AllowUserToAddRows = false;
            this.dgvAssessments.AllowUserToDeleteRows = false;
            this.dgvAssessments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAssessments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAssessments.Location = new System.Drawing.Point(12, 12);
            this.dgvAssessments.MultiSelect = false;
            this.dgvAssessments.Name = "dgvAssessments";
            this.dgvAssessments.ReadOnly = true;
            this.dgvAssessments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAssessments.Size = new System.Drawing.Size(776, 250);
            this.dgvAssessments.TabIndex = 0;
            this.dgvAssessments.SelectionChanged += new System.EventHandler(this.DgvAssessments_SelectionChanged);

            // lblAssignment
            this.lblAssignment.AutoSize = true;
            this.lblAssignment.Location = new System.Drawing.Point(12, 280);
            this.lblAssignment.Name = "lblAssignment";
            this.lblAssignment.Size = new System.Drawing.Size(64, 13);
            this.lblAssignment.TabIndex = 1;
            this.lblAssignment.Text = "Assignment:";

            // cboAssignment
            this.cboAssignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAssignment.FormattingEnabled = true;
            this.cboAssignment.Location = new System.Drawing.Point(92, 277);
            this.cboAssignment.Name = "cboAssignment";
            this.cboAssignment.Size = new System.Drawing.Size(250, 21);
            this.cboAssignment.TabIndex = 2;

            // lblStudent
            this.lblStudent.AutoSize = true;
            this.lblStudent.Location = new System.Drawing.Point(12, 307);
            this.lblStudent.Name = "lblStudent";
            this.lblStudent.Size = new System.Drawing.Size(47, 13);
            this.lblStudent.TabIndex = 3;
            this.lblStudent.Text = "Student:";

            // cboStudent
            this.cboStudent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStudent.FormattingEnabled = true;
            this.cboStudent.Location = new System.Drawing.Point(92, 304);
            this.cboStudent.Name = "cboStudent";
            this.cboStudent.Size = new System.Drawing.Size(250, 21);
            this.cboStudent.TabIndex = 4;

            // lblScore
            this.lblScore.AutoSize = true;
            this.lblScore.Location = new System.Drawing.Point(12, 334);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(38, 13);
            this.lblScore.TabIndex = 5;
            this.lblScore.Text = "Score:";

            // nudScore
            this.nudScore.Location = new System.Drawing.Point(92, 331);
            this.nudScore.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.nudScore.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudScore.Name = "nudScore";
            this.nudScore.Size = new System.Drawing.Size(80, 20);
            this.nudScore.TabIndex = 6;
            this.nudScore.Value = new decimal(new int[] { 0, 0, 0, 0 });

            // lblComments
            this.lblComments.AutoSize = true;
            this.lblComments.Location = new System.Drawing.Point(12, 360);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(59, 13);
            this.lblComments.TabIndex = 7;
            this.lblComments.Text = "Comments:";

            // txtComments
            this.txtComments.Location = new System.Drawing.Point(92, 357);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(250, 60);
            this.txtComments.TabIndex = 8;

            // btnAdd
            this.btnAdd.Location = new System.Drawing.Point(12, 435);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);

            // btnEdit
            this.btnEdit.Location = new System.Drawing.Point(93, 435);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 10;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);

            // btnDelete
            this.btnDelete.Location = new System.Drawing.Point(174, 435);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);

            // btnSave
            this.btnSave.Location = new System.Drawing.Point(632, 435);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(713, 435);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);

            // AssessmentForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 470);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtComments);
            this.Controls.Add(this.lblComments);
            this.Controls.Add(this.nudScore);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.cboStudent);
            this.Controls.Add(this.lblStudent);
            this.Controls.Add(this.cboAssignment);
            this.Controls.Add(this.lblAssignment);
            this.Controls.Add(this.dgvAssessments);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AssessmentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Assessments";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssessments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScore)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}