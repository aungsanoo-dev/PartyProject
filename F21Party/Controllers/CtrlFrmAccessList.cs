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
    internal class CtrlFrmAccessList
    {
        public frm_AccessList frmAccessList; // Declare the View
        public CtrlFrmAccessList(frm_AccessList accessForm)
        {
            frmAccessList = accessForm; // Create the View
        }
        string spString = "";
        DbaConnection dbaConnection = new DbaConnection();

        public void ShowData()
        {
            spString = string.Format("SP_Select_Access N'{0}', N'{1}', N'{2}'", "0", "0", "0");
            frmAccessList.dgvAccessSetting.DataSource = dbaConnection.SelectData(spString);
            frmAccessList.dgvAccessSetting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            frmAccessList.dgvAccessSetting.Columns[0].FillWeight = 25;
            frmAccessList.dgvAccessSetting.Columns[1].Visible = false;
            frmAccessList.dgvAccessSetting.Columns[2].FillWeight = 25;
            frmAccessList.dgvAccessSetting.Columns[3].FillWeight = 25;
            frmAccessList.dgvAccessSetting.Columns[4].FillWeight = 25;

            dbaConnection.ToolStripTextBoxData(frmAccessList.tstSearchWith, spString, "AccessLevel");
        }

        public void ShowEntry()
        {
     
            if (frmAccessList.dgvAccessSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There is No Data");
            }
            // Not Allowed to change SuperAdmin
            else if (Convert.ToInt32(frmAccessList.dgvAccessSetting.CurrentRow.Cells["Authority"].Value) == 1 && Program.UserAuthority != 1) // AccessID 1 is SuperAdmin
            {
                MessageBox.Show("You cannont change SuperAdmin account!");
            }
            else
            {
                frm_CreateAccessAuthority frmCreateAccessAuthority = new frm_CreateAccessAuthority();

                frmCreateAccessAuthority._AccessID = Convert.ToInt32(frmAccessList.dgvAccessSetting.CurrentRow.Cells["AccessID"].Value);
                frmCreateAccessAuthority.txtAccessLevel.Text = frmAccessList.dgvAccessSetting.CurrentRow.Cells["AccessLevel"].Value.ToString();
                frmCreateAccessAuthority.cboLogInAccess.DisplayMember = frmAccessList.dgvAccessSetting.CurrentRow.Cells["LogInAccess"].Value.ToString();
                frmCreateAccessAuthority.txtAuthority.Text = frmAccessList.dgvAccessSetting.CurrentRow.Cells["Authority"].Value.ToString();



                frmCreateAccessAuthority._IsEdit = true;
                frmCreateAccessAuthority.btnCreate.Text = "Save";
                frmCreateAccessAuthority.ShowDialog();
                ShowData();
            }
        }

        public void TsbNew()
        {
            frm_CreateAccess frmCreateAccess = new frm_CreateAccess();
            frmCreateAccess.ShowDialog();
            ShowData();
        }

        public void TsbSearch()
        {
            spString = string.Format("SP_Select_Access N'{0}', N'{1}', N'{2}'", frmAccessList.tstSearchWith.Text.Trim().ToString(), "0", "4");
            frmAccessList.dgvAccessSetting.DataSource = dbaConnection.SelectData(spString);
        }
        public void TsbDelete()
        {
            DbaAccessSetting dbaAccessSetting = new DbaAccessSetting();
            if (frmAccessList.dgvAccessSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else
            {
                if (MessageBox.Show("Are You Sure You Want To Delete?", "Confirm",
                 MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    spString = string.Format("SP_Select_Access N'{0}', N'{1}', N'{2}'", Convert.ToInt32(frmAccessList.dgvAccessSetting.CurrentRow.Cells["AccessID"].Value), "0", "3");
                    DataTable DT = new DataTable();
                    DT = dbaConnection.SelectData(spString);

                    //int selectedRowIndex = frmAccessList.dgvAccessSetting.CurrentRow.Index;
                    //int lastDataRowIndex = frmAccessList.dgvAccessSetting.Rows.Count - 2; // exclude empty new row

                    //if (selectedRowIndex != lastDataRowIndex)
                    //{
                    //    MessageBox.Show("You can only delete the last row!");
                    //    return;
                    //}
                    //else 
                    if (DT.Rows.Count > 0)
                    {
                        MessageBox.Show("You cannont delete the Access which is currently used by the User's Account!");
                    }
                    else if(Convert.ToInt32(frmAccessList.dgvAccessSetting.CurrentRow.Cells["Authority"].Value) == 1)
                    {
                        MessageBox.Show("You cannot delete SuperAdmin!");
                    }
                    else
                    {
                        dbaAccessSetting.AID = Convert.ToInt32(frmAccessList.dgvAccessSetting.CurrentRow.Cells["AccessID"].Value.ToString());
                        dbaAccessSetting.ACTION = 2;
                        dbaAccessSetting.SaveData();
                        MessageBox.Show("Successfully Delete");
                        ShowData();
                    }


                }
            }
        }
    }
}
