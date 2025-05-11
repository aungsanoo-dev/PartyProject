using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace F21Party.DBA
{
    internal class DbaPermissionSetting
    {
        public int PERMISSIONID { get; set; }
        public int ACCESSID { get; set; }
        public int PAGEID { get; set; }
        public int PERMISSIONTYPEID { get; set; }
        public string ACCESSVALUE { get; set; }
        public int ACTION { get; set; }

        DbaConnection dbaConnection = new DbaConnection();

        public void SaveData()
        {
            try
            {
                dbaConnection.DataBaseConn();
                SqlCommand sql = new SqlCommand("SP_Insert_Permission", dbaConnection.con);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@PermissionID", PERMISSIONID);
                sql.Parameters.AddWithValue("@AccessID", ACCESSID);
                sql.Parameters.AddWithValue("@PageID", PAGEID);
                sql.Parameters.AddWithValue("@PermissionTypeID", PERMISSIONTYPEID);
                sql.Parameters.AddWithValue("@AccessValue", ACCESSVALUE);
                sql.Parameters.AddWithValue("@action", ACTION);
                sql.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error In Save Data");
            }
            finally
            {
                dbaConnection.con.Close();
            }
        }
    }
}
