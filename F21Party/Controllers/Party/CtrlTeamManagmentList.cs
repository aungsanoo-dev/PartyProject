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
    internal class CtrlTeamManagmentList
    {
        public frm_TeamManagmentList frmTeamManagmentList;

        public CtrlTeamManagmentList(frm_TeamManagmentList teamManagmentListForm)
        {
            frmTeamManagmentList = teamManagmentListForm;
        }

        DbaConnection dbaConnection = new DbaConnection();
        DbaTeamManagment dbaTeamManagment = new DbaTeamManagment();
        DbaTeam dbaTeam = new DbaTeam();
        string spString = "";

        public void ShowData()
        {
            spString = string.Format("SP_Select_TeamManagment N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            frmTeamManagmentList.dgvTeamManagment.DataSource = dbaConnection.SelectData(spString);

            frmTeamManagmentList.dgvTeamManagment.Columns[0].Width = (frmTeamManagmentList.dgvTeamManagment.Width / 100) * 20;
            frmTeamManagmentList.dgvTeamManagment.Columns[1].Visible = false;
            frmTeamManagmentList.dgvTeamManagment.Columns[2].Visible = false;
            //frmTeamManagmentList.dgvTeamManagment.Columns[3].Visible = false;
            frmTeamManagmentList.dgvTeamManagment.Columns[3].Width = (frmTeamManagmentList.dgvTeamManagment.Width / 100) * 40;
            frmTeamManagmentList.dgvTeamManagment.Columns[4].Visible = false;
            frmTeamManagmentList.dgvTeamManagment.Columns[5].Width = (frmTeamManagmentList.dgvTeamManagment.Width / 100) * 40;

            dbaConnection.ToolStripTextBoxData(frmTeamManagmentList.tstSearchWith, spString, "PlayerName");
            frmTeamManagmentList.tslLabel.Text = "PlayerName";

            if (!Program.PublicArrWriteAccessPages.Contains("TeamManagment"))
            {
                frmTeamManagmentList.tsbNew.ForeColor = System.Drawing.SystemColors.GrayText;
                frmTeamManagmentList.tsbEdit.ForeColor = System.Drawing.SystemColors.GrayText;
                frmTeamManagmentList.tsbDelete.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }

        public void ShowEntry()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("TeamManagment"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            string AddValue = frmTeamManagmentList.dgvTeamManagment.CurrentRow.Cells["TeamName"].Value.ToString();
            if (frmTeamManagmentList.dgvTeamManagment.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There is No Data");
            }
            else
            {
                frm_CreateTeamManagment frm = new frm_CreateTeamManagment();
                frm.TeamManagmentID = Convert.ToInt32(frmTeamManagmentList.dgvTeamManagment.CurrentRow.Cells["TeamManagmentID"].Value.ToString());
                frm.cboFullNames.DisplayMember = frmTeamManagmentList.dgvTeamManagment.CurrentRow.Cells["UserID"].Value.ToString();
                frm.cboTeam.DisplayMember = frmTeamManagmentList.dgvTeamManagment.CurrentRow.Cells["TeamID"].Value.ToString();
                frm.IsEdit = true;
                frm.ShowDialog();
                ShowData();
            }
        }

        public void TsbNew()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("TeamManagment"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            frm_CreateTeamManagment frm = new frm_CreateTeamManagment();
            frm.ShowDialog();
            ShowData();
        }

        public void TsbDelete()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("TeamManagment"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            string TeamManagmentID = frmTeamManagmentList.dgvTeamManagment.CurrentRow.Cells["TeamManagmentID"].Value.ToString();
            string TeamID = frmTeamManagmentList.dgvTeamManagment.CurrentRow.Cells["TeamID"].Value.ToString();
            if (TeamManagmentID == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else
            {
                if (MessageBox.Show("Are You Sure You Want To Delete?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dbaTeamManagment.TMID = Convert.ToInt32(TeamManagmentID);
                    dbaTeamManagment.ACTION = 2;
                    dbaTeamManagment.SaveData();

                    dbaTeam.TID = Convert.ToInt32(TeamID);
                    dbaTeam.TOTALPLAYER = 1;
                    dbaTeam.ACTION = 4;
                    dbaTeam.SaveData();
                    MessageBox.Show("Successfully Delete");
                    ShowData();
                }
            }
        }

        public void TsmSearchLabelClick(string textLabel)
        {
            frmTeamManagmentList.tslLabel.Text = textLabel;
            spString = string.Format("SP_Select_Team N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            dbaConnection.ToolStripTextBoxData(frmTeamManagmentList.tstSearchWith, spString, textLabel);
        }

        public void TsmSearch()
        {
            if (frmTeamManagmentList.tslLabel.Text == "PlayerName")
            {
                spString = string.Format("SP_Select_TeamManagment N'{0}',N'{1}',N'{2}'", frmTeamManagmentList.tstSearchWith.Text.Trim().ToString(), "0", "2");
            }
            else if (frmTeamManagmentList.tslLabel.Text == "TeamName")
            {
                spString = string.Format("SP_Select_TeamManagment N'{0}',N'{1}',N'{2}'", frmTeamManagmentList.tstSearchWith.Text.Trim().ToString(), "0", "3");
            }
            frmTeamManagmentList.dgvTeamManagment.DataSource = dbaConnection.SelectData(spString);
        }
    }
}
