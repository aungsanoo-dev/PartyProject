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
        public frm_CreatePartyItem frmPartyItem;

        public CtrlFrmCreatePartyItem(frm_CreatePartyItem partyItemForm) 
        {
            frmPartyItem = partyItemForm;
        }

        DbaPartyItem dbaPartyItem = new DbaPartyItem();
        DbaConnection dbaConnection = new DbaConnection();

        private bool _IsEdit;
        private int _ItemID;

        public void CreateClick()
        {
            DataTable dt = new DataTable();
            string spString = "";
            _ItemID = frmPartyItem.ItemID;
            _IsEdit = frmPartyItem.IsEdit;

            int Ok;
            if (frmPartyItem.txtItemName.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type ItemName");
                frmPartyItem.txtItemName.Focus();
            }
            else if (frmPartyItem.txtQty.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Qty");
                frmPartyItem.txtQty.Focus();
            }
            else if (int.TryParse(frmPartyItem.txtQty.Text, out Ok) == false)
            {
                MessageBox.Show("Qty Should Be Number");
                frmPartyItem.txtQty.Focus();
                frmPartyItem.txtQty.SelectAll();
            }
            else if (!_IsEdit && (Convert.ToInt32(frmPartyItem.txtQty.Text) <= 0 || Convert.ToInt32(frmPartyItem.txtQty.Text) > 100))
            {
                MessageBox.Show("Qty Should Be Between 0 and 100");
                frmPartyItem.txtQty.Focus();
                frmPartyItem.txtQty.SelectAll();
            }
            else if (frmPartyItem.txtPrice.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Price");
                frmPartyItem.txtPrice.Focus();
            }
            else if (int.TryParse(frmPartyItem.txtPrice.Text, out Ok) == false)
            {
                MessageBox.Show("Price Should Be Number");
                frmPartyItem.txtPrice.Focus();
                frmPartyItem.txtPrice.SelectAll();
            }
            else if (Convert.ToInt32(frmPartyItem.txtPrice.Text) <= 0 || Convert.ToInt32(frmPartyItem.txtPrice.Text) > 1000000)
            {
                MessageBox.Show("Price Should Be Between 1 Thousand and 10 Lakh Or 0 Price");
                frmPartyItem.txtPrice.Focus();
                frmPartyItem.txtPrice.SelectAll();
            }
            else
            {

                spString = string.Format("SP_Select_PartyItem N'{0}',N'{1}',N'{2}'", frmPartyItem.txtItemName.Text.Trim().ToString(), "0", "1");
                dt = dbaConnection.SelectData(spString);
                if (dt.Rows.Count > 0 && _ItemID != Convert.ToInt32(dt.Rows[0]["ItemID"]))
                {
                    MessageBox.Show("This Item is Already Exist");
                    frmPartyItem.txtItemName.Focus();
                    frmPartyItem.txtItemName.SelectAll();
                }
                else
                {
                    dbaPartyItem.ITEMID = _ItemID;
                    dbaPartyItem.ITEMNAME = frmPartyItem.txtItemName.Text;
                    dbaPartyItem.QTY = Convert.ToInt32(frmPartyItem.txtQty.Text);
                    dbaPartyItem.PRICE = Convert.ToInt32(frmPartyItem.txtPrice.Text);
                    if (_IsEdit)
                    {
                        dbaPartyItem.ACTION = 1;
                        dbaPartyItem.SaveData();
                        MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                        frmPartyItem.Close();
                    }
                    else
                    {
                        dbaPartyItem.ACTION = 0;
                        dbaPartyItem.SaveData();
                        MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                        frmPartyItem.Close();
                    }
                }
            }
        }

        public void ItemLoad()
        {
            if (!frmPartyItem.IsEdit)
            {
                frmPartyItem.txtQty.Text = "0";
                frmPartyItem.txtPrice.Text = "0";
                frmPartyItem.txtItemName.Focus();
            }
        }
    }
}
