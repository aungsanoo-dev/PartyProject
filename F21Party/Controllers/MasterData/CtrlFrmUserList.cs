using F21Party.DBA;
using F21Party.Views;
using F21Party.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F21Party.Controllers
{
    internal class CtrlFrmUserList
    {
        private readonly frm_UserList _frmUserList; // Declare the View
        private readonly DbaConnection _dbaConnection = new DbaConnection();
        private string _spString = "";
        public CtrlFrmUserList(frm_UserList userForm)
        {
            _frmUserList = userForm; // Create the View
        }

        public void ShowData()
        {
            _spString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", "0", "0", "0", "7");
            _frmUserList.dgvUserSetting.DataSource = _dbaConnection.SelectData(_spString);
            _frmUserList.dgvUserSetting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            _frmUserList.dgvUserSetting.Columns[0].FillWeight = 6;
            _frmUserList.dgvUserSetting.Columns[1].FillWeight = 6;
            _frmUserList.dgvUserSetting.Columns[2].FillWeight = 16;
            _frmUserList.dgvUserSetting.Columns[3].FillWeight = 25;
            _frmUserList.dgvUserSetting.Columns[4].FillWeight = 25;
            _frmUserList.dgvUserSetting.Columns[5].Visible = false;
            _frmUserList.dgvUserSetting.Columns[6].FillWeight = 6;
            _frmUserList.dgvUserSetting.Columns[7].FillWeight = 16;

            _dbaConnection.ToolStripTextBoxData(_frmUserList.tstSearchWith, _spString, "FullName");

            if (!Program.PublicArrWriteAccessPages.Contains("Users"))
            {
                _frmUserList.tsbNew.ForeColor = System.Drawing.SystemColors.GrayText;
                _frmUserList.tsbEdit.ForeColor = System.Drawing.SystemColors.GrayText;
                _frmUserList.tsbDelete.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }

        public void ShowEntry()
        {
            if (!Function.HasWriteAccess("Users")) return;

            if (_frmUserList.dgvUserSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There is No Data");
            }
            else
            {
                frm_CreateUser frmCreateUser = new frm_CreateUser();
                
                frmCreateUser.UserID = Convert.ToInt32(_frmUserList.dgvUserSetting.CurrentRow.Cells["UserID"].Value);
                frmCreateUser.txtFullName.Text = _frmUserList.dgvUserSetting.CurrentRow.Cells["FullName"].Value.ToString();
                frmCreateUser.txtAddress.Text = _frmUserList.dgvUserSetting.CurrentRow.Cells["Address"].Value.ToString();
                frmCreateUser.txtPhone.Text = _frmUserList.dgvUserSetting.CurrentRow.Cells["Phone"].Value.ToString();
                frmCreateUser.cboPosition.DisplayMember = _frmUserList.dgvUserSetting.CurrentRow.Cells["PositionID"].Value.ToString();

                _spString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", Convert.ToInt32(_frmUserList.dgvUserSetting.CurrentRow.Cells["UserID"].Value), "0", "0", "10");
                DataTable dt = new DataTable();
                dt = _dbaConnection.SelectData(_spString);

                if (dt.Rows.Count > 0 && Program.UserAuthority != 1 && Convert.ToInt32(dt.Rows[0]["Authority"]) == 1)
                {
                    frmCreateUser.txtFullName.Enabled = false;
                }

                frmCreateUser.IsEdit = true;
                frmCreateUser.btnCreate.Text = "Save";
                frmCreateUser.ShowDialog();
                ShowData();
            }
        }
        public void TsbDelete()
        {
            if (!Function.HasWriteAccess("Users")) return;

            _spString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", Convert.ToInt32(_frmUserList.dgvUserSetting.CurrentRow.Cells["UserID"].Value), "0", "0", "8");
            DataTable dt = new DataTable();
            dt = _dbaConnection.SelectData(_spString);

            _spString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", Convert.ToInt32(_frmUserList.dgvUserSetting.CurrentRow.Cells["UserID"].Value), "0", "0", "11");
            DataTable dtTeam = new DataTable();
            dtTeam = _dbaConnection.SelectData(_spString);

            _spString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", Convert.ToInt32(_frmUserList.dgvUserSetting.CurrentRow.Cells["UserID"].Value), "0", "0", "12");
            DataTable dtDonation = new DataTable();
            dtDonation = _dbaConnection.SelectData(_spString);

            _spString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", Convert.ToInt32(_frmUserList.dgvUserSetting.CurrentRow.Cells["UserID"].Value), "0", "0", "13");
            DataTable dtItemRequest = new DataTable();
            dtItemRequest = _dbaConnection.SelectData(_spString);

            DbaUsers dbaUserSetting = new DbaUsers();
            if (_frmUserList.dgvUserSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else if (_frmUserList.dgvUserSetting.CurrentRow.Cells["UserID"].Value.ToString() == Program.UserID.ToString())
            {
                MessageBox.Show("You cannot delete your own user!");
            }
            else if (dt.Rows.Count > 0)
            {
                MessageBox.Show("You cannont delete the user which has the user account!");
            }
            else if (dtTeam.Rows.Count > 0)
            {
                MessageBox.Show("You cannont delete the user which has Team! Delete this user in Team Managment first!");
            }
            else if (dtDonation.Rows.Count > 0)
            {
                MessageBox.Show("You cannont delete the user which is in Donation! Delete this user in Donation first!");
            }
            else if (dtItemRequest.Rows.Count > 0)
            {
                MessageBox.Show("You cannont delete the user which is in Item Request! Delete this user in Item Request first!");
            }
            else
            {
                if (MessageBox.Show("Are You Sure You Want To Delete?", "Confirm",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dbaUserSetting.UID = Convert.ToInt32(_frmUserList.dgvUserSetting.CurrentRow.Cells["UserID"].Value.ToString());
                    dbaUserSetting.ACTION = 2;
                    dbaUserSetting.SaveData();
                    MessageBox.Show("Successfully Delete");
                    ShowData();
                }
            }
        }
        public void TsbSearch()
        {
            _spString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", _frmUserList.tstSearchWith.Text.Trim().ToString(), "0", "0", "9");
            _frmUserList.dgvUserSetting.DataSource = _dbaConnection.SelectData(_spString);
        }

        public void HoverToolTip()
        {
            if (!Program.PublicArrReadAccessPages.Contains("Accounts"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            _frmUserList.dgvUserSetting.ShowCellToolTips = true;

            foreach (DataGridViewRow row in _frmUserList.dgvUserSetting.Rows)
            {
                if (!row.IsNewRow)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.ToolTipText = "Double-click to see the User's Account!";
                    }
                }
            }
        }

        public void TsbNew()
        {
            if (!Function.HasWriteAccess("Users")) return;

            frm_CreateUser frmCreateUser = new frm_CreateUser();
            frmCreateUser.ShowDialog();
            ShowData();
        }
    }
}
