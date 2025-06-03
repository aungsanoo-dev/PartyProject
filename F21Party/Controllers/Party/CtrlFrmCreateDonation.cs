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
    internal class CtrlFrmCreateDonation
    {
        private readonly frm_CreateDonation _frmCreateDonation;
        private readonly DbaDonation _dbaDonation = new DbaDonation();
        private readonly DbaDonationDetail _dbaDonationDetail = new DbaDonationDetail();
        private readonly DbaPartyItem _dbaPartyItem = new DbaPartyItem();
        private readonly DbaConnection _dbaConnection = new DbaConnection();
        private readonly DataTable _dTDonation = new DataTable();
        private DataTable _dt = new DataTable();
        private int _donationDetailID = 0;
        private int _donationID = 0;
        private string _spString = "";
        private int _count = 0;
        private int _addCount = 0;

        public CtrlFrmCreateDonation(frm_CreateDonation donationForm)
        {
            _frmCreateDonation = donationForm;
        }

        public void AddCombo(ComboBox cboCombo, string spString, string display, string value)
        {
            DataTable dtAccessSp = new DataTable();
            DataTable dtCombo = new DataTable();
            DataRow dr;

            dtCombo.Columns.Add(display);
            dtCombo.Columns.Add(value);

            dr = dtCombo.NewRow();
            dr[display] = "---Select---";
            dr[value] = 0;
            dtCombo.Rows.Add(dr);

            try
            {
                _dbaConnection.DataBaseConn();
                SqlDataAdapter adpt = new SqlDataAdapter(spString, _dbaConnection.con);
                adpt.Fill(dtAccessSp);
                for (int i = 0; i < dtAccessSp.Rows.Count; i++)
                {
                    dr = dtCombo.NewRow();

                    if (display == "FullName")
                    {
                        if (Convert.ToInt32(dtAccessSp.Rows[i][value]) == 1) // 1 is SuperAdmin UserID.
                        {
                            continue;
                        }
                    }

                    dr[display] = dtAccessSp.Rows[i][display];
                    dr[value] = dtAccessSp.Rows[i][value];
                    dtCombo.Rows.Add(dr);
                }

                if(display == "ItemName")
                {
                    dr = dtCombo.NewRow();
                    dr[display] = "*NewItem*";
                    dr[value] = -1;
                    dtCombo.Rows.Add(dr);
                }
               
                cboCombo.DisplayMember = display;
                cboCombo.ValueMember = value;
                cboCombo.DataSource = dtCombo;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error In AddCombo");
            }

            finally
            {
                _dbaConnection.con.Close();
            }
        }

        public void CreateTable()
        {
            _dTDonation.Rows.Clear();
            _dTDonation.Columns.Clear();
            _dTDonation.Columns.Add("ItemID");
            _dTDonation.Columns.Add("ItemName");
            _dTDonation.Columns.Add("Qty");
            _dTDonation.Columns.Add("Price");
            _dTDonation.Columns.Add("Total");
            _frmCreateDonation.dgvDonation.DataSource = _dTDonation;
        }

        public void ShowData()
        {
            //string spString;
            string partyItemDisplay = _frmCreateDonation.cboPartyItem.DisplayMember;
            if (partyItemDisplay == string.Empty)
                partyItemDisplay = "0";

            _spString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", "0", "0", "5");
            AddCombo(_frmCreateDonation.cboPartyItem, _spString, "ItemName", "ItemID");

            _frmCreateDonation.cboPartyItem.SelectedValue = Convert.ToInt32(partyItemDisplay);

            string usersDisplay = _frmCreateDonation.cboFullNames.DisplayMember;
            if (usersDisplay == string.Empty)
                usersDisplay = "0";

            _spString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", "0", "0", "6");
            AddCombo(_frmCreateDonation.cboFullNames, _spString, "FullName", "UserID");

            _frmCreateDonation.cboFullNames.SelectedValue = Convert.ToInt32(usersDisplay);

            _frmCreateDonation.txtPrice.Enabled = false;
            _frmCreateDonation.txtQty.Enabled = false;

        }

        public void ItemSelectedChanged()
        {
            _spString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", _frmCreateDonation.cboPartyItem.SelectedValue.ToString(), "0", "7");
            _dt = _dbaConnection.SelectData(_spString);
            if (_dt.Rows.Count > 0)
            {
                _frmCreateDonation.txtNewItem.Visible = false;
                _frmCreateDonation.txtPrice.Text = _dt.Rows[0]["Price"].ToString();
                _frmCreateDonation.txtPrice.Enabled = false;
                _frmCreateDonation.txtQty.Text = "";
                _frmCreateDonation.txtQty.Enabled = true;
            }
            else if (_frmCreateDonation.cboPartyItem.SelectedValue.ToString() == "0")
            {
                _frmCreateDonation.txtNewItem.Visible = false;
                _frmCreateDonation.txtPrice.Text = "";
                _frmCreateDonation.txtPrice.Enabled = false;
                _frmCreateDonation.txtQty.Text = "";
                _frmCreateDonation.txtQty.Enabled = false;
            }
            else if (_frmCreateDonation.cboPartyItem.SelectedValue.ToString() == "-1")
            {
                _frmCreateDonation.txtNewItem.Visible = true;
                _frmCreateDonation.txtPrice.Text = "";
                _frmCreateDonation.txtPrice.Enabled = true;
                _frmCreateDonation.txtQty.Text = "";
                _frmCreateDonation.txtQty.Enabled = true;
                _frmCreateDonation.txtQty.Focus();
            }
            else
            {
                _frmCreateDonation.txtNewItem.Visible = false;
                _frmCreateDonation.txtPrice.Text = "";
                _frmCreateDonation.txtPrice.Enabled = true;
                _frmCreateDonation.txtQty.Text = "";
                _frmCreateDonation.txtQty.Enabled = true;
                _frmCreateDonation.txtQty.Focus();
            }
        }

        public void CalculateTotal()
        {
            int total = 0;
            if (_dTDonation.Rows.Count > 0)
            {
                foreach (DataRow dr in _dTDonation.Rows)
                    total += Convert.ToInt32(dr["Total"]);
            }
            _frmCreateDonation.lblTotalAmount.Text = total.ToString();
        }

        public void AddClick()
        {
            int ok = 0;
            if (Convert.ToInt32(_frmCreateDonation.cboPartyItem.SelectedValue.ToString()) == -1)
            {
                _spString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", _frmCreateDonation.txtNewItem.Text.Trim().ToString(), "0", "8");
                _dt = _dbaConnection.SelectData(_spString);
                if (_dt.Rows.Count > 0)
                {
                    MessageBox.Show("This Item is already exist. Please choose in Item box!");
                    _frmCreateDonation.cboPartyItem.Focus();
                    return;
                }
            }

            if (Convert.ToInt32(_frmCreateDonation.cboPartyItem.SelectedValue.ToString()) == 0)
            {
                MessageBox.Show("Please Choose Item");
                _frmCreateDonation.cboPartyItem.Focus();
            }
            else if(Convert.ToInt32(_frmCreateDonation.cboPartyItem.SelectedValue.ToString()) == -1 && _frmCreateDonation.txtNewItem.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Item Name");
                _frmCreateDonation.txtNewItem.Focus();
            }
            else if (_frmCreateDonation.txtQty.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Qty");
                _frmCreateDonation.txtQty.Focus();
            }
            else if (int.TryParse(_frmCreateDonation.txtQty.Text, out ok) == false)
            {
                MessageBox.Show("Qty Should Be Number");
                _frmCreateDonation.txtQty.Focus();
                _frmCreateDonation.txtQty.SelectAll();
            }
            else if (Convert.ToInt32(_frmCreateDonation.txtQty.Text.Trim().ToString()) <= 0 || Convert.ToInt32(_frmCreateDonation.txtQty.Text.Trim().ToString()) > 10000)
            {
                MessageBox.Show("Qty Should Be Between 1 And 10-Thousand");
                _frmCreateDonation.txtQty.Focus();
                _frmCreateDonation.txtQty.SelectAll();
            }
            else if (_frmCreateDonation.txtPrice.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Price");
                _frmCreateDonation.txtPrice.Focus();
            }
            else if (int.TryParse(_frmCreateDonation.txtPrice.Text, out ok) == false)
            {
                MessageBox.Show("Price Should Be Number");
                _frmCreateDonation.txtPrice.Focus();
                _frmCreateDonation.txtPrice.SelectAll();
            }
            else if (Convert.ToInt32(_frmCreateDonation.txtPrice.Text.Trim().ToString()) < 1 || Convert.ToInt32(_frmCreateDonation.txtPrice.Text.Trim().ToString()) > 10000000)
            {
                MessageBox.Show("Price Should Be Between 1 And 100-Lakh");
                _frmCreateDonation.txtPrice.Focus();
                _frmCreateDonation.txtPrice.SelectAll();
            }
            else
            {
                if (_dTDonation.Rows.Count > 0)
                {
                    DataRow[] arrDr;
                    if (_frmCreateDonation.cboPartyItem.SelectedValue.ToString() == "-1")
                    {
                        arrDr = _dTDonation.Select("ItemName='" + Regex.Replace(_frmCreateDonation.txtNewItem.Text.Trim(), @"\s+", " ") + "'");
                    }
                    else
                    {
                        arrDr = _dTDonation.Select("ItemID='" + _frmCreateDonation.cboPartyItem.SelectedValue.ToString() + "'");
                    }

                    _count = arrDr.Length;
                    if (_count != 0)
                    {
                        MessageBox.Show("This Record Is Already Exist");
                        return;
                    }
                }
                DataRow dr = _dTDonation.NewRow();
                if (_frmCreateDonation.cboPartyItem.SelectedValue.ToString() == "-1")
                {
                    dr["ItemID"] = "I" + _addCount.ToString();
                    dr["ItemName"] = Regex.Replace(_frmCreateDonation.txtNewItem.Text.Trim(), @"\s+", " ");
                }
                else
                {
                    dr["ItemID"] = _frmCreateDonation.cboPartyItem.SelectedValue.ToString();
                    dr["ItemName"] = _frmCreateDonation.cboPartyItem.Text;
                }
                
                dr["Qty"] = _frmCreateDonation.txtQty.Text;
                dr["Price"] = _frmCreateDonation.txtPrice.Text;
                dr["Total"] = Convert.ToInt32(_frmCreateDonation.txtQty.Text) * Convert.ToInt32(_frmCreateDonation.txtPrice.Text);
                _dTDonation.Rows.Add(dr);
                _frmCreateDonation.dgvDonation.DataSource = _dTDonation;
                _addCount++;
                //frmCreateDonation.cboPartyItem.SelectedIndex = 0;
                ItemSelectedChanged();
                CalculateTotal();
            }
        }

        public void RemoveClick()
        {
            if (_dTDonation.Rows.Count <= 0)
            {
                MessageBox.Show("There Is No Data");
            }
            else if (_frmCreateDonation.dgvDonation.CurrentRow.Cells["ItemID"].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else
            {
                DataRow[] arrDr = _dTDonation.Select("ItemID='" + _frmCreateDonation.dgvDonation.CurrentRow.Cells["ItemID"].Value.ToString() + "'");
                DataRow dr = arrDr[0];
                dr.Delete();
                _frmCreateDonation.dgvDonation.DataSource = _dTDonation;
                CalculateTotal();
            }
        }

        public void DtpDateValueChanged()
        {
            _spString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", _frmCreateDonation.dtpDate.Text, "0", "2");
            _dt = _dbaConnection.SelectData(_spString);
            int dateDiff = Convert.ToInt32(_dt.Rows[0]["No"]);
            if (dateDiff > 0)
            {
                MessageBox.Show("Please Check DonationDate");
                _frmCreateDonation.dtpDate.Text = DateTime.Now.ToShortDateString();
            }
            else if (dateDiff <= -7)
            {
                MessageBox.Show("Please Check DonationDate");
                _frmCreateDonation.dtpDate.Text = DateTime.Now.ToShortDateString();
            }
        }

        public void SaveClick()
        {
            _donationID = _frmCreateDonation.DonationID;
            //_DonationDetailID = 

            if (_frmCreateDonation.cboFullNames.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose Users");
                _frmCreateDonation.cboFullNames.Focus();
            }
            else if (_dTDonation.Rows.Count <= 0)
            {
                MessageBox.Show("There Is No Data");
            }
            else
            {
                _dbaDonation.DDATE = _frmCreateDonation.dtpDate.Text;
                //dbaDonation.DNID = Convert.ToInt32(cboDonor.SelectedValue.ToString());
                _dbaDonation.TOTALAMT = Convert.ToInt32(_frmCreateDonation.lblTotalAmount.Text);
                _dbaDonation.USERID = Convert.ToInt32(_frmCreateDonation.cboFullNames.SelectedValue.ToString());
                _dbaDonation.ACTION = 0;
                _dbaDonation.SaveData();

                _spString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", "0", "0", "1");
                _dt = _dbaConnection.SelectData(_spString);
                _donationID = Convert.ToInt32(_dt.Rows[0]["DonationID"].ToString());


                for (int i = 0; i < _dTDonation.Rows.Count; i++)
                {
                    if (_dTDonation.Rows[i]["ItemID"].ToString().Contains("I"))
                    {
                        _dbaPartyItem.ITEMID = 0;
                        _dbaPartyItem.ITEMNAME = _dTDonation.Rows[i]["ItemName"].ToString();
                        _dbaPartyItem.QTY = Convert.ToInt32(_dTDonation.Rows[i]["Qty"].ToString());
                        _dbaPartyItem.PRICE = Convert.ToInt32(_dTDonation.Rows[i]["Price"].ToString());
                        _dbaPartyItem.ACTION = 0;
                        _dbaPartyItem.SaveData();

                        _spString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", _dTDonation.Rows[i]["ItemName"].ToString(), "0", "8");
                        _dt = _dbaConnection.SelectData(_spString);

                        _dbaDonationDetail.ITEMID = Convert.ToInt32(_dt.Rows[0]["ItemID"].ToString());
                    }
                    else
                    {
                        _dbaPartyItem.ITEMID = Convert.ToInt32(_dTDonation.Rows[i]["ItemID"].ToString());
                        _dbaPartyItem.QTY = Convert.ToInt32(_dTDonation.Rows[i]["Qty"].ToString());
                        _dbaPartyItem.PRICE = Convert.ToInt32(_dTDonation.Rows[i]["Price"].ToString());
                        _dbaPartyItem.ACTION = 3;
                        _dbaPartyItem.SaveData();
                        _dbaDonationDetail.ITEMID = Convert.ToInt32(_dTDonation.Rows[i]["ItemID"].ToString());
                    }

                    _dbaDonationDetail.DDID = _donationDetailID;
                    _dbaDonationDetail.DID = _donationID;
                    
                    _dbaDonationDetail.DQTY = Convert.ToInt32(_dTDonation.Rows[i]["Qty"].ToString());
                    _dbaDonationDetail.PRICE = Convert.ToInt32(_dTDonation.Rows[i]["Price"].ToString());
                    _dbaDonationDetail.ACTION = 0;
                    _dbaDonationDetail.SaveData();
                }

                MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                _frmCreateDonation.Close();
            }
        }
    }
}
