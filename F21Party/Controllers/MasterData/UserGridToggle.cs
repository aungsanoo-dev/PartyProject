using F21Party.DBA;
using F21Party.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace F21Party.Controllers
{
    internal class UserGridToggle
    {
        private readonly frm_AccountList _parentForm;
        private readonly DataGridView _originalGrid;
        private readonly DbaConnection _dbaConnection = new DbaConnection();
        private DataGridView _extraUserGrid;
        private string _spString;
        
        public UserGridToggle(frm_AccountList accountForm, DataGridView originalGrid)
        {
            _parentForm = accountForm;
            this._originalGrid = originalGrid;
        }
        
        public void ToggleExtraGrid()
        {
            if (!Program.PublicArrReadAccessPages.Contains("Users"))
            {
                MessageBox.Show("You don't have 'Read' Access on User!");
                return;
            }
            
            // If extraGrid does not exist or is not currently added to the form,
            // add it and adjust the layout.
            if (_extraUserGrid == null || !_parentForm.Controls.Contains(_extraUserGrid))
            {
                if (_extraUserGrid == null)
                {
                    _extraUserGrid = new DataGridView();
                    _extraUserGrid.Name = "dgvExtraUser";
                    // Optionally: configure extraGrid (columns, default settings, etc.)
                }

                // Set the original grid's docking to Top and give it half the form's height.
                _originalGrid.Dock = DockStyle.Top;
                _originalGrid.Height = _parentForm.ClientSize.Height / 2;

                // Set the extra grid to fill the remaining space.
                _extraUserGrid.Dock = DockStyle.Fill;

                // Add the extra grid to the form.
                _parentForm.Controls.Add(_extraUserGrid);
                // Ensure the extra grid appears below the original grid.
                _extraUserGrid.BringToFront();

                // Fill extraGrid with data:
                // Instantiate your data access object.
                //DbaConnection dbaConnection = new DbaConnection();
                // Create your stored procedure string.
                _spString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", "0", "0", "0", "7");
                // Set the DataSource of the extra grid.
                _extraUserGrid.DataSource = _dbaConnection.SelectData(_spString);
                // Configure the extra grid's appearance.
                _extraUserGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                _extraUserGrid.CellPainting -= ExtraUserGrid_CellPainting;
                _extraUserGrid.CellPainting += ExtraUserGrid_CellPainting;

                _extraUserGrid.CellClick -= ExtraUserGrid_CellClick;
                _extraUserGrid.CellClick += ExtraUserGrid_CellClick;

                //extraUserGrid.CellValueChanged += ExtraUserGrid_CellValueChanged;

                // Optionally adjust the columns if the data contains at least six columns:
                if (_extraUserGrid.Columns.Count >= 7)
                {
                    _extraUserGrid.Columns[0].FillWeight = 6;
                    _extraUserGrid.Columns[1].FillWeight = 6;
                    _extraUserGrid.Columns[2].FillWeight = 18;
                    _extraUserGrid.Columns[3].FillWeight = 30;
                    _extraUserGrid.Columns[4].FillWeight = 18;
                    _extraUserGrid.Columns[5].Visible = false;
                    _extraUserGrid.Columns[6].FillWeight = 10;
                    _extraUserGrid.Columns[7].FillWeight = 12;
                }
                _parentForm.tsbUser.Text = "Close";

                _extraUserGrid.ReadOnly = true;

            }
            else // If the extra grid is already added, remove it and reset the layout.
            {
                _parentForm.Controls.Remove(_extraUserGrid);
                _originalGrid.Dock = DockStyle.Fill;
                _parentForm.tsbUser.Text = "Users";
            }
        }
        //private void ExtraUserGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    MessageBox.Show("It changed!");
        //}
        public void DoubleToggleExtraGrid()
        {
            if (!Program.PublicArrReadAccessPages.Contains("Users"))
            {
                MessageBox.Show("You don't have 'Read' Access on User!");
                return;
            }

            // If extraGrid does not exist or is not currently added to the form,
            // add it and adjust the layout.
            if (_extraUserGrid == null || !_parentForm.Controls.Contains(_extraUserGrid))
            {
                if (_extraUserGrid == null)
                {
                    _extraUserGrid = new DataGridView();
                    _extraUserGrid.Name = "dgvExtraUser";
                    // Optionally: configure extraGrid (columns, default settings, etc.)
                }

                // Set the original grid's docking to Top and give it half the form's height.
                _originalGrid.Dock = DockStyle.Top;
                _originalGrid.Height = _parentForm.ClientSize.Height / 2;

                // Set the extra grid to fill the remaining space.
                _extraUserGrid.Dock = DockStyle.Fill;

                // Add the extra grid to the form.
                _parentForm.Controls.Add(_extraUserGrid);
                // Ensure the extra grid appears below the original grid.
                _extraUserGrid.BringToFront();

                // Fill extraGrid with data:
                // Instantiate your data access object.
                DbaConnection dbaConnection = new DbaConnection();
                // Create your stored procedure string.
                _spString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", "0", "0", "0", "7");
                // Set the DataSource of the extra grid.
                _extraUserGrid.DataSource = dbaConnection.SelectData(_spString);
                // Configure the extra grid's appearance.
                _extraUserGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


                // Optionally adjust the columns if the data contains at least six columns:
                if (_extraUserGrid.Columns.Count >= 7)
                {
                    _extraUserGrid.Columns[0].FillWeight = 6;
                    _extraUserGrid.Columns[1].FillWeight = 6;
                    _extraUserGrid.Columns[2].FillWeight = 18;
                    _extraUserGrid.Columns[3].FillWeight = 30;
                    _extraUserGrid.Columns[4].FillWeight = 18;
                    _extraUserGrid.Columns[5].Visible = false;
                    _extraUserGrid.Columns[6].FillWeight = 10;
                    _extraUserGrid.Columns[7].FillWeight = 12;
                }

                _extraUserGrid.ReadOnly = true;
                //if (extraUserGrid.DataSource != null)
                //{
                //    parentForm.Controls.Remove(extraUserGrid);
                //    originalGrid.Dock = DockStyle.Fill;
                //}

                // Assume extraUserGrid.DataSource is a DataTable.
                DataTable originalTable = ((DataTable)_extraUserGrid.DataSource).Copy();

                // Get the UserID value from the currently double-clicked row in dgvUserSetting.
                var selectedUserID = _parentForm.dgvAccountSetting.CurrentRow.Cells["UserID"].Value.ToString();

                // Use the DataTable's Select method to filter rows that match the selected UserID.
                DataRow[] filteredRows = originalTable.Select("UserID = '" + selectedUserID + "'");

                if (filteredRows.Length > 0)
                {
                    // If there is at least one matching row, copy the filtered rows into a new DataTable.
                    DataTable filteredTable = filteredRows.CopyToDataTable();
                    _extraUserGrid.DataSource = filteredTable;
                }
                else
                {
                    // If no rows match the filter, clear the DataSource.
                    _extraUserGrid.DataSource = null;
                }
                _parentForm.tsbUser.Text = "Close";

            }
            else // If the extra grid is already added, remove it and reset the layout.
            {
                
                // Instantiate your data access object.
                DbaConnection dbaConnection = new DbaConnection();
                // Create your stored procedure string.
                _spString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", "0", "0", "0", "7");
                // Set the DataSource of the extra grid.
                _extraUserGrid.DataSource = dbaConnection.SelectData(_spString);

                // Assume extraUserGrid.DataSource is a DataTable.
                DataTable originalTable = ((DataTable)_extraUserGrid.DataSource).Copy();

                // Get the UserID value from the currently double-clicked row in dgvUserSetting.
                var selectedUserID = _parentForm.dgvAccountSetting.CurrentRow.Cells["UserID"].Value.ToString();

                // Use the DataTable's Select method to filter rows that match the selected UserID.
                DataRow[] filteredRows = originalTable.Select("UserID = '" + selectedUserID + "'");

                if (filteredRows.Length > 0)
                {
                    // If there is at least one matching row, copy the filtered rows into a new DataTable.
                    DataTable filteredTable = filteredRows.CopyToDataTable();
                    _extraUserGrid.DataSource = filteredTable;
                }
                else
                {
                    // If no rows match the filter, clear the DataSource.
                    _extraUserGrid.DataSource = null;
                }
                _parentForm.tsbUser.Text = "Close";

            }
        }

        private void ExtraUserGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (!Program.PublicArrWriteAccessPages.Contains("Accounts"))
            {
                return;
            }


            //if (e.ColumnIndex == 6 && e.RowIndex >= 0)
            //{
            //    var grid = (DataGridView)sender;
            //    string cellValue = grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();

            //    if (cellValue == "False")
            //    {
            //        e.PaintBackground(e.ClipBounds, true);
            //        e.PaintContent(e.ClipBounds);

            //        // Draw the text "false"
            //        TextRenderer.DrawText(e.Graphics, "False", grid.Font, e.CellBounds, grid.ForeColor, TextFormatFlags.Left);

            //        // Draw the button
            //        Rectangle buttonRect = new Rectangle(
            //            e.CellBounds.Left + 40, // position to the right of "false"
            //            e.CellBounds.Top + 2,
            //            40, // button width
            //            e.CellBounds.Height - 4 // button height
            //        );

            //        ButtonRenderer.DrawButton(e.Graphics, buttonRect, "Add", grid.Font, false, PushButtonState.Default);
            //        e.Handled = true;
            //    }
            //}
            if (e.ColumnIndex == 6 && e.RowIndex >= 0)
            {
                object value = _extraUserGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (value != null && value.ToString() == "False")
                {
                    e.PaintBackground(e.ClipBounds, true);

                    // Draw the "False" text manually
                    TextRenderer.DrawText(e.Graphics, "False", e.CellStyle.Font,
                        new Rectangle(e.CellBounds.X + 2, e.CellBounds.Y + 2, 40, e.CellBounds.Height - 4),
                        e.CellStyle.ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

                    // Draw the "Add" button next to it
                    Rectangle buttonRect = new Rectangle(
                        e.CellBounds.X + 45, e.CellBounds.Y + 2, 40, e.CellBounds.Height - 4);

                    ButtonRenderer.DrawButton(e.Graphics, buttonRect, "Add", e.CellStyle.Font,
                        false, PushButtonState.Default);

                    // prevent default painting of "False"
                    e.Handled = true;
                }
            }
            //e.Handled = true;
        }

        private void ExtraUserGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!Program.PublicArrWriteAccessPages.Contains("Accounts"))
            {
                return;
            }

            if (e.ColumnIndex == 6 && e.RowIndex >= 0)
            {
                var grid = (DataGridView)sender;
                string cellValue = grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();

                if (cellValue == "False")
                {
                    // Simulate button click if user clicks inside the "Add" button area
                    var cellBounds = grid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                    var mouse = grid.PointToClient(Cursor.Position);

                    Rectangle buttonRect = new Rectangle(
                        cellBounds.Left + 40,
                        cellBounds.Top + 2,
                        40,
                        cellBounds.Height - 4
                    );

                    if (buttonRect.Contains(mouse))
                    {
                        
                        frm_CreateAccount frmCreateAccount = new frm_CreateAccount();

                        frmCreateAccount.UserID = Convert.ToInt32(_extraUserGrid.CurrentRow.Cells["UserID"].Value.ToString());
                        frmCreateAccount.txtFullName.Text = _extraUserGrid.CurrentRow.Cells["FullName"].Value.ToString();
                        frmCreateAccount.txtAddress.Text = _extraUserGrid.CurrentRow.Cells["Address"].Value.ToString();
                        frmCreateAccount.txtPhone.Text = _extraUserGrid.CurrentRow.Cells["Phone"].Value.ToString();
                        frmCreateAccount.cboPosition.DisplayMember = _extraUserGrid.CurrentRow.Cells["PositionID"].Value.ToString();

                        frmCreateAccount.txtFullName.Enabled = false;
                        frmCreateAccount.txtAddress.Enabled = false;
                        frmCreateAccount.txtPhone.Enabled = false;
                        frmCreateAccount.cboPosition.Enabled = false;

                        frmCreateAccount.btnCreate.Text = "Add";
                        frmCreateAccount.ShowDialog();

                        _parentForm.RefreshAccountList();
                        RefreshExtraGrid();
                        //ToggleExtraGrid();
                        //ToggleExtraGrid();
                    }
                }

            }
            
        }

        public void RefreshExtraGrid()
        {
            if (_extraUserGrid != null && _parentForm.Controls.Contains(_extraUserGrid))
            {
                // Instantiate your data access object.
                DbaConnection dbaConnection = new DbaConnection();
                // Create your stored procedure string.
                _spString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", "0", "0", "0", "7");
                // Refresh the DataSource of the extra grid.
                _extraUserGrid.DataSource = dbaConnection.SelectData(_spString);
            }
        }
    }
}
