using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using F21Party;


namespace F21Party.DBA
{
    class DbaConnection
    {
        public SqlConnection con;
        //DataSet DS = new DataSet();

        public void DataBaseConn()
        {
            try
            {
                SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
                {
                    DataSource = ".\\SQL2022Express",
                    InitialCatalog = "F21Party",
                    UserID = "sa",
                    Password = "sasa@123",
                    TrustServerCertificate = true
                };
                
                //con = new SqlConnection(F21Party.Properties.Settings.Default.F21PartyCon);
                con = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error In DataBaseConn");
            }
        }

        public DataTable SelectData(string spString)
        {
            DataTable dt = new DataTable();
            try
            {
                DataBaseConn();
                SqlDataAdapter adpt = new SqlDataAdapter(spString, con);
                adpt.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error In SelectData");
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public void ToolStripTextBoxData(ToolStripTextBox tstToolStrip, string spString, string fieldName)
        {
            DataTable dt = new DataTable();
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            try
            {
                DataBaseConn();
                SqlDataAdapter adpt = new SqlDataAdapter(spString, con);
                adpt.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    tstToolStrip.AutoCompleteCustomSource.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        source.Add(dt.Rows[i][fieldName].ToString());
                    }
                    tstToolStrip.AutoCompleteCustomSource = source;
                    tstToolStrip.Text = "";
                    tstToolStrip.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error In ToolStripTextBoxData");
            }
            finally
            {
                con.Close();
            }

        }

    }
}
