using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using F21Party.DBA;
using F21Party.Views;
using System.Xml.Linq;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;

namespace F21Party.Controllers
{
    internal class ctrlFrmCreateAccount
    {
        public frm_CreateAccount frm_CreateAccount;// Declare the View
        public bool _IsEnabled = true;
        public bool _LocalIsEnabled;

        public ctrlFrmCreateAccount(frm_CreateAccount createAccountForm)
        {
            frm_CreateAccount = createAccountForm; // Create the View
        }

        DbaConnection dbaConnection = new DbaConnection();
        DbaAccountSetting dbaAccountSetting = new DbaAccountSetting();
        DbaUserSetting dbaUserSetting = new DbaUserSetting();

        public int accesslevelindex;
        public int positionlevelindex;
        public bool _IsEdit;
        public int _UserID;
        public int _AccountID;

        public void AccessComboChange(bool _IsEdit)
        {
            string SPString;
            SPString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            DataTable DT = new DataTable();
            DT = dbaConnection.SelectData(SPString);
            List<string> FalseLogIn = new List<string>();

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                if (DT.Rows[i]["LogIn"].ToString() == "False")
                {
                    FalseLogIn.Add(DT.Rows[i]["AccessLevel"].ToString());
                }
            }
            if (_IsEdit)
            {
                _LocalIsEnabled = _IsEnabled;
            }
            else
            {
                if (FalseLogIn != null)
                {
                    if (FalseLogIn.Contains(frm_CreateAccount.cboAccessLevel.Text))
                    {
                        frm_CreateAccount.txtUserName.Enabled = false;
                        frm_CreateAccount.txtPassword.Enabled = false;
                        frm_CreateAccount.txtConfirmPassword.Enabled = false;
                        _IsEnabled = false;
                        _LocalIsEnabled = _IsEnabled;
                    }
                    else
                    {
                        frm_CreateAccount.txtUserName.Enabled = true;
                        frm_CreateAccount.txtPassword.Enabled = true;
                        frm_CreateAccount.txtConfirmPassword.Enabled = true;
                        _IsEnabled = true;
                        _LocalIsEnabled = _IsEnabled;
                    }
                }
            }
            //MessageBox.Show(frm_CreateAccount.cboAccessLevel.DisplayMember);
            //frm_CreateAccount.cboAccessLevel.SelectedValue = "1003";



        }



