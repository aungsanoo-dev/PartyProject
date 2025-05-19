using F21Party.DBA;
using F21Party.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F21Party.Controllers
{
    internal class CtrlFrmPermissionList
    {
        public frm_PermissionList frmPermissionList; // Declare the View
        //public frm_Main frmMain;
        public bool Logout;
        public CtrlFrmPermissionList(frm_PermissionList permissionForm)
        {
            frmPermissionList = permissionForm; // Create the View
            Logout = false;
        }
        //public CtrlFrmPermissionList(frm_Main mainForm)
        //{
        //    frmMain = mainForm; // Create the View
        //}
        string spString = "";
        DbaConnection dbaConnection = new DbaConnection();


        public void ShowData()
        {
            spString = string.Format("SP_Select_Permission N'{0}', N'{1}', N'{2}',N'{3}'", "0", "0", "0", "4");
            frmPermissionList.dgvPermissionSetting.DataSource = dbaConnection.SelectData(spString);
            frmPermissionList.dgvPermissionSetting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            frmPermissionList.dgvPermissionSetting.Columns[0].FillWeight = 20;
            frmPermissionList.dgvPermissionSetting.Columns[1].Visible = false;
            frmPermissionList.dgvPermissionSetting.Columns[2].Visible = false;
            frmPermissionList.dgvPermissionSetting.Columns[3].FillWeight = 20;
            frmPermissionList.dgvPermissionSetting.Columns[4].Visible = false;
            frmPermissionList.dgvPermissionSetting.Columns[5].FillWeight = 20;
            frmPermissionList.dgvPermissionSetting.Columns[6].Visible = false;
            frmPermissionList.dgvPermissionSetting.Columns[7].FillWeight = 20;
            frmPermissionList.dgvPermissionSetting.Columns[8].FillWeight = 20;

            dbaConnection.ToolStripTextBoxData(frmPermissionList.tstSearchWith, spString, "AccessLevel");

            if (!Program.PublicArrWriteAccessPages.Contains("Permission"))
            {
                frmPermissionList.tsbNew.ForeColor = System.Drawing.SystemColors.GrayText;
                frmPermissionList.tsbEdit.ForeColor = System.Drawing.SystemColors.GrayText;
                frmPermissionList.tsbDelete.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }

        public void ShowEntry()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("Permission"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            string SPAccess = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["AccessID"].Value.ToString(),
                    "", 1);
            DataTable DTAccess = dbaConnection.SelectData(SPAccess);

            if (frmPermissionList.dgvPermissionSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There is No Data");
            }
            // Not Allowed to change SuperAdmin
            else if (Convert.ToInt32(DTAccess.Rows[0]["AccessID"]) == 1 && Program.UserAuthority != 1) // AccessID 1 is SuperAdmin
            {
                MessageBox.Show("You cannont change SuperAdmin account!");
            }
            else if (Program.UserAuthority > Convert.ToInt32(DTAccess.Rows[0]["Authority"]) && Program.UserAuthority != 1)
            {
                MessageBox.Show("You cannont change Higher Authority Account!");
            }
            else if (frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["AccessID"].Value.ToString() == Program.UserAccessID.ToString() && frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["PageName"].Value.ToString() == "Permission")
            {
                MessageBox.Show("You cannot change your own permission for this page!");
            }
            else
            {
                frm_CreatePermission frmCreatePermission = new frm_CreatePermission();

                frmCreatePermission.PermissionID = Convert.ToInt32(frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["PermissionID"].Value);

                frmCreatePermission.cboAccessLevel.DisplayMember = frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["AccessID"].Value.ToString();
                frmCreatePermission.cboPageName.DisplayMember = frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["PageID"].Value.ToString();
                frmCreatePermission.cboPermissionType.DisplayMember = frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["PermissionTypeID"].Value.ToString();
                frmCreatePermission.cboAccessValue.DisplayMember = frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["AccessValue"].Value.ToString();
              
                string accessValue = frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["AccessValue"].Value.ToString();
                
                frmCreatePermission.cboAccessLevel.Enabled = false;
                frmCreatePermission.cboPageName.Enabled = false;
                frmCreatePermission.cboPermissionType.Enabled = false;

                frmCreatePermission.IsEdit = true;
                frmCreatePermission.btnCreate.Text = "Save";

                frmCreatePermission.accessValue = accessValue;
                var result = frmCreatePermission.ShowDialog();
                ShowData();
 
                if (result == DialogResult.OK)
                {
                    frmCreatePermission.DialogResult = DialogResult.None;
                    frmCreatePermission.Close();
                    frmPermissionList.IsLogout = true;
                }
                
            }
        }

        public void TsbNew()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("Permission"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            frm_CreatePermission frmCreatePermission = new frm_CreatePermission();
            frmCreatePermission.ShowDialog();
            ShowData();
        }

        public void TsbDelete()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("Permission"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            DbaPermission dbaPermissionSetting = new DbaPermission();
            string SPAccess = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["AccessID"].Value.ToString(),
                    "", 1);
            DataTable DTAccess = dbaConnection.SelectData(SPAccess);
            if (frmPermissionList.dgvPermissionSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else if (Convert.ToInt32(DTAccess.Rows[0]["Authority"]) == 1 && Program.UserAuthority != 1) // AccessID 1 is SuperAdmin
            {
                MessageBox.Show("You cannont delete SuperAdmin permission!");
            }
            else
            {
                if (MessageBox.Show("Are You Sure You Want To Delete?", "Confirm",
                 MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["AccessID"].Value.ToString() == Program.UserAccessID.ToString() && frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["PageName"].Value.ToString() == "Permission")
                    {
                        MessageBox.Show("You cannot delete your own permission for this page!");
                    }
                    else if (Program.UserAuthority >= Convert.ToInt32(DTAccess.Rows[0]["Authority"]) && Program.UserAuthority != 1)
                    {
                        MessageBox.Show("You cannont delete Higher or Same Authority Permission!");
                    }
                    else
                    {
                        dbaPermissionSetting.PERMISSIONID = Convert.ToInt32(frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["PermissionID"].Value.ToString());
                        dbaPermissionSetting.ACTION = 2;
                        dbaPermissionSetting.SaveData();
                        MessageBox.Show("Successfully Delete");
                        ShowData();
                    }
                }
            }
        }
        public void TsmiAccessLevelClick()
        {
            frmPermissionList.tslLabel.Text = "Access Level:";
            spString = string.Format("SP_Select_Permission N'{0}', N'{1}', N'{2}',N'{3}'", "0", "0", "0", "4");
            dbaConnection.ToolStripTextBoxData(frmPermissionList.tstSearchWith, spString, "AccessLevel");
        }

        public void TsmiPageNameClick()
        {
            frmPermissionList.tslLabel.Text = "Page Name:";
            spString = string.Format("SP_Select_Permission N'{0}', N'{1}', N'{2}',N'{3}'", "0", "0", "0", "4");
            dbaConnection.ToolStripTextBoxData(frmPermissionList.tstSearchWith, spString, "PageName");
        }

        public void TsmiPermissionNameClick()
        {
            frmPermissionList.tslLabel.Text = "Permission Name:";
            spString = string.Format("SP_Select_Permission N'{0}', N'{1}', N'{2}',N'{3}'", "0", "0", "0", "4");
            dbaConnection.ToolStripTextBoxData(frmPermissionList.tstSearchWith, spString, "PermissionName");
        }

        public void TstSearchWithTextChanged()
        {
            if(frmPermissionList.tslLabel.Text == "Access Level:")
            {
                spString = string.Format("SP_Select_Permission N'{0}', N'{1}', N'{2}',N'{3}'", frmPermissionList.tstSearchWith.Text.Trim().ToString(), "0", "0", "5");
            }
            else if (frmPermissionList.tslLabel.Text == "Page Name:")
            {
                spString = string.Format("SP_Select_Permission N'{0}', N'{1}', N'{2}',N'{3}'", frmPermissionList.tstSearchWith.Text.Trim().ToString(), "0", "0", "6");
            }
            else if (frmPermissionList.tslLabel.Text == "Permission Name:")
            {
                spString = string.Format("SP_Select_Permission N'{0}', N'{1}', N'{2}',N'{3}'", frmPermissionList.tstSearchWith.Text.Trim().ToString(), "0", "0", "7");
            }
            else
            {
                MessageBox.Show("Error: Something went wrong.");
            }
            frmPermissionList.dgvPermissionSetting.DataSource = dbaConnection.SelectData(spString);
        }

    }
}
