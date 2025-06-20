﻿using F21Party.Controllers;
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
    public partial class frm_PageList : Form
    {

        private readonly CtrlFrmPageList _ctrlFrmPageList; // Declare the controller
        public frm_PageList()
        {
            InitializeComponent();
            _ctrlFrmPageList = new CtrlFrmPageList(this); // Create the controller and pass itself to ctrlFrmMain()
        }

        private void frm_PageList_Load(object sender, EventArgs e)
        {
            _ctrlFrmPageList.ShowData();
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            _ctrlFrmPageList.TsbNew();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            _ctrlFrmPageList.ShowEntry();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            _ctrlFrmPageList.TsbDelete();
        }

        private void tstSearchWith_TextChanged(object sender, EventArgs e)
        {
            _ctrlFrmPageList.TsbSearch();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
