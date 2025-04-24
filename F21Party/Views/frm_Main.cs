using F21Party.DBA;
using F21Party.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace F21Party.Views
{
    public partial class frm_Main : Form
    {
        CtrlFrmMain ctrlFrmMain; // Declare the controller
        public frm_Main()
        {
            InitializeComponent();
            ctrlFrmMain = new CtrlFrmMain(this); // Create the controller and pass itself to ctrlFrmMain()
        }
        

        private void frmMain_Load(object sender, EventArgs e)
        {
            ctrlFrmMain.ShowMenu("");
        }

        private void mnuLogin_Click(object sender, EventArgs e)
        {
            ctrlFrmMain.LoginAccount();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuAccounts_Click(object sender, EventArgs e)
        {
            frm_AccountList frm = new frm_AccountList();
            frm.ShowDialog();
        }

        private void mnuPartyItem_Click(object sender, EventArgs e)
        {
            
        }

        private void mnuRegister_Click(object sender, EventArgs e)
        {
            frm_CreateAccount frm = new frm_CreateAccount();
            frm.ShowDialog();
        }

        private void mnuEmployee_Click(object sender, EventArgs e)
        {
            frm_UserList frm = new frm_UserList();
            frm.ShowDialog();
        }
    }
}
