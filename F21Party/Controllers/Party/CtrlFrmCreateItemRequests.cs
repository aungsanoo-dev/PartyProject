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
        private readonly frm_CreateItemRequests _frmCreateItemRequests;
        private readonly DbaItemRequests _dbaItemRequests = new DbaItemRequests();
        private readonly DbaItemRequestsDetail _dbaItemRequestsDetail = new DbaItemRequestsDetail();
        private readonly DbaPartyItem _dbaPartyItem = new DbaPartyItem();
        private readonly DbaConnection _dbaConnection = new DbaConnection();
        private DataTable _dt = new DataTable();
        private readonly DataTable _dtItemRequests = new DataTable();
        private int _itemRequestsDetailID = 0;
        private int _itemRequestsID = 0;
        private int _count = 0;
        private string _spString = "";

        public CtrlFrmCreateItemRequests(frm_CreateItemRequests itemRequestsForm)
        {
            _frmCreateItemRequests = itemRequestsForm;
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
            _dtItemRequests.Rows.Clear();
            _dtItemRequests.Columns.Clear();
            _dtItemRequests.Columns.Add("ItemID");
            _dtItemRequests.Columns.Add("ItemName");
            _dtItemRequests.Columns.Add("Qty");
            _dtItemRequests.Columns.Add("Price");
            _dtItemRequests.Columns.Add("Total");
            _frmCreateItemRequests.dgvItemRequests.DataSource = _dtItemRequests;
        }

        public void ShowData()
        {
            string partyItemDisplay = _frmCreateItemRequests.cboPartyItem.DisplayMember;
            if (partyItemDisplay == string.Empty)
                partyItemDisplay = "0";

            _spString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", "0", "0", "5");
            AddCombo(_frmCreateItemRequests.cboPartyItem, _spString, "ItemName", "ItemID");

            _frmCreateItemRequests.cboPartyItem.SelectedValue = Convert.ToInt32(partyItemDisplay);

            string usersDisplay = _frmCreateItemRequests.cboFullNames.DisplayMember;
            if (usersDisplay == string.Empty)
                usersDisplay = "0";

            _spString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", "0", "0", "6");
            AddCombo(_frmCreateItemRequests.cboFullNames, _spString, "FullName", "UserID");

            _frmCreateItemRequests.cboFullNames.SelectedValue = Convert.ToInt32(usersDisplay);

            _frmCreateItemRequests.txtPrice.Text = "";
            _frmCreateItemRequests.txtPrice.Enabled = false;
            _frmCreateItemRequests.txtQty.Text = "";
            _frmCreateItemRequests.txtQty.Enabled = false;
            _frmCreateItemRequests.txtMax.Text = "";
            _frmCreateItemRequests.txtMax.Enabled = false;

        }

        public void ItemSelectedChanged()
        {
            _spString = string.Format("SP_Select_Donation N'{0}',N'{1}',N'{2}'", _frmCreateItemRequests.cboPartyItem.SelectedValue.ToString(), "0", "7");
            _dt = _dbaConnection.SelectData(_spString);
            if (_dt.Rows.Count > 0)
            {
                _frmCreateItemRequests.txtPrice.Text = _dt.Rows[0]["Price"].ToString();
                _frmCreateItemRequests.txtPrice.Enabled = false;
                _frmCreateItemRequests.txtMax.Text = _dt.Rows[0]["Qty"].ToString();
                _frmCreateItemRequests.txtMax.Enabled = false;
                _frmCreateItemRequests.txtQty.Text = "";
                _frmCreateItemRequests.txtQty.Enabled = true;
                _frmCreateItemRequests.txtQty.Focus();
            }
            else
            {
                _frmCreateItemRequests.txtPrice.Text = "";
                _frmCreateItemRequests.txtPrice.Enabled = false;
                _frmCreateItemRequests.txtMax.Text = "";
                _frmCreateItemRequests.txtMax.Enabled = false;
                _frmCreateItemRequests.txtQty.Text = "";
                _frmCreateItemRequests.txtQty.Enabled = false;
            }
        }

        public void CalculateTotal()
        {
            int total = 0;
            if (_dtItemRequests.Rows.Count > 0)
            {
                foreach (DataRow dr in _dtItemRequests.Rows)
                    total += Convert.ToInt32(dr["Total"]);
            }
            _frmCreateItemRequests.lblTotalAmount.Text = total.ToString();
        }

        public void AddClick()
        {
            int ok = 0;

            if (Convert.ToInt32(_frmCreateItemRequests.cboPartyItem.SelectedValue.ToString()) == 0)
            {
                MessageBox.Show("Please Choose Item");
                _frmCreateItemRequests.cboPartyItem.Focus();
            }
            else if (_frmCreateItemRequests.txtQty.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Qty");
                _frmCreateItemRequests.txtQty.Focus();
            }
            else if (int.TryParse(_frmCreateItemRequests.txtQty.Text, out ok) == false)
            {
                MessageBox.Show("Qty Should Be Number");
                _frmCreateItemRequests.txtQty.Focus();
                _frmCreateItemRequests.txtQty.SelectAll();
            }
            else if (Convert.ToInt32(_frmCreateItemRequests.txtQty.Text.Trim().ToString()) == 0)
            {
                MessageBox.Show(_frmCreateItemRequests.cboPartyItem.Text + " Is Empty. Cannot Be Requested!");
                _frmCreateItemRequests.txtQty.Focus();
                _frmCreateItemRequests.txtQty.SelectAll();
            }
            else if (Convert.ToInt32(_frmCreateItemRequests.txtQty.Text.Trim().ToString()) <= 0)
            {
                MessageBox.Show("Qty Cannot Be 0 Or Lower");
                _frmCreateItemRequests.txtQty.Focus();
                _frmCreateItemRequests.txtQty.SelectAll();
            }
            else if (Convert.ToInt32(_frmCreateItemRequests.txtQty.Text.Trim().ToString()) > Convert.ToInt32(_frmCreateItemRequests.txtMax.Text.Trim().ToString()))
            {
                MessageBox.Show("Qty Should Be Between 1 And " + _frmCreateItemRequests.txtMax.Text.Trim().ToString());
                _frmCreateItemRequests.txtQty.Focus();
                _frmCreateItemRequests.txtQty.SelectAll();
            }
            else if (_frmCreateItemRequests.txtPrice.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Price");
                _frmCreateItemRequests.txtPrice.Focus();
            }
            else if (int.TryParse(_frmCreateItemRequests.txtPrice.Text, out ok) == false)
            {
                MessageBox.Show("Price Should Be Number");
                _frmCreateItemRequests.txtPrice.Focus();
                _frmCreateItemRequests.txtPrice.SelectAll();
            }
            else if (Convert.ToInt32(_frmCreateItemRequests.txtPrice.Text.Trim().ToString()) < 1 || Convert.ToInt32(_frmCreateItemRequests.txtPrice.Text.Trim().ToString()) > 10000000)
            {
                MessageBox.Show("Price Should Be Between 1 And 100-Lakh");
                _frmCreateItemRequests.txtPrice.Focus();
                _frmCreateItemRequests.txtPrice.SelectAll();
            }
            else
            {
                if (_dtItemRequests.Rows.Count > 0)
                {
                    DataRow[] arrDr;
                    
                    arrDr = _dtItemRequests.Select("ItemID='" + _frmCreateItemRequests.cboPartyItem.SelectedValue.ToString() + "'");
                    
                    _count = arrDr.Length;
                    if (_count != 0)
                    {
                        MessageBox.Show("This Record Is Already Exist");
                        return;
                    }
                }
                DataRow dr = _dtItemRequests.NewRow();
                dr["ItemID"] = _frmCreateItemRequests.cboPartyItem.SelectedValue;
                dr["ItemName"] = _frmCreateItemRequests.cboPartyItem.Text;
                dr["Qty"] = _frmCreateItemRequests.txtQty.Text;
                dr["Price"] = _frmCreateItemRequests.txtPrice.Text;
                dr["Total"] = Convert.ToInt32(_frmCreateItemRequests.txtQty.Text) * Convert.ToInt32(_frmCreateItemRequests.txtPrice.Text);
                _dtItemRequests.Rows.Add(dr);
                _frmCreateItemRequests.dgvItemRequests.DataSource = _dtItemRequests;
                //addCount++;
                //frmCreateDonation.cboPartyItem.SelectedIndex = 0;
                //frmCreateItemRequests.cboPartyItem.SelectedIndex = 0;
                ItemSelectedChanged();
                CalculateTotal();
            }
        }

        public void RemoveClick()
        {
            if (_dtItemRequests.Rows.Count <= 0)
            {
                MessageBox.Show("There Is No Data");
            }
            else if (_frmCreateItemRequests.dgvItemRequests.CurrentRow.Cells["ItemID"].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else
            {
                DataRow[] arrDr = _dtItemRequests.Select("ItemID='" + _frmCreateItemRequests.dgvItemRequests.CurrentRow.Cells["ItemID"].Value.ToString() + "'");
                DataRow dr = arrDr[0];
                dr.Delete();
                _frmCreateItemRequests.dgvItemRequests.DataSource = _dtItemRequests;
                CalculateTotal();
            }
        }

        public void DtpDateValueChanged()
        {
            _spString = string.Format("SP_Select_ItemRequests N'{0}',N'{1}',N'{2}'", _frmCreateItemRequests.dtpDate.Text, "0", "2");
            _dt = _dbaConnection.SelectData(_spString);
            int dateDiff = Convert.ToInt32(_dt.Rows[0]["No"]);
            if (dateDiff > 0)
            {
                MessageBox.Show("Please Check RequestDate");
                _frmCreateItemRequests.dtpDate.Text = DateTime.Now.ToShortDateString();
            }
            else if (dateDiff <= -7)
            {
                MessageBox.Show("Please Check RequestDate");
                _frmCreateItemRequests.dtpDate.Text = DateTime.Now.ToShortDateString();
            }
        }

        public void SaveClick()
        {
            _itemRequestsID = _frmCreateItemRequests.RequestsID;
            //_ItemRequestsDetailID = frmCreateItemRequests.

            if (_frmCreateItemRequests.cboFullNames.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose Users");
                _frmCreateItemRequests.cboFullNames.Focus();
            }
            else if (_dtItemRequests.Rows.Count <= 0)
            {
                MessageBox.Show("There Is No Data");
            }
            else
            {
                _dbaItemRequests.RDATE = _frmCreateItemRequests.dtpDate.Text;
                //dbaItemRequests.DNID = Convert.ToInt32(cboDonor.SelectedValue.ToString());
                _dbaItemRequests.TOTALAMT = Convert.ToInt32(_frmCreateItemRequests.lblTotalAmount.Text);
                _dbaItemRequests.USERID = Convert.ToInt32(_frmCreateItemRequests.cboFullNames.SelectedValue.ToString());
                _dbaItemRequests.ACTION = 0;
                _dbaItemRequests.SaveData();

                _spString = string.Format("SP_Select_ItemRequests N'{0}',N'{1}',N'{2}'", "0", "0", "1");
                _dt = _dbaConnection.SelectData(_spString);
                _itemRequestsID = Convert.ToInt32(_dt.Rows[0]["RequestID"].ToString());


                for (int i = 0; i < _dtItemRequests.Rows.Count; i++)
                {     
                    
                    _dbaPartyItem.ITEMID = Convert.ToInt32(_dtItemRequests.Rows[i]["ItemID"].ToString());
                    _dbaPartyItem.QTY = Convert.ToInt32(_dtItemRequests.Rows[i]["Qty"].ToString());
                    _dbaPartyItem.PRICE = Convert.ToInt32(_dtItemRequests.Rows[i]["Price"].ToString());
                    _dbaPartyItem.ACTION = 4;
                    _dbaPartyItem.SaveData();

                    _dbaItemRequestsDetail.RDID = _itemRequestsDetailID;
                    _dbaItemRequestsDetail.RID = _itemRequestsID;
                    _dbaItemRequestsDetail.ITEMID = Convert.ToInt32(_dtItemRequests.Rows[i]["ItemID"].ToString());
                    _dbaItemRequestsDetail.RQTY = Convert.ToInt32(_dtItemRequests.Rows[i]["Qty"].ToString());
                    _dbaItemRequestsDetail.PRICE = Convert.ToInt32(_dtItemRequests.Rows[i]["Price"].ToString());
                    _dbaItemRequestsDetail.ACTION = 0;
                    _dbaItemRequestsDetail.SaveData();
                }

                MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                _frmCreateItemRequests.Close();
            }
        }
    }
}
