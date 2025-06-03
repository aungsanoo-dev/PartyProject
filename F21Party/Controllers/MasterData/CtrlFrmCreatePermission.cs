using F21Party.DBA;
using F21Party.Views;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace F21Party.Controllers
{
    internal class CtrlFrmCreatePermission
    {
        private frm_CreatePermission _frmCreatePermission;// Declare the View
        private readonly DbaConnection _dbaConnection = new DbaConnection();
        private readonly DbaPermission _dbaPermissionSetting = new DbaPermission();
        private bool _isEdit;
        private int _permissionID;
        private string _spString;
        public CtrlFrmCreatePermission(frm_CreatePermission createPermissionForm)
        {
            _frmCreatePermission = createPermissionForm; // Create the View
        }

        public void AddCombo(ComboBox cboCombo, string spString, string display, string value, bool isEdit)
        {
            DataTable dtAccessSp = new DataTable();
            DataTable dtCombo = new DataTable();
            DataRow dr;


            dtCombo.Columns.Add(display);
            dtCombo.Columns.Add(value);

            dr = dtCombo.NewRow();
            dr[display] = "---Select---";
            dr[value] = 0;
            dtCombo.Rows.Add(dr);

            // For Your Authority
            DataTable dtAccess = new DataTable();
            _spString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", Program.UserAccessID, "0", "1");
            dtAccess = _dbaConnection.SelectData(_spString);

            //if (Program.UserID != 0)
            //{
            //    Program.UserAuthority = Convert.ToInt32(dtAccess.Rows[0]["Authority"]);
            //}

            try
            {
                _dbaConnection.DataBaseConn();
                SqlDataAdapter Adpt = new SqlDataAdapter(spString, _dbaConnection.con);
                Adpt.Fill(dtAccessSp);
                for (int i = 0; i < dtAccessSp.Rows.Count; i++)
                {
                    dr = dtCombo.NewRow();

                    if (_frmCreatePermission.cboAccessLevel.DisplayMember == "AccessLevel" && display == "AccessLevel")
                    {
                        goto Bottom;
                    }

                    // For Your Selected Authority
                    DataTable dtAccess2 = new DataTable();
                    if (display == "AccessLevel" && _frmCreatePermission.cboAccessLevel.DisplayMember != "")
                    {
                        _spString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", Convert.ToInt32(_frmCreatePermission.cboAccessLevel.DisplayMember), "0", "1");

                        dtAccess2 = _dbaConnection.SelectData(_spString);
                    }

                    if (isEdit)
                    {
                        if (Program.UserAuthority != 1 && display == "AccessLevel")
                        {
                            if (Convert.ToInt32(dtAccessSp.Rows[i][value]) == 1) // 1 is SuperAdmin.
                            {
                                continue;
                            }
                        }

                        if (display == "AccessLevel" && Convert.ToInt32(dtAccess2.Rows[0]["Authority"]) >= Program.UserAuthority)
                        {
                            if (Convert.ToInt32(dtAccessSp.Rows[i]["Authority"]) < Program.UserAuthority) // AccessID comparison
                            {
                                continue; // Remove Specific Accesslevel from Combo box.
                            }
                        }
                    }
                    else
                    {
                        if (Program.UserAuthority != 0 && display == "AccessLevel")
                        {
                            if (Convert.ToInt32(dtAccessSp.Rows[i][value]) < Program.UserAuthority) // AccessID comparison
                            {
                                continue; // Remove Specific Accesslevel from Combo box.
                            }
                        }

                        if (Program.UserAuthority == 0 && display == "AccessLevel")
                        {
                            MessageBox.Show(Program.UserAuthority.ToString());
                            if (Convert.ToInt32(dtAccessSp.Rows[i][value]) == 1 || Convert.ToInt32(dtAccessSp.Rows[i][value]) == 2) // 1 is SuperAdmin. 2 is Admin.
                            {
                                continue;
                            }
                        }
                    }

                Bottom:

                    dr[display] = dtAccessSp.Rows[i][display];
                    dr[value] = dtAccessSp.Rows[i][value];
                    dtCombo.Rows.Add(dr);
                }
                cboCombo.DisplayMember = display;
                cboCombo.ValueMember = value;
                cboCombo.DataSource = dtCombo;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error In AddCombo");
            }

            finally
            {
                _dbaConnection.con.Close();
            }
        }
        public void ShowAccessValue()
        {
            DataTable dtCombo = new DataTable();
            DataRow dr;

            string logInAccessDisplay = _frmCreatePermission.cboAccessValue.DisplayMember;
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
            _frmCreatePermission.cboAccessValue.DisplayMember = "AccessText";
            _frmCreatePermission.cboAccessValue.ValueMember = "AccessValue";
            _frmCreatePermission.cboAccessValue.DataSource = dtCombo;

            _frmCreatePermission.cboAccessValue.SelectedValue = logInAccessDisplay;

        }
        public void ShowCombo(bool _isEdit)
        {
            //string spString;

            // For AccessLevel Combobox
            string accessLevelDisplay = _frmCreatePermission.cboAccessLevel.DisplayMember;
            if (accessLevelDisplay == string.Empty)
                accessLevelDisplay = "0";

            _spString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", "0", "0", "0");

            AddCombo(_frmCreatePermission.cboAccessLevel, _spString, "AccessLevel", "AccessID", _isEdit);

            _frmCreatePermission.cboAccessLevel.SelectedValue = Convert.ToInt32(accessLevelDisplay); //This is in the box value you see

            // For PageName Combobox
            string pageNameDisplay = _frmCreatePermission.cboPageName.DisplayMember;
            if (pageNameDisplay == string.Empty)
                pageNameDisplay = "0";

            _spString = string.Format("SP_Select_Page N'{0}',N'{1}',N'{2}'", "0", "0", "0");

            AddCombo(_frmCreatePermission.cboPageName, _spString, "PageName", "PageID", _isEdit);

            _frmCreatePermission.cboPageName.SelectedValue = Convert.ToInt32(pageNameDisplay); //This is in the box value you see

            // For PermissionType Combobox
            string permissionTypeDisplay = _frmCreatePermission.cboPermissionType.DisplayMember;
            if (permissionTypeDisplay == string.Empty)
                permissionTypeDisplay = "0";

            _spString = string.Format("SP_Select_PermissionType N'{0}',N'{1}',N'{2}'", "0", "0", "0");

            AddCombo(_frmCreatePermission.cboPermissionType, _spString, "PermissionName", "PermissionTypeID", _isEdit);

            _frmCreatePermission.cboPermissionType.SelectedValue = Convert.ToInt32(permissionTypeDisplay); //This is in the box value you see

            ShowAccessValue();
        }

        public void SaveClick()
        {
            DataTable dt = new DataTable();
            _isEdit = _frmCreatePermission.IsEdit;
            _permissionID = _frmCreatePermission.PermissionID;

            if (_frmCreatePermission.cboAccessLevel.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose AccessLevel.");
                _frmCreatePermission.cboAccessLevel.Focus();
            }
            else if (_frmCreatePermission.cboPageName.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose Page Name.");
                _frmCreatePermission.cboPageName.Focus();
            }
            else if (_frmCreatePermission.cboPermissionType.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose Permission Type.");
                _frmCreatePermission.cboPermissionType.Focus();
            }
            else if (_frmCreatePermission.cboAccessValue.SelectedValue.ToString() == "")
            {
                MessageBox.Show("Please Choose Access Value.");
                _frmCreatePermission.cboAccessLevel.Focus();
            }
            else
            {
                _spString = string.Format("SP_Select_Permission N'{0}',N'{1}',N'{2}',N'{3}'"
                ,_frmCreatePermission.cboAccessLevel.SelectedValue.ToString()
                ,_frmCreatePermission.cboPageName.SelectedValue.ToString()
                ,_frmCreatePermission.cboPermissionType.SelectedValue.ToString(), "1");

                dt = _dbaConnection.SelectData(_spString);
                if (dt.Rows.Count > 0 && _permissionID != Convert.ToInt32(dt.Rows[0]["PermissionID"]))
                {
                    MessageBox.Show("This Permission is Already Exist");
                    _frmCreatePermission.cboAccessLevel.Focus();
                    _frmCreatePermission.cboPageName.Focus();
                    _frmCreatePermission.cboPermissionType.Focus();

                }
                else
                {
                    if(_isEdit)
                    {
                        if (Convert.ToInt32(_frmCreatePermission.cboAccessLevel.SelectedValue) == Program.UserAccessID && _frmCreatePermission.cboAccessValue.SelectedValue.ToString() != _frmCreatePermission.AccessValue)
                        {
                            if (MessageBox.Show("You have to Logout your account if you want to change AccessValue for your Access!", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                _dbaPermissionSetting.PERMISSIONID = Convert.ToInt32(_permissionID);
                                _dbaPermissionSetting.ACCESSID = Convert.ToInt32(_frmCreatePermission.cboAccessLevel.SelectedValue.ToString());
                                _dbaPermissionSetting.PAGEID = Convert.ToInt32(_frmCreatePermission.cboPageName.SelectedValue.ToString());
                                _dbaPermissionSetting.PERMISSIONTYPEID = Convert.ToInt32(_frmCreatePermission.cboPermissionType.SelectedValue.ToString());
                                _dbaPermissionSetting.ACCESSVALUE = _frmCreatePermission.cboAccessValue.SelectedValue.ToString();

                                _dbaPermissionSetting.PERMISSIONID = Convert.ToInt32(_permissionID);
                                _dbaPermissionSetting.ACTION = 1;
                                _dbaPermissionSetting.SaveData();
                                //MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                                _frmCreatePermission.DialogResult = DialogResult.OK;
                                return;
                            }
                            else
                            {
                                _frmCreatePermission.DialogResult = DialogResult.None;
                                return;
                            }
                        }
                    }
                    
                    _dbaPermissionSetting.PERMISSIONID = Convert.ToInt32(_permissionID);
                    _dbaPermissionSetting.ACCESSID = Convert.ToInt32(_frmCreatePermission.cboAccessLevel.SelectedValue.ToString());
                    _dbaPermissionSetting.PAGEID = Convert.ToInt32(_frmCreatePermission.cboPageName.SelectedValue.ToString());
                    _dbaPermissionSetting.PERMISSIONTYPEID = Convert.ToInt32(_frmCreatePermission.cboPermissionType.SelectedValue.ToString());
                    _dbaPermissionSetting.ACCESSVALUE = _frmCreatePermission.cboAccessValue.SelectedValue.ToString();

                    if (_isEdit)
                    {
                        _dbaPermissionSetting.PERMISSIONID = Convert.ToInt32(_permissionID);
                        _dbaPermissionSetting.ACTION = 1;
                        _dbaPermissionSetting.SaveData();
                        MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                        _frmCreatePermission.Close();
                    }
                    else
                    {
                        _dbaPermissionSetting.ACTION = 0;
                        _dbaPermissionSetting.SaveData();
                        MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                        _frmCreatePermission.Close();
                    }
                }
            }
        }
    }
}
