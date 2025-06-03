using F21Party.DBA;
using F21Party.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace F21Party.Controllers
{
    internal class CtrlFrmCreateTeam
    {
        private readonly frm_CreateTeam _frmCreateTeam;

        public CtrlFrmCreateTeam(frm_CreateTeam teamForm) 
        { 
            _frmCreateTeam = teamForm;
        }

        private readonly DbaTeam _dbaTeam = new DbaTeam();
        private readonly DbaConnection _dbaConnection = new DbaConnection();
        private DataTable _dt = new DataTable();
        private int _teamID = 0;
        private bool _isEdit = false;
        private int _totalPlayer = 0;
        //public string _EditDate = "";

        private string _spString = "";

        public void SaveClick()
        {
            _teamID = _frmCreateTeam.TeamID;
            _isEdit = _frmCreateTeam.IsEdit;
            _totalPlayer = _frmCreateTeam.TotalPlayer;
            int Ok = 0;
            if (_frmCreateTeam.txtTeamName.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type TeamName");
                _frmCreateTeam.txtTeamName.Focus();
            }
            else if (_frmCreateTeam.txtPhone.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Phone");
                _frmCreateTeam.txtPhone.Focus();
            }
            else if (_frmCreateTeam.txtMaxPlayer.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type MaxPlayer");
                _frmCreateTeam.txtMaxPlayer.Focus();
            }
            else if (int.TryParse(_frmCreateTeam.txtMaxPlayer.Text, out Ok) == false)
            {
                MessageBox.Show("MaxPlayer Should Be Number");
            }
            else if (Convert.ToInt32(_frmCreateTeam.txtMaxPlayer.Text) < _totalPlayer)
            {
                MessageBox.Show("MaxPlayer Should Not Be Lower Than Total Player.");
            }
            else if (Convert.ToInt32(_frmCreateTeam.txtMaxPlayer.Text.Trim().ToString()) < 1 ||
                Convert.ToInt32(_frmCreateTeam.txtMaxPlayer.Text.Trim().ToString()) > 10000)
            {
                MessageBox.Show("MaxPlayer Should Be Between 1 And 1000");
            }
            else
            {
                _spString = string.Format("SP_Select_Team N'{0}',N'{1}',N'{2}'", _frmCreateTeam.txtTeamName.Text.Trim().ToString(), "0", "1");
                _dt = _dbaConnection.SelectData(_spString);
                if (_dt.Rows.Count > 0 && _teamID != Convert.ToInt32(_dt.Rows[0]["TeamID"]))
                {
                    MessageBox.Show("This Team is Already Exist");
                    _frmCreateTeam.txtTeamName.Focus();
                    _frmCreateTeam.txtTeamName.SelectAll();
                }
                else
                {
                    _dbaTeam.TID = _teamID;
                    _dbaTeam.TNAME = _frmCreateTeam.txtTeamName.Text;
                    _dbaTeam.PHONE = _frmCreateTeam.txtPhone.Text;
                    _dbaTeam.TOTALPLAYER = _totalPlayer;

                    //dbaTeam.OPENDATE = dtpDate.Text;

                    _dbaTeam.MAXPLAYER = Convert.ToInt32(_frmCreateTeam.txtMaxPlayer.Text);
                    //dbaTeam.TOTALPLAYER = 0;

                    if (_isEdit)
                    {
                        //dbaTeam.OPENDATE = _EditDate;
                        _dbaTeam.ACTION = 1;
                        _dbaTeam.SaveData();
                        MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                        _frmCreateTeam.Close();
                    }
                    else
                    {
                        //dbaTeam.OPENDATE = dtpDate.Text;
                        _dbaTeam.ACTION = 0;
                        _dbaTeam.SaveData();
                        MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                        _frmCreateTeam.Close();
                    }
                }
            }
        }
    }
}
