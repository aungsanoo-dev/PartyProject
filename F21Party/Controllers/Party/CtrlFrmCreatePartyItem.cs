using F21Party.Views;
using F21Party.DBA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F21Party.Controllers
{
    internal class CtrlFrmCreatePartyItem
    {
        private readonly frm_CreatePartyItem _frmPartyItem;
        private readonly DbaPartyItem _dbaPartyItem = new DbaPartyItem();
        private readonly DbaConnection _dbaConnection = new DbaConnection();
        private bool _isEdit;
        private int _itemID;
        private string _spString;
        public CtrlFrmCreatePartyItem(frm_CreatePartyItem partyItemForm) 
        {
            _frmPartyItem = partyItemForm;
        }


        public void CreateClick()
        {
            DataTable dt = new DataTable();
            _itemID = _frmPartyItem.ItemID;
            _isEdit = _frmPartyItem.IsEdit;

            int Ok;
            if (_frmPartyItem.txtItemName.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type ItemName");
                _frmPartyItem.txtItemName.Focus();
            }
            else if (_frmPartyItem.txtQty.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Qty");
                _frmPartyItem.txtQty.Focus();
            }
            else if (int.TryParse(_frmPartyItem.txtQty.Text, out Ok) == false)
            {
                MessageBox.Show("Qty Should Be Number");
                _frmPartyItem.txtQty.Focus();
                _frmPartyItem.txtQty.SelectAll();
            }
            else if (!_isEdit && (Convert.ToInt32(_frmPartyItem.txtQty.Text) <= 0 || Convert.ToInt32(_frmPartyItem.txtQty.Text) > 100))
            {
                MessageBox.Show("Qty Should Be Between 0 and 100");
                _frmPartyItem.txtQty.Focus();
                _frmPartyItem.txtQty.SelectAll();
            }
            else if (_frmPartyItem.txtPrice.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Price");
                _frmPartyItem.txtPrice.Focus();
            }
            else if (int.TryParse(_frmPartyItem.txtPrice.Text, out Ok) == false)
            {
                MessageBox.Show("Price Should Be Number");
                _frmPartyItem.txtPrice.Focus();
                _frmPartyItem.txtPrice.SelectAll();
            }
            else if (Convert.ToInt32(_frmPartyItem.txtPrice.Text) <= 0 || Convert.ToInt32(_frmPartyItem.txtPrice.Text) > 1000000)
            {
                MessageBox.Show("Price Should Be Between 1 Thousand and 10 Lakh Or 0 Price");
                _frmPartyItem.txtPrice.Focus();
                _frmPartyItem.txtPrice.SelectAll();
            }
            else
            {

                _spString = string.Format("SP_Select_PartyItem N'{0}',N'{1}',N'{2}'", _frmPartyItem.txtItemName.Text.Trim().ToString(), "0", "1");
                dt = _dbaConnection.SelectData(_spString);
                if (dt.Rows.Count > 0 && _itemID != Convert.ToInt32(dt.Rows[0]["ItemID"]))
                {
                    MessageBox.Show("This Item is Already Exist");
                    _frmPartyItem.txtItemName.Focus();
                    _frmPartyItem.txtItemName.SelectAll();
                }
                else
                {
                    _dbaPartyItem.ITEMID = _itemID;
                    _dbaPartyItem.ITEMNAME = _frmPartyItem.txtItemName.Text;
                    _dbaPartyItem.QTY = Convert.ToInt32(_frmPartyItem.txtQty.Text);
                    _dbaPartyItem.PRICE = Convert.ToInt32(_frmPartyItem.txtPrice.Text);
                    if (_isEdit)
                    {
                        _dbaPartyItem.ACTION = 1;
                        _dbaPartyItem.SaveData();
                        MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                        _frmPartyItem.Close();
                    }
                    else
                    {
                        _dbaPartyItem.ACTION = 0;
                        _dbaPartyItem.SaveData();
                        MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                        _frmPartyItem.Close();
                    }
                }
            }
        }

        public void ItemLoad()
        {
            if (!_frmPartyItem.IsEdit)
            {
                _frmPartyItem.txtQty.Text = "0";
                _frmPartyItem.txtPrice.Text = "0";
                _frmPartyItem.txtItemName.Focus();
            }
        }
    }
}
