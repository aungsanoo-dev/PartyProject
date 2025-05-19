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
using F21Party.Donation;



namespace F21Party.Views
{
    public partial class frm_Main : Form
    {
        CtrlFrmMain ctrlFrmMain; // Declare the controller
        public frm_Main()
        {
            InitializeComponent();
            ctrlFrmMain = new CtrlFrmMain(this); // Create the controller and pass itself to ctrlFrmMain()
            //ctrlFrmPermissionList = new CtrlFrmPermissionList(this);
        }
        public void RefreshMenu()
        {
            mnuLogIn.Text = "LogIn";
            btnLogIn.Text = "LogIn";
            Program.UserID = 0;
            Program.UserAccessID = 0;
            Program.UserAccessLevel = "";
            Program.UserAuthority = 0;
            Program.PublicArrWriteAccessPages = Array.Empty<string>();
            Program.PublicArrReadAccessPages = Array.Empty<string>();
            ctrlFrmMain.ShowMenu("");

        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            ctrlFrmMain.ShowMenu("");
            this.ActiveControl = null;
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

        private void mnuAccess_Click(object sender, EventArgs e)
        {
            frm_AccessList frm = new frm_AccessList();
            frm.ShowDialog();
        }

        private void mnuPermission_Click(object sender, EventArgs e)
        {
            frm_PermissionList frm = new frm_PermissionList(this); 
            frm.ShowDialog();
            
        }

        private void mnuPage_Click(object sender, EventArgs e)
        {
            frm_PageList frm = new frm_PageList();
            frm.ShowDialog();
        }

        private void mnuPermissionType_Click(object sender, EventArgs e)
        {
            frm_PermissionTypeList frm = new frm_PermissionTypeList();
            frm.ShowDialog();
        }

        private void mnuPosition_Click(object sender, EventArgs e)
        {
            frm_PositionList frm = new frm_PositionList();
            frm.ShowDialog();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            ctrlFrmMain.LoginAccount();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            frm_CreateAccount frm = new frm_CreateAccount();
            frm.ShowDialog();
        }

        private void munProfile_Click(object sender, EventArgs e)
        {
            if (Program.UserID == 0)
            {
                ctrlFrmMain.LoginAccount();
            }
            else
            {
                frm_Profile frm = new frm_Profile(this);
                frm.ShowDialog();
            }
        }

        private void mnuPartyItem_Click(object sender, EventArgs e)
        {
            frm_PartyItemList frm = new frm_PartyItemList();
            frm.ShowDialog();
        }

        private void mnuDonation_Click(object sender, EventArgs e)
        {
            frm_DonationList frm = new frm_DonationList();
            frm.ShowDialog();
        }

        private void mnuItemRequests_Click(object sender, EventArgs e)
        {
            frm_ItemRequestsList frm = new frm_ItemRequestsList();
            frm.ShowDialog();
        }
    }
}
