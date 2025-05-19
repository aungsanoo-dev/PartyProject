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
    public partial class frm_CreatePosition : Form
    {
        private CtrlFrmCreatePosition ctrlFrmCreatePosition; // Declare the View
        public int _PositionID = 0;
        public bool _IsEdit = false;

        public frm_CreatePosition()
        {
            InitializeComponent();
            ctrlFrmCreatePosition = new CtrlFrmCreatePosition(this);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            ctrlFrmCreatePosition.CreateClick();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
