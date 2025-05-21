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
    class DbaTeam
    {
        public int TID { get; set; }
        public string TNAME { get; set; }
        public string PHONE { get; set; }
        public int MAXPLAYER { get; set; }
        public int TOTALPLAYER { get; set; }
        public int ACTION { get; set; }

        DbaConnection dbaConnection = new DbaConnection();

        public void SaveData()
        {
            try
            {
                dbaConnection.DataBaseConn();
                SqlCommand sql = new SqlCommand("SP_Insert_Team", dbaConnection.con);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@TeamID", TID);
                sql.Parameters.AddWithValue("@TeamName", TNAME);
                sql.Parameters.AddWithValue("@Phone", PHONE);
                sql.Parameters.AddWithValue("@MaxPlayer", MAXPLAYER);
                sql.Parameters.AddWithValue("@TotalPlayer", TOTALPLAYER);
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
