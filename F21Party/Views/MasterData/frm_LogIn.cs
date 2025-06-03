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

namespace F21Party.Views
{
    public partial class frm_LogIn : Form
    {
        private readonly CtrlFrmLogIn _ctrlFrmLogIn;

        public frm_LogIn()
        {
            InitializeComponent();
            _ctrlFrmLogIn = new CtrlFrmLogIn(this);
        }

        private void btnEye_Click(object sender, EventArgs e)
        {
            _ctrlFrmLogIn.EyeToggle();
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
