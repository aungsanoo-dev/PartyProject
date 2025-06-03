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
        private readonly frm_TeamManagmentList _frmTeamManagmentList;
        private readonly DbaConnection _dbaConnection = new DbaConnection();
        private readonly DbaTeamManagment _dbaTeamManagment = new DbaTeamManagment();
        private readonly DbaTeam _dbaTeam = new DbaTeam();
        private string _spString = "";

        public CtrlTeamManagmentList(frm_TeamManagmentList teamManagmentListForm)
        {
            _frmTeamManagmentList = teamManagmentListForm;
        }


        public void ShowData()
        {
            _spString = string.Format("SP_Select_TeamManagment N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            _frmTeamManagmentList.dgvTeamManagment.DataSource = _dbaConnection.SelectData(_spString);

            _frmTeamManagmentList.dgvTeamManagment.Columns[0].Width = (_frmTeamManagmentList.dgvTeamManagment.Width / 100) * 20;
            _frmTeamManagmentList.dgvTeamManagment.Columns[1].Visible = false;
            _frmTeamManagmentList.dgvTeamManagment.Columns[2].Visible = false;
            _frmTeamManagmentList.dgvTeamManagment.Columns[3].Width = (_frmTeamManagmentList.dgvTeamManagment.Width / 100) * 40;
            _frmTeamManagmentList.dgvTeamManagment.Columns[4].Visible = false;
            _frmTeamManagmentList.dgvTeamManagment.Columns[5].Width = (_frmTeamManagmentList.dgvTeamManagment.Width / 100) * 40;

            _dbaConnection.ToolStripTextBoxData(_frmTeamManagmentList.tstSearchWith, _spString, "PlayerName");
            _frmTeamManagmentList.tslLabel.Text = "PlayerName";

            if (!Program.PublicArrWriteAccessPages.Contains("TeamManagment"))
            {
                _frmTeamManagmentList.tsbNew.ForeColor = System.Drawing.SystemColors.GrayText;
                _frmTeamManagmentList.tsbEdit.ForeColor = System.Drawing.SystemColors.GrayText;
                _frmTeamManagmentList.tsbDelete.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }

        public void ShowEntry()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("TeamManagment"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            //string AddValue = _frmTeamManagmentList.dgvTeamManagment.CurrentRow.Cells["TeamName"].Value.ToString();
            if (_frmTeamManagmentList.dgvTeamManagment.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There is No Data");
            }
            else
            {
                frm_CreateTeamManagment frm = new frm_CreateTeamManagment();
                frm.TeamManagmentID = Convert.ToInt32(_frmTeamManagmentList.dgvTeamManagment.CurrentRow.Cells["TeamManagmentID"].Value.ToString());
                frm.cboFullNames.DisplayMember = _frmTeamManagmentList.dgvTeamManagment.CurrentRow.Cells["UserID"].Value.ToString();
                frm.cboTeam.DisplayMember = _frmTeamManagmentList.dgvTeamManagment.CurrentRow.Cells["TeamID"].Value.ToString();
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

            string teamManagmentID = _frmTeamManagmentList.dgvTeamManagment.CurrentRow.Cells["TeamManagmentID"].Value.ToString();
            string teamID = _frmTeamManagmentList.dgvTeamManagment.CurrentRow.Cells["TeamID"].Value.ToString();
            if (teamManagmentID == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else
            {
                if (MessageBox.Show("Are You Sure You Want To Delete?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _dbaTeamManagment.TMID = Convert.ToInt32(teamManagmentID);
                    _dbaTeamManagment.ACTION = 2;
                    _dbaTeamManagment.SaveData();

                    _dbaTeam.TID = Convert.ToInt32(teamID);
                    _dbaTeam.TOTALPLAYER = 1;
                    _dbaTeam.ACTION = 4;
                    _dbaTeam.SaveData();
                    MessageBox.Show("Successfully Delete");
                    ShowData();
                }
            }
        }

        public void TsmSearchLabelClick(string textLabel)
        {
            _frmTeamManagmentList.tslLabel.Text = textLabel;
            _spString = string.Format("SP_Select_Team N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            _dbaConnection.ToolStripTextBoxData(_frmTeamManagmentList.tstSearchWith, _spString, textLabel);
        }

        public void TsmSearch()
        {
            if (_frmTeamManagmentList.tslLabel.Text == "PlayerName")
            {
                _spString = string.Format("SP_Select_TeamManagment N'{0}',N'{1}',N'{2}'", _frmTeamManagmentList.tstSearchWith.Text.Trim().ToString(), "0", "2");
            }
            else if (_frmTeamManagmentList.tslLabel.Text == "TeamName")
            {
                _spString = string.Format("SP_Select_TeamManagment N'{0}',N'{1}',N'{2}'", _frmTeamManagmentList.tstSearchWith.Text.Trim().ToString(), "0", "3");
            }
            _frmTeamManagmentList.dgvTeamManagment.DataSource = _dbaConnection.SelectData(_spString);
        }
    }
}
