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
        frm_AccountList frmAccountList; // Declare the View
        public CtrlFrmAccountList(frm_AccountList accountForm)
        {
            frmAccountList = accountForm; // Create the View
        }
        string spString = "";
        DbaConnection dbaConnection = new DbaConnection();

        
        public void ShowData()
        {
            

            spString = string.Format("SP_Select_Accounts N'{0}', N'{1}', N'{2}', N'{3}'", "0", "0", "0", "5");
            frmAccountList.dgvAccountSetting.DataSource = dbaConnection.SelectData(spString);
            frmAccountList.dgvAccountSetting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            frmAccountList.dgvAccountSetting.Columns[0].FillWeight = 10;
            frmAccountList.dgvAccountSetting.Columns[1].Visible = false;
            frmAccountList.dgvAccountSetting.Columns[2].FillWeight = 10;
            frmAccountList.dgvAccountSetting.Columns[3].FillWeight = 24;
            frmAccountList.dgvAccountSetting.Columns[4].FillWeight = 24;
            frmAccountList.dgvAccountSetting.Columns[5].FillWeight = 10;
            frmAccountList.dgvAccountSetting.Columns[6].FillWeight = 22;

            dbaConnection.ToolStripTextBoxData(frmAccountList.tstSearchWith, spString, "UserName");

            if (!Program.PublicArrWriteAccessPages.Contains("Accounts"))
            {
                frmAccountList.tsbNew.ForeColor = System.Drawing.SystemColors.GrayText;
                frmAccountList.tsbEdit.ForeColor = System.Drawing.SystemColors.GrayText;
                frmAccountList.tsbDelete.ForeColor = System.Drawing.SystemColors.GrayText;
            }
            if(!Program.PublicArrReadAccessPages.Contains("User"))
            {
                frmAccountList.tsbUser.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }
        
        public void ShowEntry()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("Accounts"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            string SPAccess = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", frmAccountList.dgvAccountSetting.CurrentRow.Cells["AccessID"].Value.ToString(),
                    "", 1);
            DataTable DTAccess = dbaConnection.SelectData(SPAccess);

            if (frmAccountList.dgvAccountSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There is No Data");
            }
            // Not Allowed to change SuperAdmin
            else if (Convert.ToInt32(DTAccess.Rows[0]["Authority"]) == 1 && Program.UserAuthority != 1) // Authority 1 is SuperAdmin
            {
                MessageBox.Show("You cannont change SuperAdmin account!");
            }
            else if(Program.UserAuthority >= Convert.ToInt32(DTAccess.Rows[0]["Authority"]) && Program.UserAuthority != 1)
            {
                MessageBox.Show("You cannont change Higher or Same Authority Account!");
            }
            else
            {
                frm_CreateAccount frmCreateAccount = new frm_CreateAccount();

                frmCreateAccount._UserID = Convert.ToInt32(frmAccountList.dgvAccountSetting.CurrentRow.Cells["UserID"].Value.ToString());
                frmCreateAccount.txtUserName.Text = frmAccountList.dgvAccountSetting.CurrentRow.Cells["UserName"].Value.ToString();
                frmCreateAccount.txtPassword.Text = frmAccountList.dgvAccountSetting.CurrentRow.Cells["Password"].Value.ToString();
                frmCreateAccount.txtConfirmPassword.Text = frmAccountList.dgvAccountSetting.CurrentRow.Cells["Password"].Value.ToString();
                frmCreateAccount.cboAccessLevel.DisplayMember = frmAccountList.dgvAccountSetting.CurrentRow.Cells["AccessID"].Value.ToString();
                frmCreateAccount._AccountID = Convert.ToInt32(frmAccountList.dgvAccountSetting.CurrentRow.Cells["AccountID"].Value);

                spString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", frmCreateAccount._UserID, "0", "0", "6");
                DataTable DT = new DataTable();
                DT = dbaConnection.SelectData(spString);

                frmCreateAccount.txtFullName.Text = DT.Rows[0]["FullName"].ToString();
                frmCreateAccount.txtFullName.Enabled = false;

                frmCreateAccount.txtAddress.Text = DT.Rows[0]["Address"].ToString();
                frmCreateAccount.txtAddress.Enabled = false;

                frmCreateAccount.txtPhone.Text = DT.Rows[0]["Phone"].ToString();
                frmCreateAccount.txtPhone.Enabled = false;

                frmCreateAccount.cboPosition.DisplayMember = DT.Rows[0]["PositionID"].ToString();
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



                if (Program.UserAuthority >= Convert.ToInt32(DTAccess.Rows[0]["Authority"]))
                {
                    frmCreateAccount.cboAccessLevel.Enabled = false;
                }
                if (Convert.ToInt32(frmAccountList.dgvAccountSetting.CurrentRow.Cells["UserID"].Value) == Program.UserID)
                {
                    frmCreateAccount.cboAccessLevel.Enabled = false;
                }

                frmCreateAccount._IsEdit = true;
                frmCreateAccount.btnCreate.Text = "Save";
                frmCreateAccount.ShowDialog();
                ShowData();
            }
        }
        public void TsbDelete()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("Accounts"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            DbaAccounts dbaAccountSetting = new DbaAccounts();
            string SPAccess = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", frmAccountList.dgvAccountSetting.CurrentRow.Cells["AccessID"].Value.ToString(),
                    "", 1);
            DataTable DTAccess = dbaConnection.SelectData(SPAccess);
            if (frmAccountList.dgvAccountSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else if (Convert.ToInt32(DTAccess.Rows[0]["AccessID"]) == 1) // AccessID 1 is SuperAdmin
            {
                MessageBox.Show("You cannont delete SuperAdmin account.");
            }
            else
            {
                if (MessageBox.Show("Are You Sure You Want To Delete?", "Confirm",
                 MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (frmAccountList.dgvAccountSetting.CurrentRow.Cells["UserID"].Value.ToString() == Program.UserID.ToString())
                    {
                        MessageBox.Show("You cannot delete your own account!");
                    }
                    else if (Program.UserAuthority >= Convert.ToInt32(DTAccess.Rows[0]["Authority"]) && Program.UserAuthority != 1)
                    {
                        MessageBox.Show("You cannont delete Higher or Same Authority Account!");
                    }
                    else
                    {
                        dbaAccountSetting.ACCOUNTID = Convert.ToInt32(frmAccountList.dgvAccountSetting.CurrentRow.Cells["AccountID"].Value.ToString());
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
            spString = string.Format("SP_Select_Accounts N'{0}', N'{1}', N'{2}', N'{3}'", frmAccountList.tstSearchWith.Text.Trim().ToString(), "0", "0", "7");
            frmAccountList.dgvAccountSetting.DataSource = dbaConnection.SelectData(spString);
        }
        public void HoverToolTip()
        {
            if (!Program.PublicArrReadAccessPages.Contains("Users"))
            {
                return;
            }

            frmAccountList.dgvAccountSetting.ShowCellToolTips = true;

            foreach (DataGridViewRow row in frmAccountList.dgvAccountSetting.Rows)
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
            if (!Program.PublicArrWriteAccessPages.Contains("Accounts"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

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
