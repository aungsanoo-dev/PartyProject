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
        public frm_CreateAccess frmCreateAccess;// Declare the View
        public frm_CreateAccessAuthority frmCreateAccessAuthority;// Declare the View for Authority
        public CtrlFrmCreateAccess(frm_CreateAccess createAccessForm)
        {
            frmCreateAccess = createAccessForm; // Create the View
        }
        public CtrlFrmCreateAccess(frm_CreateAccessAuthority createAccessAuthorityForm)
        {
            frmCreateAccessAuthority = createAccessAuthorityForm; // Create the View
        }

        DbaConnection dbaConnection = new DbaConnection();
        DbaAccessSetting dbaAccessSetting = new DbaAccessSetting();
        public bool _IsEdit;
        private int _AccessID;
        private int _AccessAuthorityID;
        private int _Authority;
        
        public void ShowCombo(dynamic form)
        {
            DataTable DTCombo = new DataTable();
            DataRow Dr;

            string _LogInAccessDisplay = "";
            _LogInAccessDisplay = form.cboLogInAccess.DisplayMember;
            // Define columns
            DTCombo.Columns.Add("AccessText");
            DTCombo.Columns.Add("AccessValue");

            // Add default selection
            Dr = DTCombo.NewRow();
            Dr["AccessText"] = "---Select---";
            Dr["AccessValue"] = "";
            DTCombo.Rows.Add(Dr);

            // Add True
            Dr = DTCombo.NewRow();
            Dr["AccessText"] = "True";
            Dr["AccessValue"] = "True";
            DTCombo.Rows.Add(Dr);

            // Add False
            Dr = DTCombo.NewRow();
            Dr["AccessText"] = "False";
            Dr["AccessValue"] = "False";
            DTCombo.Rows.Add(Dr);

            // Bind to ComboBox
            form.cboLogInAccess.DisplayMember = "AccessText";
            form.cboLogInAccess.ValueMember = "AccessValue";
            form.cboLogInAccess.DataSource = DTCombo;
            
            form.cboLogInAccess.SelectedValue = _LogInAccessDisplay;
           
        }

        public void CreateClick()
        {
            DataTable DT = new DataTable();
            string spString = "";
            _AccessID = frmCreateAccess._AccessID;
            //_IsEdit = frmCreateAccessAuthority._IsEdit;
            //_AccessAuthorityID = frmCreateAccessAuthority._AccessID;
            _Authority = frmCreateAccess._Authority;

            if (frmCreateAccess.txtAccessLevel.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type FullName.");
                frmCreateAccess.txtAccessLevel.Focus();
            }
            else if (frmCreateAccess.cboLogInAccess.SelectedValue.ToString() == "")
            {
                MessageBox.Show("Please Choose LogIn Access.");
                frmCreateAccess.cboLogInAccess.Focus();
            }
            else
            {
                // For Access
                spString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", Regex.Replace(frmCreateAccess.txtAccessLevel.Text.Trim(), @"\s+", " "),
                "0", "2");

                DT = dbaConnection.SelectData(spString);
                if (DT.Rows.Count > 0 && _AccessID != Convert.ToInt32(DT.Rows[0]["AccessID"]))
                {
                    MessageBox.Show("This AccessLevel is Already Exist");
                    frmCreateAccess.txtAccessLevel.Focus();
                    frmCreateAccess.txtAccessLevel.SelectAll();
                }
                else
                {
                    dbaAccessSetting.AID = Convert.ToInt32(_AccessID);
                    dbaAccessSetting.ALEVEL = Regex.Replace(frmCreateAccess.txtAccessLevel.Text.Trim(), @"\s+", " ");
                    dbaAccessSetting.LIACCESS = frmCreateAccess.cboLogInAccess.SelectedValue.ToString();
                    dbaAccessSetting.AUTHORITY = Convert.ToInt32(_Authority);

                    dbaAccessSetting.ACTION = 0;
                    dbaAccessSetting.SaveData();
                    MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                    frmCreateAccess.Close();

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
            DataTable DT = new DataTable();
            string spString = "";
            //_IsEdit = frmCreateAccessAuthority._IsEdit;
            _AccessAuthorityID = frmCreateAccessAuthority._AccessID;

            if (frmCreateAccessAuthority.txtAccessLevel.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type FullName.");
                frmCreateAccessAuthority.txtAccessLevel.Focus();
            }
            else if (frmCreateAccessAuthority.cboLogInAccess.SelectedValue.ToString() == "")
            {
                MessageBox.Show("Please Choose LogIn Access.");
                frmCreateAccessAuthority.cboLogInAccess.Focus();
            }
            else if(frmCreateAccessAuthority.txtAuthority.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Authority.(e.g 1,2,3...etc)");
                frmCreateAccessAuthority.txtAuthority.Focus();
            }
            else if (!int.TryParse(frmCreateAccessAuthority.txtAuthority.Text.Trim(), out _))
            {
                MessageBox.Show("Authority must be a valid number.");
            }
            else
            {
                spString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", Regex.Replace(frmCreateAccessAuthority.txtAuthority.Text.Trim(), @"\s+", " "),"0", "5");

                DT = dbaConnection.SelectData(spString);

                // For Authority
                if (DT.Rows.Count > 0 && _AccessAuthorityID != Convert.ToInt32(DT.Rows[0]["AccessID"]))
                {
                    MessageBox.Show("This Authority is Already Exist");
                    frmCreateAccessAuthority.txtAccessLevel.Focus();
                    frmCreateAccessAuthority.txtAccessLevel.SelectAll();
                }
                else
                {
                    dbaAccessSetting.AID = Convert.ToInt32(_AccessAuthorityID);
                    dbaAccessSetting.ALEVEL = Regex.Replace(frmCreateAccessAuthority.txtAccessLevel.Text.Trim(), @"\s+", " ");
                    dbaAccessSetting.LIACCESS = frmCreateAccessAuthority.cboLogInAccess.SelectedValue.ToString();
                    dbaAccessSetting.AUTHORITY = Convert.ToInt32(frmCreateAccessAuthority.txtAuthority.Text.Trim());
                    dbaAccessSetting.ACTION = 1;
                    dbaAccessSetting.SaveData();

                    MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                    frmCreateAccessAuthority.Close();
                }
            }
        }
    }
}
