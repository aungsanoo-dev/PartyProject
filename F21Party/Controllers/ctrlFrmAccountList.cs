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
        public frm_AccountList frm_AccountList; // Declare the View
        public CtrlFrmAccountList(frm_AccountList accountForm)
        {
            frm_AccountList = accountForm; // Create the View
        }
        string SPString = "";
        DbaConnection dbaConnection = new DbaConnection();

        public void ShowData()
        {
            SPString = string.Format("SP_Select_Accounts N'{0}', N'{1}', N'{2}', N'{3}'", "0", "0", "0", "5");
            frm_AccountList.dgvUserSetting.DataSource = dbaConnection.SelectData(SPString);
            frm_AccountList.dgvUserSetting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            frm_AccountList.dgvUserSetting.Columns[0].FillWeight = 10;
            frm_AccountList.dgvUserSetting.Columns[1].Visible = false;
            frm_AccountList.dgvUserSetting.Columns[2].FillWeight = 10;
            frm_AccountList.dgvUserSetting.Columns[3].FillWeight = 24;
            frm_AccountList.dgvUserSetting.Columns[4].FillWeight = 24;
            frm_AccountList.dgvUserSetting.Columns[5].FillWeight = 10;
            frm_AccountList.dgvUserSetting.Columns[6].FillWeight = 22;

            dbaConnection.ToolStripTextBoxData(frm_AccountList.tstSearchWith, SPString, "UserName");
        }

        public void ShowEntry()
        {
            if (frm_AccountList.dgvUserSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There is No Data");
            }
            else
            {
                frm_CreateAccount frm = new frm_CreateAccount();
                
                frm._UserID = Convert.ToInt32(frm_AccountList.dgvUserSetting.CurrentRow.Cells["UserID"].Value.ToString());
                frm.txtUserName.Text = frm_AccountList.dgvUserSetting.CurrentRow.Cells["UserName"].Value.ToString();
                frm.txtPassword.Text = frm_AccountList.dgvUserSetting.CurrentRow.Cells["Password"].Value.ToString();
                frm.txtConfirmPassword.Text = frm_AccountList.dgvUserSetting.CurrentRow.Cells["Password"].Value.ToString();
                frm.cboAccessLevel.DisplayMember = frm_AccountList.dgvUserSetting.CurrentRow.Cells["AccessID"].Value.ToString();
                frm._AccountID = Convert.ToInt32(frm_AccountList.dgvUserSetting.CurrentRow.Cells["AccountID"].Value);

                SPString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", frm._UserID, "0", "0", "6");
                DataTable DT = new DataTable();
                DT = dbaConnection.SelectData(SPString);

                

                frm.txtFullName.Text = DT.Rows[0]["FullName"].ToString();
                frm.txtFullName.Enabled = false;

                frm.txtAddress.Text = DT.Rows[0]["Address"].ToString();
                frm.txtAddress.Enabled = false;

                frm.txtPhone.Text = DT.Rows[0]["Phone"].ToString();
                frm.txtPhone.Enabled = false;

                frm.cboPosition.DisplayMember = DT.Rows[0]["PositionID"].ToString();
                frm.cboPosition.Enabled = false;

                frm._IsEdit = true;
                frm.ShowDialog();
                ShowData();
            }
        }
        public void TsbDelete()
        {
            DbaAccountSetting dbaAccountSetting = new DbaAccountSetting();
            if (frm_AccountList.dgvUserSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else
            {
                if (MessageBox.Show("Are You Sure You Want To Delete?", "Confirm",
                 MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if(frm_AccountList.dgvUserSetting.CurrentRow.Cells["UserID"].Value.ToString() == Program.UserID.ToString())
                    {
                        MessageBox.Show("You cannot delete your own account!");
                    }
                    else
                    {
                        dbaAccountSetting.USERID = Convert.ToInt32(frm_AccountList.dgvUserSetting.CurrentRow.Cells["UserID"].Value.ToString());
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
            SPString = string.Format("SP_Select_Accounts N'{0}', N'{1}', N'{2}', N'{3}'", frm_AccountList.tstSearchWith.Text.Trim().ToString(), "0", "0", "2");
            frm_AccountList.dgvUserSetting.DataSource = dbaConnection.SelectData(SPString);
        }
        public void HoverToolTip()
        {
            frm_AccountList.dgvUserSetting.ShowCellToolTips = true;

            foreach (DataGridViewRow row in frm_AccountList.dgvUserSetting.Rows)
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
            frm_CreateAccount frm_CreateAccount = new frm_CreateAccount();
            frm_CreateAccount.ShowDialog();
            ShowData();
        }
    }
}
