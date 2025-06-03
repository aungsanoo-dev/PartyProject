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

        private readonly CtrlFrmCreatePage _ctrlFrmCreatePage; // Declare the View
        public int PageID = 0;
        public bool IsEdit = false;
        public frm_CreatePage()
        {
            InitializeComponent();
            _ctrlFrmCreatePage = new CtrlFrmCreatePage(this);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            _ctrlFrmCreatePage.CreateClick();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
