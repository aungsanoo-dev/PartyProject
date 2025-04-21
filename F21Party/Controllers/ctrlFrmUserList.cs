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
    internal class ctrlFrmUserList
    {
        public frm_UserList frm_UserList; // Declare the View
        public ctrlFrmUserList(frm_UserList userForm)
        {
            frm_UserList = userForm; // Create the View
        }
        string SPString = "";
        clsMainDB obj_clsMainDB = new clsMainDB();

        public void ShowData()
        {
            SPString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", "0", "0", "0", "0");
            frm_UserList.dgvUserSetting.DataSource = obj_clsMainDB.SelectData(SPString);
            frm_UserList.dgvUserSetting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            frm_UserList.dgvUserSetting.Columns[0].FillWeight = 10;
            frm_UserList.dgvUserSetting.Columns[1].Visible = false;
            frm_UserList.dgvUserSetting.Columns[2].FillWeight = 10;
            frm_UserList.dgvUserSetting.Columns[3].FillWeight = 35;
            frm_UserList.dgvUserSetting.Columns[4].FillWeight = 35;
            frm_UserList.dgvUserSetting.Columns[5].FillWeight = 10;
            //frm_AccountList.dgvUserSetting.Columns[6].FillWeight = 22;

            obj_clsMainDB.ToolStripTextBoxData(frm_UserList.tstSearchWith, SPString, "FullName");
        }

        public void ShowEntry()
        {
            if (frm_UserList.dgvUserSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There is No Data");
            }
            else
            {
                frm_RegisterUser frm = new frm_RegisterUser();
                
                frm._UserID = Convert.ToInt32(frm_UserList.dgvUserSetting.CurrentRow.Cells["UserID"].Value);
                frm.txtFullName.Text = frm_UserList.dgvUserSetting.CurrentRow.Cells["FullName"].Value.ToString();
                frm.txtAddress.Text = frm_UserList.dgvUserSetting.CurrentRow.Cells["Address"].Value.ToString();
                frm.txtPhone.Text = frm_UserList.dgvUserSetting.CurrentRow.Cells["Phone"].Value.ToString();
                frm.cboPosition.DisplayMember = frm_UserList.dgvUserSetting.CurrentRow.Cells["PositionID"].Value.ToString();

                frm._IsEdit = true;
                frm.ShowDialog();
                ShowData();
            }
        }
        public void TsbDelete()
        {
            clsUserSetting clsUserSetting = new clsUserSetting();
            if (frm_UserList.dgvUserSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else
            {
                if (MessageBox.Show("Are You Sure You Want To Delete?", "Confirm",
                 MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SPString = string.Format("SP_Select_Accounts N'{0}', N'{1}', N'{2}', N'{3}'", Convert.ToInt32(frm_UserList.dgvUserSetting.CurrentRow.Cells["UserID"].Value), "0", "0", "6");
                    DataTable DT = new DataTable();
                    DT = obj_clsMainDB.SelectData(SPString);


                    if (frm_UserList.dgvUserSetting.CurrentRow.Cells["UserID"].Value.ToString() == Program.UserID.ToString())
                    {
                        MessageBox.Show("You cannot delete your own user!");
                    }
                    else if (DT.Rows.Count > 0)
                    {
                        MessageBox.Show("You cannont delete the user which has the user account!");
                    }
                    else
                    {
                        clsUserSetting.UID = Convert.ToInt32(frm_UserList.dgvUserSetting.CurrentRow.Cells["UserID"].Value.ToString());
                        clsUserSetting.ACTION = 2;
                        clsUserSetting.SaveData();
                        MessageBox.Show("Successfully Delete");
                        ShowData();
                    }  


                }
            }
        }
        public void TsbSearch()
        {
            SPString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", frm_UserList.tstSearchWith.Text.Trim().ToString(), "0", "0", "2");
            frm_UserList.dgvUserSetting.DataSource = obj_clsMainDB.SelectData(SPString);
        }
        
        public void TsbNew()
        {
            frm_RegisterUser frm_RegisterUser = new frm_RegisterUser();
            frm_RegisterUser.ShowDialog();
            ShowData();
        }
    }
}
