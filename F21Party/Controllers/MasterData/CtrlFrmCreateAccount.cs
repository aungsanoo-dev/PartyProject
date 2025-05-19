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
    internal class CtrlFrmCreateAccount
    {
        frm_AccountList frmAccountList; // Declare the View
        frm_CreateAccount frmCreateAccount;// Declare the View
        private bool _IsEnabled = true;
        private bool _LocalIsEnabled;

        public CtrlFrmCreateAccount(frm_CreateAccount createAccountForm)
        {
            frmCreateAccount = createAccountForm; // Create the View
        }

        DbaConnection dbaConnection = new DbaConnection();
        DbaAccounts dbaAccountSetting = new DbaAccounts();
        DbaUsers dbaUserSetting = new DbaUsers();

        
        //private int accesslevelindex;
        //private int positionlevelindex;
        bool _IsEdit;
        int _UserID;
        int _AccountID;

        public void AccessComboChange(bool _IsEdit)
        {
            string spString;
            spString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            DataTable DT = new DataTable();
            DT = dbaConnection.SelectData(spString);
            List<string> FalseLogIn = new List<string>();

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                if (DT.Rows[i]["LogInAccess"].ToString() == "False")
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
                    if (FalseLogIn.Contains(frmCreateAccount.cboAccessLevel.Text))
                    {
                        frmCreateAccount.txtUserName.Enabled = false;
                        frmCreateAccount.txtPassword.Enabled = false;
                        frmCreateAccount.txtConfirmPassword.Enabled = false;
                        _IsEnabled = false;
                        _LocalIsEnabled = _IsEnabled;
                    }
                    else
                    {
                        frmCreateAccount.txtUserName.Enabled = true;
                        frmCreateAccount.txtPassword.Enabled = true;
                        frmCreateAccount.txtConfirmPassword.Enabled = true;
                        _IsEnabled = true;
                        _LocalIsEnabled = _IsEnabled;
                    }
                }
            }
            //MessageBox.Show(frmCreateAccount.cboAccessLevel.DisplayMember);
            //frmCreateAccount.cboAccessLevel.SelectedValue = "1003";



        }
        public void AddAccountClick()
        {
            //frmMain obj_frmMain = new frmMain();
            DataTable DT = new DataTable();
            string spString = "";
            //_IsEdit = frmCreateAccount._IsEdit;
            _AccountID = frmCreateAccount._AccountID;
            _UserID = frmCreateAccount._UserID;

            if (frmCreateAccount.cboAccessLevel.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose AccessLevel");
                frmCreateAccount.cboAccessLevel.Focus();
            }
            else if (frmCreateAccount.txtUserName.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type UserName");
                frmCreateAccount.txtUserName.Focus();
            }
            else if (frmCreateAccount.txtPassword.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Password");
                frmCreateAccount.txtPassword.Focus();
            }
            else if (frmCreateAccount.txtConfirmPassword.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type ConfirmPassword");
                frmCreateAccount.txtConfirmPassword.Focus();
            }
            else if (frmCreateAccount.txtPassword.Text.Trim().ToString() != frmCreateAccount.txtConfirmPassword.Text.Trim().ToString())
            {
                MessageBox.Show("Password And Confirm Password Should Be Same");
                frmCreateAccount.txtConfirmPassword.Focus();
                frmCreateAccount.txtConfirmPassword.SelectAll();
            }
            else 
            {
                spString = string.Format("SP_Select_Accounts N'{0}',N'{1}',N'{2}',N'{3}'", Regex.Replace(frmCreateAccount.txtUserName.Text.Trim(), @"\s+", " "),
                        "0", "0", "4");

                DT = dbaConnection.SelectData(spString);
                if (DT.Rows.Count > 0 && _AccountID != Convert.ToInt32(DT.Rows[0]["AccountID"]))
                {

                    MessageBox.Show("This Account is Already Exist");
                    frmCreateAccount.txtUserName.Focus();
                    frmCreateAccount.txtUserName.SelectAll();
                }
                else
                {
                    // For User
                    dbaUserSetting.UID = Convert.ToInt32(_UserID);
                    dbaUserSetting.FNAME = Regex.Replace(frmCreateAccount.txtFullName.Text.Trim(), @"\s+", " ");
                    dbaUserSetting.ADDRESS = Regex.Replace(frmCreateAccount.txtAddress.Text.Trim(), @"\s+", " ");
                    dbaUserSetting.PHONE = Regex.Replace(frmCreateAccount.txtPhone.Text.Trim(), @"\s+", " ");
                    dbaUserSetting.PID = Convert.ToInt32(frmCreateAccount.cboPosition.SelectedValue);
                    dbaUserSetting.HASACC = "True";

                    // For Account
                    dbaAccountSetting.ACCOUNTID = Convert.ToInt32(_AccountID);
                    dbaAccountSetting.USERID = Convert.ToInt32(_UserID);
                    dbaAccountSetting.UNAME = Regex.Replace(frmCreateAccount.txtUserName.Text.Trim(), @"\s+", " ");
                    dbaAccountSetting.PASS = PwEncryption.Encrypt(Regex.Replace(frmCreateAccount.txtPassword.Text.Trim(), @"\s+", " "));
                    dbaAccountSetting.ACCESSID = Convert.ToInt32(frmCreateAccount.cboAccessLevel.SelectedValue);

                    dbaAccountSetting.ACTION = 0;
                    dbaAccountSetting.SaveData();

                    dbaUserSetting.ACTION = 3;
                    dbaUserSetting.SaveData();
                    MessageBox.Show("Successfully Add", "Successfully", MessageBoxButtons.OK);
                    frmCreateAccount.Close();



                    //spString = string.Format("SP_Select_Accounts N'{0}', N'{1}', N'{2}', N'{3}'", "0", "0", "0", "5");
                    //frmAccountList.dgvAccountSetting.DataSource = dbaConnection.SelectData(spString);
                    //frmAccountList.dgvAccountSetting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    //frmAccountList.dgvAccountSetting.Columns[0].FillWeight = 10;
                    //frmAccountList.dgvAccountSetting.Columns[1].Visible = false;
                    //frmAccountList.dgvAccountSetting.Columns[2].FillWeight = 10;
                    //frmAccountList.dgvAccountSetting.Columns[3].FillWeight = 24;
                    //frmAccountList.dgvAccountSetting.Columns[4].FillWeight = 24;
                    //frmAccountList.dgvAccountSetting.Columns[5].FillWeight = 10;
                    //frmAccountList.dgvAccountSetting.Columns[6].FillWeight = 22;

                    //dbaConnection.ToolStripTextBoxData(frmAccountList.tstSearchWith, spString, "UserName");
                }
            }
        }


        public void SaveClick()
        {
            //frmMain obj_frmMain = new frmMain();
            DataTable DT = new DataTable();
            string spString = "";
            _IsEdit = frmCreateAccount._IsEdit;
            _AccountID = frmCreateAccount._AccountID;
            _UserID = frmCreateAccount._UserID;

            if (frmCreateAccount.txtFullName.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type FullName");
                frmCreateAccount.txtFullName.Focus();
            }
            else if (frmCreateAccount.txtAddress.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Address");
                frmCreateAccount.txtAddress.Focus();
            }
            else if (frmCreateAccount.txtPhone.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Phone");
                frmCreateAccount.txtPhone.Focus();
            }
            else if (frmCreateAccount.cboPosition.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose Position");
                frmCreateAccount.cboPosition.Focus();
            }
            else if (frmCreateAccount.cboAccessLevel.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose AccessLevel");
                frmCreateAccount.cboAccessLevel.Focus();
            }
            else if (_LocalIsEnabled)
            {
                if (frmCreateAccount.txtUserName.Text.Trim().ToString() == string.Empty)
                {
                    MessageBox.Show("Please Type UserName");
                    frmCreateAccount.txtUserName.Focus();
                }
                else if (frmCreateAccount.txtPassword.Text.Trim().ToString() == string.Empty)
                {
                    MessageBox.Show("Please Type Password");
                    frmCreateAccount.txtPassword.Focus();
                }
                else if (frmCreateAccount.txtConfirmPassword.Text.Trim().ToString() == string.Empty)
                {
                    MessageBox.Show("Please Type ConfirmPassword");
                    frmCreateAccount.txtConfirmPassword.Focus();
                }
                else if (frmCreateAccount.txtPassword.Text.Trim().ToString() != frmCreateAccount.txtConfirmPassword.Text.Trim().ToString())
                {
                    MessageBox.Show("Password And Confirm Password Should Be Same");
                    frmCreateAccount.txtConfirmPassword.Focus();
                    frmCreateAccount.txtConfirmPassword.SelectAll();
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
                    spString = string.Format("SP_Select_Users N'{0}',N'{1}',N'{2}',N'{3}'", Regex.Replace(frmCreateAccount.txtFullName.Text.Trim(), @"\s+", " "),
                    Regex.Replace(frmCreateAccount.txtAddress.Text.Trim(), @"\s+", " "), "0", "5");

                    DT = dbaConnection.SelectData(spString);
                    if (DT.Rows.Count > 0 && _UserID != Convert.ToInt32(DT.Rows[0]["UserID"]))
                    {
                        
                        MessageBox.Show("This User is Already Exist");
                        frmCreateAccount.txtFullName.Focus();
                        frmCreateAccount.txtFullName.SelectAll();
                    } 
                    else
                    {
                        // For Accounts
                        spString = string.Format("SP_Select_Accounts N'{0}',N'{1}',N'{2}',N'{3}'", Regex.Replace(frmCreateAccount.txtUserName.Text.Trim(), @"\s+", " "),
                        "0", "0", "4");

                        DT = dbaConnection.SelectData(spString);
                        if (DT.Rows.Count > 0 && _AccountID != Convert.ToInt32(DT.Rows[0]["AccountID"]))
                        {
                            
                            MessageBox.Show("This Account is Already Exist");
                            frmCreateAccount.txtUserName.Focus();
                            frmCreateAccount.txtUserName.SelectAll();
                        }
                        else
                        {
                            // For User
                            dbaUserSetting.UID = Convert.ToInt32(_UserID);
                            dbaUserSetting.FNAME = Regex.Replace(frmCreateAccount.txtFullName.Text.Trim(), @"\s+", " ");
                            dbaUserSetting.ADDRESS = Regex.Replace(frmCreateAccount.txtAddress.Text.Trim(), @"\s+", " ");
                            dbaUserSetting.PHONE = Regex.Replace(frmCreateAccount.txtPhone.Text.Trim(), @"\s+", " ");
                            dbaUserSetting.PID = Convert.ToInt32(frmCreateAccount.cboPosition.SelectedValue);

                            // For Account
                            dbaAccountSetting.ACCOUNTID = Convert.ToInt32(_AccountID);
                            //dbaAccountSetting.USERID = Convert.ToInt32(_UserID);
                            dbaAccountSetting.UNAME = Regex.Replace(frmCreateAccount.txtUserName.Text.Trim(), @"\s+", " ");
                            dbaAccountSetting.PASS = PwEncryption.Encrypt(Regex.Replace(frmCreateAccount.txtPassword.Text.Trim(), @"\s+", " "));
                            dbaAccountSetting.ACCESSID = Convert.ToInt32(frmCreateAccount.cboAccessLevel.SelectedValue);


                            if (_IsEdit)
                            {
                                //dbaUserSetting.ACTION = 1;
                                //dbaUserSetting.SaveData();


                                //string SPAccess = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", "",
                                //    "", 0);
                                //DataTable DTAccess = dbaConnection.SelectData(SPAccess);

                                //for(int i = 0; i <=  DTAccess.Rows.Count; i++)
                                //{
                                //    MessageBox.Show(DTAccess);
                                //}

                                dbaAccountSetting.USERID = Convert.ToInt32(_UserID);
                                if (dbaAccountSetting.USERID == Program.UserID)
                                {
                                    spString = string.Format("SP_Select_Accounts N'{0}',N'{1}',N'{2}',N'{3}'", Program.UserID.ToString(),
                                        "0", "0", "6");

                                    DT = dbaConnection.SelectData(spString);

                                    if(dbaAccountSetting.ACCESSID != Convert.ToInt32(DT.Rows[0]["AccessID"]))
                                    {
                                        MessageBox.Show("You cannot change your own account's access");
                                    }
                                    else
                                    {
                                        dbaAccountSetting.ACTION = 3;
                                        dbaAccountSetting.SaveData();

                                        MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                                        frmCreateAccount.Close();
                                    }

                                    
                                }
                                else
                                {
                                    //MessageBox.Show("Only Admin and SuperAdmin can change the access level!");

                                    dbaAccountSetting.ACTION = 3;
                                    dbaAccountSetting.SaveData();

                                    MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                                    frmCreateAccount.Close();
                                }

                                
                            }

                            else
                            {
                                dbaUserSetting.ACTION = 0;
                                dbaUserSetting.SaveData();

                                spString = string.Format("SP_Select_Users N'{0}',N'{1}',N'{2}',N'{3}'", Regex.Replace(frmCreateAccount.txtFullName.Text.Trim(), @"\s+", " "),
                                    Regex.Replace(frmCreateAccount.txtAddress.Text.Trim(), @"\s+", " "), Regex.Replace(frmCreateAccount.txtPhone.Text.Trim(), @"\s+", " "), "1");

                                DT = dbaConnection.SelectData(spString);
                                dbaAccountSetting.USERID = Convert.ToInt32(DT.Rows[0]["UserID"]);

                                dbaAccountSetting.ACTION = 0;
                                dbaAccountSetting.SaveData();

                                MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                                frmCreateAccount.Close();
                            }
                        }
                    }

                    
                }
                else
                {
                    // For Users
                    spString = string.Format("SP_Select_Users N'{0}',N'{1}',N'{2}',N'{3}'", Regex.Replace(frmCreateAccount.txtFullName.Text.Trim(), @"\s+", " "),
                    Regex.Replace(frmCreateAccount.txtAddress.Text.Trim(), @"\s+", " "), "0", "5");

                    DT = dbaConnection.SelectData(spString);
                    if (DT.Rows.Count > 0 && _UserID != Convert.ToInt32(DT.Rows[0]["UserID"]))
                    {
                        MessageBox.Show("This User is Already Exist");
                        frmCreateAccount.txtFullName.Focus();
                        frmCreateAccount.txtFullName.SelectAll();
                    }
                    else
                    {
                        dbaUserSetting.UID = Convert.ToInt32(_UserID);
                        dbaUserSetting.FNAME = Regex.Replace(frmCreateAccount.txtFullName.Text.Trim(), @"\s+", " ");
                        dbaUserSetting.ADDRESS = Regex.Replace(frmCreateAccount.txtAddress.Text.Trim(), @"\s+", " ");
                        dbaUserSetting.PHONE = Regex.Replace(frmCreateAccount.txtPhone.Text.Trim(), @"\s+", " ");
                        dbaUserSetting.PID = Convert.ToInt32(frmCreateAccount.cboPosition.SelectedValue);


                        if (_IsEdit)
                        {
                            dbaUserSetting.UID = Convert.ToInt32(_UserID);
                            dbaUserSetting.ACTION = 1;
                            dbaUserSetting.SaveData();

                            MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                            frmCreateAccount.Close();
                        }
                        else
                        {
                            dbaUserSetting.ACTION = 0;
                            dbaUserSetting.SaveData();
                            MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                            frmCreateAccount.Close();
                        }
                    }
                }
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
            spAccess = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", Program.UserAccessID,"0", "1");
            dtAccess = dbaConnection.SelectData(spAccess);

            //if(Program.UserID != 0)
            //{
            //    Program.UserAuthority =  Convert.ToInt32(dtAccess.Rows[0]["Authority"]);
            //}

            try
            {
                dbaConnection.DataBaseConn();
                SqlDataAdapter Adpt = new SqlDataAdapter(spString, dbaConnection.con);
                Adpt.Fill(DTAC);
                for (int i = 0; i < DTAC.Rows.Count; i++)
                {
                    Dr = DTCombo.NewRow(); 
                    if (frmCreateAccount.cboAccessLevel.DisplayMember == "AccessLevel" && Display == "AccessLevel")
                    {
                        goto Bottom;
                    }
                    //if (Program.UserID == 0)
                    //{
                    //    goto Bottom;
                    //}
                    // For Your Selected Authority
                    string spAccess2 = "";
                    DataTable dtAccess2 = new DataTable();
                    if (Display == "AccessLevel" && frmCreateAccount.cboAccessLevel.DisplayMember != "")
                    {
                        //MessageBox.Show(frmCreateAccount.cboAccessLevel.DisplayMember);
                        spAccess2 = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", Convert.ToInt32(frmCreateAccount.cboAccessLevel.DisplayMember), "0", "1");

                        dtAccess2 = dbaConnection.SelectData(spAccess2);
                    }
                    
                    if (Program.UserAuthority == 1 && Display == "AccessLevel")
                    {
                        // SuperAdmin Access is only visible when it is edited by SuperAdmin to itself
                        //MessageBox.Show(frmCreateAccount.cboAccessLevel.DisplayMember.ToString());
                        if (_IsEdit && Convert.ToInt32(dtAccess2.Rows[0]["Authority"]) == Program.UserAuthority)
                        {
                            
                        }
                        else
                        {
                            if (Convert.ToInt32(DTAC.Rows[i][Value]) == 1) // 1 is SuperAdmin.
                            {
                                continue;
                            }
                        }
                    }
                    if(_IsEdit)
                    {
                        if (Program.UserAuthority != 1 && Display == "AccessLevel")
                        {
                            if (Convert.ToInt32(DTAC.Rows[i][Value]) == 1) // 1 is SuperAdmin.
                            {
                                continue;
                            }
                        }
                        
                        //int spAccess3 = Convert.ToInt32(frmCreateAccount.cboAccessLevel.DisplayMember);
                        //if(frmCreateAccount.cboAccessLevel.DisplayMember != "AccessLevel")
                        //{
                        

                        if (Display == "AccessLevel" && Convert.ToInt32(dtAccess2.Rows[0]["Authority"]) >= Program.UserAuthority)
                        {
                            //MessageBox.Show(Convert.ToInt32(frmCreateAccount.cboAccessLevel.DisplayMember).ToString());

                            if (Convert.ToInt32(DTAC.Rows[i]["Authority"]) < Program.UserAuthority) // AccessID comparison
                            {
                                continue; // Remove Specific Accesslevel from Combo box.
                            }
                        }
                        //}
                        
                        
                        
                    }
                    else
                    {
                        if (Program.UserAuthority != 0 && Display == "AccessLevel")
                        {
                            if (Convert.ToInt32(DTAC.Rows[i][Value]) < Program.UserAuthority) // AccessID comparison
                            {
                                continue; // Remove Specific Accesslevel from Combo box.
                            }
                        }

                        if (Program.UserAuthority == 0 && Display == "AccessLevel")
                        {
                            if (Convert.ToInt32(DTAC.Rows[i][Value]) == 1 || Convert.ToInt32(DTAC.Rows[i][Value]) == 2) // 1 is SuperAdmin. 2 is Admin.
                            {
                                continue;
                            }
                        }
                    }

                Bottom:
                    //if (_IsEdit != true)
                    //{

                    //    if (DTAC.Rows[i][Display].ToString().Trim().ToUpper() == "ADMIN")
                    //    {
                    //        continue;
                    //    }
                    //}

                    //if (Program.UserAccessLevel.Trim().ToUpper() != "SUPERADMIN")
                    //{
                    //    if (DTAC.Rows[i][Display].ToString().Trim().ToUpper() == "SUPERADMIN")
                    //    {
                    //        continue;
                    //    }
                    //}

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
            _AccessLevelDisplay = frmCreateAccount.cboAccessLevel.DisplayMember;
            if (_AccessLevelDisplay == string.Empty)
                _AccessLevelDisplay = "0";

            spString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", "0", "0", "0");

            AddCombo(frmCreateAccount.cboAccessLevel, spString, "AccessLevel", "AccessID", _IsEdit);

            frmCreateAccount.cboAccessLevel.SelectedValue = Convert.ToInt32(_AccessLevelDisplay); //This is in the box value you see


            // For Position Combobox
            string _PositionDisplay = "";
            _PositionDisplay = frmCreateAccount.cboPosition.DisplayMember;
            if (_PositionDisplay == string.Empty)
                _PositionDisplay = "0";

            spString = string.Format("SP_Select_Position N'{0}',N'{1}',N'{2}'", "0", "0", "0");

            AddCombo(frmCreateAccount.cboPosition, spString, "PositionName", "PositionID", _IsEdit);

            frmCreateAccount.cboPosition.SelectedValue = Convert.ToInt32(_PositionDisplay); //This is in the box value you see
            //positionlevelindex = frmCreateAccount.cboPosition.SelectedIndex;

        }

        bool isPasswordShown = false;

        

        public void EyeToggle()
        {
            if(frmCreateAccount.btnEye.Text == "👁️")
            {
                frmCreateAccount.txtPassword.UseSystemPasswordChar = false;
                //frmCreateAccount.txtPassword.PasswordChar = '\0';
                frmCreateAccount.btnEye.Text = "🚫";
            }
            else
            {
                frmCreateAccount.txtPassword.UseSystemPasswordChar = true;
                //frmCreateAccount.txtPassword.PasswordChar = '*';
                frmCreateAccount.btnEye.Text = "👁️";
            }
        }

    }
}
