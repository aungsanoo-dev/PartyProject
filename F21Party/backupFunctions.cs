using F21Party.DBA;
using F21Party.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F21Party
{
    internal class BackupFunctions
    {
        public void LoginAccount()
        {
        //    if (frmMain.mnuLogIn.Text == "Logout")
        //    {
        //        if (MessageBox.Show("Are You Sure You Want To Logout", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
        //        {
        //            frmMain.mnuLogIn.Text = "LogIn";
        //            ShowMenu("");
        //        }
        //        return;
        //    }
        //    clsMainDB obj_clsMainDB = new clsMainDB();
        //    frm_LogIn frmLogIn = new frm_LogIn();
        //    DataTable DT = new DataTable();
        //    DataTable DTPage = new DataTable();
        //    String UserName = "";
        //    String Password = "";


        //Start:
        //    frmLogIn.txtUserName.Text = UserName;
        //    frmLogIn.txtPassword.Text = Password;
        //    if (frmLogIn.ShowDialog() == DialogResult.OK)
        //    {
        //        // Username Validation
        //        if (frmLogIn.txtUserName.Text.Trim().ToString() == string.Empty)
        //        {
        //            MessageBox.Show("Please Type User Name");
        //            frmLogIn.txtUserName.Focus();
        //            goto Start;
        //        }
        //        UserName = frmLogIn.txtUserName.Text;

        //        // Password Validation
        //        if (frmLogIn.txtPassword.Text.Trim().ToString() == string.Empty)
        //        {
        //            MessageBox.Show("Please Type Password");
        //            frmLogIn.txtPassword.Focus();
        //            goto Start;
        //        }
        //        Password = frmLogIn.txtPassword.Text;

        //        // For Select Procedure
        //        string spString = string.Format("SP_Select_Accounts N'{0}',N'{1}',N'{2}',N'{3}'",
        //        frmLogIn.txtUserName.Text.Trim().ToString(),
        //        frmLogIn.txtPassword.Text.Trim().ToString(), "", "1");

        //        // Pass it to DT to check the UserLevel
        //        DT = obj_clsMainDB.SelectData(spString);

        //        // For Pages
        //        string SPpage = string.Format("SP_Select_Page N'{0}',N'{1}',N'{2}'", "", "", 1);

        //        DTPage = obj_clsMainDB.SelectData(SPpage);

        //        if (DT.Rows.Count > 0)
        //        {
        //            // For Global UserID and UserAccessID
        //            Program.UserID = Convert.ToInt32(DT.Rows[0]["UserID"].ToString());
        //            Program.UserAccessID = Convert.ToInt32(DT.Rows[0]["AccessID"]);

        //            // Userlevel checking
        //            List<String> pagename = new List<String>();
        //            string UserLevel = "";
        //            if (DTPage.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < DTPage.Rows.Count; i++)
        //                {
        //                    pagename.Add(DTPage.Rows[i]["PageName"].ToString());
        //                }

        //                UserLevel = String.Join(",", pagename);
        //            }


        //            frmMain.mnuLogIn.Text = "Logout";
        //            ShowMenu(UserLevel);
        //        }
        //        else
        //        {
        //            MessageBox.Show("Invalid UserName And Password");
        //            goto Start;
        //        }
        //    }
        }
    }
}
