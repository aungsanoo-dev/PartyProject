﻿using System;
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
        private CtrlFrmCreateUser ctrlFrmRegisterUser;
        public int _AccountID = 0;
        public int _UserID = 0;
        public bool _IsEdit = false;
        public frm_CreateUser()
        {
            InitializeComponent();
            ctrlFrmRegisterUser = new CtrlFrmCreateUser(this);
        }

        private void frm_CreateAccount_Load(object sender, EventArgs e)
        {
            ctrlFrmRegisterUser.ShowCombo();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            ctrlFrmRegisterUser.SaveClick();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
