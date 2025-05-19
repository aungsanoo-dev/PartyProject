using F21Party.DBA;
using F21Party.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F21Party.Controllers
{
    internal class CtrlFrmCreatePage
    {
        public frm_CreatePage frmCreatePage;// Declare the View
        public CtrlFrmCreatePage(frm_CreatePage createPageForm)
        {
            frmCreatePage = createPageForm; // Create the View
        }

        DbaConnection dbaConnection = new DbaConnection();
        DbaPage dbaPage = new DbaPage();
        private bool _IsEdit;
        private int _PageID;

        public void CreateClick()
        {
            DataTable DT = new DataTable();
            string spString = "";
            //_IsEdit = frmCreatePage._IsEdit;
            _PageID = frmCreatePage._PageID;
            _IsEdit = frmCreatePage._IsEdit;

            if (frmCreatePage.txtPageName.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Page Name");
                frmCreatePage.txtPageName.Focus();
            }
            else
            {
                // For Page
                spString = string.Format("SP_Select_Page N'{0}',N'{1}',N'{2}'", Regex.Replace(frmCreatePage.txtPageName.Text.Trim(), @"\s+", " "),
                "0", "2");

                DT = dbaConnection.SelectData(spString);
                if (DT.Rows.Count > 0 && _PageID != Convert.ToInt32(DT.Rows[0]["PageID"]))
                {
                    MessageBox.Show("This PageName is Already Exist");
                    frmCreatePage.txtPageName.Focus();
                    frmCreatePage.txtPageName.SelectAll();
                }
                else
                {
                    dbaPage.PID = Convert.ToInt32(_PageID);
                    dbaPage.PNAME = Regex.Replace(frmCreatePage.txtPageName.Text.Trim(), @"\s+", " ");

                    if (_IsEdit)
                    {
                        dbaPage.PID = Convert.ToInt32(_PageID);
                        dbaPage.ACTION = 1;
                        dbaPage.SaveData();

                        MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                        frmCreatePage.Close();
                    }
                    else
                    {
                        dbaPage.ACTION = 0;
                        dbaPage.SaveData();
                        MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                        frmCreatePage.Close();
                    }
                }
            }
        }
    }
}
