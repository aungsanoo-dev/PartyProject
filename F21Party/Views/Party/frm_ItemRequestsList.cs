using F21Party.Controllers;
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
    public partial class frm_ItemRequestsList : Form
    {
        private readonly CtrlFrmItemRequestsList _ctrlFrmItemRequestsList;
        public frm_ItemRequestsList()
        {
            InitializeComponent();
            _ctrlFrmItemRequestsList = new CtrlFrmItemRequestsList(this);
        }

        private void frm_ItemRequestsList_Load(object sender, EventArgs e)
        {
            _ctrlFrmItemRequestsList.RemoveDelete();
            _ctrlFrmItemRequestsList.ShowItemRequests();
            _ctrlFrmItemRequestsList.ShowItemRequestsDetail();

            foreach (Control ctrl in this.Controls)
            {
                ctrl.MouseDown += frm_ItemRequestsList_MouseDown;
            }
        }

        private void dgvItemRequests_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _ctrlFrmItemRequestsList.DgvItemRequestsCellClick(e);
        }
        private void tsbNew_Click(object sender, EventArgs e)
        {
            _ctrlFrmItemRequestsList.TsbNewClick();
        }

        private void tsmRequestDate_Click(object sender, EventArgs e)
        {
            _ctrlFrmItemRequestsList.TsmRequestDateClick();
        }

        private void tsmFullName_Click(object sender, EventArgs e)
        {
            _ctrlFrmItemRequestsList.TsmFullNameClick();
        }

        private void tstSearchWith_TextChanged(object sender, EventArgs e)
        {
            _ctrlFrmItemRequestsList.TsmSearchWithChanged();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvItemRequests_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            _ctrlFrmItemRequestsList.DgvItemRequestsCellClick(e);
        }

        private void frm_ItemRequestsList_MouseDown(object sender, MouseEventArgs e)
        {
            _ctrlFrmItemRequestsList.FrmItemRequestsMouseDown(e);
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            _ctrlFrmItemRequestsList.TsbDelete();
        }
    }
}
