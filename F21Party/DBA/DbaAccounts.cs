﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace F21Party.DBA
{
    class DbaAccounts
    {
        public int ACCOUNTID { get; set; }
        public int USERID { get; set; }
        public string UNAME { get; set; }
        public string PASS { get; set; }
        public int ACCESSID { get; set; }
        public int ACTION { get; set; }

        private readonly DbaConnection _dbaConnection = new DbaConnection();

        public void SaveData()
        {
            try
            {
                _dbaConnection.DataBaseConn();
                SqlCommand sql = new SqlCommand("SP_Insert_Accounts", _dbaConnection.con);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@AccountID", ACCOUNTID);
                sql.Parameters.AddWithValue("@UserID", USERID);
                sql.Parameters.AddWithValue("@UserName", UNAME);
                sql.Parameters.AddWithValue("@Password", PASS);
                sql.Parameters.AddWithValue("@AccessID", ACCESSID);
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
