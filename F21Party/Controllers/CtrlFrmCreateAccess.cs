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
using F21Party.DBA;


namespace F21Party.Controllers
{
    internal class CtrlFrmCreateAccess
    {
        public frm_CreateAccess frmCreateAccess;// Declare the View
        public CtrlFrmCreateAccess(frm_CreateAccess createAccessForm)
        {
            frmCreateAccess = createAccessForm; // Create the View
        }

        DbaConnection dbaConnection = new DbaConnection();
        DbaAccessSetting dbaAccessSetting = new DbaAccessSetting();
        public bool _IsEdit;
        private int _AccessID;
        
        public void ShowCombo(bool isEdit)
        {
            DataTable DTCombo = new DataTable();
            DataRow Dr;

            string _LogInAccessDisplay = "";
            _LogInAccessDisplay = frmCreateAccess.cboLogInAccess.DisplayMember;
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
            frmCreateAccess.cboLogInAccess.DisplayMember = "AccessText";
            frmCreateAccess.cboLogInAccess.ValueMember = "AccessValue";
            frmCreateAccess.cboLogInAccess.DataSource = DTCombo;
            
            frmCreateAccess.cboLogInAccess.SelectedValue = _LogInAccessDisplay;
           
        }

        public void CreateClick()
        {
            DataTable DT = new DataTable();
            string spString = "";
            //_IsEdit = frmCreateAccess._IsEdit;
            _AccessID = frmCreateAccess._AccessID;
            _IsEdit = frmCreateAccess._IsEdit;

            if (frmCreateAccess.txtAccessLevel.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type FullName");
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

                    if (_IsEdit)
                    {
                        dbaAccessSetting.AID = Convert.ToInt32(_AccessID);
                        dbaAccessSetting.ACTION = 1;
                        dbaAccessSetting.SaveData();

                        MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                        frmCreateAccess.Close();
                    }
                    else
                    {
                        dbaAccessSetting.ACTION = 0;
                        dbaAccessSetting.SaveData();
                        MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                        frmCreateAccess.Close();
                    }
                }
            }
        }
    }
}
