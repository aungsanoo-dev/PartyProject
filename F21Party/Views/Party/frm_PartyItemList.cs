﻿using F21Party.Controllers;
using F21Party.DBA;
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
    public partial class frm_PartyItemList : Form
    {
        private readonly CtrlFrmPartyItemList _ctrlFrmPartyItem; // Declare the controller
        public frm_PartyItemList()
        {
            InitializeComponent();
            _ctrlFrmPartyItem = new CtrlFrmPartyItemList(this);
        }

        private void frm_ItemList_Load(object sender, EventArgs e)
        {
            _ctrlFrmPartyItem.ShowData();
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            _ctrlFrmPartyItem.TsbNewClick();
        }

        private void dgvItem_DoubleClick(object sender, EventArgs e)
        {
            _ctrlFrmPartyItem.ShowEntry();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            _ctrlFrmPartyItem.ShowEntry();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            _ctrlFrmPartyItem.TsbDelete();
        }

        private void tsmItemName_Click(object sender, EventArgs e)
        {
            _ctrlFrmPartyItem.TsmSearchLabelClick("ItemName");
        }

        private void tsmQty_Click(object sender, EventArgs e)
        {
            _ctrlFrmPartyItem.TsmSearchLabelClick("Qty");
        }

        private void tsmPrice_Click(object sender, EventArgs e)
        {
            _ctrlFrmPartyItem.TsmSearchLabelClick("Price");
        }

        private void tstSearchWith_TextChanged(object sender, EventArgs e)
        {
            _ctrlFrmPartyItem.TsbSearch();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
