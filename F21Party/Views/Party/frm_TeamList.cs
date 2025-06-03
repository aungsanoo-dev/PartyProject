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
    public partial class frm_TeamList : Form
    {
        private readonly CtrlFrmTeamList _ctrlFrmTeamList;
        
        public frm_TeamList()
        {
            InitializeComponent();
            _ctrlFrmTeamList = new CtrlFrmTeamList(this);
        }

        private void frm_TeamList_Load(object sender, EventArgs e)
        {
            _ctrlFrmTeamList.ShowData();
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            _ctrlFrmTeamList.TsbNewClick();
        }

        private void dgvTeam_DoubleClick(object sender, EventArgs e)
        {
            _ctrlFrmTeamList.ShowEntry();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            _ctrlFrmTeamList.ShowEntry();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            _ctrlFrmTeamList.TsbDelete();
        }

        private void tsmName_Click(object sender, EventArgs e)
        {
            _ctrlFrmTeamList.TsmSearchLabelClick("TeamName");
        }

        private void tsmPhone_Click(object sender, EventArgs e)
        {
            _ctrlFrmTeamList.TsmSearchLabelClick("Phone");
        }

        private void tsmTotal_Click(object sender, EventArgs e)
        {
            _ctrlFrmTeamList.TsmSearchLabelClick("TotalPlayer");
        }

        private void tstSearchWith_TextChanged(object sender, EventArgs e)
        {
            _ctrlFrmTeamList.TsmSearch();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();  
        }
    }
}
