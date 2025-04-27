namespace LectureAssessmentManager.Forms
{
    partial class RubricForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.grpCriteria = new System.Windows.Forms.GroupBox();
            this.dgvCriteria = new System.Windows.Forms.DataGridView();
            this.btnAddCriterion = new System.Windows.Forms.Button();
            this.btnEditCriterion = new System.Windows.Forms.Button();
            this.btnDeleteCriterion = new System.Windows.Forms.Button();
            this.lblTotalWeight = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

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
            // grpCriteria
            // 
            this.grpCriteria.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCriteria.Controls.Add(this.dgvCriteria);
            this.grpCriteria.Location = new System.Drawing.Point(12, 104);
            this.grpCriteria.Name = "grpCriteria";
            this.grpCriteria.Size = new System.Drawing.Size(560, 250);
            this.grpCriteria.TabIndex = 4;
            this.grpCriteria.Text = "Criteria";

            // 
            // dgvCriteria
            // 
            this.dgvCriteria.AllowUserToAddRows = false;
            this.dgvCriteria.AllowUserToDeleteRows = false;
            this.dgvCriteria.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCriteria.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCriteria.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCriteria.Location = new System.Drawing.Point(6, 19);
            this.dgvCriteria.MultiSelect = false;
            this.dgvCriteria.Name = "dgvCriteria";
            this.dgvCriteria.ReadOnly = true;
            this.dgvCriteria.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCriteria.Size = new System.Drawing.Size(548, 225);
            this.dgvCriteria.TabIndex = 0;

            // 
            // btnAddCriterion
            // 
            this.btnAddCriterion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddCriterion.Location = new System.Drawing.Point(12, 360);
            this.btnAddCriterion.Name = "btnAddCriterion";
            this.btnAddCriterion.Size = new System.Drawing.Size(75, 23);
            this.btnAddCriterion.TabIndex = 5;
            this.btnAddCriterion.Text = "Add";

            // 
            // btnEditCriterion
            // 
            this.btnEditCriterion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditCriterion.Location = new System.Drawing.Point(93, 360);
            this.btnEditCriterion.Name = "btnEditCriterion";
            this.btnEditCriterion.Size = new System.Drawing.Size(75, 23);
            this.btnEditCriterion.TabIndex = 6;
            this.btnEditCriterion.Text = "Edit";

            // 
            // btnDeleteCriterion
            // 
            this.btnDeleteCriterion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteCriterion.Location = new System.Drawing.Point(174, 360);
            this.btnDeleteCriterion.Name = "btnDeleteCriterion";
            this.btnDeleteCriterion.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteCriterion.TabIndex = 7;
            this.btnDeleteCriterion.Text = "Delete";

            // 
            // lblTotalWeight
            // 
            this.lblTotalWeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalWeight.AutoSize = true;
            this.lblTotalWeight.Location = new System.Drawing.Point(447, 365);
            this.lblTotalWeight.Name = "lblTotalWeight";
            this.lblTotalWeight.Size = new System.Drawing.Size(71, 13);
            this.lblTotalWeight.TabIndex = 8;
            this.lblTotalWeight.Text = "Total Weight: 0";

            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(416, 400);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";

            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(497, 400);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";

            // 
            // RubricForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(584, 435);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblTotalWeight);
            this.Controls.Add(this.btnDeleteCriterion);
            this.Controls.Add(this.btnEditCriterion);
            this.Controls.Add(this.btnAddCriterion);
            this.Controls.Add(this.grpCriteria);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblTitle);
            this.MinimumSize = new System.Drawing.Size(600, 474);
            this.Name = "RubricForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rubric";
            this.grpCriteria.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCriteria)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.GroupBox grpCriteria;
        private System.Windows.Forms.DataGridView dgvCriteria;
        private System.Windows.Forms.Button btnAddCriterion;
        private System.Windows.Forms.Button btnEditCriterion;
        private System.Windows.Forms.Button btnDeleteCriterion;
        private System.Windows.Forms.Label lblTotalWeight;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}