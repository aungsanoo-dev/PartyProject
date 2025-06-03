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
        private readonly frm_DonationList _frmDonationList;
        private readonly DbaConnection _dbaConnection = new DbaConnection();
        private DataGridViewRow _expandedRow = null;
        private UserControl _donationDetail;
        private UserControl _activeDonationDetail = null;
        private Point? _lastClickedCell = null;
        private string _spString = "";
        public CtrlFrmDonationList(frm_DonationList donationListForm)
        {
            _frmDonationList = donationListForm;
        }


        public void RemoveDelete()
        {
            if(Program.UserAuthority != 1)
            {
                _frmDonationList.toolStripSeparator3.Visible = false;
                _frmDonationList.tsbDelete.Visible = false;
            }
            else
            {
                _frmDonationList.toolStripSeparator3.Visible = true;
                _frmDonationList.tsbDelete.Visible = true;
            }
        }
        public void ShowDonation()
        {
            DataGridViewTextBoxColumn dgCol = new DataGridViewTextBoxColumn();
            dgCol.DefaultCellStyle.NullValue = "+";
            dgCol.HeaderText = "";
            dgCol.Width = 30;
            dgCol.ReadOnly = true;
            dgCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            _frmDonationList.dgvDonation.Columns.Add(dgCol);

            _spString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            _frmDonationList.dgvDonation.DataSource = _dbaConnection.SelectData(_spString);

            _frmDonationList.dgvDonation.Columns[1].Width = (_frmDonationList.dgvDonation.Width / 100) * 10;
            _frmDonationList.dgvDonation.Columns[2].Visible = false;
            _frmDonationList.dgvDonation.Columns[3].Width = (_frmDonationList.dgvDonation.Width / 100) * 20;
            _frmDonationList.dgvDonation.Columns[4].Visible = false;
            _frmDonationList.dgvDonation.Columns[5].Width = (_frmDonationList.dgvDonation.Width / 100) * 35;
            _frmDonationList.dgvDonation.Columns[6].Width = (_frmDonationList.dgvDonation.Width / 100) * 35;

            _dbaConnection.ToolStripTextBoxData(_frmDonationList.tstSearchWith, _spString, "DonationDate");

            if (!Program.PublicArrWriteAccessPages.Contains("Donation"))
            {
                _frmDonationList.tsbNew.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }

        public void ShowDonationDetail()
        {
            //UserControl Form
            _donationDetail = new ctl_DonationDetail();
            _donationDetail.Hide();
            _frmDonationList.Controls.Add(_donationDetail);
            _frmDonationList.Controls.SetChildIndex(_donationDetail, 0);

        }

        public void DgvDonationCellClick(DataGridViewCellEventArgs e)
        {
            _lastClickedCell = new Point(e.ColumnIndex, e.RowIndex);
            if (e.RowIndex == -1 || e.ColumnIndex == -1) 
                return;

            var cell = _frmDonationList.dgvDonation[e.ColumnIndex, e.RowIndex];
            if (e.ColumnIndex == 0)
            {
                if (cell.Value == null)
                    cell.Value = "+";

                if (cell.Value.ToString().Trim() == "+")
                {
                    Rectangle cellBounds = _frmDonationList.dgvDonation.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                    Point offsetLocation = new Point(cellBounds.X, cellBounds.Y + cellBounds.Height);
                    offsetLocation.Offset(_frmDonationList.dgvDonation.Location);

                    // Create or use the existing control
                    if (_donationDetail == null)
                        _donationDetail = new ctl_DonationDetail();

                    _donationDetail.Location = offsetLocation;

                    // Access specific properties through cast
                    var detailControl = (ctl_DonationDetail)_donationDetail;
                    int donationID = Convert.ToInt32(_frmDonationList.dgvDonation.Rows[e.RowIndex].Cells["DonationID"].Value.ToString());
                    _spString = string.Format("SP_Select_DonationDetail N'{0}',N'{1}',N'{2}'", donationID, "0", "0");

                    var dgv = detailControl.dgvDonationDetail;
                    dgv.DataSource = _dbaConnection.SelectData(_spString);
                    dgv.Columns[0].Visible = false;
                    dgv.Columns[1].Visible = false;
                    dgv.Columns[2].Width = (dgv.Width / 100) * 50;
                    dgv.Columns[3].Width = (dgv.Width / 100) * 20;
                    dgv.Columns[4].Width = (dgv.Width / 100) * 15;

                    _frmDonationList.Controls.Add(_donationDetail);
                    _frmDonationList.Controls.SetChildIndex(_donationDetail, 0);
                    _donationDetail.Show();

                    cell.Value = "-";
                    _activeDonationDetail = _donationDetail;
                    _expandedRow = _frmDonationList.dgvDonation.Rows[e.RowIndex];
                }
                else
                {
                    _frmDonationList.Controls.Remove(_donationDetail);
                    cell.Value = "+";
                    _activeDonationDetail = null;
                    _expandedRow = null;
                }
            }
        }
        public void TsbNewClick()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("Donation"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            frm_CreateDonation frm = new frm_CreateDonation();
            frm.ShowDialog();
            _spString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            _frmDonationList.dgvDonation.DataSource = _dbaConnection.SelectData(_spString);
        }

        public void TsmDonationDateClick()
        {
            _frmDonationList.tslLabel.Text = "DonationDate";
            _spString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            _dbaConnection.ToolStripTextBoxData(_frmDonationList.tstSearchWith, _spString, "DonationDate");
        }

        public void TsmFullNameClick()
        {
            _frmDonationList.tslLabel.Text = "FullName";
            _spString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            _dbaConnection.ToolStripTextBoxData(_frmDonationList.tstSearchWith, _spString, "FullName");
        }

        public void TsmSearchWithChanged()
        {
            if (_frmDonationList.tslLabel.Text == "DonationDate")
            {
                _spString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", _frmDonationList.tstSearchWith.Text.Trim().ToString(), "0", "3");
            }
            else if (_frmDonationList.tslLabel.Text == "FullName")
            {
                _spString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", _frmDonationList.tstSearchWith.Text.Trim().ToString(), "0", "4");
            }
            _frmDonationList.dgvDonation.DataSource = _dbaConnection.SelectData(_spString);
        }

        public void TsbDelete()
        {
            string donationID = _frmDonationList.dgvDonation.CurrentRow.Cells["DonationID"].Value.ToString();
            DbaDonation dbaDonation = new DbaDonation();
            DbaDonationDetail dbaDonationDetail = new DbaDonationDetail();

            if (_frmDonationList.dgvDonation.CurrentRow.Cells[1].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else
            {
                if (MessageBox.Show("Are You Sure You Want To Delete?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dbaDonation.DID = Convert.ToInt32(donationID);
                    dbaDonation.ACTION = 1;
                    dbaDonation.SaveData();

                    dbaDonationDetail.DID = Convert.ToInt32(donationID);
                    dbaDonationDetail.ACTION = 1;
                    dbaDonationDetail.SaveData();
                    MessageBox.Show("Successfully Delete");
                    RemoveDelete();

                    _spString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", "0", "0", "0");
                    _frmDonationList.dgvDonation.DataSource = _dbaConnection.SelectData(_spString);
                    //ShowDonationDetail();
                }
            }
        }


        public void FrmDonationMouseDown(MouseEventArgs e)
        {
            
            if (_activeDonationDetail != null)
            {
                // Convert mouse click point to DataGridView client coords
                Point dgvPoint = _frmDonationList.dgvDonation.PointToClient(Cursor.Position);

                var hit = _frmDonationList.dgvDonation.HitTest(dgvPoint.X, dgvPoint.Y);

                // If click was on the same toggle cell, skip hiding here
                if (_lastClickedCell.HasValue
                    && hit.ColumnIndex == _lastClickedCell.Value.X
                    && hit.RowIndex == _lastClickedCell.Value.Y)
                {
                    // Click was on the toggle cell itself, so do nothing here
                    return;
                }

                Point clickPoint = _frmDonationList.PointToScreen(e.Location);
                Rectangle bounds = _activeDonationDetail.RectangleToScreen(
                    _activeDonationDetail.ClientRectangle);

                if (!bounds.Contains(clickPoint))
                {
                    _frmDonationList.Controls.Remove(_activeDonationDetail);
                    _activeDonationDetail = null;

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
