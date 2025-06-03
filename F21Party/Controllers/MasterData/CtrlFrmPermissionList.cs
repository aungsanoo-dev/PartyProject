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
        private readonly frm_PermissionList _frmPermissionList; // Declare the View
        //private readonly bool _logout;
        private string _spString = "";
        private readonly DbaConnection _dbaConnection = new DbaConnection();
        public CtrlFrmPermissionList(frm_PermissionList permissionForm)
        {
            _frmPermissionList = permissionForm; // Create the View
            //_logout = false;
        }

        public void ShowData()
        {
            _spString = string.Format("SP_Select_Permission N'{0}', N'{1}', N'{2}',N'{3}'", "0", "0", "0", "4");
            _frmPermissionList.dgvPermissionSetting.DataSource = _dbaConnection.SelectData(_spString);
            _frmPermissionList.dgvPermissionSetting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            _frmPermissionList.dgvPermissionSetting.Columns[0].FillWeight = 20;
            _frmPermissionList.dgvPermissionSetting.Columns[1].Visible = false;
            _frmPermissionList.dgvPermissionSetting.Columns[2].Visible = false;
            _frmPermissionList.dgvPermissionSetting.Columns[3].FillWeight = 20;
            _frmPermissionList.dgvPermissionSetting.Columns[4].Visible = false;
            _frmPermissionList.dgvPermissionSetting.Columns[5].FillWeight = 20;
            _frmPermissionList.dgvPermissionSetting.Columns[6].Visible = false;
            _frmPermissionList.dgvPermissionSetting.Columns[7].FillWeight = 20;
            _frmPermissionList.dgvPermissionSetting.Columns[8].FillWeight = 20;

            _dbaConnection.ToolStripTextBoxData(_frmPermissionList.tstSearchWith, _spString, "AccessLevel");

            if (!Program.PublicArrWriteAccessPages.Contains("Permission"))
            {
                _frmPermissionList.tsbNew.ForeColor = System.Drawing.SystemColors.GrayText;
                _frmPermissionList.tsbEdit.ForeColor = System.Drawing.SystemColors.GrayText;
                _frmPermissionList.tsbDelete.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }

        public void ShowEntry()
        {
            if (!Function.HasWriteAccess("Permission")) return;

            _spString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", _frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["AccessID"].Value.ToString(),
                    "", 1);
            DataTable dtAccess = _dbaConnection.SelectData(_spString);

            _spString = string.Format("SP_Select_Permission N'{0}', N'{1}', N'{2}',N'{3}'", Program.UserAccessID,
                _frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["PageID"].Value.ToString(),
                _frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["PermissionTypeID"].Value.ToString(), "1");
            DataTable dtPermissionAccValue = _dbaConnection.SelectData(_spString);

            if (_frmPermissionList.dgvPermissionSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There is No Data");
            }
            // Not Allowed to change SuperAdmin
            else if (Convert.ToInt32(dtAccess.Rows[0]["AccessID"]) == 1 && Program.UserAuthority != 1) // AccessID 1 is SuperAdmin
            {
                MessageBox.Show("You cannont change SuperAdmin account!");
            }
            else if (Program.UserAuthority > Convert.ToInt32(dtAccess.Rows[0]["Authority"]) && Program.UserAuthority != 1)
            {
                MessageBox.Show("You cannont change Higher Authority Account!");
            }
            else if (_frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["AccessID"].Value.ToString() == Program.UserAccessID.ToString() && _frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["PageName"].Value.ToString() == "Permission")
            {
                MessageBox.Show("You cannot change your own permission for this page!");
            }
            else if (dtPermissionAccValue.Rows[0]["AccessValue"].ToString() == "False" && _frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["AccessValue"].Value.ToString() == "False")
            {
                MessageBox.Show("You cannot edit this permission because you have no access to it!");
            }
            else
            {
                frm_CreatePermission frmCreatePermission = new frm_CreatePermission();

                frmCreatePermission.PermissionID = Convert.ToInt32(_frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["PermissionID"].Value);

                frmCreatePermission.cboAccessLevel.DisplayMember = _frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["AccessID"].Value.ToString();
                frmCreatePermission.cboPageName.DisplayMember = _frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["PageID"].Value.ToString();
                frmCreatePermission.cboPermissionType.DisplayMember = _frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["PermissionTypeID"].Value.ToString();
                frmCreatePermission.cboAccessValue.DisplayMember = _frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["AccessValue"].Value.ToString();
              
                string accessValue = _frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["AccessValue"].Value.ToString();
                
                frmCreatePermission.cboAccessLevel.Enabled = false;
                frmCreatePermission.cboPageName.Enabled = false;
                frmCreatePermission.cboPermissionType.Enabled = false;

                frmCreatePermission.IsEdit = true;
                frmCreatePermission.btnCreate.Text = "Save";

                frmCreatePermission.AccessValue = accessValue;
                var result = frmCreatePermission.ShowDialog();
                ShowData();
 
                if (result == DialogResult.OK)
                {
                    frmCreatePermission.DialogResult = DialogResult.None;
                    frmCreatePermission.Close();
                    _frmPermissionList.IsLogout = true;
                }
            }
        }

        public void TsbNew()
        {
            if (!Function.HasWriteAccess("Permission")) return;

            frm_CreatePermission frmCreatePermission = new frm_CreatePermission();
            frmCreatePermission.ShowDialog();
            ShowData();
        }

        public void TsbDelete()
        {
            if (!Function.HasWriteAccess("Permission")) return;

            DbaPermission dbaPermissionSetting = new DbaPermission();
            _spString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", _frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["AccessID"].Value.ToString(), "", 1);
            DataTable dtAccess = _dbaConnection.SelectData(_spString);
            if (_frmPermissionList.dgvPermissionSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else if (Convert.ToInt32(dtAccess.Rows[0]["Authority"]) == 1 && Program.UserAuthority != 1) // AccessID 1 is SuperAdmin
            {
                MessageBox.Show("You cannont delete SuperAdmin permission!");
            }
            else if (_frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["AccessID"].Value.ToString() == Program.UserAccessID.ToString() && _frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["PageName"].Value.ToString() == "Permission")
            {
                MessageBox.Show("You cannot delete your own permission for this page!");
            }
            else if (Program.UserAuthority >= Convert.ToInt32(dtAccess.Rows[0]["Authority"]) && Program.UserAuthority != 1)
            {
                MessageBox.Show("You cannont delete Higher or Same Authority Permission!");
            }
            else if(MessageBox.Show("Are You Sure You Want To Delete?", "Confirm",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dbaPermissionSetting.PERMISSIONID = Convert.ToInt32(_frmPermissionList.dgvPermissionSetting.CurrentRow.Cells["PermissionID"].Value.ToString());
                dbaPermissionSetting.ACTION = 2;
                dbaPermissionSetting.SaveData();
                MessageBox.Show("Successfully Delete");
                ShowData();
            }
            
        }
        public void TsmiAccessLevelClick()
        {
            _frmPermissionList.tslLabel.Text = "Access Level:";
            _spString = string.Format("SP_Select_Permission N'{0}', N'{1}', N'{2}',N'{3}'", "0", "0", "0", "4");
            _dbaConnection.ToolStripTextBoxData(_frmPermissionList.tstSearchWith, _spString, "AccessLevel");
        }

        public void TsmiPageNameClick()
        {
            _frmPermissionList.tslLabel.Text = "Page Name:";
            _spString = string.Format("SP_Select_Permission N'{0}', N'{1}', N'{2}',N'{3}'", "0", "0", "0", "4");
            _dbaConnection.ToolStripTextBoxData(_frmPermissionList.tstSearchWith, _spString, "PageName");
        }

        public void TsmiPermissionNameClick()
        {
            _frmPermissionList.tslLabel.Text = "Permission Name:";
            _spString = string.Format("SP_Select_Permission N'{0}', N'{1}', N'{2}',N'{3}'", "0", "0", "0", "4");
            _dbaConnection.ToolStripTextBoxData(_frmPermissionList.tstSearchWith, _spString, "PermissionName");
        }

        public void TstSearchWithTextChanged()
        {
            if(_frmPermissionList.tslLabel.Text == "Access Level:")
            {
                _spString = string.Format("SP_Select_Permission N'{0}', N'{1}', N'{2}',N'{3}'", _frmPermissionList.tstSearchWith.Text.Trim().ToString(), "0", "0", "5");
            }
            else if (_frmPermissionList.tslLabel.Text == "Page Name:")
            {
                _spString = string.Format("SP_Select_Permission N'{0}', N'{1}', N'{2}',N'{3}'", _frmPermissionList.tstSearchWith.Text.Trim().ToString(), "0", "0", "6");
            }
            else if (_frmPermissionList.tslLabel.Text == "Permission Name:")
            {
                _spString = string.Format("SP_Select_Permission N'{0}', N'{1}', N'{2}',N'{3}'", _frmPermissionList.tstSearchWith.Text.Trim().ToString(), "0", "0", "7");
            }
            else
            {
                MessageBox.Show("Error: Something went wrong.");
            }
            _frmPermissionList.dgvPermissionSetting.DataSource = _dbaConnection.SelectData(_spString);
        }

    }
}
