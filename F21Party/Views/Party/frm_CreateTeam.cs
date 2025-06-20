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
    public partial class frm_CreateTeam : Form
    {
        private readonly CtrlFrmCreateTeam _ctrlFrmCreateTeam;
        public int TeamID;
        public bool IsEdit = false;
        public int TotalPlayer = 0;
        public frm_CreateTeam()
        {
            InitializeComponent();
            _ctrlFrmCreateTeam = new CtrlFrmCreateTeam(this);
        }

        private void frm_CreateTeam_Load(object sender, EventArgs e)
        {
            txtTeamName.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _ctrlFrmCreateTeam.SaveClick();
        }
    }
}
