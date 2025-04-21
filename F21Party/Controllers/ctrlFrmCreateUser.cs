using F21Party.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F21Party.DBA;
using F21Party.Views;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace F21Party.Controllers
{
    internal class ctrlFrmCreateUser
    {
        public frm_CreateUser frm_CreateUser;// Declare the View
        public ctrlFrmCreateUser(frm_CreateUser createUserForm)
        {
            frm_CreateUser = createUserForm; // Create the View
        }
        clsMainDB obj_clsMainDB = new clsMainDB();
        clsUserSetting obj_clsUserSetting = new clsUserSetting();
        public int positionlevelindex;
        public bool _IsEdit;
        public int _UserID;

        public void AddCombo(ComboBox cboCombo, string SPString, string Display, string Value)
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
                obj_clsMainDB.DataBaseConn();
                SqlDataAdapter Adpt = new SqlDataAdapter(SPString, obj_clsMainDB.con);
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
                obj_clsMainDB.con.Close();
            }
        }

        public void ShowCombo()
        {
            string _PositionDisplay = "";
            string SPString;
            _PositionDisplay = frm_CreateUser.cboPosition.DisplayMember;
            if (_PositionDisplay == string.Empty)
                _PositionDisplay = "0";

            // For Position Combobox
            SPString = string.Format("SP_Select_Position N'{0}',N'{1}',N'{2}'", "0", "0", "0");

            AddCombo(frm_CreateUser.cboPosition, SPString, "PositionName", "PositionID");

            frm_CreateUser.cboPosition.SelectedValue = Convert.ToInt32(_PositionDisplay); //This is in the box value you see
            positionlevelindex = frm_CreateUser.cboPosition.SelectedIndex;

        }

        public void SaveClick()
        {
            //frmMain obj_frmMain = new frmMain();
            DataTable DT = new DataTable();
            string SPString = "";
            _IsEdit = frm_CreateUser._IsEdit;
            _UserID = frm_CreateUser._UserID;

            if (frm_CreateUser.txtFullName.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type FullName");
                frm_CreateUser.txtFullName.Focus();
            }
            else if (frm_CreateUser.txtAddress.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Address");
                frm_CreateUser.txtAddress.Focus();
            }
            else if (frm_CreateUser.txtPhone.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Phone");
                frm_CreateUser.txtPhone.Focus();
            }
            else if (frm_CreateUser.cboPosition.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose Position");
                frm_CreateUser.cboPosition.Focus();
            }

                
            else
            {
                // For Users
                SPString = string.Format("SP_Select_Users N'{0}',N'{1}',N'{2}',N'{3}'", Regex.Replace(frm_CreateUser.txtFullName.Text.Trim(), @"\s+", " "),
                Regex.Replace(frm_CreateUser.txtAddress.Text.Trim(), @"\s+", " "), "0", "5");

                DT = obj_clsMainDB.SelectData(SPString);
                if (DT.Rows.Count > 0 && _UserID != Convert.ToInt32(DT.Rows[0]["UserID"]))
                {
                    MessageBox.Show("This User is Already Exist");
                    frm_CreateUser.txtFullName.Focus();
                    frm_CreateUser.txtFullName.SelectAll();
                }
                else
                {
                    obj_clsUserSetting.UID = Convert.ToInt32(_UserID);
                    obj_clsUserSetting.FNAME = Regex.Replace(frm_CreateUser.txtFullName.Text.Trim(), @"\s+", " ");
                    obj_clsUserSetting.ADDRESS = Regex.Replace(frm_CreateUser.txtAddress.Text.Trim(), @"\s+", " ");
                    obj_clsUserSetting.PHONE = Regex.Replace(frm_CreateUser.txtPhone.Text.Trim(), @"\s+", " ");
                    obj_clsUserSetting.PID = Convert.ToInt32(frm_CreateUser.cboPosition.SelectedValue);


                    if (_IsEdit)
                    {
                        obj_clsUserSetting.UID = Convert.ToInt32(_UserID);
                        obj_clsUserSetting.ACTION = 1;
                        obj_clsUserSetting.SaveData();

                        MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                        frm_CreateUser.Close();
                    }
                    else
                    {
                        obj_clsUserSetting.ACTION = 0;
                        obj_clsUserSetting.SaveData();
                        MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                        frm_CreateUser.Close();
                    }
                }
            }
            
        }
    }
}
