
namespace F21Party.Controllers
{
    partial class ctl_ItemRequestsDetail
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
            this.dgvItemRequestsDetail = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemRequestsDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvItemRequestsDetail
            // 
            this.dgvItemRequestsDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemRequestsDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItemRequestsDetail.Location = new System.Drawing.Point(0, 0);
            this.dgvItemRequestsDetail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvItemRequestsDetail.Name = "dgvItemRequestsDetail";
            this.dgvItemRequestsDetail.RowHeadersWidth = 62;
            this.dgvItemRequestsDetail.RowTemplate.Height = 28;
            this.dgvItemRequestsDetail.Size = new System.Drawing.Size(1022, 248);
            this.dgvItemRequestsDetail.TabIndex = 0;
            // 
            // ctl_ItemRequestsDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvItemRequestsDetail);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ctl_ItemRequestsDetail";
            this.Size = new System.Drawing.Size(1022, 248);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemRequestsDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dgvItemRequestsDetail;
    }
}
