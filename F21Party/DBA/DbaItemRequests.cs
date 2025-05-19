using F21Party.DBA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F21Party.DBA
{
    class DbaItemRequests
    {
        public int RID { get; set; }
        public string RDATE { get; set; }
        public int USERID { get; set; }
        public int TOTALAMT { get; set; }
        public int ACTION { get; set; }


        DbaConnection dbaConnection = new DbaConnection();

        public void SaveData()
        {
            try
            {
                dbaConnection.DataBaseConn();
                SqlCommand sql = new SqlCommand("SP_Insert_ItemRequests", dbaConnection.con);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@RequestID", RID);
                sql.Parameters.AddWithValue("@RequestDate", RDATE);
                sql.Parameters.AddWithValue("@UserID", USERID);
                sql.Parameters.AddWithValue("@TotalAmount", TOTALAMT);
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
