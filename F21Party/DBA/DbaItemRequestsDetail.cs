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
    class DbaItemRequestsDetail
    {
        public int RDID { get; set; }
        public int RID { get; set; }
        public int ITEMID { get; set; }
        public int RQTY { get; set; }
        public int PRICE { get; set; }
        public int ACTION { get; set; }

        private readonly DbaConnection _dbaConnection = new DbaConnection();
        public void SaveData()
        {
            try
            {
                _dbaConnection.DataBaseConn();
                SqlCommand sql = new SqlCommand("SP_Insert_ItemRequestsDetail", _dbaConnection.con);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@RequestDetailID", RDID);
                sql.Parameters.AddWithValue("@RequestID", RID);
                sql.Parameters.AddWithValue("@ItemID", ITEMID);
                sql.Parameters.AddWithValue("@RequestQty", RQTY);
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
