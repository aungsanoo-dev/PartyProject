using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Xml.Linq;


namespace F21Party.DBA
{
    internal class DbaPosition
    {
        public int PID { get; set; }
        public string PNAME { get; set; }
        public int ACTION { get; set; }

        private readonly DbaConnection _dbaConnection = new DbaConnection();

        public void SaveData()
        {
            try
            {
                _dbaConnection.DataBaseConn();
                SqlCommand sql = new SqlCommand("SP_Insert_Position", _dbaConnection.con);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@PositionID", PID);
                sql.Parameters.AddWithValue("@PositionName", PNAME);
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
