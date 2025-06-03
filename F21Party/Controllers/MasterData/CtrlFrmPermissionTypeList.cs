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
    internal class CtrlFrmPermissionTypeList
    {
        private readonly frm_PermissionTypeList _frmPermissionTypeList; // Declare the View
        private string _spString = "";
        private readonly DbaConnection _dbaConnection = new DbaConnection();
        public CtrlFrmPermissionTypeList(frm_PermissionTypeList permissionTypeForm)
        {
            _frmPermissionTypeList = permissionTypeForm; // Create the View
        }

        public void ShowData()
        {
            _spString = string.Format("SP_Select_PermissionType N'{0}', N'{1}', N'{2}'", "0", "0", "0");
            _frmPermissionTypeList.dgvPermissionTypeSetting.DataSource = _dbaConnection.SelectData(_spString);
            _frmPermissionTypeList.dgvPermissionTypeSetting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            _frmPermissionTypeList.dgvPermissionTypeSetting.Columns[0].FillWeight = 35;
            _frmPermissionTypeList.dgvPermissionTypeSetting.Columns[1].Visible = false;
            _frmPermissionTypeList.dgvPermissionTypeSetting.Columns[2].FillWeight = 65;

            _dbaConnection.ToolStripTextBoxData(_frmPermissionTypeList.tstSearchWith, _spString, "PermissionName");

            if (!Program.PublicArrWriteAccessPages.Contains("PermissionType"))
            {
                _frmPermissionTypeList.tsbNew.ForeColor = System.Drawing.SystemColors.GrayText;
                _frmPermissionTypeList.tsbEdit.ForeColor = System.Drawing.SystemColors.GrayText;
                _frmPermissionTypeList.tsbDelete.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }
        public void ShowEntry()
        {
            if (!Function.HasWriteAccess("PermissionType")) return;

            if (_frmPermissionTypeList.dgvPermissionTypeSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There is No Data");
            }
            else
            {
                frm_CreatePermissionType frmCreatePermissionType = new frm_CreatePermissionType();

                frmCreatePermissionType.PermissionTypeID = Convert.ToInt32(_frmPermissionTypeList.dgvPermissionTypeSetting.CurrentRow.Cells["PermissionTypeID"].Value);
                frmCreatePermissionType.txtPermissionName.Text = _frmPermissionTypeList.dgvPermissionTypeSetting.CurrentRow.Cells["PermissionName"].Value.ToString();

                frmCreatePermissionType.IsEdit = true;
                frmCreatePermissionType.btnCreate.Text = "Save";
                frmCreatePermissionType.ShowDialog();
                ShowData();
            }
        }

        public void TsbNew()
        {
            if (!Function.HasWriteAccess("PermissionType")) return;

            frm_CreatePermissionType frmCreatePermissionType = new frm_CreatePermissionType();
            frmCreatePermissionType.ShowDialog();
            ShowData();
        }

        public void TsbSearch()
        {
            _spString = string.Format("SP_Select_PermissionType N'{0}', N'{1}', N'{2}'", _frmPermissionTypeList.tstSearchWith.Text.Trim().ToString(), "0", "4");
            _frmPermissionTypeList.dgvPermissionTypeSetting.DataSource = _dbaConnection.SelectData(_spString);
        }
        public void TsbDelete()
        {
            if (!Function.HasWriteAccess("PermissionType")) return;

            _spString = string.Format("SP_Select_PermissionType N'{0}', N'{1}', N'{2}'", Convert.ToInt32(_frmPermissionTypeList.dgvPermissionTypeSetting.CurrentRow.Cells["PermissionTypeID"].Value), "0", "3");
            DataTable dt = new DataTable();
            dt = _dbaConnection.SelectData(_spString);

            DbaPermissionType dbaPermissionTypeSetting = new DbaPermissionType();
            if (_frmPermissionTypeList.dgvPermissionTypeSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else if (dt.Rows.Count > 0)
            {
                MessageBox.Show("You cannont delete the PermissionType which is currently used by the Permission!");
            }
            else if (MessageBox.Show("Are You Sure You Want To Delete?", "Confirm",
                 MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dbaPermissionTypeSetting.PID = Convert.ToInt32(_frmPermissionTypeList.dgvPermissionTypeSetting.CurrentRow.Cells["PermissionTypeID"].Value.ToString());
                dbaPermissionTypeSetting.ACTION = 2;
                dbaPermissionTypeSetting.SaveData();
                MessageBox.Show("Successfully Delete");
                ShowData();
            }
        }
    }
}
