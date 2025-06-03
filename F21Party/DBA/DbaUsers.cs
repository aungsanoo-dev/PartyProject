using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace F21Party.DBA
{
    internal class DbaUsers
    {
        public int UID { get; set; }
        public string FNAME { get; set; }
        public string ADDRESS { get; set; }
        public string PHONE { get; set; }
        public int PID { get; set; }
        public string HASACC { get; set; }
        public int ACTION { get; set; }

        private readonly DbaConnection _dbaConnection = new DbaConnection();

        public void SaveData()
        {
            try
            {
                _dbaConnection.DataBaseConn();
                SqlCommand sql = new SqlCommand("SP_Insert_Users", _dbaConnection.con);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@UserID", UID);
                sql.Parameters.AddWithValue("@FullName", FNAME);
                sql.Parameters.AddWithValue("@Address", ADDRESS);
                sql.Parameters.AddWithValue("@Phone", PHONE);
                sql.Parameters.AddWithValue("@PositionID", PID);
                sql.Parameters.AddWithValue("@HasAccount", HASACC);
                sql.Parameters.AddWithValue("@action", ACTION);
                sql.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error In Save Data");
            }
            finally
            {
                _dbaConnection.con.Close();
            }
        }
    }
}
