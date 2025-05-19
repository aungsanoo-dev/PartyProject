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
        public frm_PageList frmPageList; // Declare the View
        public CtrlFrmPageList(frm_PageList pageForm)
        {
            frmPageList = pageForm; // Create the View
        }
        string spString = "";
        DbaConnection dbaConnection = new DbaConnection();

        public void ShowData()
        {
            spString = string.Format("SP_Select_Page N'{0}', N'{1}', N'{2}'", "0", "0", "0");
            frmPageList.dgvPageSetting.DataSource = dbaConnection.SelectData(spString);
            frmPageList.dgvPageSetting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            frmPageList.dgvPageSetting.Columns[0].FillWeight = 35;
            frmPageList.dgvPageSetting.Columns[1].Visible = false;
            frmPageList.dgvPageSetting.Columns[2].FillWeight = 65;

            dbaConnection.ToolStripTextBoxData(frmPageList.tstSearchWith, spString, "PageName");

            if (!Program.PublicArrWriteAccessPages.Contains("Page"))
            {
                frmPageList.tsbNew.ForeColor = System.Drawing.SystemColors.GrayText;
                frmPageList.tsbEdit.ForeColor = System.Drawing.SystemColors.GrayText;
                frmPageList.tsbDelete.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }
        public void ShowEntry()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("Page"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }


            if (frmPageList.dgvPageSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There is No Data");
            }
            else
            {
                frm_CreatePage frmCreatePage = new frm_CreatePage();

                frmCreatePage._PageID = Convert.ToInt32(frmPageList.dgvPageSetting.CurrentRow.Cells["PageID"].Value);
                frmCreatePage.txtPageName.Text = frmPageList.dgvPageSetting.CurrentRow.Cells["PageName"].Value.ToString();

                if (frmPageList.dgvPageSetting.CurrentRow.Cells["PageName"].Value.ToString() == "Page" && Program.UserAuthority != 1)
                {
                    frmCreatePage.txtPageName.Enabled = false;
                }

                frmCreatePage._IsEdit = true;
                frmCreatePage.btnCreate.Text = "Save";
                frmCreatePage.ShowDialog();
                ShowData();
            }
        }

        public void TsbNew()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("Page"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            frm_CreatePage frmCreatePage = new frm_CreatePage();
            frmCreatePage.ShowDialog();
            ShowData();
        }

        public void TsbSearch()
        {
            spString = string.Format("SP_Select_Page N'{0}', N'{1}', N'{2}'", frmPageList.tstSearchWith.Text.Trim().ToString(), "0", "4");
            frmPageList.dgvPageSetting.DataSource = dbaConnection.SelectData(spString);
        }
        public void TsbDelete()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("Page"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            DbaPage dbaPage = new DbaPage();
            if (frmPageList.dgvPageSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else
            {
                if (MessageBox.Show("Are You Sure You Want To Delete?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    spString = string.Format("SP_Select_Page N'{0}', N'{1}', N'{2}'", Convert.ToInt32(frmPageList.dgvPageSetting.CurrentRow.Cells["PageID"].Value), "0", "3");
                    DataTable DT = new DataTable();
                    DT = dbaConnection.SelectData(spString);

                    if (DT.Rows.Count > 0)
                    {
                        MessageBox.Show("You cannont delete the Page which is currently used by the Permission!");
                    }
                    else
                    {
                        dbaPage.PID = Convert.ToInt32(frmPageList.dgvPageSetting.CurrentRow.Cells["PageID"].Value.ToString());
                        dbaPage.ACTION = 2;
                        dbaPage.SaveData();
                        MessageBox.Show("Successfully Delete");
                        ShowData();
                    }


                }
            }
        }
    }
}
