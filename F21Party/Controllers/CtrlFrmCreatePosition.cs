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
        public frm_CreatePosition frmCreatePosition;// Declare the View
        public CtrlFrmCreatePosition(frm_CreatePosition createPositionForm)
        {
            frmCreatePosition = createPositionForm; // Create the View
        }

        DbaConnection dbaConnection = new DbaConnection();
        DbaPositionSetting dbaPositionSetting = new DbaPositionSetting();
        public bool _IsEdit;
        private int _PositionID;

        public void CreateClick()
        {
            DataTable DT = new DataTable();
            string spString = "";
            //_IsEdit = frmCreatePosition._IsEdit;
            _PositionID = frmCreatePosition._PositionID;
            _IsEdit = frmCreatePosition._IsEdit;

            if (frmCreatePosition.txtPositionName.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Position Name");
                frmCreatePosition.txtPositionName.Focus();
            }
            else
            {
                // For Position
                spString = string.Format("SP_Select_Position N'{0}',N'{1}',N'{2}'", Regex.Replace(frmCreatePosition.txtPositionName.Text.Trim(), @"\s+", " "),
                "0", "2");

                DT = dbaConnection.SelectData(spString);
                if (DT.Rows.Count > 0 && _PositionID != Convert.ToInt32(DT.Rows[0]["PositionID"]))
                {
                    MessageBox.Show("This PositionName is Already Exist");
                    frmCreatePosition.txtPositionName.Focus();
                    frmCreatePosition.txtPositionName.SelectAll();
                }
                else
                {
                    dbaPositionSetting.PID = Convert.ToInt32(_PositionID);
                    dbaPositionSetting.PNAME = Regex.Replace(frmCreatePosition.txtPositionName.Text.Trim(), @"\s+", " ");

                    if (_IsEdit)
                    {
                        dbaPositionSetting.PID = Convert.ToInt32(_PositionID);
                        dbaPositionSetting.ACTION = 1;
                        dbaPositionSetting.SaveData();

                        MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                        frmCreatePosition.Close();
                    }
                    else
                    {
                        dbaPositionSetting.ACTION = 0;
                        dbaPositionSetting.SaveData();
                        MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                        frmCreatePosition.Close();
                    }
                }
            }
        }
    }
}
