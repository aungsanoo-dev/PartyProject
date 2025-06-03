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
        private readonly CtrlFrmCreateTeamManagment _ctrlFrmCreateTeamManagment;

        public frm_CreateTeamManagment()
        {
            InitializeComponent();
            _ctrlFrmCreateTeamManagment = new CtrlFrmCreateTeamManagment(this);
        }

        public int TeamManagmentID = 0;
        public bool IsEdit = false;
 
        private void frm_CreateTeamManagment_Load(object sender, EventArgs e)
        {
            _ctrlFrmCreateTeamManagment.TeamManagmentLoad();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            _ctrlFrmCreateTeamManagment.SaveClick();
        }

        private void cboTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ctrlFrmCreateTeamManagment.CboSelectIndexChanged();
        }
    }
}
