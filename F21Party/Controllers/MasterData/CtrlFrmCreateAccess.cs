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
    internal class CtrlFrmCreateAccess
    {
        private readonly frm_CreateAccess _frmCreateAccess;// Declare the View
        private readonly frm_CreateAccessAuthority _frmCreateAccessAuthority;// Declare the View for Authority
        private readonly DbaConnection _dbaConnection = new DbaConnection();
        private readonly DbaAccess _dbaAccessSetting = new DbaAccess();
        //private bool _IsEdit;
        private int _AccessID;
        private int _AccessAuthorityID;
        private int _Authority;
        private string _spString;
        public CtrlFrmCreateAccess(frm_CreateAccess createAccessForm)
        {
            _frmCreateAccess = createAccessForm; // Create the View
        }
        public CtrlFrmCreateAccess(frm_CreateAccessAuthority createAccessAuthorityForm)
        {
            _frmCreateAccessAuthority = createAccessAuthorityForm; // Create the View
        }
        
        public void ShowCombo(dynamic form)
        {
            DataTable dtCombo = new DataTable();
            DataRow dr;

            string logInAccessDisplay = "";
            logInAccessDisplay = form.cboLogInAccess.DisplayMember;
            // Define columns
            dtCombo.Columns.Add("AccessText");
            dtCombo.Columns.Add("AccessValue");

            // Add default selection
            dr = dtCombo.NewRow();
            dr["AccessText"] = "---Select---";
            dr["AccessValue"] = "";
            dtCombo.Rows.Add(dr);

            // Add True
            dr = dtCombo.NewRow();
            dr["AccessText"] = "True";
            dr["AccessValue"] = "True";
            dtCombo.Rows.Add(dr);

            // Add False
            dr = dtCombo.NewRow();
            dr["AccessText"] = "False";
            dr["AccessValue"] = "False";
            dtCombo.Rows.Add(dr);

            // Bind to ComboBox
            form.cboLogInAccess.DisplayMember = "AccessText";
            form.cboLogInAccess.ValueMember = "AccessValue";
            form.cboLogInAccess.DataSource = dtCombo;
            
            form.cboLogInAccess.SelectedValue = logInAccessDisplay;
           
        }

        public void CreateClick()
        {
            DataTable dt = new DataTable();
            _AccessID = _frmCreateAccess.AccessID;
            //_IsEdit = frmCreateAccessAuthority._IsEdit;
            //_AccessAuthorityID = frmCreateAccessAuthority._AccessID;
            _Authority = _frmCreateAccess.Authority;

            if (_frmCreateAccess.txtAccessLevel.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type FullName.");
                _frmCreateAccess.txtAccessLevel.Focus();
            }
            else if (_frmCreateAccess.cboLogInAccess.SelectedValue.ToString() == "")
            {
                MessageBox.Show("Please Choose LogIn Access.");
                _frmCreateAccess.cboLogInAccess.Focus();
            }
            else
            {
                // For Access
                _spString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", Regex.Replace(_frmCreateAccess.txtAccessLevel.Text.Trim(), @"\s+", " "),
                "0", "2");

                dt = _dbaConnection.SelectData(_spString);
                if (dt.Rows.Count > 0 && _AccessID != Convert.ToInt32(dt.Rows[0]["AccessID"]))
                {
                    MessageBox.Show("This AccessLevel is Already Exist");
                    _frmCreateAccess.txtAccessLevel.Focus();
                    _frmCreateAccess.txtAccessLevel.SelectAll();
                }
                else
                {
                    _dbaAccessSetting.AID = Convert.ToInt32(_AccessID);
                    _dbaAccessSetting.ALEVEL = Regex.Replace(_frmCreateAccess.txtAccessLevel.Text.Trim(), @"\s+", " ");
                    _dbaAccessSetting.LIACCESS = _frmCreateAccess.cboLogInAccess.SelectedValue.ToString();
                    _dbaAccessSetting.AUTHORITY = Convert.ToInt32(_Authority);

                    _dbaAccessSetting.ACTION = 0;
                    _dbaAccessSetting.SaveData();
                    MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                    _frmCreateAccess.Close();

                    //if (_IsEdit)
                    //{
                    //    if (frmCreateAccessAuthority.txtAuthority.Text.Trim().ToString() == string.Empty)
                    //    {
                    //        MessageBox.Show("Please Type Authority.(e.g 1,2,3...etc)");
                    //        frmCreateAccessAuthority.txtAuthority.Focus();
                    //    }
                    //    else if(!int.TryParse(frmCreateAccessAuthority.txtAuthority.Text.Trim(), out _))
                    //    {
                    //        MessageBox.Show("Authority must be a valid number.");
                    //    }
                    //    else
                    //    {

                    //        dbaAccessSetting.AID = Convert.ToInt32(_AccessAuthorityID);
                    //        dbaAccessSetting.ALEVEL = Regex.Replace(frmCreateAccessAuthority.txtAccessLevel.Text.Trim(), @"\s+", " ");
                    //        dbaAccessSetting.LIACCESS = frmCreateAccessAuthority.cboLogInAccess.SelectedValue.ToString();
                    //        dbaAccessSetting.AUTHORITY = Convert.ToInt32(frmCreateAccessAuthority.txtAuthority.Text.Trim());
                    //        dbaAccessSetting.ACTION = 1;
                    //        dbaAccessSetting.SaveData();

                    //        MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                    //        frmCreateAccess.Close();
                    //    }
                    //}
                    //else
                    //{
                    //    dbaAccessSetting.ACTION = 0;
                    //    dbaAccessSetting.SaveData();
                    //    MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                    //    frmCreateAccess.Close();
                    //}
                }
            }
        }

        public void EditClick()
        {
            DataTable dt = new DataTable();
            //_IsEdit = frmCreateAccessAuthority._IsEdit;
            _AccessAuthorityID = _frmCreateAccessAuthority.AccessID;

            if (_frmCreateAccessAuthority.txtAccessLevel.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type FullName.");
                _frmCreateAccessAuthority.txtAccessLevel.Focus();
            }
            else if (_frmCreateAccessAuthority.cboLogInAccess.SelectedValue.ToString() == "")
            {
                MessageBox.Show("Please Choose LogIn Access.");
                _frmCreateAccessAuthority.cboLogInAccess.Focus();
            }
            else if(_frmCreateAccessAuthority.txtAuthority.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Authority.(e.g 1,2,3...etc)");
                _frmCreateAccessAuthority.txtAuthority.Focus();
            }
            else if (!int.TryParse(_frmCreateAccessAuthority.txtAuthority.Text.Trim(), out _))
            {
                MessageBox.Show("Authority must be a valid number.");
            }
            else
            {
                _spString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", Regex.Replace(_frmCreateAccessAuthority.txtAuthority.Text.Trim(), @"\s+", " "),"0", "5");

                dt = _dbaConnection.SelectData(_spString);

                // For Authority
                if (dt.Rows.Count > 0 && _AccessAuthorityID != Convert.ToInt32(dt.Rows[0]["AccessID"]))
                {
                    MessageBox.Show("This Authority is Already Exist");
                    _frmCreateAccessAuthority.txtAccessLevel.Focus();
                    _frmCreateAccessAuthority.txtAccessLevel.SelectAll();
                }
                else
                {
                    _dbaAccessSetting.AID = Convert.ToInt32(_AccessAuthorityID);
                    _dbaAccessSetting.ALEVEL = Regex.Replace(_frmCreateAccessAuthority.txtAccessLevel.Text.Trim(), @"\s+", " ");
                    _dbaAccessSetting.LIACCESS = _frmCreateAccessAuthority.cboLogInAccess.SelectedValue.ToString();
                    _dbaAccessSetting.AUTHORITY = Convert.ToInt32(_frmCreateAccessAuthority.txtAuthority.Text.Trim());
                    _dbaAccessSetting.ACTION = 1;
                    _dbaAccessSetting.SaveData();

                    MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                    _frmCreateAccessAuthority.Close();
                }
            }
        }
    }
}
