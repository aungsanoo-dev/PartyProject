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
    class DbaDonationDetail
    {
        public int DDID { get; set; }
        public int DID { get; set; }
        public int ITEMID { get; set; }
        public int DQTY { get; set; }
        public int PRICE { get; set; }
        public int ACTION { get; set; }

        private readonly DbaConnection _dbaConnection = new DbaConnection();
        public void SaveData()
        {
            try
            {
                _dbaConnection.DataBaseConn();
                SqlCommand sql = new SqlCommand("SP_Insert_DonationDetail", _dbaConnection.con);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@DonationDetailID", DDID);
                sql.Parameters.AddWithValue("@DonationID", DID);
                sql.Parameters.AddWithValue("@ItemID", ITEMID);
                sql.Parameters.AddWithValue("@DonationQty", DQTY);
                sql.Parameters.AddWithValue("@Price", PRICE);
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
