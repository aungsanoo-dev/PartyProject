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
    internal class CtrlFrmAccountList
    {
        private readonly frm_AccountList _frmAccountList; // Declare the View
        private readonly DbaConnection _dbaConnection = new DbaConnection();
        private string _spString = "";
        public CtrlFrmAccountList(frm_AccountList accountForm)
        {
            _frmAccountList = accountForm; // Create the View
        }
        
        public void ShowData()
        {
            _spString = string.Format("SP_Select_Accounts N'{0}', N'{1}', N'{2}', N'{3}'", "0", "0", "0", "5");
            _frmAccountList.dgvAccountSetting.DataSource = _dbaConnection.SelectData(_spString);
            _frmAccountList.dgvAccountSetting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            _frmAccountList.dgvAccountSetting.Columns[0].FillWeight = 10;
            _frmAccountList.dgvAccountSetting.Columns[1].Visible = false;
            _frmAccountList.dgvAccountSetting.Columns[2].FillWeight = 10;
            _frmAccountList.dgvAccountSetting.Columns[3].FillWeight = 40;
            _frmAccountList.dgvAccountSetting.Columns[4].Visible = false;
            _frmAccountList.dgvAccountSetting.Columns[5].Visible = false;
            _frmAccountList.dgvAccountSetting.Columns[6].FillWeight = 40;

            _dbaConnection.ToolStripTextBoxData(_frmAccountList.tstSearchWith, _spString, "UserName");

            if (!Program.PublicArrWriteAccessPages.Contains("Accounts"))
            {
                _frmAccountList.tsbNew.ForeColor = System.Drawing.SystemColors.GrayText;
                _frmAccountList.tsbEdit.ForeColor = System.Drawing.SystemColors.GrayText;
                _frmAccountList.tsbDelete.ForeColor = System.Drawing.SystemColors.GrayText;
            }
            if(!Program.PublicArrReadAccessPages.Contains("Users"))
            {
                _frmAccountList.tsbUser.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }
        
        public void ShowEntry()
        {
            if (!Function.HasWriteAccess("Accounts")) return;

            _spString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", _frmAccountList.dgvAccountSetting.CurrentRow.Cells["AccessID"].Value.ToString(), "", 1);
            DataTable dtAccess = _dbaConnection.SelectData(_spString);

            if (_frmAccountList.dgvAccountSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There is No Data");
            }
            // Not Allowed to change SuperAdmin
            else if (Convert.ToInt32(dtAccess.Rows[0]["Authority"]) == 1 && Program.UserAuthority != 1) // Authority 1 is SuperAdmin
            {
                MessageBox.Show("You cannont change SuperAdmin account!");
            }
            else if(Program.UserAuthority >= Convert.ToInt32(dtAccess.Rows[0]["Authority"]) && Program.UserAuthority != 1)
            {
                MessageBox.Show("You cannont change Higher or Same Authority Account!");
            }
            else
            {
                frm_CreateAccount frmCreateAccount = new frm_CreateAccount();

                frmCreateAccount.UserID = Convert.ToInt32(_frmAccountList.dgvAccountSetting.CurrentRow.Cells["UserID"].Value.ToString());
                frmCreateAccount.txtUserName.Text = _frmAccountList.dgvAccountSetting.CurrentRow.Cells["UserName"].Value.ToString();
                frmCreateAccount.txtPassword.Text = _frmAccountList.dgvAccountSetting.CurrentRow.Cells["Password"].Value.ToString();
                frmCreateAccount.txtConfirmPassword.Text = _frmAccountList.dgvAccountSetting.CurrentRow.Cells["Password"].Value.ToString();
                frmCreateAccount.cboAccessLevel.DisplayMember = _frmAccountList.dgvAccountSetting.CurrentRow.Cells["AccessID"].Value.ToString();
                frmCreateAccount.AccountID = Convert.ToInt32(_frmAccountList.dgvAccountSetting.CurrentRow.Cells["AccountID"].Value);

                _spString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", frmCreateAccount.UserID, "0", "0", "6");
                DataTable dt = new DataTable();
                dt = _dbaConnection.SelectData(_spString);

                frmCreateAccount.txtFullName.Text = dt.Rows[0]["FullName"].ToString();
                frmCreateAccount.txtFullName.Enabled = false;

                frmCreateAccount.txtAddress.Text = dt.Rows[0]["Address"].ToString();
                frmCreateAccount.txtAddress.Enabled = false;

                frmCreateAccount.txtPhone.Text = dt.Rows[0]["Phone"].ToString();
                frmCreateAccount.txtPhone.Enabled = false;

                frmCreateAccount.cboPosition.DisplayMember = dt.Rows[0]["PositionID"].ToString();
                frmCreateAccount.cboPosition.Enabled = false;

                frmCreateAccount.txtPassword.Enabled = false;

                if(Program.UserAuthority != 1)
                {
                    frmCreateAccount.btnEye.Enabled = false;
                }
                
                frmCreateAccount.txtUserName.Enabled = false;

                frmCreateAccount.txtConfirmPassword.Visible = false;
                frmCreateAccount.lblConfirmPassword.Visible=false;

                //if (Program.UserAccessID == 1) // AccessID 1 is SuperAdmin
                //{
                //    frmCreateAccount.cboAccessLevel.Enabled = false;
                //}

                if (Program.UserAuthority >= Convert.ToInt32(dtAccess.Rows[0]["Authority"]))
                {
                    frmCreateAccount.cboAccessLevel.Enabled = false;
                }
                if (Convert.ToInt32(_frmAccountList.dgvAccountSetting.CurrentRow.Cells["UserID"].Value) == Program.UserID)
                {
                    frmCreateAccount.cboAccessLevel.Enabled = false;
                }

                frmCreateAccount.IsEdit = true;
                frmCreateAccount.btnCreate.Text = "Save";
                frmCreateAccount.ShowDialog();
                ShowData();
            }
        }
        public void TsbDelete()
        {
            if(!Function.HasWriteAccess("Accounts")) return;

            DbaAccounts dbaAccountSetting = new DbaAccounts();
            _spString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", _frmAccountList.dgvAccountSetting.CurrentRow.Cells["AccessID"].Value.ToString(), "", 1);
            DataTable dtAccess = _dbaConnection.SelectData(_spString);
            if (_frmAccountList.dgvAccountSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else if (Convert.ToInt32(dtAccess.Rows[0]["AccessID"]) == 1) // AccessID 1 is SuperAdmin
            {
                MessageBox.Show("You cannont delete SuperAdmin account.");
            }
            else
            {
                if (MessageBox.Show("Are You Sure You Want To Delete?", "Confirm",
                 MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (_frmAccountList.dgvAccountSetting.CurrentRow.Cells["UserID"].Value.ToString() == Program.UserID.ToString())
                    {
                        MessageBox.Show("You cannot delete your own account!");
                    }
                    else if (Program.UserAuthority >= Convert.ToInt32(dtAccess.Rows[0]["Authority"]) && Program.UserAuthority != 1)
                    {
                        MessageBox.Show("You cannont delete Higher or Same Authority Account!");
                    }
                    else
                    {
                        dbaAccountSetting.ACCOUNTID = Convert.ToInt32(_frmAccountList.dgvAccountSetting.CurrentRow.Cells["AccountID"].Value.ToString());
                        dbaAccountSetting.ACTION = 2;
                        dbaAccountSetting.SaveData();
                        MessageBox.Show("Successfully Delete");
                        ShowData();
                    }
                }
            }
        }
        public void TsbSearch()
        {
            if (_frmAccountList.tslLabel.Text == "UserName")
            {
                _spString = string.Format("SP_Select_Accounts N'{0}', N'{1}', N'{2}', N'{3}'", _frmAccountList.tstSearchWith.Text.Trim().ToString(), "0", "0", "7");
            }
            else if (_frmAccountList.tslLabel.Text == "AccessLevel")
            {
                _spString = string.Format("SP_Select_Accounts N'{0}', N'{1}', N'{2}', N'{3}'", _frmAccountList.tstSearchWith.Text.Trim().ToString(), "0", "0", "8");
            }

            _frmAccountList.dgvAccountSetting.DataSource = _dbaConnection.SelectData(_spString);
        }

        public void TsmSearchLabelClick(string textLabel)
        {
            _frmAccountList.tslLabel.Text = textLabel;
            _spString = string.Format("SP_Select_Accounts N'{0}',N'{1}',N'{2}',N'{3}'", "0", "0", "0", "5");
            _dbaConnection.ToolStripTextBoxData(_frmAccountList.tstSearchWith, _spString, textLabel);
        }
        public void HoverToolTip()
        {
            if (!Program.PublicArrReadAccessPages.Contains("Users"))
            {
                return;
            }

            _frmAccountList.dgvAccountSetting.ShowCellToolTips = true;

            foreach (DataGridViewRow row in _frmAccountList.dgvAccountSetting.Rows)
            {
                if (!row.IsNewRow)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.ToolTipText = "Double-click to see the User info!";
                    }
                }
            }
        }
        
        public void TsbNew()
        {
            if(!Function.HasWriteAccess("Accounts")) return;

            frm_CreateAccount frmCreateAccount = new frm_CreateAccount();
            frmCreateAccount.ShowDialog();
            ShowData();
        }

        public void EditOrShow(Action action)
        {
            if (!Program.PublicArrReadAccessPages.Contains("Users"))
            {
                ShowEntry();
            }
            else
            {
                action();
            }
        }

        
    }
}
