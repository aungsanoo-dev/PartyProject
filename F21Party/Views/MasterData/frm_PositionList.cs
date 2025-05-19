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
    public partial class frm_PositionList : Form
    {
        private CtrlFrmPositionList ctrlFrmPositionList; // Declare the controller

        public frm_PositionList()
        {
            InitializeComponent();
            ctrlFrmPositionList = new CtrlFrmPositionList(this); // Create the controller and pass itself to ctrlFrmMain()
        }

        private void frm_PositionList_Load(object sender, EventArgs e)
        {
            ctrlFrmPositionList.ShowData();
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            ctrlFrmPositionList.TsbNew();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            ctrlFrmPositionList.ShowEntry();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            ctrlFrmPositionList.TsbDelete();
        }

        private void tstSearchWith_TextChanged(object sender, EventArgs e)
        {
            ctrlFrmPositionList.TsbSearch();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
