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
        private readonly CtrlFrmCreateAccess _ctrlFrmCreateAccess; // Declare the View
        public int AccessID = 0;
        public int Authority = 0;
        //public bool _IsEdit = false;
        public frm_CreateAccess()
        {
            InitializeComponent();
            _ctrlFrmCreateAccess = new CtrlFrmCreateAccess(this);
        }

        private void frm_CreateAccess_Load(object sender, EventArgs e)
        {
            _ctrlFrmCreateAccess.ShowCombo(this);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            _ctrlFrmCreateAccess.CreateClick();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
