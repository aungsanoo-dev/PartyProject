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
        public frm_CreatePermission frmCreatePermission;// Declare the View
        public CtrlFrmCreatePermission(frm_CreatePermission createPermissionForm)
        {
            frmCreatePermission = createPermissionForm; // Create the View
        }

        DbaConnection dbaConnection = new DbaConnection();
        DbaPermissionSetting dbaPermissionSetting = new DbaPermissionSetting();
        private bool _IsEdit;
        private int _PermissionID;

        public void AddCombo(ComboBox cboCombo, string spString, string Display, string Value, bool _IsEdit)
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

            // For Your Authority
            string spAccess = "";
            DataTable dtAccess = new DataTable();
            spAccess = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", Program.UserAccessID, "0", "1");
            dtAccess = dbaConnection.SelectData(spAccess);

            //if (Program.UserID != 0)
            //{
            //    Program.UserAuthority = Convert.ToInt32(dtAccess.Rows[0]["Authority"]);
            //}

            try
            {
                dbaConnection.DataBaseConn();
                SqlDataAdapter Adpt = new SqlDataAdapter(spString, dbaConnection.con);
                Adpt.Fill(DTAC);
                for (int i = 0; i < DTAC.Rows.Count; i++)
                {
                    Dr = DTCombo.NewRow();

                    if (frmCreatePermission.cboAccessLevel.DisplayMember == "AccessLevel" && Display == "AccessLevel")
                    {
                        goto Bottom;
                    }

                    // For Your Selected Authority
                    string spAccess2 = "";
                    DataTable dtAccess2 = new DataTable();
                    if (Display == "AccessLevel" && frmCreatePermission.cboAccessLevel.DisplayMember != "")
                    {
                        spAccess2 = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", Convert.ToInt32(frmCreatePermission.cboAccessLevel.DisplayMember), "0", "1");

                        dtAccess2 = dbaConnection.SelectData(spAccess2);
                    }

                    //if (Program.UserAuthority == 1 && Display == "AccessLevel")
                    //{
                    //    // SuperAdmin Access is only visible when it is edited by SuperAdmin to itself
                    //    //MessageBox.Show(frmCreatePermission.cboAccessLevel.DisplayMember.ToString());
                    //    if (_IsEdit && Convert.ToInt32(dtAccess2.Rows[0]["Authority"]) == Program.UserAuthority)
                    //    {

                    //    }
                    //    else
                    //    {
                    //        if (Convert.ToInt32(DTAC.Rows[i][Value]) == 1) // 1 is SuperAdmin.
                    //        {
                    //            continue;
                    //        }
                    //    }
                    //}
                    if (_IsEdit)
                    {
                        if (Program.UserAuthority != 1 && Display == "AccessLevel")
                        {
                            if (Convert.ToInt32(DTAC.Rows[i][Value]) == 1) // 1 is SuperAdmin.
                            {
                                continue;
                            }
                        }

                        if (Display == "AccessLevel" && Convert.ToInt32(dtAccess2.Rows[0]["Authority"]) >= Program.UserAuthority)
                        {
                            //MessageBox.Show(Convert.ToInt32(frmCreateAccount.cboAccessLevel.DisplayMember).ToString());

                            if (Convert.ToInt32(DTAC.Rows[i]["Authority"]) < Program.UserAuthority) // AccessID comparison
                            {
                                continue; // Remove Specific Accesslevel from Combo box.
                            }
                        }

                    }
                    else
                    {
                        if (Program.UserAuthority != 0 && Display == "AccessLevel")
                        {
                            if (Convert.ToInt32(DTAC.Rows[i][Value]) < Program.UserAuthority) // AccessID comparison
                            {
                                continue; // Remove Specific Accesslevel from Combo box.
                            }
                        }

                        if (Program.UserAuthority == 0 && Display == "AccessLevel")
                        {
                            if (Convert.ToInt32(DTAC.Rows[i][Value]) == 1 || Convert.ToInt32(DTAC.Rows[i][Value]) == 2) // 1 is SuperAdmin. 2 is Admin.
                            {
                                continue;
                            }
                        }
                    }

                Bottom:

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
        public void ShowAccessValue()
        {
            DataTable DTCombo = new DataTable();
            DataRow Dr;

            string _LogInAccessDisplay = "";
            _LogInAccessDisplay = frmCreatePermission.cboAccessValue.DisplayMember;
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
            frmCreatePermission.cboAccessValue.DisplayMember = "AccessText";
            frmCreatePermission.cboAccessValue.ValueMember = "AccessValue";
            frmCreatePermission.cboAccessValue.DataSource = DTCombo;

            frmCreatePermission.cboAccessValue.SelectedValue = _LogInAccessDisplay;

        }
        public void ShowCombo(bool _IsEdit)
        {
            string spString;

            // For AccessLevel Combobox
            string _AccessLevelDisplay = "";
            _AccessLevelDisplay = frmCreatePermission.cboAccessLevel.DisplayMember;
            if (_AccessLevelDisplay == string.Empty)
                _AccessLevelDisplay = "0";

            spString = string.Format("SP_Select_Access N'{0}',N'{1}',N'{2}'", "0", "0", "0");

            AddCombo(frmCreatePermission.cboAccessLevel, spString, "AccessLevel", "AccessID", _IsEdit);

            frmCreatePermission.cboAccessLevel.SelectedValue = Convert.ToInt32(_AccessLevelDisplay); //This is in the box value you see

            // For PageName Combobox
            string _PageNameDisplay = "";
            _PageNameDisplay = frmCreatePermission.cboPageName.DisplayMember;
            if (_PageNameDisplay == string.Empty)
                _PageNameDisplay = "0";

            spString = string.Format("SP_Select_Page N'{0}',N'{1}',N'{2}'", "0", "0", "0");

            AddCombo(frmCreatePermission.cboPageName, spString, "PageName", "PageID", _IsEdit);

            frmCreatePermission.cboPageName.SelectedValue = Convert.ToInt32(_PageNameDisplay); //This is in the box value you see

            // For PermissionType Combobox
            string _PermissionTypeDisplay = "";
            _PermissionTypeDisplay = frmCreatePermission.cboPermissionType.DisplayMember;
            if (_PermissionTypeDisplay == string.Empty)
                _PermissionTypeDisplay = "0";

            spString = string.Format("SP_Select_PermissionType N'{0}',N'{1}',N'{2}'", "0", "0", "0");

            AddCombo(frmCreatePermission.cboPermissionType, spString, "PermissionName", "PermissionTypeID", _IsEdit);

            frmCreatePermission.cboPermissionType.SelectedValue = Convert.ToInt32(_PermissionTypeDisplay); //This is in the box value you see

            ShowAccessValue();
        }

        public void SaveClick()
        {
            DataTable DT = new DataTable();
            string spString = "";
            _IsEdit = frmCreatePermission.IsEdit;
            _PermissionID = frmCreatePermission.PermissionID;

            if (frmCreatePermission.cboAccessLevel.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose AccessLevel.");
                frmCreatePermission.cboAccessLevel.Focus();
            }
            else if (frmCreatePermission.cboPageName.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose Page Name.");
                frmCreatePermission.cboPageName.Focus();
            }
            else if (frmCreatePermission.cboPermissionType.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please Choose Permission Type.");
                frmCreatePermission.cboPermissionType.Focus();
            }
            else if (frmCreatePermission.cboAccessValue.SelectedValue.ToString() == "")
            {
                MessageBox.Show("Please Choose Access Value.");
                frmCreatePermission.cboAccessLevel.Focus();
            }
            else
            {
                spString = string.Format("SP_Select_Permission N'{0}',N'{1}',N'{2}',N'{3}'"
                ,frmCreatePermission.cboAccessLevel.SelectedValue.ToString()
                ,frmCreatePermission.cboPageName.SelectedValue.ToString()
                ,frmCreatePermission.cboPermissionType.SelectedValue.ToString(), "1");

                DT = dbaConnection.SelectData(spString);
                if (DT.Rows.Count > 0 && _PermissionID != Convert.ToInt32(DT.Rows[0]["PermissionID"]))
                {
                    MessageBox.Show("This Permission is Already Exist");
                    frmCreatePermission.cboAccessLevel.Focus();
                    frmCreatePermission.cboPageName.Focus();
                    frmCreatePermission.cboPermissionType.Focus();

                }
                else
                {
                    dbaPermissionSetting.PERMISSIONID = Convert.ToInt32(_PermissionID);
                    dbaPermissionSetting.ACCESSID = Convert.ToInt32(frmCreatePermission.cboAccessLevel.SelectedValue.ToString());
                    dbaPermissionSetting.PAGEID = Convert.ToInt32(frmCreatePermission.cboPageName.SelectedValue.ToString());
                    dbaPermissionSetting.PERMISSIONTYPEID = Convert.ToInt32(frmCreatePermission.cboPermissionType.SelectedValue.ToString());
                    dbaPermissionSetting.ACCESSVALUE = frmCreatePermission.cboAccessValue.SelectedValue.ToString();

                    if (_IsEdit)
                    {
                        dbaPermissionSetting.PERMISSIONID = Convert.ToInt32(_PermissionID);
                        dbaPermissionSetting.ACTION = 1;
                        dbaPermissionSetting.SaveData();
                        MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                        frmCreatePermission.Close();
                    }
                    else
                    {
                        dbaPermissionSetting.ACTION = 0;
                        dbaPermissionSetting.SaveData();
                        MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                        frmCreatePermission.Close();
                    }
                }
            }
        }
    }
}
