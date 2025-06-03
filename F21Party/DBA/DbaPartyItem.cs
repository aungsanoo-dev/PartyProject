using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Xml.Linq;


namespace F21Party.DBA
{
    class DbaPartyItem
    {
        public int ITEMID { get; set; }
        public string ITEMNAME { get; set; }
        public int QTY { get; set; }
        public int PRICE { get; set; }
        public int ACTION { get; set; }

        private readonly DbaConnection _dbaConnection = new DbaConnection();

        public void SaveData()
        {
            try
            {
                _dbaConnection.DataBaseConn();
                SqlCommand sql = new SqlCommand("SP_Insert_PartyItem", _dbaConnection.con);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@ItemID", ITEMID);
                sql.Parameters.AddWithValue("@ItemName", ITEMNAME);
                sql.Parameters.AddWithValue("@Qty", QTY);
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
