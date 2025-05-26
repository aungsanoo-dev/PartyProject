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
        private readonly CtrlFrmCreateAccount _ctrlFrmCreateAccount;
        public int AccountID = 0;
        public int UserID = 0;
        public bool IsEdit = false;
        public frm_CreateAccount()
        {
            InitializeComponent();
            _ctrlFrmCreateAccount = new CtrlFrmCreateAccount(this);
        }

        private void frm_CreateAccount_Load(object sender, EventArgs e)
        {
            _ctrlFrmCreateAccount.ShowCombo(IsEdit);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if(btnCreate.Text == "Add")
            {
                _ctrlFrmCreateAccount.AddAccountClick();
            }
            else
            {
                _ctrlFrmCreateAccount.SaveClick();
            }
            
        }

        private void cboAccessLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ctrlFrmCreateAccount.AccessComboChange(IsEdit);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEye_Click(object sender, EventArgs e)
        {
            _ctrlFrmCreateAccount.EyeToggle();
        }
    }
}
