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
    public partial class frm_CreateDonation : Form
    {
        private CtrlFrmCreateDonation ctrlFrmCreateDonation;
        public int DonationID = 0;
        public bool IsEdit = false;
        
        public frm_CreateDonation()
        {
            InitializeComponent();
            ctrlFrmCreateDonation = new CtrlFrmCreateDonation(this);
        }

        private void frm_Donation_Load(object sender, EventArgs e)
        {
            ctrlFrmCreateDonation.CreateTable();
            ctrlFrmCreateDonation.ShowData();
        }

        private void cboItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            ctrlFrmCreateDonation.ItemSelectedChanged();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ctrlFrmCreateDonation.AddClick();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            ctrlFrmCreateDonation.RemoveClick();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            ctrlFrmCreateDonation.DtpDateValueChanged();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ctrlFrmCreateDonation.SaveClick();
        }
    }
}
