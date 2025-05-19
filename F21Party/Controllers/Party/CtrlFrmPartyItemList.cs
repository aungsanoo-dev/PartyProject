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
        public frm_PartyItemList frmPartyItemList;
        public CtrlFrmPartyItemList(frm_PartyItemList partyItemListform)
        {
            frmPartyItemList = partyItemListform;
        }

        DbaConnection dbaConnection = new DbaConnection();
        string spString = "";

        public void ShowData()
        {
            spString = string.Format("SP_Select_PartyItem N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            frmPartyItemList.dgvPartyItem.DataSource = dbaConnection.SelectData(spString);

            frmPartyItemList.dgvPartyItem.Columns[0].Width = (frmPartyItemList.dgvPartyItem.Width / 100) * 10;
            frmPartyItemList.dgvPartyItem.Columns[1].Visible = false;
            frmPartyItemList.dgvPartyItem.Columns[2].Width = (frmPartyItemList.dgvPartyItem.Width / 100) * 50;
            frmPartyItemList.dgvPartyItem.Columns[3].Width = (frmPartyItemList.dgvPartyItem.Width / 100) * 20;
            frmPartyItemList.dgvPartyItem.Columns[4].Width = (frmPartyItemList.dgvPartyItem.Width / 100) * 20;

            dbaConnection.ToolStripTextBoxData(frmPartyItemList.tstSearchWith, spString, "ItemName");
            frmPartyItemList.tslLabel.Text = "ItemName";

            if (!Program.PublicArrWriteAccessPages.Contains("PartyItem"))
            {
                frmPartyItemList.tsbNew.ForeColor = System.Drawing.SystemColors.GrayText;
                frmPartyItemList.tsbEdit.ForeColor = System.Drawing.SystemColors.GrayText;
                frmPartyItemList.tsbDelete.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }
        public void ShowEntry()
        {
            if (!Program.PublicArrWriteAccessPages.Contains("PartyItem"))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return;
            }

            if (frmPartyItemList.dgvPartyItem.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There is No Data");
            }
            else
            {
                frm_CreatePartyItem frmPartyItem = new frm_CreatePartyItem();
                frmPartyItem.ItemID = Convert.ToInt32(frmPartyItemList.dgvPartyItem.CurrentRow.Cells["ItemID"].Value.ToString());
                frmPartyItem.txtItemName.Text = frmPartyItemList.dgvPartyItem.CurrentRow.Cells["ItemName"].Value.ToString();
                frmPartyItem.txtQty.Text = frmPartyItemList.dgvPartyItem.CurrentRow.Cells["Qty"].Value.ToString();
                frmPartyItem.txtPrice.Text = frmPartyItemList.dgvPartyItem.CurrentRow.Cells["Price"].Value.ToString();
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

            string ItemID = frmPartyItemList.dgvPartyItem.CurrentRow.Cells["ItemID"].Value.ToString();
            DbaPartyItem dbaPartyItem = new DbaPartyItem();
            if (frmPartyItemList.dgvPartyItem.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else if (frmPartyItemList.dgvPartyItem.CurrentRow.Cells["Qty"].Value.ToString() != "0")
            {
                MessageBox.Show("This Item Has Qty. Cannot Be Delete");
            }
            else
            {
                if (MessageBox.Show("Are You Sure You Want To Delete?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //spString = string.Format("SP_Select_PartyItem N'{0}', N'{1}', N'{2}'", Convert.ToInt32(frmPartyItemList.dgvPartyItem.CurrentRow.Cells["ItemID"].Value), "0", "3");
                    //DataTable DT = new DataTable();
                    //DT = dbaConnection.SelectData(spString);

                    //if (DT.Rows.Count > 0)
                    //{
                    //    MessageBox.Show("You cannont delete the Page which is currently used by the Permission!");
                    //}

                    dbaPartyItem.ITEMID = Convert.ToInt32(ItemID);
                    dbaPartyItem.ACTION = 2;
                    dbaPartyItem.SaveData();
                    MessageBox.Show("Successfully Delete");
                    ShowData();
                }
            }
        }
        public void TsbSearch()
        {
            
            if (frmPartyItemList.tslLabel.Text == "ItemName")
            {
               spString = string.Format("SP_Select_PartyItem N'{0}',N'{1}',N'{2}'", frmPartyItemList.tstSearchWith.Text.Trim().ToString(), "0", "2");
            }
            else if (frmPartyItemList.tslLabel.Text == "Qty")
            {
                spString = string.Format("SP_Select_PartyItem N'{0}',N'{1}',N'{2}'", frmPartyItemList.tstSearchWith.Text.Trim().ToString(), "0", "3");
            }
            else if (frmPartyItemList.tslLabel.Text == "Price")
            {
                spString = string.Format("SP_Select_PartyItem N'{0}',N'{1}',N'{2}'", frmPartyItemList.tstSearchWith.Text.Trim().ToString(), "0", "4");
            }
            frmPartyItemList.dgvPartyItem.DataSource = dbaConnection.SelectData(spString);
        }

        public void TsmSearchLabelClick(string textLabel)
        {
            frmPartyItemList.tslLabel.Text = textLabel;
            spString = string.Format("SP_Select_PartyItem N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            dbaConnection.ToolStripTextBoxData(frmPartyItemList.tstSearchWith, spString, textLabel);
        }
    }
}
