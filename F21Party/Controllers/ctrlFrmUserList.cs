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
        public frm_UserList frmUserList; // Declare the View
        public CtrlFrmUserList(frm_UserList userForm)
        {
            frmUserList = userForm; // Create the View
        }
        string spString = "";
        DbaConnection dbaConnection = new DbaConnection();

        public void ShowData()
        {
            spString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", "0", "0", "0", "0");
            frmUserList.dgvUserSetting.DataSource = dbaConnection.SelectData(spString);
            frmUserList.dgvUserSetting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            frmUserList.dgvUserSetting.Columns[0].FillWeight = 10;
            frmUserList.dgvUserSetting.Columns[1].Visible = false;
            frmUserList.dgvUserSetting.Columns[2].FillWeight = 10;
            frmUserList.dgvUserSetting.Columns[3].FillWeight = 35;
            frmUserList.dgvUserSetting.Columns[4].FillWeight = 35;
            frmUserList.dgvUserSetting.Columns[5].FillWeight = 10;
            //frmAccountList.dgvUserSetting.Columns[6].FillWeight = 22;

            dbaConnection.ToolStripTextBoxData(frmUserList.tstSearchWith, spString, "FullName");
        }

        public void ShowEntry()
        {
            if (frmUserList.dgvUserSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There is No Data");
            }
            else
            {
                frm_CreateUser frmCreateUser = new frm_CreateUser();
                
                frmCreateUser._UserID = Convert.ToInt32(frmUserList.dgvUserSetting.CurrentRow.Cells["UserID"].Value);
                frmCreateUser.txtFullName.Text = frmUserList.dgvUserSetting.CurrentRow.Cells["FullName"].Value.ToString();
                frmCreateUser.txtAddress.Text = frmUserList.dgvUserSetting.CurrentRow.Cells["Address"].Value.ToString();
                frmCreateUser.txtPhone.Text = frmUserList.dgvUserSetting.CurrentRow.Cells["Phone"].Value.ToString();
                frmCreateUser.cboPosition.DisplayMember = frmUserList.dgvUserSetting.CurrentRow.Cells["PositionID"].Value.ToString();

                frmCreateUser._IsEdit = true;
                frmCreateUser.ShowDialog();
                ShowData();
            }
        }
        public void TsbDelete()
        {
            DbaUserSetting dbaUserSetting = new DbaUserSetting();
            if (frmUserList.dgvUserSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else
            {
                if (MessageBox.Show("Are You Sure You Want To Delete?", "Confirm",
                 MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    spString = string.Format("SP_Select_Accounts N'{0}', N'{1}', N'{2}', N'{3}'", Convert.ToInt32(frmUserList.dgvUserSetting.CurrentRow.Cells["UserID"].Value), "0", "0", "6");
                    DataTable DT = new DataTable();
                    DT = dbaConnection.SelectData(spString);


                    if (frmUserList.dgvUserSetting.CurrentRow.Cells["UserID"].Value.ToString() == Program.UserID.ToString())
                    {
                        MessageBox.Show("You cannot delete your own user!");
                    }
                    else if (DT.Rows.Count > 0)
                    {
                        MessageBox.Show("You cannont delete the user which has the user account!");
                    }
                    else
                    {
                        dbaUserSetting.UID = Convert.ToInt32(frmUserList.dgvUserSetting.CurrentRow.Cells["UserID"].Value.ToString());
                        dbaUserSetting.ACTION = 2;
                        dbaUserSetting.SaveData();
                        MessageBox.Show("Successfully Delete");
                        ShowData();
                    }  


                }
            }
        }
        public void TsbSearch()
        {
            spString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", frmUserList.tstSearchWith.Text.Trim().ToString(), "0", "0", "2");
            frmUserList.dgvUserSetting.DataSource = dbaConnection.SelectData(spString);
        }
        
        public void TsbNew()
        {
            frm_CreateUser frmCreateUser = new frm_CreateUser();
            frmCreateUser.ShowDialog();
            ShowData();
        }
    }
}
