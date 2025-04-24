using F21Party.DBA;
using F21Party.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F21Party.Controllers
{
    internal class UserGridToggle
    {
        public frm_AccountList parentForm;
        private DataGridView originalGrid;
        private DataGridView extraUserGrid;

        public UserGridToggle(frm_AccountList accountForm, DataGridView originalGrid)
        {
            this.parentForm = accountForm;
            this.originalGrid = originalGrid;
        }

        public void ToggleExtraGrid()
        {
            // If extraGrid does not exist or is not currently added to the form,
            // add it and adjust the layout.
            if (extraUserGrid == null || !parentForm.Controls.Contains(extraUserGrid))
            {
                if (extraUserGrid == null)
                {
                    extraUserGrid = new DataGridView();
                    extraUserGrid.Name = "dgvExtraUser";
                    // Optionally: configure extraGrid (columns, default settings, etc.)
                }

                // Set the original grid's docking to Top and give it half the form's height.
                originalGrid.Dock = DockStyle.Top;
                originalGrid.Height = parentForm.ClientSize.Height / 2;

                // Set the extra grid to fill the remaining space.
                extraUserGrid.Dock = DockStyle.Fill;

                // Add the extra grid to the form.
                parentForm.Controls.Add(extraUserGrid);
                // Ensure the extra grid appears below the original grid.
                extraUserGrid.BringToFront();

                // Fill extraGrid with data:
                // Instantiate your data access object.
                DbaConnection dbaConnection = new DbaConnection();
                // Create your stored procedure string.
                string SPString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", "0", "0", "0", "0");
                // Set the DataSource of the extra grid.
                extraUserGrid.DataSource = dbaConnection.SelectData(SPString);
                // Configure the extra grid's appearance.
                extraUserGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Optionally adjust the columns if the data contains at least six columns:
                if (extraUserGrid.Columns.Count >= 6)
                {
                    extraUserGrid.Columns[0].FillWeight = 6;
                    extraUserGrid.Columns[1].FillWeight = 6;
                    extraUserGrid.Columns[2].FillWeight = 6;
                    extraUserGrid.Columns[3].FillWeight = 38;
                    extraUserGrid.Columns[4].FillWeight = 38;
                    extraUserGrid.Columns[5].FillWeight = 6;
                }
                parentForm.tsbUser.Text = "Close";
                
                
            }
            else // If the extra grid is already added, remove it and reset the layout.
            {
                
                parentForm.Controls.Remove(extraUserGrid);
                originalGrid.Dock = DockStyle.Fill;
                parentForm.tsbUser.Text = "User";
            }
        }

        public void DoubleToggleExtraGrid()
        {
            // If extraGrid does not exist or is not currently added to the form,
            // add it and adjust the layout.
            if (extraUserGrid == null || !parentForm.Controls.Contains(extraUserGrid))
            {
                if (extraUserGrid == null)
                {
                    extraUserGrid = new DataGridView();
                    extraUserGrid.Name = "dgvExtraUser";
                    // Optionally: configure extraGrid (columns, default settings, etc.)
                }

                // Set the original grid's docking to Top and give it half the form's height.
                originalGrid.Dock = DockStyle.Top;
                originalGrid.Height = parentForm.ClientSize.Height / 2;

                // Set the extra grid to fill the remaining space.
                extraUserGrid.Dock = DockStyle.Fill;

                // Add the extra grid to the form.
                parentForm.Controls.Add(extraUserGrid);
                // Ensure the extra grid appears below the original grid.
                extraUserGrid.BringToFront();

                // Fill extraGrid with data:
                // Instantiate your data access object.
                DbaConnection dbaConnection = new DbaConnection();
                // Create your stored procedure string.
                string SPString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", "0", "0", "0", "0");
                // Set the DataSource of the extra grid.
                extraUserGrid.DataSource = dbaConnection.SelectData(SPString);
                // Configure the extra grid's appearance.
                extraUserGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Optionally adjust the columns if the data contains at least six columns:
                if (extraUserGrid.Columns.Count >= 6)
                {
                    extraUserGrid.Columns[0].FillWeight = 6;
                    extraUserGrid.Columns[1].FillWeight = 6;
                    extraUserGrid.Columns[2].FillWeight = 6;
                    extraUserGrid.Columns[3].FillWeight = 38;
                    extraUserGrid.Columns[4].FillWeight = 38;
                    extraUserGrid.Columns[5].FillWeight = 6;
                }

                
                //if (extraUserGrid.DataSource != null)
                //{
                //    parentForm.Controls.Remove(extraUserGrid);
                //    originalGrid.Dock = DockStyle.Fill;
                //}

                // Assume extraUserGrid.DataSource is a DataTable.
                DataTable originalTable = ((DataTable)extraUserGrid.DataSource).Copy();

                // Get the UserID value from the currently double-clicked row in dgvUserSetting.
                var selectedUserID = parentForm.dgvUserSetting.CurrentRow.Cells["UserID"].Value.ToString();

                // Use the DataTable's Select method to filter rows that match the selected UserID.
                DataRow[] filteredRows = originalTable.Select("UserID = '" + selectedUserID + "'");

                if (filteredRows.Length > 0)
                {
                    // If there is at least one matching row, copy the filtered rows into a new DataTable.
                    DataTable filteredTable = filteredRows.CopyToDataTable();
                    extraUserGrid.DataSource = filteredTable;
                }
                else
                {
                    // If no rows match the filter, clear the DataSource.
                    extraUserGrid.DataSource = null;
                }
                parentForm.tsbUser.Text = "Close";

            }
            else // If the extra grid is already added, remove it and reset the layout.
            {
                
                // Instantiate your data access object.
                DbaConnection dbaConnection = new DbaConnection();
                // Create your stored procedure string.
                string SPString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", "0", "0", "0", "0");
                // Set the DataSource of the extra grid.
                extraUserGrid.DataSource = dbaConnection.SelectData(SPString);

                // Assume extraUserGrid.DataSource is a DataTable.
                DataTable originalTable = ((DataTable)extraUserGrid.DataSource).Copy();

                // Get the UserID value from the currently double-clicked row in dgvUserSetting.
                var selectedUserID = parentForm.dgvUserSetting.CurrentRow.Cells["UserID"].Value.ToString();

                // Use the DataTable's Select method to filter rows that match the selected UserID.
                DataRow[] filteredRows = originalTable.Select("UserID = '" + selectedUserID + "'");

                if (filteredRows.Length > 0)
                {
                    // If there is at least one matching row, copy the filtered rows into a new DataTable.
                    DataTable filteredTable = filteredRows.CopyToDataTable();
                    extraUserGrid.DataSource = filteredTable;
                }
                else
                {
                    // If no rows match the filter, clear the DataSource.
                    extraUserGrid.DataSource = null;
                }
                parentForm.tsbUser.Text = "Close";

            }
        }
    }
}
