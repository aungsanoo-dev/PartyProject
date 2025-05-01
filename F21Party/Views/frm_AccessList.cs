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
    public partial class frm_AccessList : Form
    {
        private CtrlFrmAccessList ctrlFrmAccessList; // Declare the controller
        public frm_AccessList()
        {
            InitializeComponent();
            ctrlFrmAccessList = new CtrlFrmAccessList(this); // Create the controller and pass itself to ctrlFrmMain()
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            ctrlFrmAccessList.TsbNew();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            ctrlFrmAccessList.ShowEntry();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {

        }

        private void frm_AccessList_Load(object sender, EventArgs e)
        {
            ctrlFrmAccessList.ShowData();
        }
    }
}
