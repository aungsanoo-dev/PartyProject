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

namespace F21Party.Controllers.Party
{
    internal class CtrlFrmCreateDonation
    {
        public frm_CreateDonation frmCreateDonation;

        public CtrlFrmCreateDonation(frm_CreateDonation donationForm)
        {
            frmCreateDonation = donationForm;
        }

        DbaDonation dbaDonation = new DbaDonation();
        DbaDonationDetail dbaDonationDetail = new DbaDonationDetail();
        DbaPartyItem dbaPartyItem = new DbaPartyItem();
        DbaConnection dbaConnection = new DbaConnection();

        DataTable DT = new DataTable();
        DataTable DTDonation = new DataTable();
        int _DonationDetailID = 0;
        int _DonationID = 0;
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

                if(Display == "ItemName")
                {
                    Dr = DTCombo.NewRow();
                    Dr[Display] = "*NewItem*";
                    Dr[Value] = -1;
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

        //public void ShowCombo()
        //{
        //    string _PositionDisplay = "";
        //    string spString;
        //    _PositionDisplay = frmCreateUser.cboPosition.DisplayMember;
        //    if (_PositionDisplay == string.Empty)
        //        _PositionDisplay = "0";

        //    // For Position Combobox
        //    spString = string.Format("SP_Select_Position N'{0}',N'{1}',N'{2}'", "0", "0", "0");

        //    AddCombo(frmCreateUser.cboPosition, spString, "PositionName", "PositionID");

        //    frmCreateUser.cboPosition.SelectedValue = Convert.ToInt32(_PositionDisplay); //This is in the box value you see
        //    //positionLevelIndex = frmCreateUser.cboPosition.SelectedIndex;

        //}
        public void CreateTable()
        {
            DTDonation.Rows.Clear();
            DTDonation.Columns.Clear();
            DTDonation.Columns.Add("ItemID");
            DTDonation.Columns.Add("ItemName");
            DTDonation.Columns.Add("Qty");
            DTDonation.Columns.Add("Price");
            DTDonation.Columns.Add("Total");
            frmCreateDonation.dgvDonation.DataSource = DTDonation;
        }

        public void ShowData()
        {
            string spString;
            string partyItemDisplay = "";
            partyItemDisplay = frmCreateDonation.cboPartyItem.DisplayMember;
            if (partyItemDisplay == string.Empty)
                partyItemDisplay = "0";

            spString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", "0", "0", "5");
            AddCombo(frmCreateDonation.cboPartyItem, spString, "ItemName", "ItemID");

            frmCreateDonation.cboPartyItem.SelectedValue = Convert.ToInt32(partyItemDisplay);

            string usersDisplay = "";
            usersDisplay = frmCreateDonation.cboFullNames.DisplayMember;
            if (usersDisplay == string.Empty)
                usersDisplay = "0";

            spString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", "0", "0", "6");
            AddCombo(frmCreateDonation.cboFullNames, spString, "FullName", "UserID");

            frmCreateDonation.cboFullNames.SelectedValue = Convert.ToInt32(usersDisplay);

            frmCreateDonation.txtPrice.Enabled = false;
            frmCreateDonation.txtQty.Enabled = false;

        }

        public void ItemSelectedChanged()
        {
            string spPartyItem = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", frmCreateDonation.cboPartyItem.SelectedValue.ToString(), "0", "7");
            DT = dbaConnection.SelectData(spPartyItem);
            if (DT.Rows.Count > 0)
            {
                frmCreateDonation.txtNewItem.Visible = false;
                frmCreateDonation.txtPrice.Text = DT.Rows[0]["Price"].ToString();
                frmCreateDonation.txtPrice.Enabled = false;
                frmCreateDonation.txtQty.Text = "";
                frmCreateDonation.txtQty.Enabled = true;
            }
            else if (frmCreateDonation.cboPartyItem.SelectedValue.ToString() == "0")
            {
                frmCreateDonation.txtNewItem.Visible = false;
                frmCreateDonation.txtPrice.Text = "";
                frmCreateDonation.txtPrice.Enabled = false;
                frmCreateDonation.txtQty.Text = "";
                frmCreateDonation.txtQty.Enabled = false;
            }
            else if (frmCreateDonation.cboPartyItem.SelectedValue.ToString() == "-1")
            {
                frmCreateDonation.txtNewItem.Visible = true;
                frmCreateDonation.txtPrice.Text = "";
                frmCreateDonation.txtPrice.Enabled = true;
                frmCreateDonation.txtQty.Text = "";
                frmCreateDonation.txtQty.Enabled = true;
                frmCreateDonation.txtQty.Focus();
            }
            else
            {
                frmCreateDonation.txtNewItem.Visible = false;
                frmCreateDonation.txtPrice.Text = "";
                frmCreateDonation.txtPrice.Enabled = true;
                frmCreateDonation.txtQty.Text = "";
                frmCreateDonation.txtQty.Enabled = true;
                frmCreateDonation.txtQty.Focus();
            }
            

        }

        public void CalculateTotal()
        {
            int Total = 0;
            if (DTDonation.Rows.Count > 0)
            {
                foreach (DataRow DR in DTDonation.Rows)
                    Total += Convert.ToInt32(DR["Total"]);
            }
            frmCreateDonation.lblTotalAmount.Text = Total.ToString();
        }

        public void AddClick()
        {
            int Ok = 0;
            if (Convert.ToInt32(frmCreateDonation.cboPartyItem.SelectedValue.ToString()) == -1)
            {
                string spPartyItem = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", frmCreateDonation.txtNewItem.Text.Trim().ToString(), "0", "8");
                DT = dbaConnection.SelectData(spPartyItem);
                if (DT.Rows.Count > 0)
                {
                    MessageBox.Show("This Item is already exist. Please choose in Item box!");
                    frmCreateDonation.cboPartyItem.Focus();
                    return;
                }
            }

            if (Convert.ToInt32(frmCreateDonation.cboPartyItem.SelectedValue.ToString()) == 0)
            {
                MessageBox.Show("Please Choose Item");
                frmCreateDonation.cboPartyItem.Focus();
            }
            else if(Convert.ToInt32(frmCreateDonation.cboPartyItem.SelectedValue.ToString()) == -1 && frmCreateDonation.txtNewItem.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Item Name");
                frmCreateDonation.txtNewItem.Focus();
            }
            else if (frmCreateDonation.txtQty.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Qty");
                frmCreateDonation.txtQty.Focus();
            }
            else if (int.TryParse(frmCreateDonation.txtQty.Text, out Ok) == false)
            {
                MessageBox.Show("Qty Should Be Number");
                frmCreateDonation.txtQty.Focus();
                frmCreateDonation.txtQty.SelectAll();
            }
            else if (Convert.ToInt32(frmCreateDonation.txtQty.Text.Trim().ToString()) <= 0 || Convert.ToInt32(frmCreateDonation.txtQty.Text.Trim().ToString()) > 10000)
            {
                MessageBox.Show("Qty Should Be Between 1 And 10-Thousand");
                frmCreateDonation.txtQty.Focus();
                frmCreateDonation.txtQty.SelectAll();
            }
            else if (frmCreateDonation.txtPrice.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Price");
                frmCreateDonation.txtPrice.Focus();
            }
            else if (int.TryParse(frmCreateDonation.txtPrice.Text, out Ok) == false)
            {
                MessageBox.Show("Price Should Be Number");
                frmCreateDonation.txtPrice.Focus();
                frmCreateDonation.txtPrice.SelectAll();
            }
            else if (Convert.ToInt32(frmCreateDonation.txtPrice.Text.Trim().ToString()) < 1 || Convert.ToInt32(frmCreateDonation.txtPrice.Text.Trim().ToString()) > 10000000)
            {
                MessageBox.Show("Price Should Be Between 1 And 100-Lakh");
                frmCreateDonation.txtPrice.Focus();
                frmCreateDonation.txtPrice.SelectAll();
            }
            else
            {
                if (DTDonation.Rows.Count > 0)
                {
                    DataRow[] Arr_DR;
                    if (frmCreateDonation.cboPartyItem.SelectedValue.ToString() == "-1")
                    {
                        Arr_DR = DTDonation.Select("ItemName='" + Regex.Replace(frmCreateDonation.txtNewItem.Text.Trim(), @"\s+", " ") + "'");
                    }
                    else
                    {
                        Arr_DR = DTDonation.Select("ItemID='" + frmCreateDonation.cboPartyItem.SelectedValue.ToString() + "'");
                    }

                    Count = Arr_DR.Length;
                    if (Count != 0)
                    {
                        MessageBox.Show("This Record Is Already Exist");
                        return;
                    }
                }
                DataRow DR = DTDonation.NewRow();
                if (frmCreateDonation.cboPartyItem.SelectedValue.ToString() == "-1")
                {
                    DR["ItemID"] = "I" + addCount.ToString();
                    DR["ItemName"] = Regex.Replace(frmCreateDonation.txtNewItem.Text.Trim(), @"\s+", " ");
                }
                else
                {
                    DR["ItemID"] = frmCreateDonation.cboPartyItem.SelectedValue.ToString();
                    DR["ItemName"] = frmCreateDonation.cboPartyItem.Text;
                }
                
                DR["Qty"] = frmCreateDonation.txtQty.Text;
                DR["Price"] = frmCreateDonation.txtPrice.Text;
                DR["Total"] = Convert.ToInt32(frmCreateDonation.txtQty.Text) * Convert.ToInt32(frmCreateDonation.txtPrice.Text);
                DTDonation.Rows.Add(DR);
                frmCreateDonation.dgvDonation.DataSource = DTDonation;
                addCount++;
                //frmCreateDonation.cboPartyItem.SelectedIndex = 0;
                ItemSelectedChanged();
                CalculateTotal();
            }
        }

        public void RemoveClick()
        {
            if (DTDonation.Rows.Count <= 0)
            {
                MessageBox.Show("There Is No Data");
            }
            else if (frmCreateDonation.dgvDonation.CurrentRow.Cells["ItemID"].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else
            {
                DataRow[] Arr_DR = DTDonation.Select("ItemID='" + frmCreateDonation.dgvDonation.CurrentRow.Cells["ItemID"].Value.ToString() + "'");
                DataRow DR = Arr_DR[0];
                DR.Delete();
                frmCreateDonation.dgvDonation.DataSource = DTDonation;
                CalculateTotal();
            }
        }

        public void DtpDateValueChanged()
        {
            SPString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", frmCreateDonation.dtpDate.Text, "0", "2");
            DT = dbaConnection.SelectData(SPString);
            int DateDiff = Convert.ToInt32(DT.Rows[0]["No"]);
            if (DateDiff > 0)
            {
                MessageBox.Show("Please Check DonationDate");
                frmCreateDonation.dtpDate.Text = DateTime.Now.ToShortDateString();
            }
            else if (DateDiff <= -7)
            {
                MessageBox.Show("Please Check DonationDate");
                frmCreateDonation.dtpDate.Text = DateTime.Now.ToShortDateString();
            }
        }

        public void SaveClick()
        {
            _DonationID = frmCreateDonation.DonationID;
            //_DonationDetailID = 

            if (frmCreateDonation.cboFullNames.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose Users");
                frmCreateDonation.cboFullNames.Focus();
            }
            else if (DTDonation.Rows.Count <= 0)
            {
                MessageBox.Show("There Is No Data");
            }
            else
            {
                dbaDonation.DDATE = frmCreateDonation.dtpDate.Text;
                //dbaDonation.DNID = Convert.ToInt32(cboDonor.SelectedValue.ToString());
                dbaDonation.TOTALAMT = Convert.ToInt32(frmCreateDonation.lblTotalAmount.Text);
                dbaDonation.USERID = Convert.ToInt32(frmCreateDonation.cboFullNames.SelectedValue.ToString());
                dbaDonation.ACTION = 0;
                dbaDonation.SaveData();

                SPString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", "0", "0", "1");
                DT = dbaConnection.SelectData(SPString);
                _DonationID = Convert.ToInt32(DT.Rows[0]["DonationID"].ToString());


                for (int i = 0; i < DTDonation.Rows.Count; i++)
                {
                    if (DTDonation.Rows[i]["ItemID"].ToString().Contains("I"))
                    {
                        dbaPartyItem.ITEMID = 0;
                        dbaPartyItem.ITEMNAME = DTDonation.Rows[i]["ItemName"].ToString();
                        dbaPartyItem.QTY = Convert.ToInt32(DTDonation.Rows[i]["Qty"].ToString());
                        dbaPartyItem.PRICE = Convert.ToInt32(DTDonation.Rows[i]["Price"].ToString());
                        dbaPartyItem.ACTION = 0;
                        dbaPartyItem.SaveData();

                        string spSearchItem = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", DTDonation.Rows[i]["ItemName"].ToString(), "0", "8");
                        DT = dbaConnection.SelectData(spSearchItem);

                        dbaDonationDetail.ITEMID = Convert.ToInt32(DT.Rows[0]["ItemID"].ToString());
                    }
                    else
                    {
                        dbaPartyItem.ITEMID = Convert.ToInt32(DTDonation.Rows[i]["ItemID"].ToString());
                        dbaPartyItem.QTY = Convert.ToInt32(DTDonation.Rows[i]["Qty"].ToString());
                        dbaPartyItem.PRICE = Convert.ToInt32(DTDonation.Rows[i]["Price"].ToString());
                        dbaPartyItem.ACTION = 3;
                        dbaPartyItem.SaveData();
                        dbaDonationDetail.ITEMID = Convert.ToInt32(DTDonation.Rows[i]["ItemID"].ToString());
                    }

                    dbaDonationDetail.DDID = _DonationDetailID;
                    dbaDonationDetail.DID = _DonationID;
                    
                    dbaDonationDetail.DQTY = Convert.ToInt32(DTDonation.Rows[i]["Qty"].ToString());
                    dbaDonationDetail.PRICE = Convert.ToInt32(DTDonation.Rows[i]["Price"].ToString());
                    dbaDonationDetail.ACTION = 0;
                    dbaDonationDetail.SaveData();
                }

                MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                frmCreateDonation.Close();
            }
        }
    }
}
