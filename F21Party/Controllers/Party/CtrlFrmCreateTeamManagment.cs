using F21Party.DBA;
using F21Party.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;


namespace F21Party.Controllers
{

    internal class CtrlFrmCreateTeamManagment
    {
        private readonly frm_CreateTeamManagment _frmCreateTeamManagment;
        private bool _full = false;
        private readonly DbaTeamManagment _dbaTeamManagment = new DbaTeamManagment();
        private readonly DbaTeam _dbaTeam = new DbaTeam();
        private readonly DbaConnection _dbaConnection = new DbaConnection();
        private DataTable _dt = new DataTable();
        private bool _isEdit = false;
        private int _teamManagmentID = 0;
        private string _teamDisplay = "";
        private int _teamindex;
        private string _spString = "";

        public CtrlFrmCreateTeamManagment(frm_CreateTeamManagment teamManagmentForm)
        {
            _frmCreateTeamManagment = teamManagmentForm;
        }

        public void AddCombo(ComboBox cboCombo, string spString, string display, string value)
        {
            DataTable dtAccessSp = new DataTable();
            DataTable dtCombo = new DataTable();
            DataRow dr;


            dtCombo.Columns.Add(display);
            dtCombo.Columns.Add(value);

            dr = dtCombo.NewRow();
            dr[display] = "---Select---";
            dr[value] = 0;
            dtCombo.Rows.Add(dr);

            try
            {
                _dbaConnection.DataBaseConn();
                SqlDataAdapter adpt = new SqlDataAdapter(spString, _dbaConnection.con);
                adpt.Fill(dtAccessSp);
                for (int i = 0; i < dtAccessSp.Rows.Count; i++)
                {
                    dr = dtCombo.NewRow();

                    if (display == "FullName")
                    {
                        if (Convert.ToInt32(dtAccessSp.Rows[i][value]) == 1) // 1 is SuperAdmin UserID.
                        {
                            continue;
                        }
                    }

                    dr[display] = dtAccessSp.Rows[i][display];
                    dr[value] = dtAccessSp.Rows[i][value];
                    dtCombo.Rows.Add(dr);
                }
                cboCombo.DisplayMember = display;
                cboCombo.ValueMember = value;
                cboCombo.DataSource = dtCombo;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error In AddCombo");
            }

            finally
            {
                _dbaConnection.con.Close();
            }
        }
        public void Indexloop()
        {
            if (_teamindex == 0)
            {
                AddCombo(_frmCreateTeamManagment.cboTotal, _spString, "TotalPlayer", "TeamID");
                _frmCreateTeamManagment.cboTotal.SelectedIndex = 0;
                AddCombo(_frmCreateTeamManagment.cboMax, _spString, "MaxPlayer", "TeamID");
                _frmCreateTeamManagment.cboMax.SelectedIndex = 0;
            }

            for (int i = 1; i < _frmCreateTeamManagment.cboTeam.Items.Count; i++)
            {

                if (_teamindex == i)
                {
                    AddCombo(_frmCreateTeamManagment.cboTotal, _spString, "TotalPlayer", "TeamID");
                    _frmCreateTeamManagment.cboTotal.SelectedIndex = i;
                    AddCombo(_frmCreateTeamManagment.cboMax, _spString, "MaxPlayer", "TeamID");
                    _frmCreateTeamManagment.cboMax.SelectedIndex = i;
                    if (Convert.ToInt32(_frmCreateTeamManagment.cboTotal.Text) == Convert.ToInt32(_frmCreateTeamManagment.cboMax.Text) && _teamDisplay != _frmCreateTeamManagment.cboTeam.SelectedValue.ToString())
                    {
                        MessageBox.Show(_frmCreateTeamManagment.cboTeam.Text + " Is Full. Please Choose Other Team");
                        _full = true;
                    }
                    else
                    {
                        _full = false;
                    }

                }
            }
        }

