﻿using System;
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
    internal class DbaAccess
    {
        public int AID { get; set; }
        public string ALEVEL { get; set; }
        public string LIACCESS { get; set; }
        public int AUTHORITY { get; set; }
        public int ACTION { get; set; }

        private readonly DbaConnection _dbaConnection = new DbaConnection();

        public void SaveData()
        {
            try
            {
                _dbaConnection.DataBaseConn();
                SqlCommand sql = new SqlCommand("SP_Insert_Access", _dbaConnection.con);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@AccessID", AID);
                sql.Parameters.AddWithValue("@AccessLevel", ALEVEL);
                sql.Parameters.AddWithValue("@LogInAccess", LIACCESS);
                sql.Parameters.AddWithValue("@Authority", AUTHORITY);
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
