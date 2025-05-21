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
        public frm_CreateTeam frmCreateTeam;

        public CtrlFrmCreateTeam(frm_CreateTeam teamForm) 
        { 
            frmCreateTeam = teamForm;
        }

        DbaTeam dbaTeam = new DbaTeam();
        DbaConnection dbaConnection = new DbaConnection();

        DataTable dt = new DataTable();
        public int _TeamID = 0;
        public bool _IsEdit = false;
        private int _TotalPlayer = 0;
        //public string _EditDate = "";

        string spString = "";

        public void SaveClick()
        {
            _TeamID = frmCreateTeam.TeamID;
            _IsEdit = frmCreateTeam.IsEdit;
            _TotalPlayer = frmCreateTeam.TotalPlayer;
            int Ok = 0;
            if (frmCreateTeam.txtTeamName.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type TeamName");
                frmCreateTeam.txtTeamName.Focus();
            }
            else if (frmCreateTeam.txtPhone.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Phone");
                frmCreateTeam.txtPhone.Focus();
            }
            else if (frmCreateTeam.txtMaxPlayer.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type MaxPlayer");
                frmCreateTeam.txtMaxPlayer.Focus();
            }
            else if (int.TryParse(frmCreateTeam.txtMaxPlayer.Text, out Ok) == false)
            {
                MessageBox.Show("MaxPlayer Should Be Number");
            }
            else if (Convert.ToInt32(frmCreateTeam.txtMaxPlayer.Text) < _TotalPlayer)
            {
                MessageBox.Show("MaxPlayer Should Not Be Lower Than Total Player.");
            }
            else if (Convert.ToInt32(frmCreateTeam.txtMaxPlayer.Text.Trim().ToString()) < 1 ||
                Convert.ToInt32(frmCreateTeam.txtMaxPlayer.Text.Trim().ToString()) > 10000)
            {
                MessageBox.Show("MaxPlayer Should Be Between 1 And 1000");
            }
            else
            {
                spString = string.Format("SP_Select_Team N'{0}',N'{1}',N'{2}'", frmCreateTeam.txtTeamName.Text.Trim().ToString(), "0", "1");
                dt = dbaConnection.SelectData(spString);
                if (dt.Rows.Count > 0 && _TeamID != Convert.ToInt32(dt.Rows[0]["TeamID"]))
                {
                    MessageBox.Show("This Team is Already Exist");
                    frmCreateTeam.txtTeamName.Focus();
                    frmCreateTeam.txtTeamName.SelectAll();
                }
                else
                {
                    dbaTeam.TID = _TeamID;
                    dbaTeam.TNAME = frmCreateTeam.txtTeamName.Text;
                    dbaTeam.PHONE = frmCreateTeam.txtPhone.Text;
                    dbaTeam.TOTALPLAYER = _TotalPlayer;

                    //dbaTeam.OPENDATE = dtpDate.Text;

                    dbaTeam.MAXPLAYER = Convert.ToInt32(frmCreateTeam.txtMaxPlayer.Text);
                    //dbaTeam.TOTALPLAYER = 0;

                    if (_IsEdit)
                    {
                        //dbaTeam.OPENDATE = _EditDate;
                        dbaTeam.ACTION = 1;
                        dbaTeam.SaveData();
                        MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                        frmCreateTeam.Close();
                    }
                    else
                    {
                        //dbaTeam.OPENDATE = dtpDate.Text;
                        dbaTeam.ACTION = 0;
                        dbaTeam.SaveData();
                        MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                        frmCreateTeam.Close();
                    }
                }
            }
        }
    }
}
