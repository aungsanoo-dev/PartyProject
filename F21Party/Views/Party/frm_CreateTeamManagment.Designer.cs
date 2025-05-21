
namespace F21Party.Views
{
    partial class frm_CreateTeamManagment
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
            this.cboTeam = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboTotal = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboMax = new System.Windows.Forms.ComboBox();
            this.cboFullNames = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cboTeam
            // 
            this.cboTeam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTeam.FormattingEnabled = true;
            this.cboTeam.Location = new System.Drawing.Point(209, 85);
            this.cboTeam.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboTeam.Name = "cboTeam";
            this.cboTeam.Size = new System.Drawing.Size(261, 24);
            this.cboTeam.TabIndex = 0;
            this.cboTeam.SelectedIndexChanged += new System.EventHandler(this.cboTeam_SelectedIndexChanged);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(353, 220);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(92, 28);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(231, 220);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(92, 28);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Player Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 16);
            this.label4.TabIndex = 18;
            this.label4.Text = "Team Name:";
            // 
            // cboTotal
            // 
            this.cboTotal.Enabled = false;
            this.cboTotal.FormattingEnabled = true;
            this.cboTotal.Location = new System.Drawing.Point(209, 137);
            this.cboTotal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboTotal.Name = "cboTotal";
            this.cboTotal.Size = new System.Drawing.Size(101, 24);
            this.cboTotal.TabIndex = 55;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(52, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 16);
            this.label5.TabIndex = 56;
            this.label5.Text = "Total Player:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(332, 139);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 16);
            this.label6.TabIndex = 57;
            this.label6.Text = "Max:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboMax
            // 
            this.cboMax.Enabled = false;
            this.cboMax.FormattingEnabled = true;
            this.cboMax.Location = new System.Drawing.Point(369, 137);
            this.cboMax.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboMax.Name = "cboMax";
            this.cboMax.Size = new System.Drawing.Size(101, 24);
            this.cboMax.TabIndex = 58;
            // 
            // cboFullNames
            // 
            this.cboFullNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFullNames.FormattingEnabled = true;
            this.cboFullNames.Location = new System.Drawing.Point(209, 32);
            this.cboFullNames.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboFullNames.Name = "cboFullNames";
            this.cboFullNames.Size = new System.Drawing.Size(261, 24);
            this.cboFullNames.TabIndex = 59;
            // 
            // frm_CreateTeamManagment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 303);
            this.Controls.Add(this.cboFullNames);
            this.Controls.Add(this.cboMax);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboTotal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboTeam);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_CreateTeamManagment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Team Managment";
            this.Load += new System.EventHandler(this.frm_CreateTeamManagment_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox cboTeam;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.ComboBox cboTotal;
        public System.Windows.Forms.ComboBox cboMax;
        public System.Windows.Forms.ComboBox cboFullNames;
    }
}