using F21Party.DBA;
using F21Party.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F21Party.Controllers
{
    internal class CtrlFrmAccessList
    {
        public frm_AccessList frmAccessList; // Declare the View
        public CtrlFrmAccessList(frm_AccessList userForm)
        {
            frmAccessList = userForm; // Create the View
        }
        string spString = "";
        DbaConnection dbaConnection = new DbaConnection();

        public void ShowData()
        {
            spString = string.Format("SP_Select_Access N'{0}', N'{1}', N'{2}'", "0", "0", "0");
            frmAccessList.dgvAccessSetting.DataSource = dbaConnection.SelectData(spString);
            frmAccessList.dgvAccessSetting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            frmAccessList.dgvAccessSetting.Columns[0].FillWeight = 25;
            frmAccessList.dgvAccessSetting.Columns[1].FillWeight = 25;
            frmAccessList.dgvAccessSetting.Columns[2].FillWeight = 25;
            frmAccessList.dgvAccessSetting.Columns[3].FillWeight = 25;

            dbaConnection.ToolStripTextBoxData(frmAccessList.tstSearchWith, spString, "AccessLevel");
        }

        public void ShowEntry()
        {
            if (frmAccessList.dgvAccessSetting.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There is No Data");
            }
            else
            {
                frm_CreateAccess frmCreateAccess = new frm_CreateAccess();

                frmCreateAccess._AccessID = Convert.ToInt32(frmAccessList.dgvAccessSetting.CurrentRow.Cells["AccessID"].Value);
                frmCreateAccess.txtAccessLevel.Text = frmAccessList.dgvAccessSetting.CurrentRow.Cells["AccessLevel"].Value.ToString();
                frmCreateAccess.cboLogInAccess.DisplayMember = frmAccessList.dgvAccessSetting.CurrentRow.Cells["LogInAccess"].Value.ToString();

                frmCreateAccess._IsEdit = true;
                frmCreateAccess.btnCreate.Text = "Save";
                frmCreateAccess.ShowDialog();
                ShowData();
            }
        }

        public void TsbNew()
        {
            frm_CreateAccess frmCreateAccess = new frm_CreateAccess();
            frmCreateAccess.ShowDialog();
            ShowData();
        }
    }
}
