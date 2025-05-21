using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using F21Party.Controllers;
using F21Party.DBA;


namespace F21Party.Views
{
    public partial class frm_TeamManagmentList : Form
    {
        private CtrlTeamManagmentList ctrlTeamManagmentList;

        public frm_TeamManagmentList()
        {
            InitializeComponent();
            ctrlTeamManagmentList = new CtrlTeamManagmentList(this);
        }

        private void frm_TeamManagmentList_Load(object sender, EventArgs e)
        {
            ctrlTeamManagmentList.ShowData();
        }

        private void dgvTeamManagment_DoubleClick(object sender, EventArgs e)
        {
            ctrlTeamManagmentList.ShowEntry();
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            ctrlTeamManagmentList.TsbNew();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            ctrlTeamManagmentList.ShowEntry();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            ctrlTeamManagmentList.TsbDelete();
        }


        private void tstSearchWith_TextChanged(object sender, EventArgs e)
        {
            ctrlTeamManagmentList.TsmSearch();
        }


        private void tsmPlayerName_Click(object sender, EventArgs e)
        {
            ctrlTeamManagmentList.TsmSearchLabelClick("PlayerName");
        }

        private void tsmTeam_Click(object sender, EventArgs e)
        {
            ctrlTeamManagmentList.TsmSearchLabelClick("TeamName");
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
