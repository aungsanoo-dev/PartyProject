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
        
        private readonly CtrlFrmPermissionList _ctrlFrmPermissionList; // Declare the controller
        private readonly frm_Main _mainForm;
        public bool IsLogout;

        public frm_PermissionList()
        {
            InitializeComponent();
            _ctrlFrmPermissionList = new CtrlFrmPermissionList(this); // Create the controller and pass itself
            IsLogout = false;
        }
        
        public frm_PermissionList(frm_Main main)
            : this() // Calls the default constructor
        {
            _mainForm = main; // Store reference for later
            
        }

        private void frm_PermissionList_Load(object sender, EventArgs e)
        {
            _ctrlFrmPermissionList.ShowData();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            _ctrlFrmPermissionList.ShowEntry();
            if (IsLogout)
            {
                _mainForm.RefreshMenu();
                this.Close();
            }
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            _ctrlFrmPermissionList.TsbNew();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmiAccessLevel_Click(object sender, EventArgs e)
        {
            _ctrlFrmPermissionList.TsmiAccessLevelClick();
        }

        private void tsmiPageName_Click(object sender, EventArgs e)
        {
            _ctrlFrmPermissionList.TsmiPageNameClick();
        }

        private void tsmiPermissionName_Click(object sender, EventArgs e)
        {
            _ctrlFrmPermissionList.TsmiPermissionNameClick();
        }

        private void tstSearchWith_TextChanged(object sender, EventArgs e)
        {
            _ctrlFrmPermissionList.TstSearchWithTextChanged();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            _ctrlFrmPermissionList.TsbDelete();
        }
    }
}
