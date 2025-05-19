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
        public frm_PositionList frmPositionList; // Declare the View
        public CtrlFrmPositionList(frm_PositionList positionForm)
        {
            frmPositionList = positionForm; // Create the View
        }
        string spString = "";
        DbaConnection dbaConnection = new DbaConnection();

        public void ShowData()
        {
            spString = string.Format("SP_Select_Position N'{0}', N'{1}', N'{2}'", "0", "0", "0");
            frmPositionList.dgvPositionSetting.DataSource = dbaConnection.SelectData(spString);
            frmPositionList.dgvPositionSetting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            frmPositionList.dgvPositionSetting.Columns[0].FillWeight = 35;
            frmPositionList.dgvPositionSetting.Columns[1].Visible = false;
            frmPositionList.dgvPositionSetting.Columns[2].FillWeight = 65;

            dbaConnection.ToolStripTextBoxData(frmPositionList.tstSearchWith, spString, "PositionName");

            if (!Program.PublicArrWriteAccessPages.Contains("Position"))
            {
                frmPositionList.tsbNew.ForeColor = System.Drawing.SystemColors.GrayText;
                frmPositionList.tsbEdit.ForeColor = System.Drawing.SystemColors.GrayText;
                frmPositionList.tsbDelete.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }
        public void ShowEntry()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("Position"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            if (frmPositionList.dgvPositionSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There is No Data");
            }
            else
            {
                frm_CreatePosition frmCreatePosition = new frm_CreatePosition();

                frmCreatePosition._PositionID = Convert.ToInt32(frmPositionList.dgvPositionSetting.CurrentRow.Cells["PositionID"].Value);
                frmCreatePosition.txtPositionName.Text = frmPositionList.dgvPositionSetting.CurrentRow.Cells["PositionName"].Value.ToString();

                frmCreatePosition._IsEdit = true;
                frmCreatePosition.btnCreate.Text = "Save";
                frmCreatePosition.ShowDialog();
                ShowData();
            }
        }

        public void TsbNew()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("Position"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            frm_CreatePosition frmCreatePosition = new frm_CreatePosition();
            frmCreatePosition.ShowDialog();
            ShowData();
        }

        public void TsbSearch()
        {
            spString = string.Format("SP_Select_Position N'{0}', N'{1}', N'{2}'", frmPositionList.tstSearchWith.Text.Trim().ToString(), "0", "4");
            frmPositionList.dgvPositionSetting.DataSource = dbaConnection.SelectData(spString);
        }
        public void TsbDelete()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("Position"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            DbaPosition dbaPositionSetting = new DbaPosition();
            if (frmPositionList.dgvPositionSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else
            {
                if (MessageBox.Show("Are You Sure You Want To Delete?", "Confirm",
                 MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    spString = string.Format("SP_Select_Position N'{0}', N'{1}', N'{2}'", Convert.ToInt32(frmPositionList.dgvPositionSetting.CurrentRow.Cells["PositionID"].Value), "0", "3");
                    DataTable DT = new DataTable();
                    DT = dbaConnection.SelectData(spString);

                    if (DT.Rows.Count > 0)
                    {
                        MessageBox.Show("You cannont delete the Position which is currently used by the User!");
                    }
                    else
                    {
                        dbaPositionSetting.PID = Convert.ToInt32(frmPositionList.dgvPositionSetting.CurrentRow.Cells["PositionID"].Value.ToString());
                        dbaPositionSetting.ACTION = 2;
                        dbaPositionSetting.SaveData();
                        MessageBox.Show("Successfully Delete");
                        ShowData();
                    }


                }
            }
        }
    }
}
