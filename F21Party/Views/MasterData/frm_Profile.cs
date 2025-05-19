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
    public partial class frm_Profile : Form
    {
        CtrlFrmProfile ctrlFrmProfile;
        private frm_Main mainForm;
        public int _AccountID = 0;
        public int _UserID = 0;
        public bool _IsEdit = false;
        public bool IsLogout;
        public frm_Profile()
        {
            InitializeComponent();
            ctrlFrmProfile = new CtrlFrmProfile(this);
            IsLogout = false;
        }

        public frm_Profile(frm_Main main)
            : this() // Calls the default constructor
        {
            mainForm = main; // Store reference for later

        }
        private void frm_Profile_Load(object sender, EventArgs e)
        {
            ctrlFrmProfile.ShowData();
            ctrlFrmProfile.ShowCombo(_IsEdit);
            //ctrlFrmProfile.EyeToggle();
            
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if(btnCreate.Text == "Save")
            {
                ctrlFrmProfile.SaveClick();
                if (IsLogout)
                {
                    mainForm.RefreshMenu();
                    IsLogout = false;
                    this.Close();
                }
            }
            else
            {
                ctrlFrmProfile.EditClick();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEye_Click(object sender, EventArgs e)
        {
            ctrlFrmProfile.EyeToggle();
        }
    }
}
