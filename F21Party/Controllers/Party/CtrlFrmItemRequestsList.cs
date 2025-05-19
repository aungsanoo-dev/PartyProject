using F21Party.DBA;
using F21Party.Donation;
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
    internal class CtrlFrmItemRequestsList
    {
        public frm_ItemRequestsList frmItemRequestsList;
        public CtrlFrmItemRequestsList(frm_ItemRequestsList requestsListForm)
        {
            frmItemRequestsList = requestsListForm;
        }

        DbaConnection dbaConnection = new DbaConnection();
        UserControl ItemRequestsDetail;

        // For Mouse down event
        public UserControl activeItemRequestsDetail = null;
        public DataGridViewRow expandedRow = null;

        public Point? lastClickedCell = null;

        string SPString = "";

        public void RemoveDelete()
        {
            if (Program.UserAuthority != 1)
            {
                frmItemRequestsList.toolStripSeparator3.Visible = false;
                frmItemRequestsList.tsbDelete.Visible = false;
            }
            else
            {
                frmItemRequestsList.toolStripSeparator3.Visible = true;
                frmItemRequestsList.tsbDelete.Visible = true;
            }
        }

        public void ShowItemRequests()
        {
            DataGridViewTextBoxColumn DGCol = new DataGridViewTextBoxColumn();
            DGCol.DefaultCellStyle.NullValue = "+";
            DGCol.HeaderText = "";
            DGCol.Width = 30;
            DGCol.ReadOnly = true;
            DGCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            frmItemRequestsList.dgvItemRequests.Columns.Add(DGCol);

            SPString = string.Format("SP_Select_ItemRequests N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            frmItemRequestsList.dgvItemRequests.DataSource = dbaConnection.SelectData(SPString);

            frmItemRequestsList.dgvItemRequests.Columns[1].Width = (frmItemRequestsList.dgvItemRequests.Width / 100) * 10;
            frmItemRequestsList.dgvItemRequests.Columns[2].Visible = false;
            frmItemRequestsList.dgvItemRequests.Columns[3].Width = (frmItemRequestsList.dgvItemRequests.Width / 100) * 20;
            frmItemRequestsList.dgvItemRequests.Columns[4].Visible = false;
            frmItemRequestsList.dgvItemRequests.Columns[5].Width = (frmItemRequestsList.dgvItemRequests.Width / 100) * 35;
            frmItemRequestsList.dgvItemRequests.Columns[6].Width = (frmItemRequestsList.dgvItemRequests.Width / 100) * 35;
            //frmItemRequestsList.dgvItemRequests.Columns[7].Width = (frmItemRequestsList.dgvItemRequests.Width / 100) * 30;
            //frmItemRequestsList.dgvItemRequests.Columns[8].Width = (frmItemRequestsList.dgvItemRequests.Width / 100) * 15;

            dbaConnection.ToolStripTextBoxData(frmItemRequestsList.tstSearchWith, SPString, "RequestDate");
        }

        public void ShowItemRequestsDetail()
        {
            //UserControl Form
            ItemRequestsDetail = new ctl_ItemRequestsDetail();
            ItemRequestsDetail.Hide();
            frmItemRequestsList.Controls.Add(ItemRequestsDetail);
            frmItemRequestsList.Controls.SetChildIndex(ItemRequestsDetail, 0);

        }

        public void DgvItemRequestsCellClick(DataGridViewCellEventArgs e)
        {
            lastClickedCell = new Point(e.ColumnIndex, e.RowIndex);
            if (e.RowIndex == -1 || e.ColumnIndex == -1) 
                return;

            var cell = frmItemRequestsList.dgvItemRequests[e.ColumnIndex, e.RowIndex];
            if (e.ColumnIndex == 0)
            {
                if (cell.Value == null)
                    cell.Value = "+";

                if (cell.Value.ToString().Trim() == "+")
                {
                    Rectangle cellBounds = frmItemRequestsList.dgvItemRequests.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                    Point offsetLocation = new Point(cellBounds.X, cellBounds.Y + cellBounds.Height);
                    offsetLocation.Offset(frmItemRequestsList.dgvItemRequests.Location);

                    // Create or use the existing control
                    if (ItemRequestsDetail == null)
                        ItemRequestsDetail = new ctl_ItemRequestsDetail();

                    ItemRequestsDetail.Location = offsetLocation;

                    // Access specific properties through cast
                    var detailControl = (ctl_ItemRequestsDetail)ItemRequestsDetail;

                    int ItemRequestsID = Convert.ToInt32(frmItemRequestsList.dgvItemRequests.Rows[e.RowIndex].Cells["RequestID"].Value.ToString());
                    string SPString = string.Format("SP_Select_ItemRequestsDetail N'{0}',N'{1}',N'{2}'", ItemRequestsID, "0", "0");

                    var DGV = detailControl.dgvItemRequestsDetail;
                    DGV.DataSource = dbaConnection.SelectData(SPString);
                    DGV.Columns[0].Visible = false;
                    DGV.Columns[1].Visible = false;
                    DGV.Columns[2].Width = (DGV.Width / 100) * 50;
                    DGV.Columns[3].Width = (DGV.Width / 100) * 20;
                    DGV.Columns[4].Width = (DGV.Width / 100) * 15;

                    frmItemRequestsList.Controls.Add(ItemRequestsDetail);
                    frmItemRequestsList.Controls.SetChildIndex(ItemRequestsDetail, 0);
                    ItemRequestsDetail.Show();

                    cell.Value = "-";
                    activeItemRequestsDetail = ItemRequestsDetail;
                    expandedRow = frmItemRequestsList.dgvItemRequests.Rows[e.RowIndex];
                }
                else
                {
                    frmItemRequestsList.Controls.Remove(ItemRequestsDetail);
                    cell.Value = "+";
                    activeItemRequestsDetail = null;
                    expandedRow = null;
                }
            }
        }

        public void TsbNewClick()
        {
            frm_CreateItemRequests frm = new frm_CreateItemRequests();
            frm.ShowDialog();
            SPString = string.Format("SP_Select_ItemRequests N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            frmItemRequestsList.dgvItemRequests.DataSource = dbaConnection.SelectData(SPString);
        }

        public void TsmRequestDateClick()
        {
            frmItemRequestsList.tslLabel.Text = "RequestDate";
            SPString = string.Format("SP_Select_ItemRequests N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            dbaConnection.ToolStripTextBoxData(frmItemRequestsList.tstSearchWith, SPString, "RequestDate");
        }

        public void TsmFullNameClick()
        {
            frmItemRequestsList.tslLabel.Text = "FullName";
            SPString = string.Format("SP_Select_ItemRequests N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            dbaConnection.ToolStripTextBoxData(frmItemRequestsList.tstSearchWith, SPString, "FullName");
        }

        public void TsmSearchWithChanged()
        {
            if (frmItemRequestsList.tslLabel.Text == "RequestDate")
            {
                SPString = string.Format("SP_Select_ItemRequests N'{0}',N'{1}',N'{2}'", frmItemRequestsList.tstSearchWith.Text.Trim().ToString(), "0", "3");
            }
            else if (frmItemRequestsList.tslLabel.Text == "FullName")
            {
                SPString = string.Format("SP_Select_ItemRequests N'{0}',N'{1}',N'{2}'", frmItemRequestsList.tstSearchWith.Text.Trim().ToString(), "0", "4");
            }
            frmItemRequestsList.dgvItemRequests.DataSource = dbaConnection.SelectData(SPString);
        }

        public void TsbDelete()
        {
            string ItemRequestsID = frmItemRequestsList.dgvItemRequests.CurrentRow.Cells["RequestID"].Value.ToString();
            DbaItemRequests dbaItemRequests = new DbaItemRequests();
            DbaItemRequestsDetail dbaItemRequestsDetail = new DbaItemRequestsDetail();

            if (frmItemRequestsList.dgvItemRequests.CurrentRow.Cells[1].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else
            {
                if (MessageBox.Show("Are You Sure You Want To Delete?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dbaItemRequests.RID = Convert.ToInt32(ItemRequestsID);
                    dbaItemRequests.ACTION = 1;
                    dbaItemRequests.SaveData();

                    dbaItemRequestsDetail.RID = Convert.ToInt32(ItemRequestsID);
                    dbaItemRequestsDetail.ACTION = 1;
                    dbaItemRequestsDetail.SaveData();
                    MessageBox.Show("Successfully Delete");
                    RemoveDelete();

                    SPString = string.Format("SP_Select_ItemRequests N'{0}',N'{1}',N'{2}'", "0", "0", "0");
                    frmItemRequestsList.dgvItemRequests.DataSource = dbaConnection.SelectData(SPString);
                    //ShowItemRequestsDetail();
                }
            }
        }

        public void FrmItemRequestsMouseDown(MouseEventArgs e)
        {

            if (activeItemRequestsDetail != null)
            {
                //lastClickedCell = new Point(e.ColumnIndex, e.RowIndex);

                // Convert mouse click point to DataGridView client coords
                Point dgvPoint = frmItemRequestsList.dgvItemRequests.PointToClient(Cursor.Position);

                var hit = frmItemRequestsList.dgvItemRequests.HitTest(dgvPoint.X, dgvPoint.Y);

                // If click was on the same toggle cell, skip hiding here
                if (lastClickedCell.HasValue
                    && hit.ColumnIndex == lastClickedCell.Value.X
                    && hit.RowIndex == lastClickedCell.Value.Y)
                {
                    // Click was on the toggle cell itself, so do nothing here
                    return;
                }

                Point clickPoint = frmItemRequestsList.PointToScreen(e.Location);
                Rectangle bounds = activeItemRequestsDetail.RectangleToScreen(
                    activeItemRequestsDetail.ClientRectangle);

                if (!bounds.Contains(clickPoint))
                {
                    frmItemRequestsList.Controls.Remove(activeItemRequestsDetail);
                    activeItemRequestsDetail = null;

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
