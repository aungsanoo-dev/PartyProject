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
        private readonly CtrlTeamManagmentList _ctrlTeamManagmentList;

        public frm_TeamManagmentList()
        {
            InitializeComponent();
            _ctrlTeamManagmentList = new CtrlTeamManagmentList(this);
        }

        private void frm_TeamManagmentList_Load(object sender, EventArgs e)
        {
            _ctrlTeamManagmentList.ShowData();
        }

        private void dgvTeamManagment_DoubleClick(object sender, EventArgs e)
        {
            _ctrlTeamManagmentList.ShowEntry();
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            _ctrlTeamManagmentList.TsbNew();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            _ctrlTeamManagmentList.ShowEntry();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            _ctrlTeamManagmentList.TsbDelete();
        }

        private void tstSearchWith_TextChanged(object sender, EventArgs e)
        {
            _ctrlTeamManagmentList.TsmSearch();
        }

        private void tsmPlayerName_Click(object sender, EventArgs e)
        {
            _ctrlTeamManagmentList.TsmSearchLabelClick("PlayerName");
        }

        private void tsmTeam_Click(object sender, EventArgs e)
        {
            _ctrlTeamManagmentList.TsmSearchLabelClick("TeamName");
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
