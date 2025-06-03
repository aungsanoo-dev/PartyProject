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
    internal class CtrlFrmPageList
    {
        private readonly frm_PageList _frmPageList; // Declare the View
        private string _spString = "";
        private readonly DbaConnection _dbaConnection = new DbaConnection();
        public CtrlFrmPageList(frm_PageList pageForm)
        {
            _frmPageList = pageForm; // Create the View
        }

        public void ShowData()
        {
            _spString = string.Format("SP_Select_Page N'{0}', N'{1}', N'{2}'", "0", "0", "0");
            _frmPageList.dgvPageSetting.DataSource = _dbaConnection.SelectData(_spString);
            _frmPageList.dgvPageSetting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            _frmPageList.dgvPageSetting.Columns[0].FillWeight = 35;
            _frmPageList.dgvPageSetting.Columns[1].Visible = false;
            _frmPageList.dgvPageSetting.Columns[2].FillWeight = 65;

            _dbaConnection.ToolStripTextBoxData(_frmPageList.tstSearchWith, _spString, "PageName");

            if (!Program.PublicArrWriteAccessPages.Contains("Page"))
            {
                _frmPageList.tsbNew.ForeColor = System.Drawing.SystemColors.GrayText;
                _frmPageList.tsbEdit.ForeColor = System.Drawing.SystemColors.GrayText;
                _frmPageList.tsbDelete.ForeColor = System.Drawing.SystemColors.GrayText;
            }
            if(Program.UserAuthority != 1)
            {
                _frmPageList.tsbEdit.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }
        public void ShowEntry()
        {
            if (!Function.HasWriteAccess("Page")) return;

            if (Program.UserAuthority != 1)
            {
                MessageBox.Show("Only Super Admin can edit Page!");
                return;
            }

            if (_frmPageList.dgvPageSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There is No Data");
            }
            else
            {
                frm_CreatePage frmCreatePage = new frm_CreatePage();

                frmCreatePage.PageID = Convert.ToInt32(_frmPageList.dgvPageSetting.CurrentRow.Cells["PageID"].Value);
                frmCreatePage.txtPageName.Text = _frmPageList.dgvPageSetting.CurrentRow.Cells["PageName"].Value.ToString();

                if (_frmPageList.dgvPageSetting.CurrentRow.Cells["PageName"].Value.ToString() == "Page" && Program.UserAuthority != 1)
                {
                    frmCreatePage.txtPageName.Enabled = false;
                }

                frmCreatePage.IsEdit = true;
                frmCreatePage.btnCreate.Text = "Save";
                frmCreatePage.ShowDialog();
                ShowData();
            }
        }

        public void TsbNew()
        {
            if (!Function.HasWriteAccess("Page")) return;

            frm_CreatePage frmCreatePage = new frm_CreatePage();
            frmCreatePage.ShowDialog();
            ShowData();
        }

        public void TsbSearch()
        {
            _spString = string.Format("SP_Select_Page N'{0}', N'{1}', N'{2}'", _frmPageList.tstSearchWith.Text.Trim().ToString(), "0", "4");
            _frmPageList.dgvPageSetting.DataSource = _dbaConnection.SelectData(_spString);
        }
        public void TsbDelete()
        {
            if (!Function.HasWriteAccess("Page")) return;

            DbaPage dbaPage = new DbaPage();

            _spString = string.Format("SP_Select_Page N'{0}', N'{1}', N'{2}'", Convert.ToInt32(_frmPageList.dgvPageSetting.CurrentRow.Cells["PageID"].Value), "0", "3");
            DataTable dt = new DataTable();
            dt = _dbaConnection.SelectData(_spString);

            if (_frmPageList.dgvPageSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else if(dt.Rows.Count > 0)
            {
                MessageBox.Show("You cannont delete the Page which is currently used by the Permission!");
            }
            else if (MessageBox.Show("Are You Sure You Want To Delete?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dbaPage.PID = Convert.ToInt32(_frmPageList.dgvPageSetting.CurrentRow.Cells["PageID"].Value.ToString());
                dbaPage.ACTION = 2;
                dbaPage.SaveData();
                MessageBox.Show("Successfully Delete");
                ShowData();
            }
        }
    }
}
