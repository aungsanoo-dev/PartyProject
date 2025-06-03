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
        private readonly CtrlFrmProfile _ctrlFrmProfile;
        private readonly frm_Main _mainForm;
        public int AccountID = 0;
        public int UserID = 0;
        public bool IsEdit = false;
        public bool IsLogout;
        public frm_Profile()
        {
            InitializeComponent();
            _ctrlFrmProfile = new CtrlFrmProfile(this);
            IsLogout = false;
        }

        public frm_Profile(frm_Main main)
            : this() // Calls the default constructor
        {
            _mainForm = main; // Store reference for later

        }
        private void frm_Profile_Load(object sender, EventArgs e)
        {
            _ctrlFrmProfile.ShowData();
            _ctrlFrmProfile.ShowCombo();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if(btnCreate.Text == "Save")
            {
                _ctrlFrmProfile.SaveClick();
                if (IsLogout)
                {
                    _mainForm.RefreshMenu();
                    IsLogout = false;
                    this.Close();
                }
            }
            else
            {
                _ctrlFrmProfile.EditClick();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEye_Click(object sender, EventArgs e)
        {
            _ctrlFrmProfile.EyeToggle();
        }
    }
}
