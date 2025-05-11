using F21Party.DBA;
using F21Party.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F21Party.Controllers
{
    internal class CtrlFrmPermissionList
    {
        public frm_PermissionList frmPermissionList; // Declare the View
        public CtrlFrmPermissionList(frm_PermissionList permissionForm)
        {
            frmPermissionList = permissionForm; // Create the View
        }
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
        }

        public void ShowEntry()
        {
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
            else
            {
                frm_CreatePermission frmCreatePermission = new frm_CreatePermission();

                frmCreatePermission.PermissionID = Convert.ToInt32(frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["PermissionID"].Value);
                // Here stop
                frmCreatePermission.cboAccessLevel.DisplayMember = frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["AccessID"].Value.ToString();
                frmCreatePermission.cboPageName.DisplayMember = frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["PageID"].Value.ToString();
                frmCreatePermission.cboPermissionType.DisplayMember = frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["PermissionTypeID"].Value.ToString();
                frmCreatePermission.cboAccessValue.DisplayMember = frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["AccessValue"].Value.ToString();

                frmCreatePermission.cboAccessLevel.Enabled = false;
                frmCreatePermission.cboPageName.Enabled = false;
                frmCreatePermission.cboPermissionType.Enabled = false;

                frmCreatePermission.IsEdit = true;
                frmCreatePermission.btnCreate.Text = "Save";
                frmCreatePermission.ShowDialog();
                ShowData();
            }
        }

        public void TsbNew()
        {
            frm_CreatePermission frmCreatePermission = new frm_CreatePermission();
            frmCreatePermission.ShowDialog();
            ShowData();
        }


    }
}
