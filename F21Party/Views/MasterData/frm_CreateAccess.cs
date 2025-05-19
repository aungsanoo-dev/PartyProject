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
    public partial class frm_CreateAccess : Form
    {
        private CtrlFrmCreateAccess ctrlFrmCreateAccess; // Declare the View
        public int _AccessID = 0;
        public int _Authority = 0;
        //public bool _IsEdit = false;
        public frm_CreateAccess()
        {
            InitializeComponent();
            ctrlFrmCreateAccess = new CtrlFrmCreateAccess(this);
        }

        private void frm_CreateAccess_Load(object sender, EventArgs e)
        {
            ctrlFrmCreateAccess.ShowCombo(this);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            ctrlFrmCreateAccess.CreateClick();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
