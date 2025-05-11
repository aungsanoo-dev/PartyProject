using F21Party.Controllers;
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
    public partial class frm_CreatePage : Form
    {

        private CtrlFrmCreatePage ctrlFrmCreatePage; // Declare the View
        public int _PageID = 0;
        public bool _IsEdit = false;
        public frm_CreatePage()
        {
            InitializeComponent();
            ctrlFrmCreatePage = new CtrlFrmCreatePage(this);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            ctrlFrmCreatePage.CreateClick();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
