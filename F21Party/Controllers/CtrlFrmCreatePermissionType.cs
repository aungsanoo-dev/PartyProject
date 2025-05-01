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
    internal class CtrlFrmCreatePermissionType
    {
        public frm_CreatePermissionType frmCreatePermissionType;// Declare the View
        public CtrlFrmCreatePermissionType(frm_CreatePermissionType createPermissionTypeForm)
        {
            frmCreatePermissionType = createPermissionTypeForm; // Create the View
        }

        DbaConnection dbaConnection = new DbaConnection();
        DbaPermissionTypeSetting dbaPermissionTypeSetting = new DbaPermissionTypeSetting();
        public bool _IsEdit;
        private int _PermissionTypeID;

        public void CreateClick()
        {
            DataTable DT = new DataTable();
            string spString = "";
            //_IsEdit = frmCreatePermissionType._IsEdit;
            _PermissionTypeID = frmCreatePermissionType._PermissionTypeID;
            _IsEdit = frmCreatePermissionType._IsEdit;

            if (frmCreatePermissionType.txtPermissionName.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type PermissionType Name");
                frmCreatePermissionType.txtPermissionName.Focus();
            }
            else
            {
                // For PermissionType
                spString = string.Format("SP_Select_PermissionType N'{0}',N'{1}',N'{2}'", Regex.Replace(frmCreatePermissionType.txtPermissionName.Text.Trim(), @"\s+", " "),
                "0", "2");

                DT = dbaConnection.SelectData(spString);
                if (DT.Rows.Count > 0 && _PermissionTypeID != Convert.ToInt32(DT.Rows[0]["PermissionTypeID"]))
                {
                    MessageBox.Show("This Permission Name is Already Exist");
                    frmCreatePermissionType.txtPermissionName.Focus();
                    frmCreatePermissionType.txtPermissionName.SelectAll();
                }
                else
                {
                    dbaPermissionTypeSetting.PID = Convert.ToInt32(_PermissionTypeID);
                    dbaPermissionTypeSetting.PNAME = Regex.Replace(frmCreatePermissionType.txtPermissionName.Text.Trim(), @"\s+", " ");

                    if (_IsEdit)
                    {
                        dbaPermissionTypeSetting.PID = Convert.ToInt32(_PermissionTypeID);
                        dbaPermissionTypeSetting.ACTION = 1;
                        dbaPermissionTypeSetting.SaveData();

                        MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                        frmCreatePermissionType.Close();
                    }
                    else
                    {
                        dbaPermissionTypeSetting.ACTION = 0;
                        dbaPermissionTypeSetting.SaveData();
                        MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                        frmCreatePermissionType.Close();
                    }
                }
            }
        }
    }
}
