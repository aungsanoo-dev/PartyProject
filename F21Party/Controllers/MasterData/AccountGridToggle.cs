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
        private frm_UserList _parentForm;
        private DataGridView _originalGrid;
        private DataGridView _extraAccountGrid;

        public AccountGridToggle(frm_UserList accountForm, DataGridView originalGrid)
        {
            _parentForm = accountForm;
            this._originalGrid = originalGrid;
        }

        public void ToggleExtraGrid()
        {
            // If extraGrid does not exist or is not currently added to the form, add it and adjust the layout. 
            if (_extraAccountGrid == null || !_parentForm.Controls.Contains(_extraAccountGrid))
            {
                if (_extraAccountGrid == null)
                {
                    _extraAccountGrid = new DataGridView();
                    _extraAccountGrid.Name = "dgvExtraAccount";
                }

                // Set the original grid's docking to Top and give it half the form's height.
                _originalGrid.Dock = DockStyle.Top;
                _originalGrid.Height = _parentForm.ClientSize.Height / 2;
                _extraAccountGrid.Dock = DockStyle.Fill;

                // Add the extra grid to the form.
                _parentForm.Controls.Add(_extraAccountGrid);
                // Ensure the extra grid appears below the original grid.
                _extraAccountGrid.BringToFront();

                // Fill extraGrid with data:
                DbaConnection dbaConnection = new DbaConnection();
                string spString = string.Format("SP_Select_Accounts N'{0}', N'{1}', N'{2}', N'{3}'", "0", "0", "0", "5");
                _extraAccountGrid.DataSource = dbaConnection.SelectData(spString);

                _extraAccountGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (_extraAccountGrid.Columns.Count >= 6)
                {
                    _extraAccountGrid.Columns[0].FillWeight = 10;
                    _extraAccountGrid.Columns[1].Visible = false;
                    _extraAccountGrid.Columns[2].FillWeight = 10;
                    _extraAccountGrid.Columns[3].FillWeight = 35;
                    _extraAccountGrid.Columns[4].Visible = false;
                    _extraAccountGrid.Columns[5].FillWeight = 10;
                    _extraAccountGrid.Columns[6].FillWeight = 35;
                }

                _parentForm.tsbAccount.Text = "Close";


            }
            else // If the extra grid is already added, remove it and reset the layout.
            {
                _parentForm.Controls.Remove(_extraAccountGrid);
                _originalGrid.Dock = DockStyle.Fill;
                _parentForm.tsbAccount.Text = "Accounts";
            }
        }

        public void DoubleToggleExtraGrid()
        {
            // If extraGrid does not exist or is not currently added to the form,add it and adjust the layout.
            if (_extraAccountGrid == null || !_parentForm.Controls.Contains(_extraAccountGrid))
            {
                if (_extraAccountGrid == null)
                {
                    _extraAccountGrid = new DataGridView();
                    _extraAccountGrid.Name = "dgvExtraUser";
                }

                // Set the original grid's docking to Top and give it half the form's height.
                _originalGrid.Dock = DockStyle.Top;
                _originalGrid.Height = _parentForm.ClientSize.Height / 2;
                _extraAccountGrid.Dock = DockStyle.Fill;

                // Add the extra grid to the form.
                _parentForm.Controls.Add(_extraAccountGrid);
                // Ensure the extra grid appears below the original grid.
                _extraAccountGrid.BringToFront();

                DbaConnection dbaConnection = new DbaConnection();
                string spString = string.Format("SP_Select_Accounts N'{0}', N'{1}', N'{2}', N'{3}'", "0", "0", "0", "5");
                _extraAccountGrid.DataSource = dbaConnection.SelectData(spString);
                _extraAccountGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Optionally adjust the columns if the data contains at least six columns:
                if (_extraAccountGrid.Columns.Count >= 6)
                {
                    _extraAccountGrid.Columns[0].FillWeight = 10;
                    _extraAccountGrid.Columns[1].Visible = false;
                    _extraAccountGrid.Columns[2].FillWeight = 10;
                    _extraAccountGrid.Columns[3].FillWeight = 35;
                    _extraAccountGrid.Columns[4].Visible = false;
                    _extraAccountGrid.Columns[5].FillWeight = 10;
                    _extraAccountGrid.Columns[6].FillWeight = 35;
                }

                // Assume extraAccountGrid.DataSource is a DataTable.
                DataTable originalTable = ((DataTable)_extraAccountGrid.DataSource).Copy();

                // Get the UserID value from the currently double-clicked row in dgvUserSetting.
                var selectedUserID = _parentForm.dgvUserSetting.CurrentRow.Cells["UserID"].Value.ToString();

                // Use the DataTable's Select method to filter rows that match the selected UserID.
                DataRow[] filteredRows = originalTable.Select("UserID = '" + selectedUserID + "'");

                if (filteredRows.Length > 0)
                {
                    // If there is at least one matching row, copy the filtered rows into a new DataTable.
                    DataTable filteredTable = filteredRows.CopyToDataTable();
                    _extraAccountGrid.DataSource = filteredTable;
                }
                else
                {
                    // If no rows match the filter, clear the DataSource.
                    _extraAccountGrid.DataSource = null;
                }
                _parentForm.tsbAccount.Text = "Close";

            }
            else // If the extra grid is already added, remove it and reset the layout.
            {

                DbaConnection dbaConnection = new DbaConnection();
                string spString = string.Format("SP_Select_Accounts N'{0}', N'{1}', N'{2}', N'{3}'", "0", "0", "0", "5");
                _extraAccountGrid.DataSource = dbaConnection.SelectData(spString);

                // Assume extraAccountGrid.DataSource is a DataTable.
                DataTable originalTable = ((DataTable)_extraAccountGrid.DataSource).Copy();

                // Get the UserID value from the currently double-clicked row in dgvUserSetting.
                var selectedUserID = _parentForm.dgvUserSetting.CurrentRow.Cells["UserID"].Value.ToString();

                // Use the DataTable's Select method to filter rows that match the selected UserID.
                DataRow[] filteredRows = originalTable.Select("UserID = '" + selectedUserID + "'");

                if (filteredRows.Length > 0)
                {
                    // If there is at least one matching row, copy the filtered rows into a new DataTable.
                    DataTable filteredTable = filteredRows.CopyToDataTable();
                    _extraAccountGrid.DataSource = filteredTable;
                }
                else
                {
                    // If no rows match the filter, clear the DataSource.
                    _extraAccountGrid.DataSource = null;
                }
                _parentForm.tsbAccount.Text = "Close";

            }
        }
    }
}
