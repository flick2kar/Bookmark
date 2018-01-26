namespace NewSoft
{
    partial class AddBalance
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
            this.btnAddBal = new System.Windows.Forms.Button();
            this.lblAddBalance = new System.Windows.Forms.Label();
            this.txtAddBal = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.lblMemberID = new System.Windows.Forms.Label();
            this.txtMemberID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnAddBal
            // 
            this.btnAddBal.Location = new System.Drawing.Point(91, 175);
            this.btnAddBal.Name = "btnAddBal";
            this.btnAddBal.Size = new System.Drawing.Size(108, 23);
            this.btnAddBal.TabIndex = 0;
            this.btnAddBal.Text = "Add Balance";
            this.btnAddBal.UseVisualStyleBackColor = true;
            this.btnAddBal.Click += new System.EventHandler(this.btnAddBal_Click);
            // 
            // lblAddBalance
            // 
            this.lblAddBalance.AutoSize = true;
            this.lblAddBalance.Location = new System.Drawing.Point(49, 73);
            this.lblAddBalance.Name = "lblAddBalance";
            this.lblAddBalance.Size = new System.Drawing.Size(46, 13);
            this.lblAddBalance.TabIndex = 1;
            this.lblAddBalance.Text = "Balance";
            // 
            // txtAddBal
            // 
            this.txtAddBal.Location = new System.Drawing.Point(124, 71);
            this.txtAddBal.Name = "txtAddBal";
            this.txtAddBal.Size = new System.Drawing.Size(100, 20);
            this.txtAddBal.TabIndex = 2;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "DD-MM-YYYY";
            this.dateTimePicker1.Location = new System.Drawing.Point(124, 117);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(125, 20);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // lblMemberID
            // 
            this.lblMemberID.AutoSize = true;
            this.lblMemberID.Location = new System.Drawing.Point(49, 26);
            this.lblMemberID.Name = "lblMemberID";
            this.lblMemberID.Size = new System.Drawing.Size(56, 13);
            this.lblMemberID.TabIndex = 4;
            this.lblMemberID.Text = "MemberID";
            // 
            // txtMemberID
            // 
            this.txtMemberID.Location = new System.Drawing.Point(124, 26);
            this.txtMemberID.Name = "txtMemberID";
            this.txtMemberID.Size = new System.Drawing.Size(100, 20);
            this.txtMemberID.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Date of Trans";
            // 
            // AddBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 221);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMemberID);
            this.Controls.Add(this.lblMemberID);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.txtAddBal);
            this.Controls.Add(this.lblAddBalance);
            this.Controls.Add(this.btnAddBal);
            this.Name = "AddBalance";
            this.Text = "AddBalance";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddBal;
        private System.Windows.Forms.Label lblAddBalance;
        private System.Windows.Forms.TextBox txtAddBal;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label lblMemberID;
        private System.Windows.Forms.TextBox txtMemberID;
        private System.Windows.Forms.Label label2;
    }
}