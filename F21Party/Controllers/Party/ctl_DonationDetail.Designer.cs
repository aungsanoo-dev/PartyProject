
namespace F21Party.Controllers
{
    partial class ctl_DonationDetail
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvDonationDetail = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonationDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDonationDetail
            // 
            this.dgvDonationDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDonationDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDonationDetail.Location = new System.Drawing.Point(0, 0);
            this.dgvDonationDetail.Name = "dgvDonationDetail";
            this.dgvDonationDetail.RowHeadersWidth = 62;
            this.dgvDonationDetail.RowTemplate.Height = 28;
            this.dgvDonationDetail.Size = new System.Drawing.Size(1114, 334);
            this.dgvDonationDetail.TabIndex = 0;
            // 
            // ctl_DonationDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvDonationDetail);
            this.Name = "ctl_DonationDetail";
            this.Size = new System.Drawing.Size(1114, 334);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonationDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dgvDonationDetail;
    }
}
