using F21Party.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F21Party.DBA;
//using F21Party.Views;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace F21Party.Controllers
{
    internal class CtrlFrmCreateUser
    {
        public frm_CreateUser frmCreateUser;// Declare the View
        public CtrlFrmCreateUser(frm_CreateUser createUserForm)
        {
            frmCreateUser = createUserForm; // Create the View
        }
        DbaConnection dbaConnection = new DbaConnection();
        DbaUsers dbaUserSetting = new DbaUsers();
        //private int positionLevelIndex;
        private bool _IsEdit;
        private int _UserID;

        public void AddCombo(ComboBox cboCombo, string spString, string Display, string Value)
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

        public void ShowCombo()
        {
            string _PositionDisplay = "";
            string spString;
            _PositionDisplay = frmCreateUser.cboPosition.DisplayMember;
            if (_PositionDisplay == string.Empty)
                _PositionDisplay = "0";

            // For Position Combobox
            spString = string.Format("SP_Select_Position N'{0}',N'{1}',N'{2}'", "0", "0", "0");

            AddCombo(frmCreateUser.cboPosition, spString, "PositionName", "PositionID");

            frmCreateUser.cboPosition.SelectedValue = Convert.ToInt32(_PositionDisplay); //This is in the box value you see
            //positionLevelIndex = frmCreateUser.cboPosition.SelectedIndex;

        }

        public void SaveClick()
        {
            //frmMain obj_frmMain = new frmMain();
            DataTable DT = new DataTable();
            string spString = "";
            _IsEdit = frmCreateUser._IsEdit;
            _UserID = frmCreateUser._UserID;

            if (frmCreateUser.txtFullName.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type FullName");
                frmCreateUser.txtFullName.Focus();
            }
            else if (frmCreateUser.txtAddress.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Address");
                frmCreateUser.txtAddress.Focus();
            }
            else if (frmCreateUser.txtPhone.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Phone");
                frmCreateUser.txtPhone.Focus();
            }
            else if (frmCreateUser.cboPosition.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose Position");
                frmCreateUser.cboPosition.Focus();
            }

                
            else
            {
                // For Users
                spString = string.Format("SP_Select_Users N'{0}',N'{1}',N'{2}',N'{3}'", Regex.Replace(frmCreateUser.txtFullName.Text.Trim(), @"\s+", " "),
                Regex.Replace(frmCreateUser.txtAddress.Text.Trim(), @"\s+", " "), "0", "5");

                DT = dbaConnection.SelectData(spString);
                if (DT.Rows.Count > 0 && _UserID != Convert.ToInt32(DT.Rows[0]["UserID"]))
                {
                    MessageBox.Show("This User is Already Exist");
                    frmCreateUser.txtFullName.Focus();
                    frmCreateUser.txtFullName.SelectAll();
                }
                else
                {
                    dbaUserSetting.UID = Convert.ToInt32(_UserID);
                    dbaUserSetting.FNAME = Regex.Replace(frmCreateUser.txtFullName.Text.Trim(), @"\s+", " ");
                    dbaUserSetting.ADDRESS = Regex.Replace(frmCreateUser.txtAddress.Text.Trim(), @"\s+", " ");
                    dbaUserSetting.PHONE = Regex.Replace(frmCreateUser.txtPhone.Text.Trim(), @"\s+", " ");
                    dbaUserSetting.PID = Convert.ToInt32(frmCreateUser.cboPosition.SelectedValue);


                    if (_IsEdit)
                    {
                        dbaUserSetting.UID = Convert.ToInt32(_UserID);
                        dbaUserSetting.ACTION = 1;
                        dbaUserSetting.SaveData();

                        MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                        frmCreateUser.Close();
                    }
                    else
                    {
                        dbaUserSetting.ACTION = 0;
                        dbaUserSetting.SaveData();
                        MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                        frmCreateUser.Close();
                    }
                }
            }
            
        }
    }
}
