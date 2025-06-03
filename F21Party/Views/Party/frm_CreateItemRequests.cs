using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using F21Party.DBA;
using F21Party.Controllers;

namespace F21Party.Views
{
    public partial class frm_CreateItemRequests : Form
    {
        private readonly CtrlFrmCreateItemRequests _ctrlFrmCreateItemRequests;
        public int RequestsID = 0;

        public frm_CreateItemRequests()
        {
            InitializeComponent();
            _ctrlFrmCreateItemRequests = new CtrlFrmCreateItemRequests(this);
        }
        private void frm_Order_Load(object sender, EventArgs e)
        {
            _ctrlFrmCreateItemRequests.CreateTable();
            _ctrlFrmCreateItemRequests.ShowData();
        }

        private void cboItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ctrlFrmCreateItemRequests.ItemSelectedChanged();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _ctrlFrmCreateItemRequests.AddClick();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            _ctrlFrmCreateItemRequests.RemoveClick();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            _ctrlFrmCreateItemRequests.DtpDateValueChanged();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _ctrlFrmCreateItemRequests.SaveClick();
        }
    }
}
