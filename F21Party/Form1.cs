using F21Party.DBA;
using F21Party.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F21Party
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataTable DT = new DataTable();
        DbaConnection dbaConnection = new DbaConnection();
        List<String> pagename = new List<String>();


        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                string SPString = string.Format("SP_Select_Page N'{0}',N'{1}',N'{2}'", "", "", 1);
                DT = dbaConnection.SelectData(SPString);
                if (DT.Rows.Count > 0)
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                    pagename.Add(DT.Rows[i]["PageName"].ToString());
                    }

                    string UserLevel = String.Join(",", pagename);

                    MessageBox.Show(UserLevel);
                }
                else
                {
                    MessageBox.Show("Invalid UserName And Password");

                }
            
            
        }
    }
}
