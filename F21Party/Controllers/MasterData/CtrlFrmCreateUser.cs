using F21Party.DBA;
using F21Party.Views;
using System;
using System.Collections.Generic;
using System.Data;
//using F21Party.Views;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F21Party.Controllers
{
    internal class CtrlFrmCreateUser
    {
        private readonly frm_CreateUser _frmCreateUser;// Declare the View
        public CtrlFrmCreateUser(frm_CreateUser createUserForm)
        {
            _frmCreateUser = createUserForm; // Create the View
        }
        private readonly DbaConnection _dbaConnection = new DbaConnection();
        private readonly DbaUsers _dbaUserSetting = new DbaUsers();
        //private int positionLevelIndex;
        private bool _isEdit;
        private int _userID;
        string _spString;

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

        public void ShowCombo()
        {
            //string _PositionDisplay = "";
            //string spString;
            string positionDisplay = _frmCreateUser.cboPosition.DisplayMember;
            if (positionDisplay == string.Empty)
                positionDisplay = "0";

            // For Position Combobox
            _spString = string.Format("SP_Select_Position N'{0}',N'{1}',N'{2}'", "0", "0", "0");

            AddCombo(_frmCreateUser.cboPosition, _spString, "PositionName", "PositionID");

            _frmCreateUser.cboPosition.SelectedValue = Convert.ToInt32(positionDisplay); //This is in the box value you see
            //positionLevelIndex = frmCreateUser.cboPosition.SelectedIndex;

        }

        public void SaveClick()
        {
            //frmMain obj_frmMain = new frmMain();
            DataTable dt = new DataTable();
            //string spString = "";
            _isEdit = _frmCreateUser.IsEdit;
            _userID = _frmCreateUser.UserID;

            if (_frmCreateUser.txtFullName.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type FullName");
                _frmCreateUser.txtFullName.Focus();
            }
            else if (_frmCreateUser.txtAddress.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Address");
                _frmCreateUser.txtAddress.Focus();
            }
            else if (_frmCreateUser.txtPhone.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Phone");
                _frmCreateUser.txtPhone.Focus();
            }
            else if (!Regex.IsMatch(_frmCreateUser.txtPhone.Text.Trim().ToString(), @"^\d+$"))
            {
                MessageBox.Show("Phone number must contain digits only with no space.");
                return;
            }
            else if (_frmCreateUser.cboPosition.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose Position");
                _frmCreateUser.cboPosition.Focus();
            }

                
            else
            {
                // For Users
                _spString = string.Format("SP_Select_Users N'{0}',N'{1}',N'{2}',N'{3}'", Regex.Replace(_frmCreateUser.txtFullName.Text.Trim(), @"\s+", " "),
                Regex.Replace(_frmCreateUser.txtAddress.Text.Trim(), @"\s+", " "), "0", "5");

                dt = _dbaConnection.SelectData(_spString);
                if (dt.Rows.Count > 0 && _userID != Convert.ToInt32(dt.Rows[0]["UserID"]))
                {
                    MessageBox.Show("This User is Already Exist");
                    _frmCreateUser.txtFullName.Focus();
                    _frmCreateUser.txtFullName.SelectAll();
                }
                else
                {
                    string cleanedName = Regex.Replace(_frmCreateUser.txtFullName.Text.Trim(), @"\s+", " ");

                    _dbaUserSetting.UID = Convert.ToInt32(_userID);
                    _dbaUserSetting.FNAME = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cleanedName.ToLower());
                    _dbaUserSetting.ADDRESS = Regex.Replace(_frmCreateUser.txtAddress.Text.Trim(), @"\s+", " ");
                    _dbaUserSetting.PHONE = Regex.Replace(_frmCreateUser.txtPhone.Text.Trim(), @"\s+", " ");
                    _dbaUserSetting.PID = Convert.ToInt32(_frmCreateUser.cboPosition.SelectedValue);


                    if (_isEdit)
                    {
                        _dbaUserSetting.UID = Convert.ToInt32(_userID);
                        _dbaUserSetting.ACTION = 1;
                        _dbaUserSetting.SaveData();

                        MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                        _frmCreateUser.Close();
                    }
                    else
                    {
                        _dbaUserSetting.ACTION = 0;
                        _dbaUserSetting.SaveData();
                        MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                        _frmCreateUser.Close();
                    }
                }
            }
            
        }
    }
}
