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
    internal class CtrlFrmItemRequestsList
    {
        private readonly frm_ItemRequestsList _frmItemRequestsList;
        private readonly DbaConnection _dbaConnection = new DbaConnection();
        private UserControl _itemRequestsDetail;
        private UserControl _activeItemRequestsDetail = null;
        private DataGridViewRow _expandedRow = null;
        private Point? _lastClickedCell = null;
        private string _spString = "";
        public CtrlFrmItemRequestsList(frm_ItemRequestsList requestsListForm)
        {
            _frmItemRequestsList = requestsListForm;
        }

        public void RemoveDelete()
        {
            if (Program.UserAuthority != 1)
            {
                _frmItemRequestsList.toolStripSeparator3.Visible = false;
                _frmItemRequestsList.tsbDelete.Visible = false;
            }
            else
            {
                _frmItemRequestsList.toolStripSeparator3.Visible = true;
                _frmItemRequestsList.tsbDelete.Visible = true;
            }
        }

        public void ShowItemRequests()
        {
            DataGridViewTextBoxColumn dgCol = new DataGridViewTextBoxColumn();
            dgCol.DefaultCellStyle.NullValue = "+";
            dgCol.HeaderText = "";
            dgCol.Width = 30;
            dgCol.ReadOnly = true;
            dgCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            _frmItemRequestsList.dgvItemRequests.Columns.Add(dgCol);

            _spString = string.Format("SP_Select_ItemRequests N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            _frmItemRequestsList.dgvItemRequests.DataSource = _dbaConnection.SelectData(_spString);

            _frmItemRequestsList.dgvItemRequests.Columns[1].Width = (_frmItemRequestsList.dgvItemRequests.Width / 100) * 10;
            _frmItemRequestsList.dgvItemRequests.Columns[2].Visible = false;
            _frmItemRequestsList.dgvItemRequests.Columns[3].Width = (_frmItemRequestsList.dgvItemRequests.Width / 100) * 20;
            _frmItemRequestsList.dgvItemRequests.Columns[4].Visible = false;
            _frmItemRequestsList.dgvItemRequests.Columns[5].Width = (_frmItemRequestsList.dgvItemRequests.Width / 100) * 35;
            _frmItemRequestsList.dgvItemRequests.Columns[6].Width = (_frmItemRequestsList.dgvItemRequests.Width / 100) * 35;

            _dbaConnection.ToolStripTextBoxData(_frmItemRequestsList.tstSearchWith, _spString, "RequestDate");

            if (!Program.PublicArrWriteAccessPages.Contains("ItemRequests"))
            {
                _frmItemRequestsList.tsbNew.ForeColor = System.Drawing.SystemColors.GrayText;
                _frmItemRequestsList.tsbDelete.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }

        public void ShowItemRequestsDetail()
        {
            //UserControl Form
            _itemRequestsDetail = new ctl_ItemRequestsDetail();
            _itemRequestsDetail.Hide();
            _frmItemRequestsList.Controls.Add(_itemRequestsDetail);
            _frmItemRequestsList.Controls.SetChildIndex(_itemRequestsDetail, 0);

        }

        public void DgvItemRequestsCellClick(DataGridViewCellEventArgs e)
        {
            _lastClickedCell = new Point(e.ColumnIndex, e.RowIndex);
            if (e.RowIndex == -1 || e.ColumnIndex == -1) 
                return;

            var cell = _frmItemRequestsList.dgvItemRequests[e.ColumnIndex, e.RowIndex];
            if (e.ColumnIndex == 0)
            {
                if (cell.Value == null)
                    cell.Value = "+";

                if (cell.Value.ToString().Trim() == "+")
                {
                    Rectangle cellBounds = _frmItemRequestsList.dgvItemRequests.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                    Point offsetLocation = new Point(cellBounds.X, cellBounds.Y + cellBounds.Height);
                    offsetLocation.Offset(_frmItemRequestsList.dgvItemRequests.Location);

                    // Create or use the existing control
                    if (_itemRequestsDetail == null)
                        _itemRequestsDetail = new ctl_ItemRequestsDetail();

                    _itemRequestsDetail.Location = offsetLocation;

                    // Access specific properties through cast
                    var detailControl = (ctl_ItemRequestsDetail)_itemRequestsDetail;
                    int itemRequestsID = Convert.ToInt32(_frmItemRequestsList.dgvItemRequests.Rows[e.RowIndex].Cells["RequestID"].Value.ToString());
                    _spString = string.Format("SP_Select_ItemRequestsDetail N'{0}',N'{1}',N'{2}'", itemRequestsID, "0", "0");

                    var dgv = detailControl.dgvItemRequestsDetail;
                    dgv.DataSource = _dbaConnection.SelectData(_spString);
                    dgv.Columns[0].Visible = false;
                    dgv.Columns[1].Visible = false;
                    dgv.Columns[2].Width = (dgv.Width / 100) * 50;
                    dgv.Columns[3].Width = (dgv.Width / 100) * 20;
                    dgv.Columns[4].Width = (dgv.Width / 100) * 15;

                    _frmItemRequestsList.Controls.Add(_itemRequestsDetail);
                    _frmItemRequestsList.Controls.SetChildIndex(_itemRequestsDetail, 0);
                    _itemRequestsDetail.Show();

                    cell.Value = "-";
                    _activeItemRequestsDetail = _itemRequestsDetail;
                    _expandedRow = _frmItemRequestsList.dgvItemRequests.Rows[e.RowIndex];
                }
                else
                {
                    _frmItemRequestsList.Controls.Remove(_itemRequestsDetail);
                    cell.Value = "+";
                    _activeItemRequestsDetail = null;
                    _expandedRow = null;
                }
            }
        }

        public void TsbNewClick()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("ItemRequests"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            frm_CreateItemRequests frm = new frm_CreateItemRequests();
            frm.ShowDialog();
            _spString = string.Format("SP_Select_ItemRequests N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            _frmItemRequestsList.dgvItemRequests.DataSource = _dbaConnection.SelectData(_spString);
        }

        public void TsmRequestDateClick()
        {
            _frmItemRequestsList.tslLabel.Text = "RequestDate";
            _spString = string.Format("SP_Select_ItemRequests N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            _dbaConnection.ToolStripTextBoxData(_frmItemRequestsList.tstSearchWith, _spString, "RequestDate");
        }

        public void TsmFullNameClick()
        {
            _frmItemRequestsList.tslLabel.Text = "FullName";
            _spString = string.Format("SP_Select_ItemRequests N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            _dbaConnection.ToolStripTextBoxData(_frmItemRequestsList.tstSearchWith, _spString, "FullName");
        }

        public void TsmSearchWithChanged()
        {
            if (_frmItemRequestsList.tslLabel.Text == "RequestDate")
            {
                _spString = string.Format("SP_Select_ItemRequests N'{0}',N'{1}',N'{2}'", _frmItemRequestsList.tstSearchWith.Text.Trim().ToString(), "0", "3");
            }
            else if (_frmItemRequestsList.tslLabel.Text == "FullName")
            {
                _spString = string.Format("SP_Select_ItemRequests N'{0}',N'{1}',N'{2}'", _frmItemRequestsList.tstSearchWith.Text.Trim().ToString(), "0", "4");
            }
            _frmItemRequestsList.dgvItemRequests.DataSource = _dbaConnection.SelectData(_spString);
        }

        public void TsbDelete()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("ItemRequests"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            string itemRequestsID = _frmItemRequestsList.dgvItemRequests.CurrentRow.Cells["RequestID"].Value.ToString();
            DbaItemRequests dbaItemRequests = new DbaItemRequests();
            DbaItemRequestsDetail dbaItemRequestsDetail = new DbaItemRequestsDetail();

            if (_frmItemRequestsList.dgvItemRequests.CurrentRow.Cells[1].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else
            {
                if (MessageBox.Show("Are You Sure You Want To Delete?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dbaItemRequests.RID = Convert.ToInt32(itemRequestsID);
                    dbaItemRequests.ACTION = 1;
                    dbaItemRequests.SaveData();

                    dbaItemRequestsDetail.RID = Convert.ToInt32(itemRequestsID);
                    dbaItemRequestsDetail.ACTION = 1;
                    dbaItemRequestsDetail.SaveData();
                    MessageBox.Show("Successfully Delete");
                    RemoveDelete();

                    _spString = string.Format("SP_Select_ItemRequests N'{0}',N'{1}',N'{2}'", "0", "0", "0");
                    _frmItemRequestsList.dgvItemRequests.DataSource = _dbaConnection.SelectData(_spString);
                    //ShowItemRequestsDetail();
                }
            }
        }

        public void FrmItemRequestsMouseDown(MouseEventArgs e)
        {

            if (_activeItemRequestsDetail != null)
            {
                // Convert mouse click point to DataGridView client coords
                Point dgvPoint = _frmItemRequestsList.dgvItemRequests.PointToClient(Cursor.Position);

                var hit = _frmItemRequestsList.dgvItemRequests.HitTest(dgvPoint.X, dgvPoint.Y);

                // If click was on the same toggle cell, skip hiding here
                if (_lastClickedCell.HasValue
                    && hit.ColumnIndex == _lastClickedCell.Value.X
                    && hit.RowIndex == _lastClickedCell.Value.Y)
                {
                    // Click was on the toggle cell itself, so do nothing here
                    return;
                }

                Point clickPoint = _frmItemRequestsList.PointToScreen(e.Location);
                Rectangle bounds = _activeItemRequestsDetail.RectangleToScreen(
                    _activeItemRequestsDetail.ClientRectangle);

                if (!bounds.Contains(clickPoint))
                {
                    _frmItemRequestsList.Controls.Remove(_activeItemRequestsDetail);
                    _activeItemRequestsDetail = null;

                    if (_expandedRow != null)
                    {
                        _expandedRow.Cells[0].Value = "+";
                        _expandedRow = null;
                    }
                    _lastClickedCell = null; // Reset after closing
                }
            }
        }
    }
}
