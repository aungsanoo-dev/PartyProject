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
        CtrlFrmLogIn ctrlFrmLogIn;

        public frm_LogIn()
        {
            InitializeComponent();
            ctrlFrmLogIn = new CtrlFrmLogIn(this);
            
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void frm_LogIn_Load(object sender, EventArgs e)
        {
            ctrlFrmLogIn.EyeToggle();
        }

        private void frm_LogIn_Shown(object sender, EventArgs e)
        {
            
        }
    }
}
