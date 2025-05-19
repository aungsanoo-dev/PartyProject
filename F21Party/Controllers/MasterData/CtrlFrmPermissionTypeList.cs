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
        public frm_PermissionTypeList frmPermissionTypeList; // Declare the View
        public CtrlFrmPermissionTypeList(frm_PermissionTypeList permissionTypeForm)
        {
            frmPermissionTypeList = permissionTypeForm; // Create the View
        }
        string spString = "";
        DbaConnection dbaConnection = new DbaConnection();

        public void ShowData()
        {
            spString = string.Format("SP_Select_PermissionType N'{0}', N'{1}', N'{2}'", "0", "0", "0");
            frmPermissionTypeList.dgvPermissionTypeSetting.DataSource = dbaConnection.SelectData(spString);
            frmPermissionTypeList.dgvPermissionTypeSetting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            frmPermissionTypeList.dgvPermissionTypeSetting.Columns[0].FillWeight = 35;
            frmPermissionTypeList.dgvPermissionTypeSetting.Columns[1].Visible = false;
            frmPermissionTypeList.dgvPermissionTypeSetting.Columns[2].FillWeight = 65;

            dbaConnection.ToolStripTextBoxData(frmPermissionTypeList.tstSearchWith, spString, "PermissionName");

            if (!Program.PublicArrWriteAccessPages.Contains("PermissionType"))
            {
                frmPermissionTypeList.tsbNew.ForeColor = System.Drawing.SystemColors.GrayText;
                frmPermissionTypeList.tsbEdit.ForeColor = System.Drawing.SystemColors.GrayText;
                frmPermissionTypeList.tsbDelete.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }
        public void ShowEntry()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("PermissionType"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            if (frmPermissionTypeList.dgvPermissionTypeSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There is No Data");
            }
            else
            {
                frm_CreatePermissionType frmCreatePermissionType = new frm_CreatePermissionType();

                frmCreatePermissionType._PermissionTypeID = Convert.ToInt32(frmPermissionTypeList.dgvPermissionTypeSetting.CurrentRow.Cells["PermissionTypeID"].Value);
                frmCreatePermissionType.txtPermissionName.Text = frmPermissionTypeList.dgvPermissionTypeSetting.CurrentRow.Cells["PermissionName"].Value.ToString();

                frmCreatePermissionType._IsEdit = true;
                frmCreatePermissionType.btnCreate.Text = "Save";
                frmCreatePermissionType.ShowDialog();
                ShowData();
            }
        }

        public void TsbNew()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("PermissionType"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            frm_CreatePermissionType frmCreatePermissionType = new frm_CreatePermissionType();
            frmCreatePermissionType.ShowDialog();
            ShowData();
        }

        public void TsbSearch()
        {
            spString = string.Format("SP_Select_PermissionType N'{0}', N'{1}', N'{2}'", frmPermissionTypeList.tstSearchWith.Text.Trim().ToString(), "0", "4");
            frmPermissionTypeList.dgvPermissionTypeSetting.DataSource = dbaConnection.SelectData(spString);
        }
        public void TsbDelete()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("PermissionType"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            DbaPermissionType dbaPermissionTypeSetting = new DbaPermissionType();
            if (frmPermissionTypeList.dgvPermissionTypeSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else
            {
                if (MessageBox.Show("Are You Sure You Want To Delete?", "Confirm",
                 MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    spString = string.Format("SP_Select_PermissionType N'{0}', N'{1}', N'{2}'", Convert.ToInt32(frmPermissionTypeList.dgvPermissionTypeSetting.CurrentRow.Cells["PermissionTypeID"].Value), "0", "3");
                    DataTable DT = new DataTable();
                    DT = dbaConnection.SelectData(spString);

                    if (DT.Rows.Count > 0)
                    {
                        MessageBox.Show("You cannont delete the PermissionType which is currently used by the Permission!");
                    }
                    else
                    {
                        dbaPermissionTypeSetting.PID = Convert.ToInt32(frmPermissionTypeList.dgvPermissionTypeSetting.CurrentRow.Cells["PermissionTypeID"].Value.ToString());
                        dbaPermissionTypeSetting.ACTION = 2;
                        dbaPermissionTypeSetting.SaveData();
                        MessageBox.Show("Successfully Delete");
                        ShowData();
                    }


                }
            }
        }
    }
}
