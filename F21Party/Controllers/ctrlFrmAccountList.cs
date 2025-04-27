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
            frmAccountList.dgvUserSetting.DataSource = dbaConnection.SelectData(spString);
            frmAccountList.dgvUserSetting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            frmAccountList.dgvUserSetting.Columns[0].FillWeight = 10;
            frmAccountList.dgvUserSetting.Columns[1].Visible = false;
            frmAccountList.dgvUserSetting.Columns[2].FillWeight = 10;
            frmAccountList.dgvUserSetting.Columns[3].FillWeight = 24;
            frmAccountList.dgvUserSetting.Columns[4].FillWeight = 24;
            frmAccountList.dgvUserSetting.Columns[5].FillWeight = 10;
            frmAccountList.dgvUserSetting.Columns[6].FillWeight = 22;

            dbaConnection.ToolStripTextBoxData(frmAccountList.tstSearchWith, spString, "UserName");
        }

        public void ShowEntry()
        {
            string SPAccess = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", frmAccountList.dgvUserSetting.CurrentRow.Cells["AccessID"].Value.ToString(),
                    "", 1);
            DataTable DTAccess = dbaConnection.SelectData(SPAccess);

            if (frmAccountList.dgvUserSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There is No Data");
            }
            // Not Allowed to change SuperAdmin
            else if (DTAccess.Rows[0]["AccessLevel"].ToString().Trim().ToUpper() == "SUPERADMIN")
            {
                MessageBox.Show("You cannont change SuperAdmin account!");
            }
            else
            {
                frm_CreateAccount frmCreateAccount = new frm_CreateAccount();

                frmCreateAccount._UserID = Convert.ToInt32(frmAccountList.dgvUserSetting.CurrentRow.Cells["UserID"].Value.ToString());
                frmCreateAccount.txtUserName.Text = frmAccountList.dgvUserSetting.CurrentRow.Cells["UserName"].Value.ToString();
                frmCreateAccount.txtPassword.Text = frmAccountList.dgvUserSetting.CurrentRow.Cells["Password"].Value.ToString();
                frmCreateAccount.txtConfirmPassword.Text = frmAccountList.dgvUserSetting.CurrentRow.Cells["Password"].Value.ToString();
                frmCreateAccount.cboAccessLevel.DisplayMember = frmAccountList.dgvUserSetting.CurrentRow.Cells["AccessID"].Value.ToString();
                frmCreateAccount._AccountID = Convert.ToInt32(frmAccountList.dgvUserSetting.CurrentRow.Cells["AccountID"].Value);

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

                if (Program.UserAccessLevel.Trim().ToUpper() == "SUPERADMIN")
                {
                    frmCreateAccount.cboAccessLevel.Enabled = false;
                }

                frmCreateAccount._IsEdit = true;
                frmCreateAccount.ShowDialog();
                ShowData();
            }
        }
        public void TsbDelete()
        {
            DbaAccountSetting dbaAccountSetting = new DbaAccountSetting();
            string SPAccess = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", frmAccountList.dgvUserSetting.CurrentRow.Cells["AccessID"].Value.ToString(),
                    "", 1);
            DataTable DTAccess = dbaConnection.SelectData(SPAccess);
            if (frmAccountList.dgvUserSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else if (DTAccess.Rows[0]["AccessLevel"].ToString().Trim().ToUpper() == "SUPERADMIN")
            {
                MessageBox.Show("You cannont delete SuperAdmin account.");
            }
            else
            {
                if (MessageBox.Show("Are You Sure You Want To Delete?", "Confirm",
                 MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (frmAccountList.dgvUserSetting.CurrentRow.Cells["UserID"].Value.ToString() == Program.UserID.ToString())
                    {
                        MessageBox.Show("You cannot delete your own account!");
                    }
                    else
                    {
                        dbaAccountSetting.USERID = Convert.ToInt32(frmAccountList.dgvUserSetting.CurrentRow.Cells["UserID"].Value.ToString());
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
            spString = string.Format("SP_Select_Accounts N'{0}', N'{1}', N'{2}', N'{3}'", frmAccountList.tstSearchWith.Text.Trim().ToString(), "0", "0", "2");
            frmAccountList.dgvUserSetting.DataSource = dbaConnection.SelectData(spString);
        }
        public void HoverToolTip()
        {
            frmAccountList.dgvUserSetting.ShowCellToolTips = true;

            foreach (DataGridViewRow row in frmAccountList.dgvUserSetting.Rows)
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
            frm_CreateAccount frmCreateAccount = new frm_CreateAccount();
            frmCreateAccount.ShowDialog();
            ShowData();
        }
    }
}