        public void TeamManagmentLoad()
        {
            string usersDisplay = _frmCreateTeamManagment.cboFullNames.DisplayMember;
            if (usersDisplay == string.Empty)
                usersDisplay = "0";

            _spString = string.Format("SP_Select_TeamManagment N'{0}',N'{1}',N'{2}'", "0", "0", "6");
            AddCombo(_frmCreateTeamManagment.cboFullNames, _spString, "FullName", "UserID");

            _frmCreateTeamManagment.cboFullNames.SelectedValue = Convert.ToInt32(usersDisplay);


            _teamDisplay = _frmCreateTeamManagment.cboTeam.DisplayMember;
            if (_teamDisplay == string.Empty)
                _teamDisplay = "0";

            _spString = string.Format("SP_Select_TeamManagment N'{0}',N'{1}',N'{2}'", "0", "0", "5");
            AddCombo(_frmCreateTeamManagment.cboTeam, _spString, "TeamName", "TeamID");

            _frmCreateTeamManagment.cboTeam.SelectedValue = Convert.ToInt32(_teamDisplay);
            _teamindex = _frmCreateTeamManagment.cboTeam.SelectedIndex;

            AddCombo(_frmCreateTeamManagment.cboTotal, _spString, "TotalPlayer", "TeamID");

            AddCombo(_frmCreateTeamManagment.cboMax, _spString, "MaxPlayer", "TeamID");
            Indexloop();
        }

        public void SaveClick()
        {
            _teamManagmentID = _frmCreateTeamManagment.TeamManagmentID;
            _isEdit = _frmCreateTeamManagment.IsEdit;
            if (_frmCreateTeamManagment.cboFullNames.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose Player Name.");
                _frmCreateTeamManagment.cboFullNames.Focus();
            }
            else if (_frmCreateTeamManagment.cboTeam.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose Team");
                _frmCreateTeamManagment.cboTeam.Focus();
            }
            else if (_full && _teamDisplay != _frmCreateTeamManagment.cboTeam.SelectedValue.ToString())
            {
                MessageBox.Show(_frmCreateTeamManagment.cboTeam.Text + " Is Full. Please Choose Other Team");

            }
            else
            {
                _spString = string.Format("SP_Select_TeamManagment N'{0}',N'{1}',N'{2}'", _frmCreateTeamManagment.cboFullNames.SelectedValue.ToString(), "0", "4");
                _dt = _dbaConnection.SelectData(_spString);
                if (_dt.Rows.Count > 0 && _teamManagmentID != Convert.ToInt32(_dt.Rows[0]["TeamManagmentID"]))
                {
                    MessageBox.Show("This Player Already Have Team!");
                    //frmCreateTeamManagment.txtPlayerName.Focus();
                    _frmCreateTeamManagment.cboFullNames.SelectAll();
                }
                else
                {
                    _dbaTeamManagment.TMID = Convert.ToInt32(_teamManagmentID);
                    _dbaTeamManagment.UID = Convert.ToInt32(_frmCreateTeamManagment.cboFullNames.SelectedValue.ToString());
                    _dbaTeamManagment.TID = Convert.ToInt32(_frmCreateTeamManagment.cboTeam.SelectedValue.ToString());

                    if (_teamDisplay != _frmCreateTeamManagment.cboTeam.SelectedValue.ToString())
                    {
                        _dbaTeam.TID = Convert.ToInt32(_frmCreateTeamManagment.cboTeam.SelectedValue.ToString());
                        _dbaTeam.TOTALPLAYER = 1;
                        _dbaTeam.ACTION = 3;
                        _dbaTeam.SaveData();
                    }

                    if (_isEdit)
                    {
                        if (_teamDisplay != _frmCreateTeamManagment.cboTeam.SelectedValue.ToString())
                        {
                            _dbaTeam.TID = Convert.ToInt32(_teamDisplay);
                            _dbaTeam.TOTALPLAYER = 1;
                            _dbaTeam.ACTION = 4;
                            _dbaTeam.SaveData();
                        }
                        _dbaTeamManagment.ACTION = 1;
                        _dbaTeamManagment.SaveData();
                        MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                        _frmCreateTeamManagment.Close();
                    }
                    else
                    {
                        _dbaTeamManagment.ACTION = 0;
                        _dbaTeamManagment.SaveData();
                        MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                        _frmCreateTeamManagment.Close();
                    }
                }
            }
        }

        public void CboSelectIndexChanged()
        {
            _teamindex = _frmCreateTeamManagment.cboTeam.SelectedIndex;
            Indexloop();
        }


    }
}
