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
    public partial class frm_CreateAccount : Form
    {
        ctrlFrmCreateAccount ctrlFrmCreateAccount;
        public int _AccountID = 0;
        public int _UserID = 0;
        public bool _IsEdit = false;
        public frm_CreateAccount()
        {
            InitializeComponent();
            ctrlFrmCreateAccount = new ctrlFrmCreateAccount(this);
        }

        private void frm_CreateAccount_Load(object sender, EventArgs e)
        {
            ctrlFrmCreateAccount.ShowCombo(_IsEdit);
            ctrlFrmCreateAccount.EyeToggle();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            ctrlFrmCreateAccount.SaveClick();
        }

        private void cboAccessLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ctrlFrmCreateAccount.AccessComboChange(_IsEdit);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
