using F21Party.DBA;
using F21Party.Views;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace F21Party.Controllers
{
    internal class CtrlFrmTeamList
    {
        public frm_TeamList frmTeamList;
        public CtrlFrmTeamList(frm_TeamList teamListForm)
        {
            frmTeamList = teamListForm;
        }

        DbaConnection dbaConnection = new DbaConnection();
        string spString = "";

        public void ShowData()
        {
            spString = string.Format("SP_Select_Team N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            frmTeamList.dgvTeam.DataSource = dbaConnection.SelectData(spString);

            frmTeamList.dgvTeam.Columns[0].Width = (frmTeamList.dgvTeam.Width / 100) * 10;
            frmTeamList.dgvTeam.Columns[1].Visible = false;
            frmTeamList.dgvTeam.Columns[2].Width = (frmTeamList.dgvTeam.Width / 100) * 40;
            frmTeamList.dgvTeam.Columns[3].Width = (frmTeamList.dgvTeam.Width / 100) * 15;
            frmTeamList.dgvTeam.Columns[4].Width = (frmTeamList.dgvTeam.Width / 100) * 25;
            //frmTeamList.dgvTeam.Columns[5].Width = (frmTeamList.dgvTeam.Width / 100) * 10;
            frmTeamList.dgvTeam.Columns[5].Width = (frmTeamList.dgvTeam.Width / 100) * 10;
            frmTeamList.dgvTeam.Columns[5].ReadOnly = true;


            dbaConnection.ToolStripTextBoxData(frmTeamList.tstSearchWith, spString, "TeamName");
            frmTeamList.tslLabel.Text = "TeamName";

            if (!Program.PublicArrWriteAccessPages.Contains("Team"))
            {
                frmTeamList.tsbNew.ForeColor = System.Drawing.SystemColors.GrayText;
                frmTeamList.tsbEdit.ForeColor = System.Drawing.SystemColors.GrayText;
                frmTeamList.tsbDelete.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }

        public void ShowEntry()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("Team"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            if (frmTeamList.dgvTeam.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There is No Data");
            }
            else
            {
                frm_CreateTeam frm = new frm_CreateTeam();
                frm.TeamID = Convert.ToInt32(frmTeamList.dgvTeam.CurrentRow.Cells["TeamID"].Value.ToString());
                frm.txtTeamName.Text = frmTeamList.dgvTeam.CurrentRow.Cells["TeamName"].Value.ToString();
                frm.txtPhone.Text = frmTeamList.dgvTeam.CurrentRow.Cells["Phone"].Value.ToString();
                //frm._EditDate = frmTeamList.dgvTeam.CurrentRow.Cells["OpenDate"].Value.ToString();
                //frm.dtpDate.Text = frmTeamList.dgvTeam.CurrentRow.Cells["OpenDate"].Value.ToString();
                frm.txtMaxPlayer.Text = frmTeamList.dgvTeam.CurrentRow.Cells["MaxPlayer"].Value.ToString();
                frm.TotalPlayer = Convert.ToInt32(frmTeamList.dgvTeam.CurrentRow.Cells["TotalPlayer"].Value.ToString());
                frm.IsEdit = true;
                frm.ShowDialog();
                ShowData();
            }
        }

        public void TsbNewClick()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("Team"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            frm_CreateTeam frm = new frm_CreateTeam();
            frm.ShowDialog();
            ShowData();
        }

        public void TsbDelete()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("Team"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            string teamID = frmTeamList.dgvTeam.CurrentRow.Cells["TeamID"].Value.ToString();
            DbaTeam dbaTeam = new DbaTeam();

            if (teamID == string.Empty)
            {
                MessageBox.Show("There is No Data");
            }
            else if (frmTeamList.dgvTeam.CurrentRow.Cells["TotalPlayer"].Value.ToString() != "0")
            {
                MessageBox.Show("This Team Has Player. Cannot Be Delete. " +
                    "Remove Player From Team First Before Deletion");
            }
            else 
            {
                spString = string.Format("SP_Select_Team N'{0}', N'{1}', N'{2}'", Convert.ToInt32(frmTeamList.dgvTeam.CurrentRow.Cells["TeamID"].Value.ToString()), "0", "8");
                DataTable dt = new DataTable();
                dt = dbaConnection.SelectData(spString);

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("You cannont delete the Team which is currently used by the TeamManagment!");
                    return;
                }

                if (MessageBox.Show("Are You Sure You Want To Delete?", "Delete Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dbaTeam.TID = Convert.ToInt32(teamID);
                    dbaTeam.ACTION = 2;
                    dbaTeam.SaveData();
                    MessageBox.Show("Successfully Delete");
                    ShowData();
                }
            }
        }

        public void TsmSearch()
        {
            if (frmTeamList.tslLabel.Text == "TeamName")
            {
                spString = string.Format("SP_Select_Team N'{0}',N'{1}',N'{2}'", frmTeamList.tstSearchWith.Text.Trim().ToString(), "0", "2");
            }
            else if (frmTeamList.tslLabel.Text == "Phone")
            {
                spString = string.Format("SP_Select_Team N'{0}',N'{1}',N'{2}'", frmTeamList.tstSearchWith.Text.Trim().ToString(), "0", "3");
            }
            else if (frmTeamList.tslLabel.Text == "TotalPlayer")
            {
                spString = string.Format("SP_Select_Team N'{0}',N'{1}',N'{2}'", frmTeamList.tstSearchWith.Text.Trim().ToString(), "0", "4");
            }

            frmTeamList.dgvTeam.DataSource = dbaConnection.SelectData(spString);
        }

        public void TsmSearchLabelClick(string textLabel)
        {
            frmTeamList.tslLabel.Text = textLabel;
            spString = string.Format("SP_Select_Team N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            dbaConnection.ToolStripTextBoxData(frmTeamList.tstSearchWith, spString, textLabel);
        }

    }
}
