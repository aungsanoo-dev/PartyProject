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
        public frm_CreateTeamManagment frmCreateTeamManagment;

        public CtrlFrmCreateTeamManagment(frm_CreateTeamManagment teamManagmentForm)
        {
            frmCreateTeamManagment = teamManagmentForm;
        }

        public int i;
        private bool _Full = false;
        DbaTeamManagment dbaTeamManagment = new DbaTeamManagment();
        DbaTeam dbaTeam = new DbaTeam();
        DbaConnection dbaConnection = new DbaConnection();

        DataTable dt = new DataTable();
        //public int _TeamID = 0;
        public bool _IsEdit = false;
       // public int _UserID = 0;
        public int _TeamManagmentID = 0;

        string _TeamDisplay = "";
        public int teamindex;

        string spString = "";


        public void AddCombo(ComboBox cboCombo, string spString, string Display, string Value)
        {
            DataTable DTAC = new DataTable();
            DataTable DTCombo = new DataTable();
            DataRow Dr;


            DTCombo.Columns.Add(Display);
            DTCombo.Columns.Add(Value);

            Dr = DTCombo.NewRow();
            Dr[Display] = "---Select---";
            Dr[Value] = 0;
            DTCombo.Rows.Add(Dr);

            try
            {
                dbaConnection.DataBaseConn();
                SqlDataAdapter Adpt = new SqlDataAdapter(spString, dbaConnection.con);
                Adpt.Fill(DTAC);
                for (int i = 0; i < DTAC.Rows.Count; i++)
                {
                    Dr = DTCombo.NewRow();

                    if (Display == "FullName")
                    {
                        if (Convert.ToInt32(DTAC.Rows[i][Value]) == 1) // 1 is SuperAdmin UserID.
                        {
                            continue;
                        }
                    }

                    Dr[Display] = DTAC.Rows[i][Display];
                    Dr[Value] = DTAC.Rows[i][Value];
                    DTCombo.Rows.Add(Dr);
                }
                cboCombo.DisplayMember = Display;
                cboCombo.ValueMember = Value;
                cboCombo.DataSource = DTCombo;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error In AddCombo");
            }

            finally
            {
                dbaConnection.con.Close();
            }
        }
        public void indexloop()
        {
            if (teamindex == 0)
            {
                AddCombo(frmCreateTeamManagment.cboTotal, spString, "TotalPlayer", "TeamID");
                frmCreateTeamManagment.cboTotal.SelectedIndex = 0;
                AddCombo(frmCreateTeamManagment.cboMax, spString, "MaxPlayer", "TeamID");
                frmCreateTeamManagment.cboMax.SelectedIndex = 0;
            }

            for (i = 1; i < frmCreateTeamManagment.cboTeam.Items.Count; i++)
            {

                if (teamindex == i)
                {
                    AddCombo(frmCreateTeamManagment.cboTotal, spString, "TotalPlayer", "TeamID");
                    frmCreateTeamManagment.cboTotal.SelectedIndex = i;
                    AddCombo(frmCreateTeamManagment.cboMax, spString, "MaxPlayer", "TeamID");
                    frmCreateTeamManagment.cboMax.SelectedIndex = i;
                    if (Convert.ToInt32(frmCreateTeamManagment.cboTotal.Text) == Convert.ToInt32(frmCreateTeamManagment.cboMax.Text) && _TeamDisplay != frmCreateTeamManagment.cboTeam.SelectedValue.ToString())
                    {
                        MessageBox.Show(frmCreateTeamManagment.cboTeam.Text + " Is Full. Please Choose Other Team");
                        _Full = true;
                    }
                    else
                    {
                        _Full = false;
                    }

                }
            }
        }

        public void TeamManagmentLoad()
        {
            string usersDisplay = "";
            usersDisplay = frmCreateTeamManagment.cboFullNames.DisplayMember;
            if (usersDisplay == string.Empty)
                usersDisplay = "0";

            spString = string.Format("SP_Select_TeamManagment N'{0}',N'{1}',N'{2}'", "0", "0", "6");
            AddCombo(frmCreateTeamManagment.cboFullNames, spString, "FullName", "UserID");

            frmCreateTeamManagment.cboFullNames.SelectedValue = Convert.ToInt32(usersDisplay);


            _TeamDisplay = frmCreateTeamManagment.cboTeam.DisplayMember;
            if (_TeamDisplay == string.Empty)
                _TeamDisplay = "0";

            spString = string.Format("SP_Select_TeamManagment N'{0}',N'{1}',N'{2}'", "0", "0", "5");
            AddCombo(frmCreateTeamManagment.cboTeam, spString, "TeamName", "TeamID");

            frmCreateTeamManagment.cboTeam.SelectedValue = Convert.ToInt32(_TeamDisplay);
            teamindex = frmCreateTeamManagment.cboTeam.SelectedIndex;

            AddCombo(frmCreateTeamManagment.cboTotal, spString, "TotalPlayer", "TeamID");

            AddCombo(frmCreateTeamManagment.cboMax, spString, "MaxPlayer", "TeamID");
            indexloop();
        }

        public void SaveClick()
        {
            _TeamManagmentID = frmCreateTeamManagment.TeamManagmentID;
            _IsEdit = frmCreateTeamManagment.IsEdit;
            if (frmCreateTeamManagment.cboFullNames.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose Player Name.");
                frmCreateTeamManagment.cboFullNames.Focus();
            }
            else if (frmCreateTeamManagment.cboTeam.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose Team");
                frmCreateTeamManagment.cboTeam.Focus();
            }
            else if (_Full && _TeamDisplay != frmCreateTeamManagment.cboTeam.SelectedValue.ToString())
            {
                MessageBox.Show(frmCreateTeamManagment.cboTeam.Text + " Is Full. Please Choose Other Team");

            }
            else
            {
                spString = string.Format("SP_Select_TeamManagment N'{0}',N'{1}',N'{2}'", frmCreateTeamManagment.cboFullNames.SelectedValue.ToString(), "0", "4");
                dt = dbaConnection.SelectData(spString);
                if (dt.Rows.Count > 0 && _TeamManagmentID != Convert.ToInt32(dt.Rows[0]["TeamManagmentID"]))
                {
                    MessageBox.Show("This Player Already Have Team!");
                    //frmCreateTeamManagment.txtPlayerName.Focus();
                    frmCreateTeamManagment.cboFullNames.SelectAll();
                }
                else
                {
                    dbaTeamManagment.TMID = Convert.ToInt32(_TeamManagmentID);
                    dbaTeamManagment.UID = Convert.ToInt32(frmCreateTeamManagment.cboFullNames.SelectedValue.ToString());
                    dbaTeamManagment.TID = Convert.ToInt32(frmCreateTeamManagment.cboTeam.SelectedValue.ToString());

                    if (_TeamDisplay != frmCreateTeamManagment.cboTeam.SelectedValue.ToString())
                    {
                        dbaTeam.TID = Convert.ToInt32(frmCreateTeamManagment.cboTeam.SelectedValue.ToString());
                        dbaTeam.TOTALPLAYER = 1;
                        dbaTeam.ACTION = 3;
                        dbaTeam.SaveData();
                    }

                    if (_IsEdit)
                    {
                        if (_TeamDisplay != frmCreateTeamManagment.cboTeam.SelectedValue.ToString())
                        {
                            dbaTeam.TID = Convert.ToInt32(_TeamDisplay);
                            dbaTeam.TOTALPLAYER = 1;
                            dbaTeam.ACTION = 4;
                            dbaTeam.SaveData();
                        }
                        dbaTeamManagment.ACTION = 1;
                        dbaTeamManagment.SaveData();
                        MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                        frmCreateTeamManagment.Close();
                    }
                    else
                    {
                        dbaTeamManagment.ACTION = 0;
                        dbaTeamManagment.SaveData();
                        MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                        frmCreateTeamManagment.Close();
                    }
                }
            }
        }

        public void CboSelectIndexChanged()
        {
            teamindex = frmCreateTeamManagment.cboTeam.SelectedIndex;
            indexloop();
        }


    }
}
