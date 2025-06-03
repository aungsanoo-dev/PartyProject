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
        private readonly frm_CreatePermissionType _frmCreatePermissionType;// Declare the View
        private readonly DbaConnection _dbaConnection = new DbaConnection();
        private readonly DbaPermissionType _dbaPermissionTypeSetting = new DbaPermissionType();
        private bool _isEdit;
        private int _permissionTypeID;
        private string _spString;
        public CtrlFrmCreatePermissionType(frm_CreatePermissionType createPermissionTypeForm)
        {
            _frmCreatePermissionType = createPermissionTypeForm; // Create the View
        }

        public void CreateClick()
        {
            DataTable dt = new DataTable();
            //_IsEdit = frmCreatePermissionType._IsEdit;
            _permissionTypeID = _frmCreatePermissionType.PermissionTypeID;
            _isEdit = _frmCreatePermissionType.IsEdit;

            if (_frmCreatePermissionType.txtPermissionName.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type PermissionType Name");
                _frmCreatePermissionType.txtPermissionName.Focus();
            }
            else
            {
                // For PermissionType
                _spString = string.Format("SP_Select_PermissionType N'{0}',N'{1}',N'{2}'", Regex.Replace(_frmCreatePermissionType.txtPermissionName.Text.Trim(), @"\s+", " "),
                "0", "2");

                dt = _dbaConnection.SelectData(_spString);
                if (dt.Rows.Count > 0 && _permissionTypeID != Convert.ToInt32(dt.Rows[0]["PermissionTypeID"]))
                {
                    MessageBox.Show("This Permission Name is Already Exist");
                    _frmCreatePermissionType.txtPermissionName.Focus();
                    _frmCreatePermissionType.txtPermissionName.SelectAll();
                }
                else
                {
                    _dbaPermissionTypeSetting.PID = Convert.ToInt32(_permissionTypeID);
                    _dbaPermissionTypeSetting.PNAME = Regex.Replace(_frmCreatePermissionType.txtPermissionName.Text.Trim(), @"\s+", " ");

                    if (_isEdit)
                    {
                        _dbaPermissionTypeSetting.PID = Convert.ToInt32(_permissionTypeID);
                        _dbaPermissionTypeSetting.ACTION = 1;
                        _dbaPermissionTypeSetting.SaveData();

                        MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                        _frmCreatePermissionType.Close();
                    }
                    else
                    {
                        _dbaPermissionTypeSetting.ACTION = 0;
                        _dbaPermissionTypeSetting.SaveData();
                        MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                        _frmCreatePermissionType.Close();
                    }
                }
            }
        }
    }
}
