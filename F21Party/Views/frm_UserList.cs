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
    public partial class frm_UserList : Form
    {
        CtrlFrmUserList ctrlFrmUserList; // Declare the controller
        
        public frm_UserList()
        {
            InitializeComponent();
            ctrlFrmUserList = new CtrlFrmUserList(this); // Create the controller and pass itself to ctrlFrmMain()
        }


        private void tsbNew_Click(object sender, EventArgs e)
        {
            ctrlFrmUserList.TsbNew();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            ctrlFrmUserList.ShowEntry();
        }

        private void dgvUserSetting_DoubleClick(object sender, EventArgs e)
        {
            ctrlFrmUserList.ShowEntry();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            ctrlFrmUserList.TsbDelete();
        }

        private void tstSearchWith_TextChanged(object sender, EventArgs e)
        {
            ctrlFrmUserList.TsbSearch();
        }

        private void frm_AccountList_Load(object sender, EventArgs e)
        {
            ctrlFrmUserList.ShowData();

        }


        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
