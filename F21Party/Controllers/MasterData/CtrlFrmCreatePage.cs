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
        private readonly frm_CreatePage _frmCreatePage;// Declare the View
        private readonly DbaConnection _dbaConnection = new DbaConnection();
        private readonly DbaPage _dbaPage = new DbaPage();
        private bool _isEdit;
        private int _pageID;
        private string _spString;
        public CtrlFrmCreatePage(frm_CreatePage createPageForm)
        {
            _frmCreatePage = createPageForm; // Create the View
        }

        public void CreateClick()
        {
            DataTable dt = new DataTable();
            //string spString = "";
            //_IsEdit = frmCreatePage._IsEdit;
            _pageID = _frmCreatePage.PageID;
            _isEdit = _frmCreatePage.IsEdit;

            if (_frmCreatePage.txtPageName.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Page Name");
                _frmCreatePage.txtPageName.Focus();
            }
            else
            {
                // For Page
                _spString = string.Format("SP_Select_Page N'{0}',N'{1}',N'{2}'", Regex.Replace(_frmCreatePage.txtPageName.Text.Trim(), @"\s+", " "),
                "0", "2");

                dt = _dbaConnection.SelectData(_spString);
                if (dt.Rows.Count > 0 && _pageID != Convert.ToInt32(dt.Rows[0]["PageID"]))
                {
                    MessageBox.Show("This PageName is Already Exist");
                    _frmCreatePage.txtPageName.Focus();
                    _frmCreatePage.txtPageName.SelectAll();
                }
                else
                {
                    _dbaPage.PID = Convert.ToInt32(_pageID);
                    _dbaPage.PNAME = Regex.Replace(_frmCreatePage.txtPageName.Text.Trim(), @"\s+", " ");

                    if (_isEdit)
                    {
                        _dbaPage.PID = Convert.ToInt32(_pageID);
                        _dbaPage.ACTION = 1;
                        _dbaPage.SaveData();

                        MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                        _frmCreatePage.Close();
                    }
                    else
                    {
                        _dbaPage.ACTION = 0;
                        _dbaPage.SaveData();
                        MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                        _frmCreatePage.Close();
                    }
                }
            }
        }
    }
}
