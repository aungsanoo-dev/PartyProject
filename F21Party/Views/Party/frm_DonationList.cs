using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using F21Party.Controllers.Party;
using F21Party.DBA;


namespace F21Party.Donation
{
    public partial class frm_DonationList : Form
    {
        private CtrlFrmDonationList ctrlFrmDonationList;
        
        public frm_DonationList()
        {
            InitializeComponent();
            ctrlFrmDonationList = new CtrlFrmDonationList(this);
        }
        //public Point? lastClickedCell = null;
        private void frm_DonationList_Load(object sender, EventArgs e)
        {
            ctrlFrmDonationList.RemoveDelete();
            ctrlFrmDonationList.ShowDonation();
            ctrlFrmDonationList.ShowDonationDetail();

            foreach (Control ctrl in this.Controls)
            {
                ctrl.MouseDown += frm_DonationList_MouseDown;
            }
        }

        private void dgvDonation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ctrlFrmDonationList.DgvDonationCellClick(e);
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            ctrlFrmDonationList.TsbNewClick();
        }

        private void tsmDonationDate_Click(object sender, EventArgs e)
        {
            ctrlFrmDonationList.TsmDonationDateClick();
        }

        private void tsmFullName_Click(object sender, EventArgs e)
        {
            ctrlFrmDonationList.TsmFullNameClick();
        }

        private void tstSearchWith_TextChanged(object sender, EventArgs e)
        {
            ctrlFrmDonationList.TsmSearchWithChanged();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            ctrlFrmDonationList.TsbDelete();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_DonationList_MouseDown(object sender, MouseEventArgs e)
        {
            ctrlFrmDonationList.FrmDonationMouseDown(e);

            //if (ctrlFrmDonationList.activeDonationDetail != null)
            //{
            //    //lastClickedCell = new Point(e.ColumnIndex, e.RowIndex);

            //    // Convert mouse click point to DataGridView client coords
            //    Point dgvPoint = dgvDonation.PointToClient(Cursor.Position);

            //    var hit = dgvDonation.HitTest(dgvPoint.X, dgvPoint.Y);

            //    // If click was on the same toggle cell, skip hiding here
            //    if (lastClickedCell.HasValue
            //        && hit.ColumnIndex == lastClickedCell.Value.X
            //        && hit.RowIndex == lastClickedCell.Value.Y)
            //    {
            //        // Click was on the toggle cell itself, so do nothing here
            //        return;
            //    }

            //    Point clickPoint = this.PointToScreen(e.Location);
            //    Rectangle bounds = ctrlFrmDonationList.activeDonationDetail.RectangleToScreen(
            //        ctrlFrmDonationList.activeDonationDetail.ClientRectangle);

            //    if (!bounds.Contains(clickPoint))
            //    {
            //        this.Controls.Remove(ctrlFrmDonationList.activeDonationDetail);
            //        ctrlFrmDonationList.activeDonationDetail = null;

            //        if (ctrlFrmDonationList.expandedRow != null)
            //        {
            //            ctrlFrmDonationList.expandedRow.Cells[0].Value = "+";
            //            ctrlFrmDonationList.expandedRow = null;
            //        }
            //        lastClickedCell = null; // Reset after closing
            //    }
            //}


        }


        //if (ctrlFrmDonationList.activeDonationDetail != null)
        //{
        //    Point clickPoint = this.PointToScreen(e.Location);
        //    Rectangle controlBounds = ctrlFrmDonationList.activeDonationDetail.RectangleToScreen(
        //        ctrlFrmDonationList.activeDonationDetail.ClientRectangle);

        //    if (!controlBounds.Contains(clickPoint))
        //    {
        //        this.Controls.Remove(ctrlFrmDonationList.activeDonationDetail);
        //        ctrlFrmDonationList.activeDonationDetail = null;
        //    }
        //}

        //if (ctrlFrmDonationList.activeDonationDetail != null)
        //{
        //    Point clickPoint = this.PointToScreen(e.Location);
        //    Rectangle controlBounds = ctrlFrmDonationList.activeDonationDetail.RectangleToScreen(
        //        ctrlFrmDonationList.activeDonationDetail.ClientRectangle);

        //    if (!controlBounds.Contains(clickPoint))
        //    {
        //        // Remove the control
        //        this.Controls.Remove(ctrlFrmDonationList.activeDonationDetail);
        //        ctrlFrmDonationList.activeDonationDetail = null;

        //        // Reset "+" / "-" icon in the row
        //        if (ctrlFrmDonationList.expandedRow != null)
        //        {
        //            ctrlFrmDonationList.expandedRow.Cells[0].Value = "+"; // assuming cell[0] is the icon
        //            ctrlFrmDonationList.expandedRow = null;
        //        }
        //    }
        //}


    }
}
