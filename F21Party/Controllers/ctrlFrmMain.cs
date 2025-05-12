using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using F21Party.DBA;
using F21Party.Views;


namespace F21Party.Controllers
{
    internal class CtrlFrmMain
    {
        public Views.frm_Main frmMain; // Declare the View
        
        public CtrlFrmMain(Views.frm_Main mainForm)
        {
            frmMain = mainForm; // Create the View
        }

        public void ShowMenu(string AccessLevel)
        {
            string[] Arr_AccessLevel = AccessLevel.Split(',');
            for (int i = 1; i < frmMain.menuStrip1.Items.Count; i++)
            {
                ToolStripMenuItem MainMenu = (ToolStripMenuItem)frmMain.menuStrip1.Items[i];
                foreach (ToolStripItem SubMenu in MainMenu.DropDownItems)
                {
                    SubMenu.Enabled = false;
                    foreach (string Menu in Arr_AccessLevel)
                    {
                        //MessageBox.Show(SubMenu.Name);
                        if (SubMenu.Name.ToString() == ("mnu" + Menu.ToString()))
                        {
                            SubMenu.Enabled = true;
                            break;
                        }
                    }


                 
                }
            }
            
        }

        public void LoginAccount()
        {
            if (frmMain.mnuLogIn.Text == "Logout")
            {
                if (MessageBox.Show("Are You Sure You Want To Logout", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    frmMain.mnuLogIn.Text = "LogIn";
                    Program.UserID = 0;
                    Program.UserAccessID = 0;
                    Program.UserAccessLevel = "";
                    Program.UserAuthority = 0;
                    Program.PublicArrWriteAccessPages = Array.Empty<string>();
                    Program.PublicArrReadAccessPages = Array.Empty<string>();
                    ShowMenu("");
                }
                return;
            }
            

            DbaConnection dbaConnection = new DbaConnection();
            DataTable DT = new DataTable();
            DataTable DTPage = new DataTable();
            DataTable dtAccess = new DataTable();
            DataTable DTAccessPage = new DataTable();

            // Reuse the same login form
            frm_LogIn frmLogIn = new frm_LogIn();
            bool focusPasswordNextTime = false;

            // Handle focus once when shown
            frmLogIn.Shown += (s, e) =>
            {
                frmLogIn.BeginInvoke(new Action(() =>
                {
                    if (focusPasswordNextTime)
                        frmLogIn.txtPassword.Focus();
                    else
                        frmLogIn.txtUserName.Focus();
                }));
            };

            while (true)
            {
                var result = frmLogIn.ShowDialog();

                if (result != DialogResult.OK)
                {
                    break; // Cancelled
                }

                string UserName = frmLogIn.txtUserName.Text.Trim();
                string Password = frmLogIn.txtPassword.Text.Trim();

                if (string.IsNullOrWhiteSpace(UserName))
                {
                    MessageBox.Show("Please Type User Name");
                    focusPasswordNextTime = false;
                    continue;
                }

                if (string.IsNullOrWhiteSpace(Password))
                {
                    MessageBox.Show("Please Type Password");
                    focusPasswordNextTime = true;
                    continue;
                }

                string spString = string.Format("SP_Select_Accounts N'{0}',N'{1}',N'{2}',N'{3}'",
                    UserName, Password, "", "1");

                DT = dbaConnection.SelectData(spString);

                if (DT.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid UserName And Password");
                    focusPasswordNextTime = false;
                    continue;
                }

                // Get User AccessID
                Program.UserAccessID = Convert.ToInt32(DT.Rows[0]["AccessID"]);

                // For AccessLevel
                string SPAccess = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", Program.UserAccessID,
                    "", 1);
                dtAccess = dbaConnection.SelectData(SPAccess);

                Program.UserAuthority = Convert.ToInt32(dtAccess.Rows[0]["Authority"]);

                // Check Log In Access
                if (dtAccess.Rows[0]["LogInAccess"].ToString() == "False")
                {
                    MessageBox.Show("You don't have 'LogIn' Access!");
                    Program.UserAccessID = 0;
                    break;
                }

                // Set global user information
                Program.UserAccessLevel = dtAccess.Rows[0]["AccessLevel"].ToString();
                Program.UserID = Convert.ToInt32(DT.Rows[0]["UserID"]);

                // For Pages
                string SPpage = string.Format("SP_Select_View_AccessPage N'{0}',N'{1}',N'{2}',N'{3}'", Program.UserAccessID, "", "", 1);
                //string SPpage = string.Format("SP_Select_Page N'{0}',N'{1}',N'{2}'", "", "", 1);
                DTPage = dbaConnection.SelectData(SPpage);

                // To Check Read and Write Value
                List<string> ReadWrite = new List<string>();

                // For AccessLevel
                foreach (DataRow row in DTPage.Rows)
                {
                    ReadWrite.Add(row["PermissionName"].ToString()); // To check if Read and Write are exist
                }

                if (!ReadWrite.Contains("Read") && !ReadWrite.Contains("Write"))
                {
                    MessageBox.Show("Error in Database. Read and Write Accesses aren't found");
                    Program.UserAccessID = 0;
                    Program.UserAccessLevel = "";
                    Program.UserID = 0;
                    break;
                }

                // For AccessLevel (Write)
                List<string> WriteAccessPages = new List<string>();
                foreach (DataRow row in DTPage.Rows)
                {
                    string SPLevel = string.Format("SP_Select_View_AccessPage N'{0}',N'{1}',N'{2}',N'{3}'", 
                        Program.UserAccessID, row["PageName"].ToString(), "Write", 3);

                    DTAccessPage = dbaConnection.SelectData(SPLevel);
                    if (DTAccessPage == null || DTAccessPage.Rows.Count == 0)
                    {
                        continue;
                    }
                    if (DTAccessPage.Rows[0]["AccessValue"].ToString() == "True")
                    {
                        WriteAccessPages.Add(DTAccessPage.Rows[0]["PageName"].ToString());
                    }
                }
                string WritePagesString = string.Join(",", WriteAccessPages.Distinct()); // Page Name Values
                Program.PublicArrWriteAccessPages = WritePagesString.Split(',');

                // For AccessLevel (Read)
                List<string> ReadAccessPages = new List<string>();
                foreach (DataRow row in DTPage.Rows)
                {
                    string SPLevel = string.Format("SP_Select_View_AccessPage N'{0}',N'{1}',N'{2}',N'{3}'",
                        Program.UserAccessID, row["PageName"].ToString(), "Read", 3);

                    DTAccessPage = dbaConnection.SelectData(SPLevel);
                    if (DTAccessPage == null || DTAccessPage.Rows.Count == 0)
                    {
                        continue;
                    }
                    if (DTAccessPage.Rows[0]["AccessValue"].ToString() == "True")
                    {
                        ReadAccessPages.Add(DTAccessPage.Rows[0]["PageName"].ToString());
                    }
                }
                string ReadPagesString = string.Join(",", ReadAccessPages.Distinct()); // Page Name Values
                Program.PublicArrReadAccessPages = ReadPagesString.Split(',');


                frmMain.mnuLogIn.Text = "Logout";
                //MessageBox.Show(Program.UserAccessLevel.ToString());
                //MessageBox.Show(Program.UserAuthority.ToString());
                ShowMenu(ReadPagesString);
                break;
            }
        }

        //bool isPasswordShown = false;

        //public void EyeToggle()
        //{
        //    frm_LogIn frmLogIn = new frm_LogIn();

        //    // Set initial icon and masking state
        //    frmLogIn.btnEye.Text = "👁️";
        //    frmLogIn.txtPassword.UseSystemPasswordChar = true;

        //    // Attach the toggle event
        //    frmLogIn.btnEye.Click += (s, e) =>
        //    {
        //        isPasswordShown = !isPasswordShown;

        //        frmLogIn.txtPassword.UseSystemPasswordChar = !isPasswordShown;

        //        frmLogIn.btnEye.Text = isPasswordShown ? "🚫" : "👁️";
        //    };
        //}




    }
}
