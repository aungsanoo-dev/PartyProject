using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace F21Party.DBA
{
    class DbaTeamManagment
    {
        public int TMID { get; set; }
        public int TID { get; set; }
        public int UID { get; set; }
        public int ACTION { get; set; }

        private readonly DbaConnection _dbaConnection = new DbaConnection();

        public void SaveData()
        {
            try
            {
                _dbaConnection.DataBaseConn();
                SqlCommand sql = new SqlCommand("SP_insert_TeamManagment", _dbaConnection.con);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@TeamManagmentID", TMID);
                sql.Parameters.AddWithValue("@TeamID", TID);
                sql.Parameters.AddWithValue("@UserID", UID);
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
