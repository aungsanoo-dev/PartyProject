using F21Party.DBA;
using F21Party.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F21Party.Controllers
{
    internal class CtrlFrmPartyItemList
    {
        private readonly frm_PartyItemList _frmPartyItemList;
        private readonly DbaConnection _dbaConnection = new DbaConnection();
        private string _spString = "";
        public CtrlFrmPartyItemList(frm_PartyItemList partyItemListform)
        {
            _frmPartyItemList = partyItemListform;
        }

        public void ShowData()
        {
            _spString = string.Format("SP_Select_PartyItem N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            _frmPartyItemList.dgvPartyItem.DataSource = _dbaConnection.SelectData(_spString);

            _frmPartyItemList.dgvPartyItem.Columns[0].Width = (_frmPartyItemList.dgvPartyItem.Width / 100) * 10;
            _frmPartyItemList.dgvPartyItem.Columns[1].Visible = false;
            _frmPartyItemList.dgvPartyItem.Columns[2].Width = (_frmPartyItemList.dgvPartyItem.Width / 100) * 50;
            _frmPartyItemList.dgvPartyItem.Columns[3].Width = (_frmPartyItemList.dgvPartyItem.Width / 100) * 20;
            _frmPartyItemList.dgvPartyItem.Columns[4].Width = (_frmPartyItemList.dgvPartyItem.Width / 100) * 20;

            _dbaConnection.ToolStripTextBoxData(_frmPartyItemList.tstSearchWith, _spString, "ItemName");
            _frmPartyItemList.tslLabel.Text = "ItemName";

            if (!Program.PublicArrWriteAccessPages.Contains("PartyItem"))
            {
                _frmPartyItemList.tsbNew.ForeColor = System.Drawing.SystemColors.GrayText;
                _frmPartyItemList.tsbEdit.ForeColor = System.Drawing.SystemColors.GrayText;
                _frmPartyItemList.tsbDelete.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }
        public void ShowEntry()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("PartyItem"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            if (_frmPartyItemList.dgvPartyItem.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There is No Data");
            }
            else
            {
                frm_CreatePartyItem frmPartyItem = new frm_CreatePartyItem();
                frmPartyItem.ItemID = Convert.ToInt32(_frmPartyItemList.dgvPartyItem.CurrentRow.Cells["ItemID"].Value.ToString());
                frmPartyItem.txtItemName.Text = _frmPartyItemList.dgvPartyItem.CurrentRow.Cells["ItemName"].Value.ToString();
                frmPartyItem.txtQty.Text = _frmPartyItemList.dgvPartyItem.CurrentRow.Cells["Qty"].Value.ToString();
                frmPartyItem.txtPrice.Text = _frmPartyItemList.dgvPartyItem.CurrentRow.Cells["Price"].Value.ToString();
                frmPartyItem.IsEdit = true;
                frmPartyItem.ShowDialog();
                ShowData();
            }
        }

        public void TsbNewClick()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("PartyItem"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            frm_CreatePartyItem frm = new frm_CreatePartyItem();
            //frm_Item frm = new frm_Item();
            frm.ShowDialog();
            ShowData();
        }

        public void TsbDelete()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("PartyItem"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            string itemID = _frmPartyItemList.dgvPartyItem.CurrentRow.Cells["ItemID"].Value.ToString();
            DbaPartyItem dbaPartyItem = new DbaPartyItem();
            if (_frmPartyItemList.dgvPartyItem.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else if (_frmPartyItemList.dgvPartyItem.CurrentRow.Cells["Qty"].Value.ToString() != "0")
            {
                MessageBox.Show("This Item Has Qty. Cannot Be Delete");
            }
            else
            {
                if (MessageBox.Show("Are You Sure You Want To Delete?", "Delete Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    dbaPartyItem.ITEMID = Convert.ToInt32(itemID);
                    dbaPartyItem.ACTION = 2;
                    dbaPartyItem.SaveData();
                    MessageBox.Show("Successfully Delete");
                    ShowData();
                }
            }
        }
        public void TsbSearch()
        {
            
            if (_frmPartyItemList.tslLabel.Text == "ItemName")
            {
               _spString = string.Format("SP_Select_PartyItem N'{0}',N'{1}',N'{2}'", _frmPartyItemList.tstSearchWith.Text.Trim().ToString(), "0", "2");
            }
            else if (_frmPartyItemList.tslLabel.Text == "Qty")
            {
                _spString = string.Format("SP_Select_PartyItem N'{0}',N'{1}',N'{2}'", _frmPartyItemList.tstSearchWith.Text.Trim().ToString(), "0", "3");
            }
            else if (_frmPartyItemList.tslLabel.Text == "Price")
            {
                _spString = string.Format("SP_Select_PartyItem N'{0}',N'{1}',N'{2}'", _frmPartyItemList.tstSearchWith.Text.Trim().ToString(), "0", "4");
            }
            _frmPartyItemList.dgvPartyItem.DataSource = _dbaConnection.SelectData(_spString);
        }

        public void TsmSearchLabelClick(string textLabel)
        {
            _frmPartyItemList.tslLabel.Text = textLabel;
            _spString = string.Format("SP_Select_PartyItem N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            _dbaConnection.ToolStripTextBoxData(_frmPartyItemList.tstSearchWith, _spString, textLabel);
        }
    }
}
