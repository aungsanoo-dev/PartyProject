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
    public partial class frm_CreateUser : Form
    {
        private readonly CtrlFrmCreateUser _ctrlFrmRegisterUser;
        public int UserID = 0;
        public bool IsEdit = false;
        public frm_CreateUser()
        {
            InitializeComponent();
            _ctrlFrmRegisterUser = new CtrlFrmCreateUser(this);
        }

        private void frm_CreateAccount_Load(object sender, EventArgs e)
        {
            _ctrlFrmRegisterUser.ShowCombo();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            _ctrlFrmRegisterUser.SaveClick();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
