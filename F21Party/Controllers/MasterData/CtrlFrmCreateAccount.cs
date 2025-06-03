using F21Party.DBA;
using F21Party.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace F21Party.Controllers
{
    internal class CtrlFrmCreateAccount
    {
        //frm_AccountList frmAccountList; // Declare the View
        private readonly frm_CreateAccount _frmCreateAccount;// Declare the View
        private bool _isEnabled = true;
        private bool _localIsEnabled;

        private readonly DbaConnection _dbaConnection = new DbaConnection();
        private readonly DbaAccounts _dbaAccountSetting = new DbaAccounts();
        private readonly DbaUsers _dbaUserSetting = new DbaUsers();

        private readonly Image _imageEye = new Bitmap(Properties.Resources.eye, new Size(16, 16));
        private readonly Image _imageEyeSlash = new Bitmap(Properties.Resources.eye_slash, new Size(16, 16));
        private bool _isPasswordVisible = false;

        //private int accesslevelindex;
        //private int positionlevelindex;
        private bool _isEdit;
        private int _userID;
        private int _accountID;
        private string _spString;

        public CtrlFrmCreateAccount(frm_CreateAccount createAccountForm)
        {
            _frmCreateAccount = createAccountForm; // Create the View
            _frmCreateAccount.btnEye.Image = _imageEye; // Set the initial eye icon
        }

        public void AccessComboChange(bool isEdit)
        {
            _spString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            DataTable dt = new DataTable();
            dt = _dbaConnection.SelectData(_spString);
            List<string> falseLogIn = new List<string>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["LogInAccess"].ToString() == "False")
                {
                    falseLogIn.Add(dt.Rows[i]["AccessLevel"].ToString());
                }
            }
            if (isEdit)
            {
                _localIsEnabled = _isEnabled;
            }
            else
            {
                if (falseLogIn != null)
                {
                    if (falseLogIn.Contains(_frmCreateAccount.cboAccessLevel.Text))
                    {
                        _frmCreateAccount.txtUserName.Enabled = false;
                        _frmCreateAccount.txtPassword.Enabled = false;
                        _frmCreateAccount.txtConfirmPassword.Enabled = false;
                        _isEnabled = false;
                        _localIsEnabled = _isEnabled;
                    }
                    else
                    {
                        _frmCreateAccount.txtUserName.Enabled = true;
                        _frmCreateAccount.txtPassword.Enabled = true;
                        _frmCreateAccount.txtConfirmPassword.Enabled = true;
                        _isEnabled = true;
                        _localIsEnabled = _isEnabled;
                    }
                }
            }
        }
        public void AddAccountClick()
        {
            DataTable dt = new DataTable();
            //_IsEdit = frmCreateAccount._IsEdit;
            _accountID = _frmCreateAccount.AccountID;
            _userID = _frmCreateAccount.UserID;

            if (_frmCreateAccount.cboAccessLevel.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose AccessLevel");
                _frmCreateAccount.cboAccessLevel.Focus();
            }
            else if (_frmCreateAccount.txtUserName.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type UserName");
                _frmCreateAccount.txtUserName.Focus();
            }
            else if (_frmCreateAccount.txtUserName.Text.Contains(" "))
            {
                MessageBox.Show("Spaces are not allowed in UserName.");
                return;
            }
            else if (_frmCreateAccount.txtPassword.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Password");
                _frmCreateAccount.txtPassword.Focus();
            }
            else if (_frmCreateAccount.txtConfirmPassword.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type ConfirmPassword");
                _frmCreateAccount.txtConfirmPassword.Focus();
            }
            else if (_frmCreateAccount.txtPassword.Text.Trim().ToString() != _frmCreateAccount.txtConfirmPassword.Text.Trim().ToString())
            {
                MessageBox.Show("Password And Confirm Password Should Be Same");
                _frmCreateAccount.txtConfirmPassword.Focus();
                _frmCreateAccount.txtConfirmPassword.SelectAll();
            }
            else 
            {
                _spString = string.Format("SP_Select_Accounts N'{0}',N'{1}',N'{2}',N'{3}'", Regex.Replace(_frmCreateAccount.txtUserName.Text.Trim(), @"\s+", " "), "0", "0", "4");

                dt = _dbaConnection.SelectData(_spString);
                if (dt.Rows.Count > 0 && _accountID != Convert.ToInt32(dt.Rows[0]["AccountID"]))
                {

                    MessageBox.Show("This Account is Already Exist");
                    _frmCreateAccount.txtUserName.Focus();
                    _frmCreateAccount.txtUserName.SelectAll();
                }
                else
                {
                    // For User
                    _dbaUserSetting.UID = Convert.ToInt32(_userID);
                    _dbaUserSetting.FNAME = Regex.Replace(_frmCreateAccount.txtFullName.Text.Trim(), @"\s+", " ");
                    _dbaUserSetting.ADDRESS = Regex.Replace(_frmCreateAccount.txtAddress.Text.Trim(), @"\s+", " ");
                    _dbaUserSetting.PHONE = Regex.Replace(_frmCreateAccount.txtPhone.Text.Trim(), @"\s+", " ");
                    _dbaUserSetting.PID = Convert.ToInt32(_frmCreateAccount.cboPosition.SelectedValue);
                    _dbaUserSetting.HASACC = "True";

                    // For Account
                    _dbaAccountSetting.ACCOUNTID = Convert.ToInt32(_accountID);
                    _dbaAccountSetting.USERID = Convert.ToInt32(_userID);
                    _dbaAccountSetting.UNAME = Regex.Replace(_frmCreateAccount.txtUserName.Text.Trim(), @"\s+", " ");
                    _dbaAccountSetting.PASS = PwEncryption.Encrypt(Regex.Replace(_frmCreateAccount.txtPassword.Text.Trim(), @"\s+", " "));
                    _dbaAccountSetting.ACCESSID = Convert.ToInt32(_frmCreateAccount.cboAccessLevel.SelectedValue);

                    _dbaAccountSetting.ACTION = 0;
                    _dbaAccountSetting.SaveData();

                    _dbaUserSetting.ACTION = 3;
                    _dbaUserSetting.SaveData();
                    MessageBox.Show("Successfully Add", "Successfully", MessageBoxButtons.OK);
                    _frmCreateAccount.Close();
                }
            }
        }

        public void SaveClick()
        {
            DataTable dt = new DataTable();
            _isEdit = _frmCreateAccount.IsEdit;
            _accountID = _frmCreateAccount.AccountID;
            _userID = _frmCreateAccount.UserID;

            if (_frmCreateAccount.txtFullName.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type FullName");
                _frmCreateAccount.txtFullName.Focus();
            }
            else if (_frmCreateAccount.txtAddress.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Address");
                _frmCreateAccount.txtAddress.Focus();
            }
            else if (_frmCreateAccount.txtPhone.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Phone");
                _frmCreateAccount.txtPhone.Focus();
            }
            else if (_frmCreateAccount.cboPosition.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose Position");
                _frmCreateAccount.cboPosition.Focus();
            }
            else if (_frmCreateAccount.cboAccessLevel.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose AccessLevel");
                _frmCreateAccount.cboAccessLevel.Focus();
            }
            else if (_localIsEnabled)
            {
                if (_frmCreateAccount.txtUserName.Text.Trim().ToString() == string.Empty)
                {
                    MessageBox.Show("Please Type UserName");
                    _frmCreateAccount.txtUserName.Focus();
                }
                else if (_frmCreateAccount.txtUserName.Text.Contains(" "))
                {
                    MessageBox.Show("Spaces are not allowed in UserName.");
                    return;
                }
                else if (_frmCreateAccount.txtPassword.Text.Trim().ToString() == string.Empty)
                {
                    MessageBox.Show("Please Type Password");
                    _frmCreateAccount.txtPassword.Focus();
                }
                else if (_frmCreateAccount.txtConfirmPassword.Text.Trim().ToString() == string.Empty)
                {
                    MessageBox.Show("Please Type ConfirmPassword");
                    _frmCreateAccount.txtConfirmPassword.Focus();
                }
                else if (_frmCreateAccount.txtPassword.Text.Trim().ToString() != _frmCreateAccount.txtConfirmPassword.Text.Trim().ToString())
                {
                    MessageBox.Show("Password And Confirm Password Should Be Same");
                    _frmCreateAccount.txtConfirmPassword.Focus();
                    _frmCreateAccount.txtConfirmPassword.SelectAll();
                }
                else
                {
                    _localIsEnabled = false;
                    SaveClick();
                }
            }
            else
            {

                // For Both Users and Accounts 
                if (_isEnabled)
                {
                    // For Users
                    _spString = string.Format("SP_Select_Users N'{0}',N'{1}',N'{2}',N'{3}'", Regex.Replace(_frmCreateAccount.txtFullName.Text.Trim(), @"\s+", " "),
                    Regex.Replace(_frmCreateAccount.txtAddress.Text.Trim(), @"\s+", " "), "0", "5");

                    dt = _dbaConnection.SelectData(_spString);
                    if (dt.Rows.Count > 0 && _userID != Convert.ToInt32(dt.Rows[0]["UserID"]))
                    {
                        
                        MessageBox.Show("This User is Already Exist");
                        _frmCreateAccount.txtFullName.Focus();
                        _frmCreateAccount.txtFullName.SelectAll();
                    } 
                    else
                    {
                        // For Accounts
                        _spString = string.Format("SP_Select_Accounts N'{0}',N'{1}',N'{2}',N'{3}'", Regex.Replace(_frmCreateAccount.txtUserName.Text.Trim(), @"\s+", " "),
                        "0", "0", "4");

                        dt = _dbaConnection.SelectData(_spString);
                        if (dt.Rows.Count > 0 && _accountID != Convert.ToInt32(dt.Rows[0]["AccountID"]))
                        {
                            
                            MessageBox.Show("This Account is Already Exist");
                            _frmCreateAccount.txtUserName.Focus();
                            _frmCreateAccount.txtUserName.SelectAll();
                        }
                        else
                        {
                            // For User
                            _dbaUserSetting.UID = Convert.ToInt32(_userID);
                            _dbaUserSetting.FNAME = Regex.Replace(_frmCreateAccount.txtFullName.Text.Trim(), @"\s+", " ");
                            _dbaUserSetting.ADDRESS = Regex.Replace(_frmCreateAccount.txtAddress.Text.Trim(), @"\s+", " ");
                            _dbaUserSetting.PHONE = Regex.Replace(_frmCreateAccount.txtPhone.Text.Trim(), @"\s+", " ");
                            _dbaUserSetting.PID = Convert.ToInt32(_frmCreateAccount.cboPosition.SelectedValue);

                            // For Account
                            _dbaAccountSetting.ACCOUNTID = Convert.ToInt32(_accountID);
                            //dbaAccountSetting.USERID = Convert.ToInt32(_UserID);
                            _dbaAccountSetting.UNAME = Regex.Replace(_frmCreateAccount.txtUserName.Text.Trim(), @"\s+", " ");
                            _dbaAccountSetting.PASS = PwEncryption.Encrypt(Regex.Replace(_frmCreateAccount.txtPassword.Text.Trim(), @"\s+", " "));
                            _dbaAccountSetting.ACCESSID = Convert.ToInt32(_frmCreateAccount.cboAccessLevel.SelectedValue);

                            if (_isEdit)
                            {

                                _dbaAccountSetting.USERID = Convert.ToInt32(_userID);
                                if (_dbaAccountSetting.USERID == Program.UserID)
                                {
                                    _spString = string.Format("SP_Select_Accounts N'{0}',N'{1}',N'{2}',N'{3}'", Program.UserID.ToString(),
                                        "0", "0", "6");

                                    dt = _dbaConnection.SelectData(_spString);

                                    if(_dbaAccountSetting.ACCESSID != Convert.ToInt32(dt.Rows[0]["AccessID"]))
                                    {
                                        MessageBox.Show("You cannot change your own account's access");
                                    }
                                    else
                                    {
                                        _dbaAccountSetting.ACTION = 3;
                                        _dbaAccountSetting.SaveData();

                                        MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                                        _frmCreateAccount.Close();
                                    }
                                }
                                else
                                {
                                    //MessageBox.Show("Only Admin and SuperAdmin can change the access level!");

                                    _dbaAccountSetting.ACTION = 3;
                                    _dbaAccountSetting.SaveData();

                                    MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                                    _frmCreateAccount.Close();
                                }

                                
                            }

                            else
                            {
                                _dbaUserSetting.ACTION = 0;
                                _dbaUserSetting.SaveData();

                                _spString = string.Format("SP_Select_Users N'{0}',N'{1}',N'{2}',N'{3}'", Regex.Replace(_frmCreateAccount.txtFullName.Text.Trim(), @"\s+", " "),
                                    Regex.Replace(_frmCreateAccount.txtAddress.Text.Trim(), @"\s+", " "), Regex.Replace(_frmCreateAccount.txtPhone.Text.Trim(), @"\s+", " "), "1");

                                dt = _dbaConnection.SelectData(_spString);
                                _dbaAccountSetting.USERID = Convert.ToInt32(dt.Rows[0]["UserID"]);

                                _dbaAccountSetting.ACTION = 0;
                                _dbaAccountSetting.SaveData();

                                MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                                _frmCreateAccount.Close();
                            }
                        }
                    }

                    
                }
                else
                {
                    // For Users
                    _spString = string.Format("SP_Select_Users N'{0}',N'{1}',N'{2}',N'{3}'", Regex.Replace(_frmCreateAccount.txtFullName.Text.Trim(), @"\s+", " "),
                    Regex.Replace(_frmCreateAccount.txtAddress.Text.Trim(), @"\s+", " "), "0", "5");

                    dt = _dbaConnection.SelectData(_spString);
                    if (dt.Rows.Count > 0 && _userID != Convert.ToInt32(dt.Rows[0]["UserID"]))
                    {
                        MessageBox.Show("This User is Already Exist");
                        _frmCreateAccount.txtFullName.Focus();
                        _frmCreateAccount.txtFullName.SelectAll();
                    }
                    else
                    {
                        _dbaUserSetting.UID = Convert.ToInt32(_userID);
                        _dbaUserSetting.FNAME = Regex.Replace(_frmCreateAccount.txtFullName.Text.Trim(), @"\s+", " ");
                        _dbaUserSetting.ADDRESS = Regex.Replace(_frmCreateAccount.txtAddress.Text.Trim(), @"\s+", " ");
                        _dbaUserSetting.PHONE = Regex.Replace(_frmCreateAccount.txtPhone.Text.Trim(), @"\s+", " ");
                        _dbaUserSetting.PID = Convert.ToInt32(_frmCreateAccount.cboPosition.SelectedValue);


                        if (_isEdit)
                        {
                            _dbaUserSetting.UID = Convert.ToInt32(_userID);
                            _dbaUserSetting.ACTION = 1;
                            _dbaUserSetting.SaveData();

                            MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                            _frmCreateAccount.Close();
                        }
                        else
                        {
                            _dbaUserSetting.ACTION = 0;
                            _dbaUserSetting.SaveData();

                            MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                            _frmCreateAccount.Close();
                        }
                    }
                }
            }
        }

        public void AddCombo(ComboBox cboCombo, string spString, string display, string value, bool isEdit)
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

            // For Your Authority
            DataTable dtAccess = new DataTable();
            _spString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", Program.UserAccessID,"0", "1");
            dtAccess = _dbaConnection.SelectData(_spString);

            //if(Program.UserID != 0)
            //{
            //    Program.UserAuthority =  Convert.ToInt32(dtAccess.Rows[0]["Authority"]);
            //}

            try
            {
                _dbaConnection.DataBaseConn();
                SqlDataAdapter Adpt = new SqlDataAdapter(spString, _dbaConnection.con);
                Adpt.Fill(dtAccessSp);
                for (int i = 0; i < dtAccessSp.Rows.Count; i++)
                {
                    dr = dtCombo.NewRow(); 
                    if (_frmCreateAccount.cboAccessLevel.DisplayMember == "AccessLevel" && display == "AccessLevel")
                    {
                        goto Bottom;
                    }
                    DataTable dtAccess2 = new DataTable();
                    if (display == "AccessLevel" && _frmCreateAccount.cboAccessLevel.DisplayMember != "")
                    {
                        _spString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", Convert.ToInt32(_frmCreateAccount.cboAccessLevel.DisplayMember), "0", "1");

                        dtAccess2 = _dbaConnection.SelectData(_spString);
                    }
                    
                    if (Program.UserAuthority == 1 && display == "AccessLevel")
                    {
                        // SuperAdmin Access is only visible when it is edited by SuperAdmin to itself
                        if (isEdit && Convert.ToInt32(dtAccess2.Rows[0]["Authority"]) == Program.UserAuthority)
                        {
                            
                        }
                        else
                        {
                            if (Convert.ToInt32(dtAccessSp.Rows[i][value]) == 1) // 1 is SuperAdmin.
                            {
                                continue;
                            }
                        }
                    }
                    if(isEdit)
                    {
                        if (Program.UserAuthority != 1 && display == "AccessLevel")
                        {
                            if (Convert.ToInt32(dtAccessSp.Rows[i][value]) == 1) // 1 is SuperAdmin.
                            {
                                continue;
                            }
                        }
                        
                        if (display == "AccessLevel" && Convert.ToInt32(dtAccess2.Rows[0]["Authority"]) >= Program.UserAuthority)
                        {

                            if (Convert.ToInt32(dtAccessSp.Rows[i]["Authority"]) < Program.UserAuthority) // AccessID comparison
                            {
                                continue; // Remove Specific Accesslevel from Combo box.
                            }
                        }
                        
                        
                        
                    }
                    else
                    {
                        if (Program.UserAuthority != 0 && display == "AccessLevel")
                        {
                            if (Convert.ToInt32(dtAccessSp.Rows[i][value]) < Program.UserAuthority) // AccessID comparison
                            {
                                continue; // Remove Specific Accesslevel from Combo box.
                            }
                        }

                        if (Program.UserAuthority == 0 && display == "AccessLevel")
                        {
                            if (Convert.ToInt32(dtAccessSp.Rows[i][value]) == 1 || Convert.ToInt32(dtAccessSp.Rows[i][value]) == 2) // 1 is SuperAdmin. 2 is Admin.
                            {
                                continue;
                            }
                        }
                    }

                Bottom:

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

        public void ShowCombo(bool isEdit)
        {
            // For Access Combobox
            string accessLevelDisplay;
            accessLevelDisplay = _frmCreateAccount.cboAccessLevel.DisplayMember;
            if (accessLevelDisplay == string.Empty)
                accessLevelDisplay = "0";

            _spString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", "0", "0", "0");

            AddCombo(_frmCreateAccount.cboAccessLevel, _spString, "AccessLevel", "AccessID", isEdit);

            _frmCreateAccount.cboAccessLevel.SelectedValue = Convert.ToInt32(accessLevelDisplay); //This is in the box value you see

            // For Position Combobox
            string positionDisplay;
            positionDisplay = _frmCreateAccount.cboPosition.DisplayMember;
            if (positionDisplay == string.Empty)
                positionDisplay = "0";

            _spString = string.Format("SP_Select_Position N'{0}',N'{1}',N'{2}'", "0", "0", "0");

            AddCombo(_frmCreateAccount.cboPosition, _spString, "PositionName", "PositionID", isEdit);

            _frmCreateAccount.cboPosition.SelectedValue = Convert.ToInt32(positionDisplay); //This is in the box value you see
            //positionlevelindex = frmCreateAccount.cboPosition.SelectedIndex;
        }
        public void EyeToggle()
        {
            if(_isPasswordVisible)
            {
                _frmCreateAccount.txtPassword.UseSystemPasswordChar = true;
                _frmCreateAccount.btnEye.Image = _imageEye;
            }
            else
            {
                _frmCreateAccount.txtPassword.UseSystemPasswordChar = false;
                _frmCreateAccount.btnEye.Image = _imageEyeSlash;
            }
            _isPasswordVisible = !_isPasswordVisible;
        }

    }
}
