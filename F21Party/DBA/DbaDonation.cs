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
    class DbaDonation
    {
        public int DID { get; set; }
        public string DDATE { get; set; }
        public int USERID { get; set; }
        public int TOTALAMT { get; set; }
        public int ACTION { get; set; }


        DbaConnection dbaConnection = new DbaConnection();

        public void SaveData()
        {
            try
            {
                dbaConnection.DataBaseConn();
                SqlCommand sql = new SqlCommand("SP_Insert_Donation", dbaConnection.con);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@DonationID", DID);
                sql.Parameters.AddWithValue("@DonationDate", DDATE);
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
