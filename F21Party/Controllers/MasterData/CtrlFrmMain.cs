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
        private readonly frm_Main _frmMain; // Declare the View
        private string _spString;
        public CtrlFrmMain(frm_Main mainForm)
        {
            _frmMain = mainForm; // Create the View
        }

        public void ShowMenu(string accessLevel)
        {
            string[] arrAccessLevel = accessLevel.Split(',');
            for (int i = 1; i < _frmMain.menuStrip1.Items.Count; i++)
            {
                ToolStripMenuItem mainMenu = (ToolStripMenuItem)_frmMain.menuStrip1.Items[i];
                foreach (ToolStripItem subMenu in mainMenu.DropDownItems)
                {
                    subMenu.Enabled = false;
                    foreach (string Menu in arrAccessLevel)
                    {
                        //MessageBox.Show(SubMenu.Name);
                        if (subMenu.Name.ToString() == ("mnu" + Menu.ToString()))
                        {
                            subMenu.Enabled = true;
                            //MessageBox.Show("Hello");
                            break;
                        }
                    }
                }
            }
        }

        public void LoginAccount()
        {
            if (_frmMain.mnuLogIn.Text == "Logout" || _frmMain.btnLogIn.Text == "Logout")
            {
                if (MessageBox.Show("Are You Sure You Want To Logout", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _frmMain.mnuLogIn.Text = "LogIn";
                    _frmMain.btnLogIn.Text = "LogIn";
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
            DataTable dt = new DataTable();
            DataTable dtPage = new DataTable();
            DataTable dtAccess = new DataTable();
            DataTable dtAccessPage = new DataTable();

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

                string userName = frmLogIn.txtUserName.Text.Trim();
                string password = PwEncryption.Encrypt(frmLogIn.txtPassword.Text.Trim());

                if (string.IsNullOrWhiteSpace(userName))
                {
                    MessageBox.Show("Please Type User Name");
                    focusPasswordNextTime = false;
                    continue;
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Please Type Password");
                    focusPasswordNextTime = true;
                    continue;
                }

                _spString = string.Format("SP_Select_Accounts N'{0}',N'{1}',N'{2}',N'{3}'",
                    userName, password, "", "1");

                dt = dbaConnection.SelectData(_spString);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid UserName And Password");
                    focusPasswordNextTime = false;
                    continue;
                }

                // Get User AccessID
                Program.UserAccessID = Convert.ToInt32(dt.Rows[0]["AccessID"]);

                // For AccessLevel
                _spString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", Program.UserAccessID,
                    "", 1);
                dtAccess = dbaConnection.SelectData(_spString);

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
                Program.UserID = Convert.ToInt32(dt.Rows[0]["UserID"]);

                // For Pages
                _spString = string.Format("SP_Select_View_AccessPage N'{0}',N'{1}',N'{2}',N'{3}'", Program.UserAccessID, "", "", 1);
                //string SPpage = string.Format("SP_Select_Page N'{0}',N'{1}',N'{2}'", "", "", 1);
                dtPage = dbaConnection.SelectData(_spString);

                // To Check Read and Write Value
                List<string> readWrite = new List<string>();

                // For AccessLevel
                foreach (DataRow row in dtPage.Rows)
                {
                    readWrite.Add(row["PermissionName"].ToString()); // To check if Read and Write are exist
                }

                if (!readWrite.Contains("Read") && !readWrite.Contains("Write"))
                {
                    MessageBox.Show("Error in Database. Read and Write Accesses aren't found");
                    Program.UserAccessID = 0;
                    Program.UserAccessLevel = "";
                    Program.UserID = 0;
                    break;
                }

                // For AccessLevel (Write)
                List<string> writeAccessPages = new List<string>();
                foreach (DataRow row in dtPage.Rows)
                {
                    _spString = string.Format("SP_Select_View_AccessPage N'{0}',N'{1}',N'{2}',N'{3}'", 
                        Program.UserAccessID, row["PageName"].ToString(), "Write", 3);

                    dtAccessPage = dbaConnection.SelectData(_spString);
                    if (dtAccessPage == null || dtAccessPage.Rows.Count == 0)
                    {
                        continue;
                    }
                    if (dtAccessPage.Rows[0]["AccessValue"].ToString() == "True")
                    {
                        writeAccessPages.Add(dtAccessPage.Rows[0]["PageName"].ToString());
                    }
                }
                string writePagesString = string.Join(",", writeAccessPages.Distinct()); // Page Name Values
                Program.PublicArrWriteAccessPages = writePagesString.Split(',');

                // For AccessLevel (Read)
                List<string> readAccessPages = new List<string>();
                foreach (DataRow row in dtPage.Rows)
                {
                    _spString = string.Format("SP_Select_View_AccessPage N'{0}',N'{1}',N'{2}',N'{3}'",
                        Program.UserAccessID, row["PageName"].ToString(), "Read", 3);

                    dtAccessPage = dbaConnection.SelectData(_spString);
                    if (dtAccessPage == null || dtAccessPage.Rows.Count == 0)
                    {
                        continue;
                    }
                    if (dtAccessPage.Rows[0]["AccessValue"].ToString() == "True")
                    {
                        readAccessPages.Add(dtAccessPage.Rows[0]["PageName"].ToString());
                    }
                }
                string readPagesString = string.Join(",", readAccessPages.Distinct()); // Page Name Values
                Program.PublicArrReadAccessPages = readPagesString.Split(',');


                _frmMain.mnuLogIn.Text = "Logout";
                _frmMain.btnLogIn.Text = "Logout";
                //MessageBox.Show(Program.UserAccessLevel.ToString());
                //MessageBox.Show(Program.UserAuthority.ToString());
                ShowMenu(readPagesString);
                break;
            }
        }
    }
}