        public void SaveClick()
        {
            //frmMain obj_frmMain = new frmMain();
            DataTable DT = new DataTable();
            string SPString = "";
            _IsEdit = frm_CreateAccount._IsEdit;
            _AccountID = frm_CreateAccount._AccountID;
            _UserID = frm_CreateAccount._UserID;

            if (frm_CreateAccount.txtFullName.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type FullName");
                frm_CreateAccount.txtFullName.Focus();
            }
            else if (frm_CreateAccount.txtAddress.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Address");
                frm_CreateAccount.txtAddress.Focus();
            }
            else if (frm_CreateAccount.txtPhone.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Phone");
                frm_CreateAccount.txtPhone.Focus();
            }
            else if (frm_CreateAccount.cboPosition.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose Position");
                frm_CreateAccount.cboPosition.Focus();
            }
            else if (frm_CreateAccount.cboAccessLevel.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose AccessLevel");
                frm_CreateAccount.cboAccessLevel.Focus();
            }
            else if (_LocalIsEnabled)
            {
                if (frm_CreateAccount.txtUserName.Text.Trim().ToString() == string.Empty)
                {
                    MessageBox.Show("Please Type UserName");
                    frm_CreateAccount.txtUserName.Focus();
                }
                else if (frm_CreateAccount.txtPassword.Text.Trim().ToString() == string.Empty)
                {
                    MessageBox.Show("Please Type Password");
                    frm_CreateAccount.txtPassword.Focus();
                }
                else if (frm_CreateAccount.txtConfirmPassword.Text.Trim().ToString() == string.Empty)
                {
                    MessageBox.Show("Please Type ConfirmPassword");
                    frm_CreateAccount.txtConfirmPassword.Focus();
                }
                else if (frm_CreateAccount.txtPassword.Text.Trim().ToString() != frm_CreateAccount.txtConfirmPassword.Text.Trim().ToString())
                {
                    MessageBox.Show("Password And Confirm Password Should Be Same");
                    frm_CreateAccount.txtConfirmPassword.Focus();
                    frm_CreateAccount.txtConfirmPassword.SelectAll();
                }
                else
                {
                    _LocalIsEnabled = false;
                    SaveClick();
                }
            }
            else
            {

                // For Both Users and Accounts 
                if (_IsEnabled)
                {
                    // For Users
                    SPString = string.Format("SP_Select_Users N'{0}',N'{1}',N'{2}',N'{3}'", Regex.Replace(frm_CreateAccount.txtFullName.Text.Trim(), @"\s+", " "),
                    Regex.Replace(frm_CreateAccount.txtAddress.Text.Trim(), @"\s+", " "), "0", "5");

                    DT = dbaConnection.SelectData(SPString);
                    if (DT.Rows.Count > 0 && _UserID != Convert.ToInt32(DT.Rows[0]["UserID"]))
                    {
                        
                        MessageBox.Show("This User is Already Exist");
                        frm_CreateAccount.txtFullName.Focus();
                        frm_CreateAccount.txtFullName.SelectAll();
                    } 
                    else
                    {
                        // For Accounts
                        SPString = string.Format("SP_Select_Accounts N'{0}',N'{1}',N'{2}',N'{3}'", Regex.Replace(frm_CreateAccount.txtUserName.Text.Trim(), @"\s+", " "),
                        "0", "0", "4");

                        DT = dbaConnection.SelectData(SPString);
                        if (DT.Rows.Count > 0 && _AccountID != Convert.ToInt32(DT.Rows[0]["AccountID"]))
                        {
                            
                            MessageBox.Show("This Account is Already Exist");
                            frm_CreateAccount.txtUserName.Focus();
                            frm_CreateAccount.txtUserName.SelectAll();
                        }
                        else
                        {
                            // For User
                            dbaUserSetting.UID = Convert.ToInt32(_UserID);
                            dbaUserSetting.FNAME = Regex.Replace(frm_CreateAccount.txtFullName.Text.Trim(), @"\s+", " ");
                            dbaUserSetting.ADDRESS = Regex.Replace(frm_CreateAccount.txtAddress.Text.Trim(), @"\s+", " ");
                            dbaUserSetting.PHONE = Regex.Replace(frm_CreateAccount.txtPhone.Text.Trim(), @"\s+", " ");
                            dbaUserSetting.PID = Convert.ToInt32(frm_CreateAccount.cboPosition.SelectedValue);

                            // For Account
                            dbaAccountSetting.AccountID = Convert.ToInt32(_AccountID);

                            dbaAccountSetting.UNAME = Regex.Replace(frm_CreateAccount.txtUserName.Text.Trim(), @"\s+", " ");
                            dbaAccountSetting.PASS = Regex.Replace(frm_CreateAccount.txtPassword.Text.Trim(), @"\s+", " ");
                            dbaAccountSetting.ACCESSID = Convert.ToInt32(frm_CreateAccount.cboAccessLevel.SelectedValue);


                            if (_IsEdit)
                            {
                                //dbaUserSetting.ACTION = 1;
                                //dbaUserSetting.SaveData();
                    
                                dbaAccountSetting.USERID = Convert.ToInt32(_UserID);
                                if (dbaAccountSetting.USERID == Program.UserID)
                                {
                                    SPString = string.Format("SP_Select_Accounts N'{0}',N'{1}',N'{2}',N'{3}'", Program.UserID.ToString(),
                                        "0", "0", "6");

                                    DT = dbaConnection.SelectData(SPString);

                                    if(dbaAccountSetting.ACCESSID != Convert.ToInt32(DT.Rows[0]["AccessID"]))
                                    {
                                        MessageBox.Show("You cannot change your own account's access");
                                    }
                                    else
                                    {
                                        dbaAccountSetting.ACTION = 1;
                                        dbaAccountSetting.SaveData();

                                        MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                                        frm_CreateAccount.Close();
                                    }

                                    
                                }
                                else if (Program.UserAccessLevel == "Admin" || Program.UserAccessLevel == "Admin VIP")
                                {

                                    dbaAccountSetting.ACTION = 1;
                                    dbaAccountSetting.SaveData();

                                    MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                                    frm_CreateAccount.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Only Admin and Admin VIP can change the access level!");
                                }

                                
                            }

                            else
                            {
                                dbaUserSetting.ACTION = 0;
                                dbaUserSetting.SaveData();

                                SPString = string.Format("SP_Select_Users N'{0}',N'{1}',N'{2}',N'{3}'", Regex.Replace(frm_CreateAccount.txtFullName.Text.Trim(), @"\s+", " "),
                                    Regex.Replace(frm_CreateAccount.txtAddress.Text.Trim(), @"\s+", " "), Regex.Replace(frm_CreateAccount.txtPhone.Text.Trim(), @"\s+", " "), "1");

                                DT = dbaConnection.SelectData(SPString);
                                dbaAccountSetting.USERID = Convert.ToInt32(DT.Rows[0]["UserID"]);

                                dbaAccountSetting.ACTION = 0;
                                dbaAccountSetting.SaveData();

                                MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                                frm_CreateAccount.Close();
                            }
                        }
                    }

                    
                }
                else
                {
                    // For Users
                    SPString = string.Format("SP_Select_Users N'{0}',N'{1}',N'{2}',N'{3}'", Regex.Replace(frm_CreateAccount.txtFullName.Text.Trim(), @"\s+", " "),
                    Regex.Replace(frm_CreateAccount.txtAddress.Text.Trim(), @"\s+", " "), "0", "5");

                    DT = dbaConnection.SelectData(SPString);
                    if (DT.Rows.Count > 0 && _UserID != Convert.ToInt32(DT.Rows[0]["UserID"]))
                    {
                        MessageBox.Show("This User is Already Exist");
                        frm_CreateAccount.txtFullName.Focus();
                        frm_CreateAccount.txtFullName.SelectAll();
                    }
                    else
                    {
                        dbaUserSetting.UID = Convert.ToInt32(_UserID);
                        dbaUserSetting.FNAME = Regex.Replace(frm_CreateAccount.txtFullName.Text.Trim(), @"\s+", " ");
                        dbaUserSetting.ADDRESS = Regex.Replace(frm_CreateAccount.txtAddress.Text.Trim(), @"\s+", " ");
                        dbaUserSetting.PHONE = Regex.Replace(frm_CreateAccount.txtPhone.Text.Trim(), @"\s+", " ");
                        dbaUserSetting.PID = Convert.ToInt32(frm_CreateAccount.cboPosition.SelectedValue);


                        if (_IsEdit)
                        {
                            dbaUserSetting.UID = Convert.ToInt32(_UserID);
                            dbaUserSetting.ACTION = 1;
                            dbaUserSetting.SaveData();

                            MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                            frm_CreateAccount.Close();
                        }
                        else
                        {
                            dbaUserSetting.ACTION = 0;
                            dbaUserSetting.SaveData();
                            MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                            frm_CreateAccount.Close();
                        }
                    }
                }
            }
        }

