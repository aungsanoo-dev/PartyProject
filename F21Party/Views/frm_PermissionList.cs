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
    public partial class frm_PermissionList : Form
    {
        
        private CtrlFrmPermissionList ctrlFrmPermissionList; // Declare the controller

        public frm_PermissionList()
        {
            InitializeComponent();
            ctrlFrmPermissionList = new CtrlFrmPermissionList(this); // Create the controller and pass itself
        }

        private void frm_PermissionList_Load(object sender, EventArgs e)
        {
            ctrlFrmPermissionList.ShowData();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            ctrlFrmPermissionList.ShowEntry();
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            ctrlFrmPermissionList.TsbNew();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
