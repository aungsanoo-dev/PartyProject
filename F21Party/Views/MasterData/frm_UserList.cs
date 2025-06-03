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
    public partial class frm_UserList : Form
    {
        private readonly CtrlFrmUserList _ctrlFrmUserList; // Declare the controller
        private readonly AccountGridToggle _accountGridToggle; // Declare new DGV
        public frm_UserList()
        {
            InitializeComponent();
            _ctrlFrmUserList = new CtrlFrmUserList(this); // Create the controller and pass itself to ctrlFrmMain()
            _accountGridToggle = new AccountGridToggle(this, dgvUserSetting);
            this.dgvUserSetting.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvUserSetting_DataBindingComplete);
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            _ctrlFrmUserList.TsbNew();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            _ctrlFrmUserList.ShowEntry();
        }

        private void dgvUserSetting_DoubleClick(object sender, EventArgs e)
        {
            //accountGridToggle.DoubleToggleExtraGrid();
            _ctrlFrmUserList.ShowEntry();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            _ctrlFrmUserList.TsbDelete();
        }

        private void tstSearchWith_TextChanged(object sender, EventArgs e)
        {
            _ctrlFrmUserList.TsbSearch();
        }

        private void frm_AccountList_Load(object sender, EventArgs e)
        {
            _ctrlFrmUserList.ShowData();

        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvUserSetting_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            _ctrlFrmUserList.HoverToolTip();
        }

        private void tsbAccount_Click(object sender, EventArgs e)
        {
            _accountGridToggle.ToggleExtraGrid();
        }
    }
}
