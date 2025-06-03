using F21Party.DBA;
using F21Party.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F21Party.Controllers
{
    internal class CtrlFrmProfile
    {
        private readonly frm_Profile _frmProfile;// Declare the View
        //private bool _isEdit;
        private int _userID;
        private int _accountID;
        private string _oldUserPassword;
        private string _oldUserName;
        private string _spString;
        private readonly DbaConnection _dbaConnection = new DbaConnection();
        private readonly DbaAccounts _dbaAccountSetting = new DbaAccounts();
        private readonly DbaUsers _dbaUserSetting = new DbaUsers();
        private readonly Image _imageEye = new Bitmap(Properties.Resources.eye, new Size(16, 16));
        private readonly Image _imageEyeSlash = new Bitmap(Properties.Resources.eye_slash, new Size(16, 16));
        private bool _isPasswordVisible = false;
        private string _cleanedName;

        public CtrlFrmProfile(frm_Profile createAccountForm)
        {
            _frmProfile = createAccountForm; // Create the View
            _frmProfile.btnEye.Image = _imageEye; // Set the initial eye icon
        }

        public void ShowData()
        {

            _spString = string.Format("SP_Select_Accounts N'{0}', N'{1}', N'{2}', N'{3}'", Program.UserID, "0", "0", "6");
            DataTable dtAccount = new DataTable();
            dtAccount = _dbaConnection.SelectData(_spString);

            _frmProfile.UserID = Program.UserID;
            _frmProfile.txtUserName.Text = dtAccount.Rows[0]["UserName"].ToString();
            _frmProfile.txtPassword.Text = PwEncryption.Decrypt(dtAccount.Rows[0]["Password"].ToString());
            _frmProfile.txtConfirmPassword.Text = PwEncryption.Decrypt(dtAccount.Rows[0]["Password"].ToString());
            _frmProfile.cboAccessLevel.DisplayMember = dtAccount.Rows[0]["AccessID"].ToString();
            _frmProfile.AccountID = Convert.ToInt32(dtAccount.Rows[0]["AccountID"].ToString());

            _oldUserPassword = PwEncryption.Decrypt(dtAccount.Rows[0]["Password"].ToString());
            _oldUserName = dtAccount.Rows[0]["UserName"].ToString();

            _spString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", Program.UserID, "0", "0", "6");
            DataTable dtUser = new DataTable();
            dtUser = _dbaConnection.SelectData(_spString);

            _frmProfile.txtFullName.Text = dtUser.Rows[0]["FullName"].ToString();
            //frmProfile.txtFullName.Enabled = false;

            _frmProfile.txtAddress.Text = dtUser.Rows[0]["Address"].ToString();
            //frmProfile.txtAddress.Enabled = false;

            _frmProfile.txtPhone.Text = dtUser.Rows[0]["Phone"].ToString();
            //frmProfile.txtPhone.Enabled = false;

            _frmProfile.cboPosition.DisplayMember = dtUser.Rows[0]["PositionID"].ToString();
            //frmProfile.cboPosition.Enabled = false;

            _frmProfile.txtFullName.Enabled = false;
            _frmProfile.txtAddress.Enabled = false;
            _frmProfile.txtPhone.Enabled = false;
            _frmProfile.txtAddress.Enabled = false;
            _frmProfile.cboPosition.Enabled = false;
            _frmProfile.txtUserName.Enabled = false;
            _frmProfile.txtPassword.Enabled = false;
            _frmProfile.cboAccessLevel.Enabled = false;

            _frmProfile.lblConfirmPassword.Visible = false;
            _frmProfile.txtConfirmPassword.Visible = false;

        }
        public void EditClick()
        {
            _frmProfile.txtFullName.Enabled = true;
            _frmProfile.txtAddress.Enabled = true;
            _frmProfile.txtPhone.Enabled = true;
            _frmProfile.txtAddress.Enabled = true;
            //frmProfile.cboPosition.Enabled = false;
            _frmProfile.txtUserName.Enabled = true;
            _frmProfile.txtPassword.Enabled = true;
            //frmProfile.cboAccessLevel.Enabled = false;

            _frmProfile.lblConfirmPassword.Visible = true;
            _frmProfile.txtConfirmPassword.Visible = true;
            _frmProfile.IsEdit = true;

            _frmProfile.btnCreate.Text = "Save";
        }

        public void SaveClick()
        {

            DataTable dt = new DataTable();
            //_isEdit = _frmProfile.IsEdit;
            _accountID = _frmProfile.AccountID;
            _userID = _frmProfile.UserID;

            if (_frmProfile.txtFullName.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type FullName");
                _frmProfile.txtFullName.Focus();
            }
            else if (_frmProfile.txtAddress.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Address");
                _frmProfile.txtAddress.Focus();
            }
            else if (_frmProfile.txtPhone.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Phone");
                _frmProfile.txtPhone.Focus();
            }
            else if (!Regex.IsMatch(_frmProfile.txtPhone.Text.Trim().ToString(), @"^\d+$"))
            {
                MessageBox.Show("Phone number must contain digits only with no space.");
                return;
            }
            else if (_frmProfile.cboPosition.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose Position");
                _frmProfile.cboPosition.Focus();
            }
            else if (_frmProfile.txtUserName.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type UserName");
                _frmProfile.txtUserName.Focus();
            }
            else if (_frmProfile.txtUserName.Text.Contains(" "))
            {
                MessageBox.Show("Spaces are not allowed in UserName.");
                return;
            }
            else if (_frmProfile.txtPassword.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Password");
                _frmProfile.txtPassword.Focus();
            }
            else if (_frmProfile.txtConfirmPassword.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type ConfirmPassword");
                _frmProfile.txtConfirmPassword.Focus();
            }
            else if (_frmProfile.txtPassword.Text.Trim().ToString() != _frmProfile.txtConfirmPassword.Text.Trim().ToString())
            {
                MessageBox.Show("Password And Confirm Password Should Be Same");
                _frmProfile.txtConfirmPassword.Focus();
                _frmProfile.txtConfirmPassword.SelectAll();
            }
            else
            {
                // For Users
                _spString = string.Format("SP_Select_Users N'{0}',N'{1}',N'{2}',N'{3}'", Regex.Replace(_frmProfile.txtFullName.Text.Trim(), @"\s+", " "),
                Regex.Replace(_frmProfile.txtAddress.Text.Trim(), @"\s+", " "), "0", "5");

                dt = _dbaConnection.SelectData(_spString);
                if (dt.Rows.Count > 0 && _userID != Convert.ToInt32(dt.Rows[0]["UserID"]))
                {

                    MessageBox.Show("This User is Already Exist");
                    _frmProfile.txtFullName.Focus();
                    _frmProfile.txtFullName.SelectAll();
                }
                else
                {
                    
                    // For Accounts
                    _spString = string.Format("SP_Select_Accounts N'{0}',N'{1}',N'{2}',N'{3}'", Regex.Replace(_frmProfile.txtUserName.Text.Trim(), @"\s+", " "),
                    "0", "0", "4");

                    dt = _dbaConnection.SelectData(_spString);
                    if (dt.Rows.Count > 0 && _accountID != Convert.ToInt32(dt.Rows[0]["AccountID"]))
                    {

                        MessageBox.Show("This Account is Already Exist");
                        _frmProfile.txtUserName.Focus();
                        _frmProfile.txtUserName.SelectAll();
                    }
                    else
                    {
                        if (_oldUserPassword != _frmProfile.txtPassword.Text || _oldUserName != _frmProfile.txtUserName.Text)
                        {
                            if (MessageBox.Show("You have to Logout your account if you want to change Username or Password", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                // For User
                                _cleanedName = Regex.Replace(_frmProfile.txtFullName.Text.Trim(), @"\s+", " ");
                                _dbaUserSetting.UID = Convert.ToInt32(_userID);
                                _dbaUserSetting.FNAME = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_cleanedName.ToLower());
                                _dbaUserSetting.ADDRESS = Regex.Replace(_frmProfile.txtAddress.Text.Trim(), @"\s+", " ");
                                _dbaUserSetting.PHONE = Regex.Replace(_frmProfile.txtPhone.Text.Trim(), @"\s+", " ");
                                _dbaUserSetting.PID = Convert.ToInt32(_frmProfile.cboPosition.SelectedValue);

                                _dbaUserSetting.ACTION = 1;
                                _dbaUserSetting.SaveData();

                                // For Account
                                _dbaAccountSetting.ACCOUNTID = Convert.ToInt32(_accountID);
                                _dbaAccountSetting.USERID = Convert.ToInt32(_userID);
                                _dbaAccountSetting.UNAME = Regex.Replace(_frmProfile.txtUserName.Text.Trim(), @"\s+", " ");
                                _dbaAccountSetting.PASS = PwEncryption.Encrypt(Regex.Replace(_frmProfile.txtPassword.Text.Trim(), @"\s+", " "));
                                _dbaAccountSetting.ACCESSID = Convert.ToInt32(_frmProfile.cboAccessLevel.SelectedValue);


                                _dbaAccountSetting.ACTION = 1;
                                _dbaAccountSetting.SaveData();

                                _frmProfile.txtFullName.Enabled = false;
                                _frmProfile.txtAddress.Enabled = false;
                                _frmProfile.txtPhone.Enabled = false;
                                _frmProfile.txtAddress.Enabled = false;
                                //frmProfile.cboPosition.Enabled = false;
                                //frmProfile.txtUserName.Enabled = false;
                                _frmProfile.txtPassword.Enabled = false;
                                //frmProfile.cboAccessLevel.Enabled = false;

                                _frmProfile.lblConfirmPassword.Visible = false;
                                _frmProfile.txtConfirmPassword.Visible = false;

                                _frmProfile.IsEdit = false;
                                _frmProfile.btnCreate.Text = "Edit";
                                _frmProfile.IsLogout = true;
                                //frmProfile.Close();
                                return;
                            }
                            else 
                            { 
                                return; 
                            }
                        }

                        // For User
                        _cleanedName = Regex.Replace(_frmProfile.txtFullName.Text.Trim(), @"\s+", " ");
                        _dbaUserSetting.UID = Convert.ToInt32(_userID);
                        _dbaUserSetting.FNAME = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_cleanedName.ToLower());
                        _dbaUserSetting.ADDRESS = Regex.Replace(_frmProfile.txtAddress.Text.Trim(), @"\s+", " ");
                        _dbaUserSetting.PHONE = Regex.Replace(_frmProfile.txtPhone.Text.Trim(), @"\s+", " ");
                        _dbaUserSetting.PID = Convert.ToInt32(_frmProfile.cboPosition.SelectedValue);

                        _dbaUserSetting.ACTION = 1;
                        _dbaUserSetting.SaveData();

                        // For Account
                        _dbaAccountSetting.ACCOUNTID = Convert.ToInt32(_accountID);
                        _dbaAccountSetting.USERID = Convert.ToInt32(_userID);
                        _dbaAccountSetting.UNAME = Regex.Replace(_frmProfile.txtUserName.Text.Trim(), @"\s+", " ");
                        _dbaAccountSetting.PASS = PwEncryption.Encrypt(Regex.Replace(_frmProfile.txtPassword.Text.Trim(), @"\s+", " "));
                        _dbaAccountSetting.ACCESSID = Convert.ToInt32(_frmProfile.cboAccessLevel.SelectedValue);


                        _dbaAccountSetting.ACTION = 1;
                        _dbaAccountSetting.SaveData();

                        MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);

                        _frmProfile.IsEdit = false;
                        _frmProfile.btnCreate.Text = "Edit";
                        ShowData();
                    }
                }
            }
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

            // For Your Authority
            //string spAccess = "";
            DataTable dtAccess = new DataTable();
            _spString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", Program.UserAccessID, "0", "1");
            dtAccess = _dbaConnection.SelectData(_spString);

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
            //string spString;

            // For Access Combobox
            string accessLevelDisplay = _frmProfile.cboAccessLevel.DisplayMember;
            if (accessLevelDisplay == string.Empty)
                accessLevelDisplay = "0";

            _spString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", "0", "0", "0");

            AddCombo(_frmProfile.cboAccessLevel, _spString, "AccessLevel", "AccessID");

            _frmProfile.cboAccessLevel.SelectedValue = Convert.ToInt32(accessLevelDisplay); //This is in the box value you see

            // For Position Combobox
            string positionDisplay = _frmProfile.cboPosition.DisplayMember;
            if (positionDisplay == string.Empty)
                positionDisplay = "0";

            _spString = string.Format("SP_Select_Position N'{0}',N'{1}',N'{2}'", "0", "0", "0");

            AddCombo(_frmProfile.cboPosition, _spString, "PositionName", "PositionID");

            _frmProfile.cboPosition.SelectedValue = Convert.ToInt32(positionDisplay); //This is in the box value you see
            //positionlevelindex = frmProfile.cboPosition.SelectedIndex;


        }

        public void EyeToggle()
        {
            if (_isPasswordVisible)
            {
                _frmProfile.txtPassword.UseSystemPasswordChar = true;
                _frmProfile.btnEye.Image = _imageEye;
            }
            else
            {
                _frmProfile.txtPassword.UseSystemPasswordChar = false;
                _frmProfile.btnEye.Image = _imageEyeSlash;
            }
            _isPasswordVisible = !_isPasswordVisible;
        }
    }
}
