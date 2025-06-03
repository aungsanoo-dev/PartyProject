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
        private readonly CtrlFrmAccountList _ctrlFrmAccountList; // Declare the controller
        private readonly UserGridToggle _userGridToggle; // Declare new DGV
        public frm_AccountList()
        {
            InitializeComponent();
            _ctrlFrmAccountList = new CtrlFrmAccountList(this); // Create the controller and pass itself to ctrlFrmMain()
            _userGridToggle = new UserGridToggle(this,dgvAccountSetting);
            this.dgvAccountSetting.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvAccountSetting_DataBindingComplete);
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            _ctrlFrmAccountList.TsbNew();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            _ctrlFrmAccountList.ShowEntry();
        }

        private void dgvUserSetting_DoubleClick(object sender, EventArgs e)
        {
            _ctrlFrmAccountList.EditOrShow(_userGridToggle.DoubleToggleExtraGrid);
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            _ctrlFrmAccountList.TsbDelete();
            _userGridToggle.RefreshExtraGrid();
        }

        private void tstSearchWith_TextChanged(object sender, EventArgs e)
        {
            _ctrlFrmAccountList.TsbSearch();
        }

        
        private void frm_AccountList_Load(object sender, EventArgs e)
        {
            _ctrlFrmAccountList.ShowData();
            
        }

        private void tsbUser_Click(object sender, EventArgs e)
        {
            _userGridToggle.ToggleExtraGrid();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dgvAccountSetting_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            _ctrlFrmAccountList.HoverToolTip();
        }

        private void dgvAccountSetting_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                if (e.Value != null)
                {
                    e.Value = new string('*', e.Value.ToString().Length); // Display asterisks
                }
            }
        }

        public void RefreshAccountList()
        {
            _ctrlFrmAccountList.ShowData();
        }

        private void tsmUserName_Click(object sender, EventArgs e)
        {
            _ctrlFrmAccountList.TsmSearchLabelClick("UserName");
        }

        private void tsmAccessLevel_Click(object sender, EventArgs e)
        {
            _ctrlFrmAccountList.TsmSearchLabelClick("AccessLevel");
        }
    }
}
