using F21Party.DBA;
using F21Party.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F21Party.Controllers
{
    internal class AccessGridToggle
    {
        private frm_AccountList frmAccountList;
        private DataGridView originalGrid;
        private DataGridView extraAccessGrid;

        public AccessGridToggle(frm_AccountList accountForm, DataGridView originalGrid)
        {
            frmAccountList = accountForm;
            this.originalGrid = originalGrid;
        }

        public void ToggleExtraGrid(bool userGridExist)
        {
            // If extraGrid does not exist or is not currently added to the form,
            // add it and adjust the layout.
            if (extraAccessGrid == null || !frmAccountList.Controls.Contains(extraAccessGrid))
            {
                if (extraAccessGrid == null)
                {
                    extraAccessGrid = new DataGridView();
                    extraAccessGrid.Name = "dgvExtra";
                    // Optionally: configure extraGrid (columns, default settings, etc.)
                }

                // Set the original grid's docking to Top and give it half the form's height.
                originalGrid.Dock = DockStyle.Top;
                originalGrid.Height = frmAccountList.ClientSize.Height / 2;

                // Set the extra grid to fill the remaining space.
                extraAccessGrid.Dock = DockStyle.Fill;

                // Add the extra grid to the form.
                frmAccountList.Controls.Add(extraAccessGrid);
                // Ensure the extra grid appears below the original grid.
                extraAccessGrid.BringToFront();

                // Fill extraGrid with data:
                // Instantiate your data access object.
                DbaConnection dbaConnection = new DbaConnection();
                // Create your stored procedure string.
                string spString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", "0", "0", "0", "0");
                // Set the DataSource of the extra grid.
                extraAccessGrid.DataSource = dbaConnection.SelectData(spString);
                // Configure the extra grid's appearance.
                extraAccessGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Optionally adjust the columns if the data contains at least six columns:
                if (extraAccessGrid.Columns.Count >= 6)
                {
                    extraAccessGrid.Columns[0].FillWeight = 6;
                    extraAccessGrid.Columns[1].FillWeight = 6;
                    extraAccessGrid.Columns[2].FillWeight = 6;
                    extraAccessGrid.Columns[3].FillWeight = 38;
                    extraAccessGrid.Columns[4].FillWeight = 38;
                    extraAccessGrid.Columns[5].FillWeight = 6;
                }

                
            }
            else // If the extra grid is already added, remove it and reset the layout.
            {
                frmAccountList.Controls.Remove(extraAccessGrid);
                originalGrid.Dock = DockStyle.Fill;
            }
        }
    }
}
