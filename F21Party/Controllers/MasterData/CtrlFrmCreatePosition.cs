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
    internal class CtrlFrmCreatePosition
    {
        private readonly frm_CreatePosition _frmCreatePosition;// Declare the View
        private readonly DbaConnection _dbaConnection = new DbaConnection();
        private readonly DbaPosition _dbaPositionSetting = new DbaPosition();
        private bool _isEdit;
        private int _positionID;
        private string _spString;
        public CtrlFrmCreatePosition(frm_CreatePosition createPositionForm)
        {
            _frmCreatePosition = createPositionForm; // Create the View
        }
        public void CreateClick()
        {
            DataTable dt = new DataTable();
            //_IsEdit = frmCreatePosition._IsEdit;
            _positionID = _frmCreatePosition.PositionID;
            _isEdit = _frmCreatePosition.IsEdit;

            if (_frmCreatePosition.txtPositionName.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Position Name");
                _frmCreatePosition.txtPositionName.Focus();
            }
            else
            {
                // For Position
                _spString = string.Format("SP_Select_Position N'{0}',N'{1}',N'{2}'", Regex.Replace(_frmCreatePosition.txtPositionName.Text.Trim(), @"\s+", " "),
                "0", "2");

                dt = _dbaConnection.SelectData(_spString);
                if (dt.Rows.Count > 0 && _positionID != Convert.ToInt32(dt.Rows[0]["PositionID"]))
                {
                    MessageBox.Show("This PositionName is Already Exist");
                    _frmCreatePosition.txtPositionName.Focus();
                    _frmCreatePosition.txtPositionName.SelectAll();
                }
                else
                {
                    _dbaPositionSetting.PID = Convert.ToInt32(_positionID);
                    _dbaPositionSetting.PNAME = Regex.Replace(_frmCreatePosition.txtPositionName.Text.Trim(), @"\s+", " ");

                    if (_isEdit)
                    {
                        _dbaPositionSetting.PID = Convert.ToInt32(_positionID);
                        _dbaPositionSetting.ACTION = 1;
                        _dbaPositionSetting.SaveData();

                        MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                        _frmCreatePosition.Close();
                    }
                    else
                    {
                        _dbaPositionSetting.ACTION = 0;
                        _dbaPositionSetting.SaveData();
                        MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                        _frmCreatePosition.Close();
                    }
                }
            }
        }
    }
}