        public void AddCombo(ComboBox cboCombo, string SPString, string Display, string Value, bool _IsEdit)
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
                SqlDataAdapter Adpt = new SqlDataAdapter(SPString, dbaConnection.con);
                Adpt.Fill(DTAC);
                for (int i = 0; i < DTAC.Rows.Count; i++)
                {
                    Dr = DTCombo.NewRow();
                    if (_IsEdit != true)
                    {
                        if (DTAC.Rows[i][Display].ToString().Trim().ToUpper() == "ADMIN")
                        {
                            continue;
                        }
                    }
                    

                    if (DTAC.Rows[i][Display].ToString().Trim().ToUpper() == "ADMIN VIP")
                    {
                        continue;
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

        public void ShowCombo(bool _IsEdit)
        {
            string _AccessLevelDisplay = "";
            string _PositionDisplay = "";
            string SPString;
            _AccessLevelDisplay = frm_CreateAccount.cboAccessLevel.DisplayMember;
            if (_AccessLevelDisplay == string.Empty)
                _AccessLevelDisplay = "0";
            _PositionDisplay = frm_CreateAccount.cboPosition.DisplayMember;
            if (_PositionDisplay == string.Empty)
                _PositionDisplay = "0";


            // For Access Combobox
            SPString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", "0", "0", "0");

            AddCombo(frm_CreateAccount.cboAccessLevel, SPString, "AccessLevel", "AccessID", _IsEdit);

            frm_CreateAccount.cboAccessLevel.SelectedValue = Convert.ToInt32(_AccessLevelDisplay); //This is in the box value you see
            accesslevelindex = frm_CreateAccount.cboAccessLevel.SelectedIndex;

            // For Position Combobox
            SPString = string.Format("SP_Select_Position N'{0}',N'{1}',N'{2}'", "0", "0", "0");

            AddCombo(frm_CreateAccount.cboPosition, SPString, "PositionName", "PositionID", _IsEdit);

            frm_CreateAccount.cboPosition.SelectedValue = Convert.ToInt32(_PositionDisplay); //This is in the box value you see
            positionlevelindex = frm_CreateAccount.cboPosition.SelectedIndex;

        }

        bool isPasswordShown = false;

        public void EyeToggle()
        {
            // Set initial state
            frm_CreateAccount.btnEye.Text = "👁️";
            frm_CreateAccount.txtPassword.UseSystemPasswordChar = true;

            // Attach click event
            frm_CreateAccount.btnEye.Click += (s, e) =>
            {
                isPasswordShown = !isPasswordShown;

                frm_CreateAccount.txtPassword.UseSystemPasswordChar = !isPasswordShown;

                // Toggle emoji on the button
                frm_CreateAccount.btnEye.Text = isPasswordShown ? "🚫" : "👁️";
            };
        }

    }
}
