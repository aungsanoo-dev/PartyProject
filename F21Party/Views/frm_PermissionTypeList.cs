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
    public partial class frm_PermissionTypeList : Form
    {

        private CtrlFrmPermissionTypeList ctrlFrmPermissionTypeList; // Declare the controller
        public frm_PermissionTypeList()
        {
            InitializeComponent();
            ctrlFrmPermissionTypeList = new CtrlFrmPermissionTypeList(this); // Create the controller and pass itself
        }

        private void frm_PermissionTypeList_Load(object sender, EventArgs e)
        {
            ctrlFrmPermissionTypeList.ShowData();
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            ctrlFrmPermissionTypeList.TsbNew();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            ctrlFrmPermissionTypeList.ShowEntry();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            ctrlFrmPermissionTypeList.TsbDelete();
        }

        private void tstSearchWith_TextChanged(object sender, EventArgs e)
        {
            ctrlFrmPermissionTypeList.TsbSearch();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
