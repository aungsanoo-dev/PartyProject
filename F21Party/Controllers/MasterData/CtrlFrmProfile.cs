using F21Party.DBA;
using F21Party.Views;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F21Party.Controllers
{
    internal class CtrlFrmProfile
    {
        frm_Profile frmProfile;// Declare the View
        private bool _IsEnabled = true;
        private bool _LocalIsEnabled;

        public CtrlFrmProfile(frm_Profile createAccountForm)
        {
            frmProfile = createAccountForm; // Create the View
        }


        DbaConnection dbaConnection = new DbaConnection();
        DbaAccounts dbaAccountSetting = new DbaAccounts();
        DbaUsers dbaUserSetting = new DbaUsers();

        //private int accesslevelindex;
        //private int positionlevelindex;
        bool _IsEdit;
        int _UserID;
        int _AccountID;
        private string _OldUserPassword;
        private string _OldUserName;
        //public void AccessComboChange(bool _IsEdit)
        //{
        //    string spString;
        //    spString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", "0", "0", "0");
        //    DataTable DT = new DataTable();
        //    DT = dbaConnection.SelectData(spString);
        //    List<string> FalseLogIn = new List<string>();

        //    for (int i = 0; i < DT.Rows.Count; i++)
        //    {
        //        if (DT.Rows[i]["LogInAccess"].ToString() == "False")
        //        {
        //            FalseLogIn.Add(DT.Rows[i]["AccessLevel"].ToString());
        //        }
        //    }
        //    if (_IsEdit)
        //    {
        //        _LocalIsEnabled = _IsEnabled;
        //    }
        //    else
        //    {
        //        if (FalseLogIn != null)
        //        {
        //            if (FalseLogIn.Contains(frmProfile.cboAccessLevel.Text))
        //            {
        //                frmProfile.txtUserName.Enabled = false;
        //                frmProfile.txtPassword.Enabled = false;
        //                frmProfile.txtConfirmPassword.Enabled = false;
        //                _IsEnabled = false;
        //                _LocalIsEnabled = _IsEnabled;
        //            }
        //            else
        //            {
        //                frmProfile.txtUserName.Enabled = true;
        //                frmProfile.txtPassword.Enabled = true;
        //                frmProfile.txtConfirmPassword.Enabled = true;
        //                _IsEnabled = true;
        //                _LocalIsEnabled = _IsEnabled;
        //            }
        //        }
        //    }
        //    //MessageBox.Show(frmProfile.cboAccessLevel.DisplayMember);
        //    //frmProfile.cboAccessLevel.SelectedValue = "1003";



        //}

        public void ShowData()
        {
           
            string spStringAccount = "";
            spStringAccount = string.Format("SP_Select_Accounts N'{0}', N'{1}', N'{2}', N'{3}'", Program.UserID, "0", "0", "6");
            DataTable dtAccount = new DataTable();
            dtAccount = dbaConnection.SelectData(spStringAccount);

            frmProfile._UserID = Program.UserID;
            frmProfile.txtUserName.Text = dtAccount.Rows[0]["UserName"].ToString();
            frmProfile.txtPassword.Text = PwEncryption.Decrypt(dtAccount.Rows[0]["Password"].ToString());
            frmProfile.txtConfirmPassword.Text = PwEncryption.Decrypt(dtAccount.Rows[0]["Password"].ToString());
            frmProfile.cboAccessLevel.DisplayMember = dtAccount.Rows[0]["AccessID"].ToString();
            frmProfile._AccountID = Convert.ToInt32(dtAccount.Rows[0]["AccountID"].ToString());

            _OldUserPassword = PwEncryption.Decrypt(dtAccount.Rows[0]["Password"].ToString());
            _OldUserName = dtAccount.Rows[0]["UserName"].ToString();
            string spStringUser = "";
            spStringUser = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", Program.UserID, "0", "0", "6");
            DataTable dtUser = new DataTable();
            dtUser = dbaConnection.SelectData(spStringUser);

            frmProfile.txtFullName.Text = dtUser.Rows[0]["FullName"].ToString();
            //frmProfile.txtFullName.Enabled = false;

            frmProfile.txtAddress.Text = dtUser.Rows[0]["Address"].ToString();
            //frmProfile.txtAddress.Enabled = false;

            frmProfile.txtPhone.Text = dtUser.Rows[0]["Phone"].ToString();
            //frmProfile.txtPhone.Enabled = false;

            frmProfile.cboPosition.DisplayMember = dtUser.Rows[0]["PositionID"].ToString();
            //frmProfile.cboPosition.Enabled = false;

            //if (Program.UserAccessID == 1) // AccessID 1 is SuperAdmin
            //{
            //    frmProfile.cboAccessLevel.Enabled = false;
            //}
            frmProfile.txtFullName.Enabled = false;
            frmProfile.txtAddress.Enabled = false;
            frmProfile.txtPhone.Enabled = false;
            frmProfile.txtAddress.Enabled = false;
            frmProfile.cboPosition.Enabled = false;
            frmProfile.txtUserName.Enabled = false;
            frmProfile.txtPassword.Enabled = false;
            frmProfile.cboAccessLevel.Enabled = false;

            frmProfile.lblConfirmPassword.Visible = false;
            frmProfile.txtConfirmPassword.Visible = false;

        }
        public void EditClick()
        {
            frmProfile.txtFullName.Enabled = true;
            frmProfile.txtAddress.Enabled = true;
            frmProfile.txtPhone.Enabled = true;
            frmProfile.txtAddress.Enabled = true;
            //frmProfile.cboPosition.Enabled = false;
            frmProfile.txtUserName.Enabled = true;
            frmProfile.txtPassword.Enabled = true;
            //frmProfile.cboAccessLevel.Enabled = false;

            frmProfile.lblConfirmPassword.Visible = true;
            frmProfile.txtConfirmPassword.Visible = true;
            frmProfile._IsEdit = true;

            frmProfile.btnCreate.Text = "Save";
        }

        public void SaveClick()
        {

            //frmMain obj_frmMain = new frmMain();
            DataTable DT = new DataTable();
            string spString = "";
            _IsEdit = frmProfile._IsEdit;
            _AccountID = frmProfile._AccountID;
            _UserID = frmProfile._UserID;

            if (frmProfile.txtFullName.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type FullName");
                frmProfile.txtFullName.Focus();
            }
            else if (frmProfile.txtAddress.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Address");
                frmProfile.txtAddress.Focus();
            }
            else if (frmProfile.txtPhone.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Phone");
                frmProfile.txtPhone.Focus();
            }
            else if (frmProfile.cboPosition.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose Position");
                frmProfile.cboPosition.Focus();
            }
            else if (frmProfile.txtUserName.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type UserName");
                frmProfile.txtUserName.Focus();
            }
            else if (frmProfile.txtPassword.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Password");
                frmProfile.txtPassword.Focus();
            }
            else if (frmProfile.txtConfirmPassword.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type ConfirmPassword");
                frmProfile.txtConfirmPassword.Focus();
            }
            else if (frmProfile.txtPassword.Text.Trim().ToString() != frmProfile.txtConfirmPassword.Text.Trim().ToString())
            {
                MessageBox.Show("Password And Confirm Password Should Be Same");
                frmProfile.txtConfirmPassword.Focus();
                frmProfile.txtConfirmPassword.SelectAll();
            }
            else
            {
                // For Users
                spString = string.Format("SP_Select_Users N'{0}',N'{1}',N'{2}',N'{3}'", Regex.Replace(frmProfile.txtFullName.Text.Trim(), @"\s+", " "),
                Regex.Replace(frmProfile.txtAddress.Text.Trim(), @"\s+", " "), "0", "5");

                DT = dbaConnection.SelectData(spString);
                if (DT.Rows.Count > 0 && _UserID != Convert.ToInt32(DT.Rows[0]["UserID"]))
                {

                    MessageBox.Show("This User is Already Exist");
                    frmProfile.txtFullName.Focus();
                    frmProfile.txtFullName.SelectAll();
                }
                else
                {
                    
                    // For Accounts
                    spString = string.Format("SP_Select_Accounts N'{0}',N'{1}',N'{2}',N'{3}'", Regex.Replace(frmProfile.txtUserName.Text.Trim(), @"\s+", " "),
                    "0", "0", "4");

                    DT = dbaConnection.SelectData(spString);
                    if (DT.Rows.Count > 0 && _AccountID != Convert.ToInt32(DT.Rows[0]["AccountID"]))
                    {

                        MessageBox.Show("This Account is Already Exist");
                        frmProfile.txtUserName.Focus();
                        frmProfile.txtUserName.SelectAll();
                    }
                    else
                    {
                        if (_OldUserPassword != frmProfile.txtPassword.Text || _OldUserName != frmProfile.txtUserName.Text)
                        {
                            if (MessageBox.Show("You have to Logout your account if you want to change Username or Password", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                // For User
                                dbaUserSetting.UID = Convert.ToInt32(_UserID);
                                dbaUserSetting.FNAME = Regex.Replace(frmProfile.txtFullName.Text.Trim(), @"\s+", " ");
                                dbaUserSetting.ADDRESS = Regex.Replace(frmProfile.txtAddress.Text.Trim(), @"\s+", " ");
                                dbaUserSetting.PHONE = Regex.Replace(frmProfile.txtPhone.Text.Trim(), @"\s+", " ");
                                dbaUserSetting.PID = Convert.ToInt32(frmProfile.cboPosition.SelectedValue);

                                dbaUserSetting.ACTION = 1;
                                dbaUserSetting.SaveData();

                                // For Account
                                dbaAccountSetting.ACCOUNTID = Convert.ToInt32(_AccountID);
                                dbaAccountSetting.USERID = Convert.ToInt32(_UserID);
                                dbaAccountSetting.UNAME = Regex.Replace(frmProfile.txtUserName.Text.Trim(), @"\s+", " ");
                                dbaAccountSetting.PASS = PwEncryption.Encrypt(Regex.Replace(frmProfile.txtPassword.Text.Trim(), @"\s+", " "));
                                dbaAccountSetting.ACCESSID = Convert.ToInt32(frmProfile.cboAccessLevel.SelectedValue);


                                dbaAccountSetting.ACTION = 1;
                                dbaAccountSetting.SaveData();

                                frmProfile.txtFullName.Enabled = false;
                                frmProfile.txtAddress.Enabled = false;
                                frmProfile.txtPhone.Enabled = false;
                                frmProfile.txtAddress.Enabled = false;
                                //frmProfile.cboPosition.Enabled = false;
                                //frmProfile.txtUserName.Enabled = false;
                                frmProfile.txtPassword.Enabled = false;
                                //frmProfile.cboAccessLevel.Enabled = false;

                                frmProfile.lblConfirmPassword.Visible = false;
                                frmProfile.txtConfirmPassword.Visible = false;

                                frmProfile._IsEdit = false;
                                frmProfile.btnCreate.Text = "Edit";
                                frmProfile.IsLogout = true;
                                //frmProfile.Close();
                                return;
                            }
                            else 
                            { 
                                return; 
                            }
                        }
                        
                        // For User
                        dbaUserSetting.UID = Convert.ToInt32(_UserID);
                        dbaUserSetting.FNAME = Regex.Replace(frmProfile.txtFullName.Text.Trim(), @"\s+", " ");
                        dbaUserSetting.ADDRESS = Regex.Replace(frmProfile.txtAddress.Text.Trim(), @"\s+", " ");
                        dbaUserSetting.PHONE = Regex.Replace(frmProfile.txtPhone.Text.Trim(), @"\s+", " ");
                        dbaUserSetting.PID = Convert.ToInt32(frmProfile.cboPosition.SelectedValue);

                        dbaUserSetting.ACTION = 1;
                        dbaUserSetting.SaveData();

                        // For Account
                        dbaAccountSetting.ACCOUNTID = Convert.ToInt32(_AccountID);
                        dbaAccountSetting.USERID = Convert.ToInt32(_UserID);
                        dbaAccountSetting.UNAME = Regex.Replace(frmProfile.txtUserName.Text.Trim(), @"\s+", " ");
                        dbaAccountSetting.PASS = PwEncryption.Encrypt(Regex.Replace(frmProfile.txtPassword.Text.Trim(), @"\s+", " "));
                        dbaAccountSetting.ACCESSID = Convert.ToInt32(frmProfile.cboAccessLevel.SelectedValue);


                        dbaAccountSetting.ACTION = 1;
                        dbaAccountSetting.SaveData();

                        MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);

                        frmProfile._IsEdit = false;
                        frmProfile.btnCreate.Text = "Edit";
                        ShowData();
                    }
                }


                // For Both Users and Accounts 
                //if (_IsEnabled)
                //{



                //}
                //else
                //{
                //    // For Users
                //    spString = string.Format("SP_Select_Users N'{0}',N'{1}',N'{2}',N'{3}'", Regex.Replace(frmProfile.txtFullName.Text.Trim(), @"\s+", " "),
                //    Regex.Replace(frmProfile.txtAddress.Text.Trim(), @"\s+", " "), "0", "5");

                //    DT = dbaConnection.SelectData(spString);
                //    if (DT.Rows.Count > 0 && _UserID != Convert.ToInt32(DT.Rows[0]["UserID"]))
                //    {
                //        MessageBox.Show("This User is Already Exist");
                //        frmProfile.txtFullName.Focus();
                //        frmProfile.txtFullName.SelectAll();
                //    }
                //    else
                //    {
                //        dbaUserSetting.UID = Convert.ToInt32(_UserID);
                //        dbaUserSetting.FNAME = Regex.Replace(frmProfile.txtFullName.Text.Trim(), @"\s+", " ");
                //        dbaUserSetting.ADDRESS = Regex.Replace(frmProfile.txtAddress.Text.Trim(), @"\s+", " ");
                //        dbaUserSetting.PHONE = Regex.Replace(frmProfile.txtPhone.Text.Trim(), @"\s+", " ");
                //        dbaUserSetting.PID = Convert.ToInt32(frmProfile.cboPosition.SelectedValue);


                //        if (_IsEdit)
                //        {
                //            dbaUserSetting.UID = Convert.ToInt32(_UserID);
                //            dbaUserSetting.ACTION = 1;
                //            dbaUserSetting.SaveData();

                //            MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                //            frmProfile.Close();
                //        }
                //        else
                //        {
                //            dbaUserSetting.ACTION = 0;
                //            dbaUserSetting.SaveData();
                //            MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                //            frmProfile.Close();
                //        }
                //    }
                //}
            }
        }

        public void AddCombo(ComboBox cboCombo, string spString, string Display, string Value, bool _IsEdit)
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

            // For Your Authority
            string spAccess = "";
            DataTable dtAccess = new DataTable();
            spAccess = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", Program.UserAccessID, "0", "1");
            dtAccess = dbaConnection.SelectData(spAccess);

            //if (Program.UserID != 0)
            //{
            //    Program.UserAuthority = Convert.ToInt32(dtAccess.Rows[0]["Authority"]);
            //}

            try
            {
                dbaConnection.DataBaseConn();
                SqlDataAdapter Adpt = new SqlDataAdapter(spString, dbaConnection.con);
                Adpt.Fill(DTAC);
                for (int i = 0; i < DTAC.Rows.Count; i++)
                {
                    Dr = DTCombo.NewRow();

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
            string spString;

            // For Access Combobox
            string _AccessLevelDisplay = "";
            _AccessLevelDisplay = frmProfile.cboAccessLevel.DisplayMember;
            if (_AccessLevelDisplay == string.Empty)
                _AccessLevelDisplay = "0";

            spString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", "0", "0", "0");

            AddCombo(frmProfile.cboAccessLevel, spString, "AccessLevel", "AccessID", _IsEdit);

            frmProfile.cboAccessLevel.SelectedValue = Convert.ToInt32(_AccessLevelDisplay); //This is in the box value you see


            // For Position Combobox
            string _PositionDisplay = "";
            _PositionDisplay = frmProfile.cboPosition.DisplayMember;
            if (_PositionDisplay == string.Empty)
                _PositionDisplay = "0";

            spString = string.Format("SP_Select_Position N'{0}',N'{1}',N'{2}'", "0", "0", "0");

            AddCombo(frmProfile.cboPosition, spString, "PositionName", "PositionID", _IsEdit);

            frmProfile.cboPosition.SelectedValue = Convert.ToInt32(_PositionDisplay); //This is in the box value you see
            //positionlevelindex = frmProfile.cboPosition.SelectedIndex;


        }

        bool isPasswordShown = false;

        //public void EyeToggle()
        //{
        //    // Set initial state
        //    frmProfile.btnEye.Text = "👁️";
        //    frmProfile.txtPassword.UseSystemPasswordChar = true;

        //    // Attach click event
        //    frmProfile.btnEye.Click += (s, e) =>
        //    {
        //        isPasswordShown = !isPasswordShown;

        //        frmProfile.txtPassword.UseSystemPasswordChar = !isPasswordShown;

        //        // Toggle emoji on the button
        //        frmProfile.btnEye.Text = isPasswordShown ? "🚫" : "👁️";
        //    };
        //}

        public void EyeToggle()
        {
            if (frmProfile.btnEye.Text == "👁️")
            {
                frmProfile.txtPassword.UseSystemPasswordChar = false;
                //frmProfile.txtPassword.PasswordChar = '\0';
                frmProfile.btnEye.Text = "🚫";
            }
            else
            {
                frmProfile.txtPassword.UseSystemPasswordChar = true;
                //frmProfile.txtPassword.PasswordChar = '*';
                frmProfile.btnEye.Text = "👁️";
            }
        }
    }
}
