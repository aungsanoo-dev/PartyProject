using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using F21Party.DBA;
using F21Party.Controllers;

namespace F21Party.Views
{
    public partial class frm_CreateTeamManagment : Form
    {
        private CtrlFrmCreateTeamManagment ctrlFrmCreateTeamManagment;

        public frm_CreateTeamManagment()
        {
            InitializeComponent();
            ctrlFrmCreateTeamManagment = new CtrlFrmCreateTeamManagment(this);
        }

        public int TeamManagmentID = 0;
        public bool IsEdit = false;
        //public int i;
        //public bool _Full = false;
        //clsDweller obj_clsDweller = new clsDweller();
        //clsShelter obj_clsShelter = new clsShelter();
        //clsMainDB obj_clsMainDB = new clsMainDB();

        //DataTable DT = new DataTable();
        //public bool _IsEdit = false;
        //public int _DwellerID = 0;
        //string SPString = "";
        //string _ShelterDisplay = "";
        //public int teamindex;
        //private void indexloop()
        //{
        //    if (shelterindex == 0)
        //    {
        //        obj_clsMainDB.AddCombo(ref cboTotal, SPString, "TotalDweller", "ShelterID");
        //        cboTotal.SelectedIndex = 0;
        //        obj_clsMainDB.AddCombo(ref cboMax, SPString, "MaxDweller", "ShelterID");
        //        cboMax.SelectedIndex = 0;
        //    }

        //    for (i = 1; i < cboTeam.Items.Count; i++)
        //    {

        //        if (shelterindex == i)
        //        {
        //            obj_clsMainDB.AddCombo(ref cboTotal, SPString, "TotalDweller", "ShelterID");
        //            cboTotal.SelectedIndex = i;
        //            obj_clsMainDB.AddCombo(ref cboMax, SPString, "MaxDweller", "ShelterID");
        //            cboMax.SelectedIndex = i;
        //            if (Convert.ToInt32(cboTotal.Text) == Convert.ToInt32(cboMax.Text) && _ShelterDisplay != cboTeam.SelectedValue.ToString())
        //            {
        //                MessageBox.Show(cboTeam.Text + " Is Full. Please Choose Other Shelter");
        //                _Full = true;
        //            }
        //            else
        //            {
        //                _Full = false;
        //            }

        //        }
        //    }
        //}
        private void frm_CreateTeamManagment_Load(object sender, EventArgs e)
        {
            ctrlFrmCreateTeamManagment.TeamManagmentLoad();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            ctrlFrmCreateTeamManagment.SaveClick();
        }



        private void cboTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            ctrlFrmCreateTeamManagment.CboSelectIndexChanged();
        }
    }
}
