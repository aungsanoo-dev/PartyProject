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
    public partial class frm_CreatePermission : Form
    {
        private CtrlFrmCreatePermission ctrlFrmCreatePermission; // Declare the View
        public int PermissionID = 0;
        public bool IsEdit = false;
        public string accessValue = "";
        public frm_CreatePermission()
        {
            InitializeComponent();
            ctrlFrmCreatePermission = new CtrlFrmCreatePermission(this);
        }

        private void frm_CreatePermission_Load(object sender, EventArgs e)
        {
            ctrlFrmCreatePermission.ShowCombo(IsEdit);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
            ctrlFrmCreatePermission.SaveClick();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
