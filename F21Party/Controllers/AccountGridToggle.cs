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
    internal class AccountGridToggle
    {
        private frm_UserList parentForm;
        private DataGridView originalGrid;
        private DataGridView extraAccountGrid;

        public AccountGridToggle(frm_UserList accountForm, DataGridView originalGrid)
        {
            parentForm = accountForm;
            this.originalGrid = originalGrid;
        }

        public void ToggleExtraGrid()
        {
            // If extraGrid does not exist or is not currently added to the form,
            // add it and adjust the layout.
            if (extraAccountGrid == null || !parentForm.Controls.Contains(extraAccountGrid))
            {
                if (extraAccountGrid == null)
                {
                    extraAccountGrid = new DataGridView();
                    extraAccountGrid.Name = "dgvExtraAccount";
                    // Optionally: configure extraGrid (columns, default settings, etc.)
                }

                // Set the original grid's docking to Top and give it half the form's height.
                originalGrid.Dock = DockStyle.Top;
                originalGrid.Height = parentForm.ClientSize.Height / 2;

                // Set the extra grid to fill the remaining space.
                extraAccountGrid.Dock = DockStyle.Fill;

                // Add the extra grid to the form.
                parentForm.Controls.Add(extraAccountGrid);
                // Ensure the extra grid appears below the original grid.
                extraAccountGrid.BringToFront();

                // Fill extraGrid with data:
                // Instantiate your data access object.
                DbaConnection dbaConnection = new DbaConnection();
                // Create your stored procedure string.
                string spString = string.Format("SP_Select_Accounts N'{0}', N'{1}', N'{2}', N'{3}'", "0", "0", "0", "5");
                // Set the DataSource of the extra grid.
                extraAccountGrid.DataSource = dbaConnection.SelectData(spString);
                // Configure the extra grid's appearance.
                extraAccountGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Optionally adjust the columns if the data contains at least six columns:
                if (extraAccountGrid.Columns.Count >= 6)
                {
                    extraAccountGrid.Columns[0].FillWeight = 10;
                    extraAccountGrid.Columns[1].Visible = false;
                    extraAccountGrid.Columns[2].FillWeight = 10;
                    extraAccountGrid.Columns[3].FillWeight = 35;
                    extraAccountGrid.Columns[4].Visible = false;
                    extraAccountGrid.Columns[5].FillWeight = 10;
                    extraAccountGrid.Columns[6].FillWeight = 35;
                }

                parentForm.tsbAccount.Text = "Close";


            }
            else // If the extra grid is already added, remove it and reset the layout.
            {
                parentForm.Controls.Remove(extraAccountGrid);
                originalGrid.Dock = DockStyle.Fill;
                parentForm.tsbAccount.Text = "Accounts";
            }
        }

        public void DoubleToggleExtraGrid()
        {
            // If extraGrid does not exist or is not currently added to the form,
            // add it and adjust the layout.
            if (extraAccountGrid == null || !parentForm.Controls.Contains(extraAccountGrid))
            {
                if (extraAccountGrid == null)
                {
                    extraAccountGrid = new DataGridView();
                    extraAccountGrid.Name = "dgvExtraUser";
                    // Optionally: configure extraGrid (columns, default settings, etc.)
                }

                // Set the original grid's docking to Top and give it half the form's height.
                originalGrid.Dock = DockStyle.Top;
                originalGrid.Height = parentForm.ClientSize.Height / 2;

                // Set the extra grid to fill the remaining space.
                extraAccountGrid.Dock = DockStyle.Fill;

                // Add the extra grid to the form.
                parentForm.Controls.Add(extraAccountGrid);
                // Ensure the extra grid appears below the original grid.
                extraAccountGrid.BringToFront();

                // Fill extraGrid with data:
                // Instantiate your data access object.
                DbaConnection dbaConnection = new DbaConnection();
                // Create your stored procedure string.
                string spString = string.Format("SP_Select_Accounts N'{0}', N'{1}', N'{2}', N'{3}'", "0", "0", "0", "5");
                // Set the DataSource of the extra grid.
                extraAccountGrid.DataSource = dbaConnection.SelectData(spString);
                // Configure the extra grid's appearance.
                extraAccountGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Optionally adjust the columns if the data contains at least six columns:
                if (extraAccountGrid.Columns.Count >= 6)
                {
                    extraAccountGrid.Columns[0].FillWeight = 10;
                    extraAccountGrid.Columns[1].Visible = false;
                    extraAccountGrid.Columns[2].FillWeight = 10;
                    extraAccountGrid.Columns[3].FillWeight = 35;
                    extraAccountGrid.Columns[4].Visible = false;
                    extraAccountGrid.Columns[5].FillWeight = 10;
                    extraAccountGrid.Columns[6].FillWeight = 35;
                }


                //if (extraAccountGrid.DataSource != null)
                //{
                //    parentForm.Controls.Remove(extraAccountGrid);
                //    originalGrid.Dock = DockStyle.Fill;
                //}

                // Assume extraAccountGrid.DataSource is a DataTable.
                DataTable originalTable = ((DataTable)extraAccountGrid.DataSource).Copy();

                // Get the UserID value from the currently double-clicked row in dgvUserSetting.
                var selectedUserID = parentForm.dgvUserSetting.CurrentRow.Cells["UserID"].Value.ToString();

                // Use the DataTable's Select method to filter rows that match the selected UserID.
                DataRow[] filteredRows = originalTable.Select("UserID = '" + selectedUserID + "'");

                if (filteredRows.Length > 0)
                {
                    // If there is at least one matching row, copy the filtered rows into a new DataTable.
                    DataTable filteredTable = filteredRows.CopyToDataTable();
                    extraAccountGrid.DataSource = filteredTable;
                }
                else
                {
                    // If no rows match the filter, clear the DataSource.
                    extraAccountGrid.DataSource = null;
                }
                parentForm.tsbAccount.Text = "Close";

            }
            else // If the extra grid is already added, remove it and reset the layout.
            {

                // Instantiate your data access object.
                DbaConnection dbaConnection = new DbaConnection();
                // Create your stored procedure string.
                string spString = string.Format("SP_Select_Accounts N'{0}', N'{1}', N'{2}', N'{3}'", "0", "0", "0", "5");
                // Set the DataSource of the extra grid.
                extraAccountGrid.DataSource = dbaConnection.SelectData(spString);

                // Assume extraAccountGrid.DataSource is a DataTable.
                DataTable originalTable = ((DataTable)extraAccountGrid.DataSource).Copy();

                // Get the UserID value from the currently double-clicked row in dgvUserSetting.
                var selectedUserID = parentForm.dgvUserSetting.CurrentRow.Cells["UserID"].Value.ToString();

                // Use the DataTable's Select method to filter rows that match the selected UserID.
                DataRow[] filteredRows = originalTable.Select("UserID = '" + selectedUserID + "'");

                if (filteredRows.Length > 0)
                {
                    // If there is at least one matching row, copy the filtered rows into a new DataTable.
                    DataTable filteredTable = filteredRows.CopyToDataTable();
                    extraAccountGrid.DataSource = filteredTable;
                }
                else
                {
                    // If no rows match the filter, clear the DataSource.
                    extraAccountGrid.DataSource = null;
                }
                parentForm.tsbAccount.Text = "Close";

            }
        }
    }
}
