using F21Party.DBA;
using F21Party.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F21Party.Controllers
{
    internal class CtrlFrmCreateItemRequests
    {
        public frm_CreateItemRequests frmCreateItemRequests;

        public CtrlFrmCreateItemRequests(frm_CreateItemRequests itemRequestsForm)
        {
            frmCreateItemRequests = itemRequestsForm;
        }

        DbaItemRequests dbaItemRequests = new DbaItemRequests();
        DbaItemRequestsDetail dbaItemRequestsDetail = new DbaItemRequestsDetail();
        DbaPartyItem dbaPartyItem = new DbaPartyItem();
        DbaConnection dbaConnection = new DbaConnection();

        DataTable DT = new DataTable();
        DataTable DTItemRequests = new DataTable();
        int _ItemRequestsDetailID = 0;
        int _ItemRequestsID = 0;
        string SPString = "";
        int Count = 0;
        int addCount = 0;

        public void AddCombo(ComboBox cboCombo, string spString, string Display, string Value)
        {
            DataTable DTAC = new DataTable();
            DataTable DTCombo = new DataTable();
            DataRow Dr;


            DTCombo.Columns.Add(Display);
            DTCombo.Columns.Add(Value);

            Dr = DTCombo.NewRow();
            Dr[Display] = "---Select---";
            Dr[Value] = 0;
            DTCombo.Rows.Add(Dr);

            try
            {
                dbaConnection.DataBaseConn();
                SqlDataAdapter Adpt = new SqlDataAdapter(spString, dbaConnection.con);
                Adpt.Fill(DTAC);
                for (int i = 0; i < DTAC.Rows.Count; i++)
                {
                    Dr = DTCombo.NewRow();

                    if (Display == "FullName")
                    {
                        if (Convert.ToInt32(DTAC.Rows[i][Value]) == 1) // 1 is SuperAdmin UserID.
                        {
                            continue;
                        }
                    }

                    Dr[Display] = DTAC.Rows[i][Display];
                    Dr[Value] = DTAC.Rows[i][Value];
                    DTCombo.Rows.Add(Dr);
                }

                cboCombo.DisplayMember = Display;
                cboCombo.ValueMember = Value;
                cboCombo.DataSource = DTCombo;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error In AddCombo");
            }

            finally
            {
                dbaConnection.con.Close();
            }
        }

        public void CreateTable()
        {
            DTItemRequests.Rows.Clear();
            DTItemRequests.Columns.Clear();
            DTItemRequests.Columns.Add("ItemID");
            DTItemRequests.Columns.Add("ItemName");
            DTItemRequests.Columns.Add("Qty");
            DTItemRequests.Columns.Add("Price");
            DTItemRequests.Columns.Add("Total");
            frmCreateItemRequests.dgvItemRequests.DataSource = DTItemRequests;
        }

        public void ShowData()
        {
            string spString;
            string partyItemDisplay = "";
            partyItemDisplay = frmCreateItemRequests.cboPartyItem.DisplayMember;
            if (partyItemDisplay == string.Empty)
                partyItemDisplay = "0";

            spString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", "0", "0", "5");
            AddCombo(frmCreateItemRequests.cboPartyItem, spString, "ItemName", "ItemID");

            frmCreateItemRequests.cboPartyItem.SelectedValue = Convert.ToInt32(partyItemDisplay);

            string usersDisplay = "";
            usersDisplay = frmCreateItemRequests.cboFullNames.DisplayMember;
            if (usersDisplay == string.Empty)
                usersDisplay = "0";

            spString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", "0", "0", "6");
            AddCombo(frmCreateItemRequests.cboFullNames, spString, "FullName", "UserID");

            frmCreateItemRequests.cboFullNames.SelectedValue = Convert.ToInt32(usersDisplay);

            frmCreateItemRequests.txtPrice.Text = "";
            frmCreateItemRequests.txtPrice.Enabled = false;
            frmCreateItemRequests.txtQty.Text = "";
            frmCreateItemRequests.txtQty.Enabled = false;
            frmCreateItemRequests.txtMax.Text = "";
            frmCreateItemRequests.txtMax.Enabled = false;

        }

        public void ItemSelectedChanged()
        {
            string spPartyItem = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", frmCreateItemRequests.cboPartyItem.SelectedValue.ToString(), "0", "7");
            DT = dbaConnection.SelectData(spPartyItem);
            if (DT.Rows.Count > 0)
            {
                frmCreateItemRequests.txtPrice.Text = DT.Rows[0]["Price"].ToString();
                frmCreateItemRequests.txtPrice.Enabled = false;
                frmCreateItemRequests.txtMax.Text = DT.Rows[0]["Qty"].ToString();
                frmCreateItemRequests.txtMax.Enabled = false;
                frmCreateItemRequests.txtQty.Text = "";
                frmCreateItemRequests.txtQty.Enabled = true;
                frmCreateItemRequests.txtQty.Focus();
            }
            else
            {
                frmCreateItemRequests.txtPrice.Text = "";
                frmCreateItemRequests.txtPrice.Enabled = false;
                frmCreateItemRequests.txtMax.Text = "";
                frmCreateItemRequests.txtMax.Enabled = false;
                frmCreateItemRequests.txtQty.Text = "";
                frmCreateItemRequests.txtQty.Enabled = false;

            }
        }

        public void CalculateTotal()
        {
            int Total = 0;
            if (DTItemRequests.Rows.Count > 0)
            {
                foreach (DataRow DR in DTItemRequests.Rows)
                    Total += Convert.ToInt32(DR["Total"]);
            }
            frmCreateItemRequests.lblTotalAmount.Text = Total.ToString();
        }

        public void AddClick()
        {
            int Ok = 0;

            if (Convert.ToInt32(frmCreateItemRequests.cboPartyItem.SelectedValue.ToString()) == 0)
            {
                MessageBox.Show("Please Choose Item");
                frmCreateItemRequests.cboPartyItem.Focus();
            }
            else if (frmCreateItemRequests.txtQty.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Qty");
                frmCreateItemRequests.txtQty.Focus();
            }
            else if (int.TryParse(frmCreateItemRequests.txtQty.Text, out Ok) == false)
            {
                MessageBox.Show("Qty Should Be Number");
                frmCreateItemRequests.txtQty.Focus();
                frmCreateItemRequests.txtQty.SelectAll();
            }
            else if (Convert.ToInt32(frmCreateItemRequests.txtQty.Text.Trim().ToString()) == 0)
            {
                MessageBox.Show(frmCreateItemRequests.cboPartyItem.Text + " Is Empty. Cannot Be Requested!");
                frmCreateItemRequests.txtQty.Focus();
                frmCreateItemRequests.txtQty.SelectAll();
            }
            else if (Convert.ToInt32(frmCreateItemRequests.txtQty.Text.Trim().ToString()) <= 0)
            {
                MessageBox.Show("Qty Cannot Be 0 Or Lower");
                frmCreateItemRequests.txtQty.Focus();
                frmCreateItemRequests.txtQty.SelectAll();
            }
            else if (Convert.ToInt32(frmCreateItemRequests.txtQty.Text.Trim().ToString()) > Convert.ToInt32(frmCreateItemRequests.txtMax.Text.Trim().ToString()))
            {
                MessageBox.Show("Qty Should Be Between 1 And " + frmCreateItemRequests.txtMax.Text.Trim().ToString());
                frmCreateItemRequests.txtQty.Focus();
                frmCreateItemRequests.txtQty.SelectAll();
            }
            else if (frmCreateItemRequests.txtPrice.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Price");
                frmCreateItemRequests.txtPrice.Focus();
            }
            else if (int.TryParse(frmCreateItemRequests.txtPrice.Text, out Ok) == false)
            {
                MessageBox.Show("Price Should Be Number");
                frmCreateItemRequests.txtPrice.Focus();
                frmCreateItemRequests.txtPrice.SelectAll();
            }
            else if (Convert.ToInt32(frmCreateItemRequests.txtPrice.Text.Trim().ToString()) < 1 || Convert.ToInt32(frmCreateItemRequests.txtPrice.Text.Trim().ToString()) > 10000000)
            {
                MessageBox.Show("Price Should Be Between 1 And 100-Lakh");
                frmCreateItemRequests.txtPrice.Focus();
                frmCreateItemRequests.txtPrice.SelectAll();
            }
            else
            {
                if (DTItemRequests.Rows.Count > 0)
                {
                    DataRow[] Arr_DR;
                    
                    Arr_DR = DTItemRequests.Select("ItemID='" + frmCreateItemRequests.cboPartyItem.SelectedValue.ToString() + "'");
                    

                    Count = Arr_DR.Length;
                    if (Count != 0)
                    {
                        MessageBox.Show("This Record Is Already Exist");
                        return;
                    }
                }
                DataRow DR = DTItemRequests.NewRow();
                DR["ItemID"] = frmCreateItemRequests.cboPartyItem.SelectedValue;
                DR["ItemName"] = frmCreateItemRequests.cboPartyItem.Text;
                DR["Qty"] = frmCreateItemRequests.txtQty.Text;
                DR["Price"] = frmCreateItemRequests.txtPrice.Text;
                DR["Total"] = Convert.ToInt32(frmCreateItemRequests.txtQty.Text) * Convert.ToInt32(frmCreateItemRequests.txtPrice.Text);
                DTItemRequests.Rows.Add(DR);
                frmCreateItemRequests.dgvItemRequests.DataSource = DTItemRequests;
                //addCount++;
                //frmCreateDonation.cboPartyItem.SelectedIndex = 0;
                //frmCreateItemRequests.cboPartyItem.SelectedIndex = 0;
                ItemSelectedChanged();
                CalculateTotal();
            }
        }

        public void RemoveClick()
        {
            if (DTItemRequests.Rows.Count <= 0)
            {
                MessageBox.Show("There Is No Data");
            }
            else if (frmCreateItemRequests.dgvItemRequests.CurrentRow.Cells["ItemID"].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else
            {
                DataRow[] Arr_DR = DTItemRequests.Select("ItemID='" + frmCreateItemRequests.dgvItemRequests.CurrentRow.Cells["ItemID"].Value.ToString() + "'");
                DataRow DR = Arr_DR[0];
                DR.Delete();
                frmCreateItemRequests.dgvItemRequests.DataSource = DTItemRequests;
                CalculateTotal();
            }
        }

        public void DtpDateValueChanged()
        {
            SPString = string.Format("SP_Select_ItemRequests N'{0}',N'{1}',N'{2}'", frmCreateItemRequests.dtpDate.Text, "0", "2");
            DT = dbaConnection.SelectData(SPString);
            int DateDiff = Convert.ToInt32(DT.Rows[0]["No"]);
            if (DateDiff > 0)
            {
                MessageBox.Show("Please Check RequestDate");
                frmCreateItemRequests.dtpDate.Text = DateTime.Now.ToShortDateString();
            }
            else if (DateDiff <= -7)
            {
                MessageBox.Show("Please Check RequestDate");
                frmCreateItemRequests.dtpDate.Text = DateTime.Now.ToShortDateString();
            }
        }

        public void SaveClick()
        {
            _ItemRequestsID = frmCreateItemRequests.RequestsID;
            //_ItemRequestsDetailID = frmCreateItemRequests.

            if (frmCreateItemRequests.cboFullNames.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose Users");
                frmCreateItemRequests.cboFullNames.Focus();
            }
            else if (DTItemRequests.Rows.Count <= 0)
            {
                MessageBox.Show("There Is No Data");
            }
            else
            {
                dbaItemRequests.RDATE = frmCreateItemRequests.dtpDate.Text;
                //dbaItemRequests.DNID = Convert.ToInt32(cboDonor.SelectedValue.ToString());
                dbaItemRequests.TOTALAMT = Convert.ToInt32(frmCreateItemRequests.lblTotalAmount.Text);
                dbaItemRequests.USERID = Convert.ToInt32(frmCreateItemRequests.cboFullNames.SelectedValue.ToString());
                dbaItemRequests.ACTION = 0;
                dbaItemRequests.SaveData();

                SPString = string.Format("SP_Select_ItemRequests N'{0}',N'{1}',N'{2}'", "0", "0", "1");
                DT = dbaConnection.SelectData(SPString);
                _ItemRequestsID = Convert.ToInt32(DT.Rows[0]["RequestID"].ToString());


                for (int i = 0; i < DTItemRequests.Rows.Count; i++)
                {     
                    
                    dbaPartyItem.ITEMID = Convert.ToInt32(DTItemRequests.Rows[i]["ItemID"].ToString());
                    dbaPartyItem.QTY = Convert.ToInt32(DTItemRequests.Rows[i]["Qty"].ToString());
                    dbaPartyItem.PRICE = Convert.ToInt32(DTItemRequests.Rows[i]["Price"].ToString());
                    dbaPartyItem.ACTION = 4;
                    dbaPartyItem.SaveData();
                    
                    

                    dbaItemRequestsDetail.RDID = _ItemRequestsDetailID;
                    dbaItemRequestsDetail.RID = _ItemRequestsID;
                    dbaItemRequestsDetail.ITEMID = Convert.ToInt32(DTItemRequests.Rows[i]["ItemID"].ToString());
                    dbaItemRequestsDetail.RQTY = Convert.ToInt32(DTItemRequests.Rows[i]["Qty"].ToString());
                    dbaItemRequestsDetail.PRICE = Convert.ToInt32(DTItemRequests.Rows[i]["Price"].ToString());
                    dbaItemRequestsDetail.ACTION = 0;
                    dbaItemRequestsDetail.SaveData();
                }

                MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                frmCreateItemRequests.Close();
            }
        }
    }
}
