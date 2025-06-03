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
    public partial class frm_DonationList : Form
    {
        private readonly CtrlFrmDonationList _ctrlFrmDonationList;
        
        public frm_DonationList()
        {
            InitializeComponent();
            _ctrlFrmDonationList = new CtrlFrmDonationList(this);
        }

        private void frm_DonationList_Load(object sender, EventArgs e)
        {
            _ctrlFrmDonationList.RemoveDelete();
            _ctrlFrmDonationList.ShowDonation();
            _ctrlFrmDonationList.ShowDonationDetail();

            foreach (Control ctrl in this.Controls)
            {
                ctrl.MouseDown += frm_DonationList_MouseDown;
            }
        }

        private void dgvDonation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _ctrlFrmDonationList.DgvDonationCellClick(e);
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            _ctrlFrmDonationList.TsbNewClick();
        }

        private void tsmDonationDate_Click(object sender, EventArgs e)
        {
            _ctrlFrmDonationList.TsmDonationDateClick();
        }

        private void tsmFullName_Click(object sender, EventArgs e)
        {
            _ctrlFrmDonationList.TsmFullNameClick();
        }

        private void tstSearchWith_TextChanged(object sender, EventArgs e)
        {
            _ctrlFrmDonationList.TsmSearchWithChanged();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            _ctrlFrmDonationList.TsbDelete();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_DonationList_MouseDown(object sender, MouseEventArgs e)
        {
            _ctrlFrmDonationList.FrmDonationMouseDown(e);
        }
    }
}
