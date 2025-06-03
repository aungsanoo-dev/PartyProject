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
    internal class CtrlFrmPositionList
    {
        private readonly frm_PositionList _frmPositionList; // Declare the View
        public CtrlFrmPositionList(frm_PositionList positionForm)
        {
            _frmPositionList = positionForm; // Create the View
        }
        private string _spString;
        private readonly DbaConnection _dbaConnection = new DbaConnection();

        public void ShowData()
        {
            _spString = string.Format("SP_Select_Position N'{0}', N'{1}', N'{2}'", "0", "0", "0");
            _frmPositionList.dgvPositionSetting.DataSource = _dbaConnection.SelectData(_spString);
            _frmPositionList.dgvPositionSetting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            _frmPositionList.dgvPositionSetting.Columns[0].FillWeight = 35;
            _frmPositionList.dgvPositionSetting.Columns[1].Visible = false;
            _frmPositionList.dgvPositionSetting.Columns[2].FillWeight = 65;

            _dbaConnection.ToolStripTextBoxData(_frmPositionList.tstSearchWith, _spString, "PositionName");

            if (!Program.PublicArrWriteAccessPages.Contains("Position"))
            {
                _frmPositionList.tsbNew.ForeColor = System.Drawing.SystemColors.GrayText;
                _frmPositionList.tsbEdit.ForeColor = System.Drawing.SystemColors.GrayText;
                _frmPositionList.tsbDelete.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }
        public void ShowEntry()
        {
            if (!Function.HasWriteAccess("Position")) return;

            if (_frmPositionList.dgvPositionSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There is No Data");
            }
            else
            {
                frm_CreatePosition frmCreatePosition = new frm_CreatePosition();

                frmCreatePosition.PositionID = Convert.ToInt32(_frmPositionList.dgvPositionSetting.CurrentRow.Cells["PositionID"].Value);
                frmCreatePosition.txtPositionName.Text = _frmPositionList.dgvPositionSetting.CurrentRow.Cells["PositionName"].Value.ToString();

                frmCreatePosition.IsEdit = true;
                frmCreatePosition.btnCreate.Text = "Save";
                frmCreatePosition.ShowDialog();
                ShowData();
            }
        }

        public void TsbNew()
        {
            if (!Function.HasWriteAccess("Position")) return;

            frm_CreatePosition frmCreatePosition = new frm_CreatePosition();
            frmCreatePosition.ShowDialog();
            ShowData();
        }

        public void TsbSearch()
        {
            _spString = string.Format("SP_Select_Position N'{0}', N'{1}', N'{2}'", _frmPositionList.tstSearchWith.Text.Trim().ToString(), "0", "4");
            _frmPositionList.dgvPositionSetting.DataSource = _dbaConnection.SelectData(_spString);
        }

        public void TsbDelete()
        {
            if (!Function.HasWriteAccess("Position")) return;

            _spString = string.Format("SP_Select_Position N'{0}', N'{1}', N'{2}'", Convert.ToInt32(_frmPositionList.dgvPositionSetting.CurrentRow.Cells["PositionID"].Value), "0", "3");
            DataTable dt = new DataTable();
            dt = _dbaConnection.SelectData(_spString);

            DbaPosition dbaPositionSetting = new DbaPosition();
            if (_frmPositionList.dgvPositionSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else if(dt.Rows.Count > 0)
            {
                MessageBox.Show("You cannont delete the Position which is currently used by the User!");
            }
            else if (MessageBox.Show("Are You Sure You Want To Delete?", "Confirm",
                 MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dbaPositionSetting.PID = Convert.ToInt32(_frmPositionList.dgvPositionSetting.CurrentRow.Cells["PositionID"].Value.ToString());
                dbaPositionSetting.ACTION = 2;
                dbaPositionSetting.SaveData();
                MessageBox.Show("Successfully Delete");
                ShowData();
            }
        }
    }
}
