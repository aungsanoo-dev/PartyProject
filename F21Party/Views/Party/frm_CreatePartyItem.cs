using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using F21Party.DBA;
using F21Party.Controllers;

namespace F21Party.Views
{
    public partial class frm_CreatePartyItem : Form
    {
        private readonly CtrlFrmCreatePartyItem _ctrlFrmPartyItem;
        public int ItemID = 0;
        public bool IsEdit = false;
        public frm_CreatePartyItem()
        {
            InitializeComponent();
            _ctrlFrmPartyItem = new CtrlFrmCreatePartyItem(this);
        }

        private void frm_Item_Load(object sender, EventArgs e)
        {
            _ctrlFrmPartyItem.ItemLoad();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            _ctrlFrmPartyItem.CreateClick();
        }
    }
}
