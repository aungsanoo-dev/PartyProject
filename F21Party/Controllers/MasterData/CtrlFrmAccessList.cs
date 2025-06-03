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
    internal class CtrlFrmAccessList
    {
        private readonly frm_AccessList _frmAccessList; // Declare the View
        private readonly DbaConnection _dbaConnection = new DbaConnection();
        private string _spString = "";
        public CtrlFrmAccessList(frm_AccessList accessForm)
        {
            _frmAccessList = accessForm; // Create the View
        }

        public void ShowData()
        {
            _spString = string.Format("SP_Select_Access N'{0}', N'{1}', N'{2}'", "0", "0", "0");
            _frmAccessList.dgvAccessSetting.DataSource = _dbaConnection.SelectData(_spString);
            _frmAccessList.dgvAccessSetting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            _frmAccessList.dgvAccessSetting.Columns[0].FillWeight = 25;
            _frmAccessList.dgvAccessSetting.Columns[1].Visible = false;
            _frmAccessList.dgvAccessSetting.Columns[2].FillWeight = 25;
            _frmAccessList.dgvAccessSetting.Columns[3].FillWeight = 25;
            _frmAccessList.dgvAccessSetting.Columns[4].FillWeight = 25;

            _dbaConnection.ToolStripTextBoxData(_frmAccessList.tstSearchWith, _spString, "AccessLevel");

            if (!Program.PublicArrWriteAccessPages.Contains("Access"))
            {
                _frmAccessList.tsbNew.ForeColor = System.Drawing.SystemColors.GrayText;
                _frmAccessList.tsbEdit.ForeColor = System.Drawing.SystemColors.GrayText;
                _frmAccessList.tsbDelete.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }

        public void ShowEntry()
        {
            if (!Function.HasWriteAccess("Access")) return;

            _spString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", _frmAccessList.dgvAccessSetting.CurrentRow.Cells["AccessID"].Value.ToString(), "", 1);
            DataTable dtAccess = _dbaConnection.SelectData(_spString);

            if (_frmAccessList.dgvAccessSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There is No Data");
            }
            // Not Allowed to change SuperAdmin
            else if (Convert.ToInt32(_frmAccessList.dgvAccessSetting.CurrentRow.Cells["Authority"].Value) == 1 && Program.UserAuthority != 1) // AccessID 1 is SuperAdmin
            {
                string accessName = _frmAccessList.dgvAccessSetting.CurrentRow.Cells["AccessLevel"].Value.ToString();
                MessageBox.Show("You cannont change " + accessName +" account!");
            }
            else if (Program.UserAuthority >= Convert.ToInt32(dtAccess.Rows[0]["Authority"]) && Program.UserAuthority != 1)
            {
                MessageBox.Show("You cannont change Higher or Same Authority Account!");
            }
            else
            {
                frm_CreateAccessAuthority frmCreateAccessAuthority = new frm_CreateAccessAuthority();

                frmCreateAccessAuthority.AccessID = Convert.ToInt32(_frmAccessList.dgvAccessSetting.CurrentRow.Cells["AccessID"].Value);
                frmCreateAccessAuthority.txtAccessLevel.Text = _frmAccessList.dgvAccessSetting.CurrentRow.Cells["AccessLevel"].Value.ToString();
                frmCreateAccessAuthority.cboLogInAccess.DisplayMember = _frmAccessList.dgvAccessSetting.CurrentRow.Cells["LogInAccess"].Value.ToString();
                frmCreateAccessAuthority.txtAuthority.Text = _frmAccessList.dgvAccessSetting.CurrentRow.Cells["Authority"].Value.ToString();

                if(Convert.ToInt32(_frmAccessList.dgvAccessSetting.CurrentRow.Cells["Authority"].Value) == 1)
                {
                    frmCreateAccessAuthority.txtAuthority.Enabled = false;
                    frmCreateAccessAuthority.cboLogInAccess.Enabled = false;
                }
                
                frmCreateAccessAuthority.IsEdit = true;
                frmCreateAccessAuthority.btnCreate.Text = "Save";
                frmCreateAccessAuthority.ShowDialog();
                ShowData();
            }
        }

        public void TsbNew()
        {
            if (!Function.HasWriteAccess("Access")) return;

            frm_CreateAccess frmCreateAccess = new frm_CreateAccess();
            frmCreateAccess.ShowDialog();
            ShowData();

        }

        public void TsbSearch()
        {
            if (_frmAccessList.tslLabel.Text == "AccessLevel")
            {
                _spString = string.Format("SP_Select_Access N'{0}', N'{1}', N'{2}'", _frmAccessList.tstSearchWith.Text.Trim().ToString(), "0", "4");
            }
            else if (_frmAccessList.tslLabel.Text == "LogInAccess")
            {
                _spString = string.Format("SP_Select_Access N'{0}', N'{1}', N'{2}'", _frmAccessList.tstSearchWith.Text.Trim().ToString(), "0", "7");
            }
            else if (_frmAccessList.tslLabel.Text == "Authority")
            {
                _spString = string.Format("SP_Select_Access N'{0}', N'{1}', N'{2}'", _frmAccessList.tstSearchWith.Text.Trim().ToString(), "0", "8");
            }
            _frmAccessList.dgvAccessSetting.DataSource = _dbaConnection.SelectData(_spString);
        }

        public void TsmSearchLabelClick(string textLabel)
        {
            _frmAccessList.tslLabel.Text = textLabel;
            _spString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            _dbaConnection.ToolStripTextBoxData(_frmAccessList.tstSearchWith, _spString, textLabel);
        }
        public void TsbDelete()
        {
            if (!Function.HasWriteAccess("Access")) return;

            DbaAccess dbaAccessSetting = new DbaAccess();
            _spString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", _frmAccessList.dgvAccessSetting.CurrentRow.Cells["AccessID"].Value.ToString(), "", 1);
            DataTable dtAccess = _dbaConnection.SelectData(_spString);

            _spString = string.Format("SP_Select_Access N'{0}', N'{1}', N'{2}'", Convert.ToInt32(_frmAccessList.dgvAccessSetting.CurrentRow.Cells["AccessID"].Value), "0", "3"); // Check User with this Access
            DataTable dtUser = new DataTable();
            dtUser = _dbaConnection.SelectData(_spString);

            _spString = string.Format("SP_Select_Access N'{0}', N'{1}', N'{2}'", Convert.ToInt32(_frmAccessList.dgvAccessSetting.CurrentRow.Cells["AccessID"].Value), "0", "6"); // Check Permission with this Access
            DataTable dtPermission = new DataTable();
            dtPermission = _dbaConnection.SelectData(_spString);

            if (_frmAccessList.dgvAccessSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else if(dtUser.Rows.Count > 0)
            {
                MessageBox.Show("You cannont delete the Access which is currently used by the User's Account!");
            }
            else if (dtPermission.Rows.Count > 0)
            {
                MessageBox.Show("You cannont delete the Access which is currently used by the Permission!");
            }
            else if (Convert.ToInt32(_frmAccessList.dgvAccessSetting.CurrentRow.Cells["Authority"].Value) == 1)
            {
                MessageBox.Show("You cannot delete SuperAdmin!");
            }
            else if (Program.UserAuthority >= Convert.ToInt32(dtAccess.Rows[0]["Authority"]) && Program.UserAuthority != 1)
            {
                MessageBox.Show("You cannont delete Higher or Same Authority Account!");
            }
            else
            {
                if (MessageBox.Show("Are You Sure You Want To Delete?", "Confirm",
                 MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dbaAccessSetting.AID = Convert.ToInt32(_frmAccessList.dgvAccessSetting.CurrentRow.Cells["AccessID"].Value.ToString());
                    dbaAccessSetting.ACTION = 2;
                    dbaAccessSetting.SaveData();
                    MessageBox.Show("Successfully Delete");
                    ShowData();
                }
            }
        }
    }
}
