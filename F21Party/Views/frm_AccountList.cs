using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using F21Party.Controllers;
using F21Party.DBA;

namespace F21Party.Views
{
    public partial class frm_AccountList : Form
    {
        CtrlFrmAccountList ctrlFrmAccountList; // Declare the controller
        UserGridToggle userGridToggle; // Declare new DGV
        public frm_AccountList()
        {
            InitializeComponent();
            ctrlFrmAccountList = new CtrlFrmAccountList(this); // Create the controller and pass itself to ctrlFrmMain()
            userGridToggle = new UserGridToggle(this,dgvUserSetting);
            this.dgvUserSetting.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvUserSetting_DataBindingComplete);
        }


        private void tsbNew_Click(object sender, EventArgs e)
        {
            ctrlFrmAccountList.TsbNew();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            ctrlFrmAccountList.ShowEntry();
        }

        private void dgvUserSetting_DoubleClick(object sender, EventArgs e)
        {
            userGridToggle.DoubleToggleExtraGrid();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            ctrlFrmAccountList.TsbDelete();
        }

        private void tstSearchWith_TextChanged(object sender, EventArgs e)
        {
            ctrlFrmAccountList.TsbSearch();
        }

        private void frm_AccountList_Load(object sender, EventArgs e)
        {
            ctrlFrmAccountList.ShowData();
            
        }

        private void tsbUser_Click(object sender, EventArgs e)
        {
            userGridToggle.ToggleExtraGrid();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvUserSetting_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ctrlFrmAccountList.HoverToolTip();
        }
    }
}
