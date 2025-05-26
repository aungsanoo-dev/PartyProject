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
    public partial class frm_AccessList : Form
    {
        private readonly CtrlFrmAccessList _ctrlFrmAccessList; // Declare the controller
        public frm_AccessList()
        {
            InitializeComponent();
            _ctrlFrmAccessList = new CtrlFrmAccessList(this); // Create the controller and pass itself to ctrlFrmMain()
        }

        private void frm_AccessList_Load(object sender, EventArgs e)
        {
            _ctrlFrmAccessList.ShowData();
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            _ctrlFrmAccessList.TsbNew();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            _ctrlFrmAccessList.ShowEntry();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            _ctrlFrmAccessList.TsbDelete();
        }

        private void tstSearchWith_TextChanged(object sender, EventArgs e)
        {
            _ctrlFrmAccessList.TsbSearch();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmAccessLevel_Click(object sender, EventArgs e)
        {
            _ctrlFrmAccessList.TsmSearchLabelClick("AccessLevel");
        }

        private void tsmLogInAccess_Click(object sender, EventArgs e)
        {
            _ctrlFrmAccessList.TsmSearchLabelClick("LogInAccess");
        }

        private void tsmAuthority_Click(object sender, EventArgs e)
        {
            _ctrlFrmAccessList.TsmSearchLabelClick("Authority");
        }
    }
}
