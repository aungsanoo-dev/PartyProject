using F21Party.DBA;
using F21Party.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F21Party.Controllers
{
    internal class CtrlFrmDonationList
    {
        public frm_DonationList frmDonationList;
        public CtrlFrmDonationList(frm_DonationList donationListForm)
        {
            frmDonationList = donationListForm;
        }

        DbaConnection dbaConnection = new DbaConnection();
        UserControl DonationDetail;

        // For Mouse down event
        public UserControl activeDonationDetail = null;
        public DataGridViewRow expandedRow = null;
        //public bool clickedToggle = false;

        public Point? lastClickedCell = null;

        string SPString = "";

        public void RemoveDelete()
        {
            if(Program.UserAuthority != 1)
            {
                frmDonationList.toolStripSeparator3.Visible = false;
                frmDonationList.tsbDelete.Visible = false;
            }
            else
            {
                frmDonationList.toolStripSeparator3.Visible = true;
                frmDonationList.tsbDelete.Visible = true;
            }
        }
        public void ShowDonation()
        {
            DataGridViewTextBoxColumn DGCol = new DataGridViewTextBoxColumn();
            DGCol.DefaultCellStyle.NullValue = "+";
            DGCol.HeaderText = "";
            DGCol.Width = 30;
            DGCol.ReadOnly = true;
            DGCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            frmDonationList.dgvDonation.Columns.Add(DGCol);

            SPString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            frmDonationList.dgvDonation.DataSource = dbaConnection.SelectData(SPString);

            frmDonationList.dgvDonation.Columns[1].Width = (frmDonationList.dgvDonation.Width / 100) * 10;
            frmDonationList.dgvDonation.Columns[2].Visible = false;
            frmDonationList.dgvDonation.Columns[3].Width = (frmDonationList.dgvDonation.Width / 100) * 20;
            frmDonationList.dgvDonation.Columns[4].Visible = false;
            frmDonationList.dgvDonation.Columns[5].Width = (frmDonationList.dgvDonation.Width / 100) * 35;
            frmDonationList.dgvDonation.Columns[6].Width = (frmDonationList.dgvDonation.Width / 100) * 35;
            //frmDonationList.dgvDonation.Columns[7].Width = (frmDonationList.dgvDonation.Width / 100) * 30;
            //frmDonationList.dgvDonation.Columns[8].Width = (frmDonationList.dgvDonation.Width / 100) * 15;

            dbaConnection.ToolStripTextBoxData(frmDonationList.tstSearchWith, SPString, "DonationDate");
        }

        public void ShowDonationDetail()
        {
            //UserControl Form
            DonationDetail = new ctl_DonationDetail();
            DonationDetail.Hide();
            frmDonationList.Controls.Add(DonationDetail);
            frmDonationList.Controls.SetChildIndex(DonationDetail, 0);

        }

        //public void ShowDonationDetail(DataGridViewRow row)
        //{
        //    //UserControl Form
        //    DonationDetail = new ctl_DonationDetail();
        //    DonationDetail.Hide();
        //    frmDonationList.Controls.Add(DonationDetail);
        //    frmDonationList.Controls.SetChildIndex(DonationDetail, 0);

        //    //DonationDetail.Show();
        //    activeDonationDetail = (ctl_DonationDetail)DonationDetail;
        //    expandedRow = row;
        //}

        //public void DgvDonationCellClick(DataGridViewCellEventArgs e)
        //{
        //    if(e.RowIndex == -1)
        //    {
        //        return;
        //    }

        //    if (e.ColumnIndex == 0)
        //    {
        //        if (frmDonationList.dgvDonation[e.ColumnIndex, e.RowIndex].Value == null)
        //            frmDonationList.dgvDonation[e.ColumnIndex, e.RowIndex].Value = "+";

        //        if (frmDonationList.dgvDonation[e.ColumnIndex, e.RowIndex].Value.ToString().Trim() == "+")
        //        {
        //            Rectangle cellBounds = frmDonationList.dgvDonation.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
        //            Point offsetLocation = new Point(cellBounds.X, cellBounds.Y + cellBounds.Height);
        //            offsetLocation.Offset(frmDonationList.dgvDonation.Location);
        //            DonationDetail.Location = offsetLocation;
        //            int DonationID = (Convert.ToInt32(frmDonationList.dgvDonation.CurrentRow.Cells["DonationID"].Value.ToString()));

        //            DataGridView DGV = ((F21Party.Controllers.ctl_DonationDetail)(DonationDetail)).dgvDonationDetail;
        //            SPString = string.Format("SP_Select_DonationDetail N'{0}',N'{1}',N'{2}'", DonationID, "0", "0");
        //            DGV.DataSource = dbaConnection.SelectData(SPString);
        //            DGV.Columns[0].Visible = false;
        //            DGV.Columns[1].Visible = false;
        //            DGV.Columns[2].Width = (DGV.Width / 100) * 50;
        //            DGV.Columns[3].Width = (DGV.Width / 100) * 20;
        //            DGV.Columns[4].Width = (DGV.Width / 100) * 20;

        //            DonationDetail.Show();
        //            frmDonationList.dgvDonation[e.ColumnIndex, e.RowIndex].Value = "-";
        //        }
        //        else
        //        {
        //            DonationDetail.Hide();
        //            frmDonationList.dgvDonation[e.ColumnIndex, e.RowIndex].Value = "+";
        //        }
        //    }
        //}

        public void DgvDonationCellClick(DataGridViewCellEventArgs e)
        {
            lastClickedCell = new Point(e.ColumnIndex, e.RowIndex);
            if (e.RowIndex == -1 || e.ColumnIndex == -1) 
                return;

            var cell = frmDonationList.dgvDonation[e.ColumnIndex, e.RowIndex];
            if (e.ColumnIndex == 0)
            {
                if (cell.Value == null)
                    cell.Value = "+";

                if (cell.Value.ToString().Trim() == "+")
                {
                    Rectangle cellBounds = frmDonationList.dgvDonation.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                    Point offsetLocation = new Point(cellBounds.X, cellBounds.Y + cellBounds.Height);
                    offsetLocation.Offset(frmDonationList.dgvDonation.Location);

                    // Create or use the existing control
                    if (DonationDetail == null)
                        DonationDetail = new ctl_DonationDetail();

                    DonationDetail.Location = offsetLocation;

                    // Access specific properties through cast
                    var detailControl = (ctl_DonationDetail)DonationDetail;

                    int DonationID = Convert.ToInt32(frmDonationList.dgvDonation.Rows[e.RowIndex].Cells["DonationID"].Value.ToString());
                    string SPString = string.Format("SP_Select_DonationDetail N'{0}',N'{1}',N'{2}'", DonationID, "0", "0");

                    var DGV = detailControl.dgvDonationDetail;
                    DGV.DataSource = dbaConnection.SelectData(SPString);
                    DGV.Columns[0].Visible = false;
                    DGV.Columns[1].Visible = false;
                    DGV.Columns[2].Width = (DGV.Width / 100) * 50;
                    DGV.Columns[3].Width = (DGV.Width / 100) * 20;
                    DGV.Columns[4].Width = (DGV.Width / 100) * 15;

                    frmDonationList.Controls.Add(DonationDetail);
                    frmDonationList.Controls.SetChildIndex(DonationDetail, 0);
                    DonationDetail.Show();

                    cell.Value = "-";
                    activeDonationDetail = DonationDetail;
                    expandedRow = frmDonationList.dgvDonation.Rows[e.RowIndex];
                }
                else
                {
                    frmDonationList.Controls.Remove(DonationDetail);
                    cell.Value = "+";
                    activeDonationDetail = null;
                    expandedRow = null;
                }
            }
        }
        public void TsbNewClick()
        {
            frm_CreateDonation frm = new frm_CreateDonation();
            frm.ShowDialog();
            SPString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            frmDonationList.dgvDonation.DataSource = dbaConnection.SelectData(SPString);
        }

        public void TsmDonationDateClick()
        {
            frmDonationList.tslLabel.Text = "DonationDate";
            SPString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            dbaConnection.ToolStripTextBoxData(frmDonationList.tstSearchWith, SPString, "DonationDate");
        }

        public void TsmFullNameClick()
        {
            frmDonationList.tslLabel.Text = "FullName";
            SPString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            dbaConnection.ToolStripTextBoxData(frmDonationList.tstSearchWith, SPString, "FullName");
        }

        public void TsmSearchWithChanged()
        {
            if (frmDonationList.tslLabel.Text == "DonationDate")
            {
                SPString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", frmDonationList.tstSearchWith.Text.Trim().ToString(), "0", "3");
            }
            else if (frmDonationList.tslLabel.Text == "FullName")
            {
                SPString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", frmDonationList.tstSearchWith.Text.Trim().ToString(), "0", "4");
            }
            frmDonationList.dgvDonation.DataSource = dbaConnection.SelectData(SPString);
        }

        public void TsbDelete()
        {
            string DonationID = frmDonationList.dgvDonation.CurrentRow.Cells["DonationID"].Value.ToString();
            DbaDonation dbaDonation = new DbaDonation();
            DbaDonationDetail dbaDonationDetail = new DbaDonationDetail();

            if (frmDonationList.dgvDonation.CurrentRow.Cells[1].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else
            {
                if (MessageBox.Show("Are You Sure You Want To Delete?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dbaDonation.DID = Convert.ToInt32(DonationID);
                    dbaDonation.ACTION = 1;
                    dbaDonation.SaveData();

                    dbaDonationDetail.DID = Convert.ToInt32(DonationID);
                    dbaDonationDetail.ACTION = 1;
                    dbaDonationDetail.SaveData();
                    MessageBox.Show("Successfully Delete");
                    RemoveDelete();

                    SPString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", "0", "0", "0");
                    frmDonationList.dgvDonation.DataSource = dbaConnection.SelectData(SPString);
                    //ShowDonationDetail();
                }
            }
        }


        public void FrmDonationMouseDown(MouseEventArgs e)
        {
            
            if (activeDonationDetail != null)
            {
                //lastClickedCell = new Point(e.ColumnIndex, e.RowIndex);

                // Convert mouse click point to DataGridView client coords
                Point dgvPoint = frmDonationList.dgvDonation.PointToClient(Cursor.Position);

                var hit = frmDonationList.dgvDonation.HitTest(dgvPoint.X, dgvPoint.Y);

                // If click was on the same toggle cell, skip hiding here
                if (lastClickedCell.HasValue
                    && hit.ColumnIndex == lastClickedCell.Value.X
                    && hit.RowIndex == lastClickedCell.Value.Y)
                {
                    // Click was on the toggle cell itself, so do nothing here
                    return;
                }

                Point clickPoint = frmDonationList.PointToScreen(e.Location);
                Rectangle bounds = activeDonationDetail.RectangleToScreen(
                    activeDonationDetail.ClientRectangle);

                if (!bounds.Contains(clickPoint))
                {
                    frmDonationList.Controls.Remove(activeDonationDetail);
                    activeDonationDetail = null;

                    if (expandedRow != null)
                    {
                        expandedRow.Cells[0].Value = "+";
                        expandedRow = null;
                    }
                    lastClickedCell = null; // Reset after closing
                }
            }
        }
    }
}
