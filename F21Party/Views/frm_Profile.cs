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
        public int _AccountID = 0;
        public int _UserID = 0;
        public bool _IsEdit = false;
        public frm_Profile()
        {
            InitializeComponent();
            ctrlFrmProfile = new CtrlFrmProfile(this);
        }

        private void frm_Profile_Load(object sender, EventArgs e)
        {
            ctrlFrmProfile.ShowData();
            ctrlFrmProfile.ShowCombo(_IsEdit);
            ctrlFrmProfile.EyeToggle();
            
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if(btnCreate.Text == "Save")
            {
                ctrlFrmProfile.SaveClick();
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
    }
}
