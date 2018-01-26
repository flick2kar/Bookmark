namespace NewSoft
{
    partial class AddFine
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
            this.lblDateAddFine = new System.Windows.Forms.Label();
            this.txtFineMemberID = new System.Windows.Forms.TextBox();
            this.lblFineMemberID = new System.Windows.Forms.Label();
            this.dateAddFine = new System.Windows.Forms.DateTimePicker();
            this.txtAddFine = new System.Windows.Forms.TextBox();
            this.lblAddFine = new System.Windows.Forms.Label();
            this.btnAddFinePop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDateAddFine
            // 
            this.lblDateAddFine.AutoSize = true;
            this.lblDateAddFine.Location = new System.Drawing.Point(35, 137);
            this.lblDateAddFine.Name = "lblDateAddFine";
            this.lblDateAddFine.Size = new System.Drawing.Size(72, 13);
            this.lblDateAddFine.TabIndex = 13;
            this.lblDateAddFine.Text = "Date of Trans";
            // 
            // txtFineMemberID
            // 
            this.txtFineMemberID.Location = new System.Drawing.Point(125, 44);
            this.txtFineMemberID.Name = "txtFineMemberID";
            this.txtFineMemberID.Size = new System.Drawing.Size(100, 20);
            this.txtFineMemberID.TabIndex = 12;
            // 
            // lblFineMemberID
            // 
            this.lblFineMemberID.AutoSize = true;
            this.lblFineMemberID.Location = new System.Drawing.Point(50, 44);
            this.lblFineMemberID.Name = "lblFineMemberID";
            this.lblFineMemberID.Size = new System.Drawing.Size(56, 13);
            this.lblFineMemberID.TabIndex = 11;
            this.lblFineMemberID.Text = "MemberID";
            // 
            // dateAddFine
            // 
            this.dateAddFine.CustomFormat = "DD-MM-YYYY";
            this.dateAddFine.Location = new System.Drawing.Point(125, 135);
            this.dateAddFine.Name = "dateAddFine";
            this.dateAddFine.Size = new System.Drawing.Size(125, 20);
            this.dateAddFine.TabIndex = 10;
            // 
            // txtAddFine
            // 
            this.txtAddFine.Location = new System.Drawing.Point(125, 89);
            this.txtAddFine.Name = "txtAddFine";
            this.txtAddFine.Size = new System.Drawing.Size(100, 20);
            this.txtAddFine.TabIndex = 9;
            // 
            // lblAddFine
            // 
            this.lblAddFine.AutoSize = true;
            this.lblAddFine.Location = new System.Drawing.Point(50, 91);
            this.lblAddFine.Name = "lblAddFine";
            this.lblAddFine.Size = new System.Drawing.Size(27, 13);
            this.lblAddFine.TabIndex = 8;
            this.lblAddFine.Text = "Fine";
            // 
            // btnAddFinePop
            // 
            this.btnAddFinePop.Location = new System.Drawing.Point(92, 193);
            this.btnAddFinePop.Name = "btnAddFinePop";
            this.btnAddFinePop.Size = new System.Drawing.Size(108, 23);
            this.btnAddFinePop.TabIndex = 7;
            this.btnAddFinePop.Text = "Add Fine";
            this.btnAddFinePop.UseVisualStyleBackColor = true;
            this.btnAddFinePop.Click += new System.EventHandler(this.btnAddFinePop_Click);
            // 
            // AddFine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.lblDateAddFine);
            this.Controls.Add(this.txtFineMemberID);
            this.Controls.Add(this.lblFineMemberID);
            this.Controls.Add(this.dateAddFine);
            this.Controls.Add(this.txtAddFine);
            this.Controls.Add(this.lblAddFine);
            this.Controls.Add(this.btnAddFinePop);
            this.Name = "AddFine";
            this.Text = "AddFine";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDateAddFine;
        private System.Windows.Forms.TextBox txtFineMemberID;
        private System.Windows.Forms.Label lblFineMemberID;
        private System.Windows.Forms.DateTimePicker dateAddFine;
        private System.Windows.Forms.TextBox txtAddFine;
        private System.Windows.Forms.Label lblAddFine;
        private System.Windows.Forms.Button btnAddFinePop;
    }
}