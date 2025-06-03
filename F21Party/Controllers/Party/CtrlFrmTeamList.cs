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
        private readonly frm_TeamList _frmTeamList;
        private readonly DbaConnection _dbaConnection = new DbaConnection();
        private string _spString = "";

        public CtrlFrmTeamList(frm_TeamList teamListForm)
        {
            _frmTeamList = teamListForm;
        }

        public void ShowData()
        {
            _spString = string.Format("SP_Select_Team N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            _frmTeamList.dgvTeam.DataSource = _dbaConnection.SelectData(_spString);

            _frmTeamList.dgvTeam.Columns[0].Width = (_frmTeamList.dgvTeam.Width / 100) * 10;
            _frmTeamList.dgvTeam.Columns[1].Visible = false;
            _frmTeamList.dgvTeam.Columns[2].Width = (_frmTeamList.dgvTeam.Width / 100) * 40;
            _frmTeamList.dgvTeam.Columns[3].Width = (_frmTeamList.dgvTeam.Width / 100) * 15;
            _frmTeamList.dgvTeam.Columns[4].Width = (_frmTeamList.dgvTeam.Width / 100) * 25;
            _frmTeamList.dgvTeam.Columns[5].Width = (_frmTeamList.dgvTeam.Width / 100) * 10;
            _frmTeamList.dgvTeam.Columns[5].ReadOnly = true;


            _dbaConnection.ToolStripTextBoxData(_frmTeamList.tstSearchWith, _spString, "TeamName");
            _frmTeamList.tslLabel.Text = "TeamName";

            if (!Program.PublicArrWriteAccessPages.Contains("Team"))
            {
                _frmTeamList.tsbNew.ForeColor = System.Drawing.SystemColors.GrayText;
                _frmTeamList.tsbEdit.ForeColor = System.Drawing.SystemColors.GrayText;
                _frmTeamList.tsbDelete.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }

        public void ShowEntry()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("Team"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            if (_frmTeamList.dgvTeam.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There is No Data");
            }
            else
            {
                frm_CreateTeam frm = new frm_CreateTeam();
                frm.TeamID = Convert.ToInt32(_frmTeamList.dgvTeam.CurrentRow.Cells["TeamID"].Value.ToString());
                frm.txtTeamName.Text = _frmTeamList.dgvTeam.CurrentRow.Cells["TeamName"].Value.ToString();
                frm.txtPhone.Text = _frmTeamList.dgvTeam.CurrentRow.Cells["Phone"].Value.ToString();
                frm.txtMaxPlayer.Text = _frmTeamList.dgvTeam.CurrentRow.Cells["MaxPlayer"].Value.ToString();
                frm.TotalPlayer = Convert.ToInt32(_frmTeamList.dgvTeam.CurrentRow.Cells["TotalPlayer"].Value.ToString());
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

            string teamID = _frmTeamList.dgvTeam.CurrentRow.Cells["TeamID"].Value.ToString();
            DbaTeam dbaTeam = new DbaTeam();

            if (teamID == string.Empty)
            {
                MessageBox.Show("There is No Data");
            }
            else if (_frmTeamList.dgvTeam.CurrentRow.Cells["TotalPlayer"].Value.ToString() != "0")
            {
                MessageBox.Show("This Team Has Player. Cannot Be Deleted. " +
                    "Remove Player From The Team First Before Deletion");
            }
            else 
            {
                _spString = string.Format("SP_Select_Team N'{0}', N'{1}', N'{2}'", Convert.ToInt32(_frmTeamList.dgvTeam.CurrentRow.Cells["TeamID"].Value.ToString()), "0", "8");
                DataTable dt = new DataTable();
                dt = _dbaConnection.SelectData(_spString);

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
            if (_frmTeamList.tslLabel.Text == "TeamName")
            {
                _spString = string.Format("SP_Select_Team N'{0}',N'{1}',N'{2}'", _frmTeamList.tstSearchWith.Text.Trim().ToString(), "0", "2");
            }
            else if (_frmTeamList.tslLabel.Text == "Phone")
            {
                _spString = string.Format("SP_Select_Team N'{0}',N'{1}',N'{2}'", _frmTeamList.tstSearchWith.Text.Trim().ToString(), "0", "3");
            }
            else if (_frmTeamList.tslLabel.Text == "TotalPlayer")
            {
                _spString = string.Format("SP_Select_Team N'{0}',N'{1}',N'{2}'", _frmTeamList.tstSearchWith.Text.Trim().ToString(), "0", "4");
            }

            _frmTeamList.dgvTeam.DataSource = _dbaConnection.SelectData(_spString);
        }

        public void TsmSearchLabelClick(string textLabel)
        {
            _frmTeamList.tslLabel.Text = textLabel;
            _spString = string.Format("SP_Select_Team N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            _dbaConnection.ToolStripTextBoxData(_frmTeamList.tstSearchWith, _spString, textLabel);
        }

    }
}
