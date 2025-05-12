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
        public frm_AccountList parentForm;
        private DataGridView originalGrid;
        private DataGridView extraUserGrid;
        
        public UserGridToggle(frm_AccountList accountForm, DataGridView originalGrid)
        {
            parentForm = accountForm;
            this.originalGrid = originalGrid;
        }
        
        public void ToggleExtraGrid()
        {
            if (!Program.PublicArrReadAccessPages.Contains("User"))
            {
                MessageBox.Show("You don't have 'Read' Access on User!");
                return;
            }
            
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
                string spString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", "0", "0", "0", "7");
                // Set the DataSource of the extra grid.
                extraUserGrid.DataSource = dbaConnection.SelectData(spString);
                // Configure the extra grid's appearance.
                extraUserGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                extraUserGrid.CellPainting -= ExtraUserGrid_CellPainting;
                extraUserGrid.CellPainting += ExtraUserGrid_CellPainting;

                extraUserGrid.CellClick -= ExtraUserGrid_CellClick;
                extraUserGrid.CellClick += ExtraUserGrid_CellClick;

                //extraUserGrid.CellValueChanged += ExtraUserGrid_CellValueChanged;

                // Optionally adjust the columns if the data contains at least six columns:
                if (extraUserGrid.Columns.Count >= 7)
                {
                    extraUserGrid.Columns[0].FillWeight = 6;
                    extraUserGrid.Columns[1].FillWeight = 6;
                    extraUserGrid.Columns[2].FillWeight = 18;
                    extraUserGrid.Columns[3].FillWeight = 30;
                    extraUserGrid.Columns[4].FillWeight = 18;
                    extraUserGrid.Columns[5].Visible = false;
                    extraUserGrid.Columns[6].FillWeight = 10;
                    extraUserGrid.Columns[7].FillWeight = 12;
                }
                parentForm.tsbUser.Text = "Close";
                
                
            }
            else // If the extra grid is already added, remove it and reset the layout.
            {
                parentForm.Controls.Remove(extraUserGrid);
                originalGrid.Dock = DockStyle.Fill;
                parentForm.tsbUser.Text = "Users";
            }
        }
        //private void ExtraUserGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    MessageBox.Show("It changed!");
        //}
        public void DoubleToggleExtraGrid()
        {
            if (!Program.PublicArrReadAccessPages.Contains("User"))
            {
                MessageBox.Show("You don't have 'Read' Access on User!");
                return;
            }

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
                string spString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", "0", "0", "0", "7");
                // Set the DataSource of the extra grid.
                extraUserGrid.DataSource = dbaConnection.SelectData(spString);
                // Configure the extra grid's appearance.
                extraUserGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


                // Optionally adjust the columns if the data contains at least six columns:
                if (extraUserGrid.Columns.Count >= 7)
                {
                    extraUserGrid.Columns[0].FillWeight = 6;
                    extraUserGrid.Columns[1].FillWeight = 6;
                    extraUserGrid.Columns[2].FillWeight = 18;
                    extraUserGrid.Columns[3].FillWeight = 30;
                    extraUserGrid.Columns[4].FillWeight = 18;
                    extraUserGrid.Columns[5].Visible = false;
                    extraUserGrid.Columns[6].FillWeight = 10;
                    extraUserGrid.Columns[7].FillWeight = 12;
                }

                
                //if (extraUserGrid.DataSource != null)
                //{
                //    parentForm.Controls.Remove(extraUserGrid);
                //    originalGrid.Dock = DockStyle.Fill;
                //}

                // Assume extraUserGrid.DataSource is a DataTable.
                DataTable originalTable = ((DataTable)extraUserGrid.DataSource).Copy();

                // Get the UserID value from the currently double-clicked row in dgvUserSetting.
                var selectedUserID = parentForm.dgvAccountSetting.CurrentRow.Cells["UserID"].Value.ToString();

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
                string spString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", "0", "0", "0", "7");
                // Set the DataSource of the extra grid.
                extraUserGrid.DataSource = dbaConnection.SelectData(spString);

                // Assume extraUserGrid.DataSource is a DataTable.
                DataTable originalTable = ((DataTable)extraUserGrid.DataSource).Copy();

                // Get the UserID value from the currently double-clicked row in dgvUserSetting.
                var selectedUserID = parentForm.dgvAccountSetting.CurrentRow.Cells["UserID"].Value.ToString();

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

        private void ExtraUserGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
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
                object value = extraUserGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
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

                        frmCreateAccount._UserID = Convert.ToInt32(extraUserGrid.CurrentRow.Cells["UserID"].Value.ToString());
                        frmCreateAccount.txtFullName.Text = extraUserGrid.CurrentRow.Cells["FullName"].Value.ToString();
                        frmCreateAccount.txtAddress.Text = extraUserGrid.CurrentRow.Cells["Address"].Value.ToString();
                        frmCreateAccount.txtPhone.Text = extraUserGrid.CurrentRow.Cells["Phone"].Value.ToString();
                        frmCreateAccount.cboPosition.DisplayMember = extraUserGrid.CurrentRow.Cells["PositionID"].Value.ToString();

                        frmCreateAccount.btnCreate.Text = "Add";
                        frmCreateAccount.ShowDialog();

                        parentForm.RefreshAccountList();
                        RefreshExtraGrid();
                        //ToggleExtraGrid();
                        //ToggleExtraGrid();
                    }
                }

            }
            
        }

        public void RefreshExtraGrid()
        {
            if (extraUserGrid != null && parentForm.Controls.Contains(extraUserGrid))
            {
                // Instantiate your data access object.
                DbaConnection dbaConnection = new DbaConnection();
                // Create your stored procedure string.
                string spString = string.Format("SP_Select_Users N'{0}', N'{1}', N'{2}', N'{3}'", "0", "0", "0", "7");
                // Refresh the DataSource of the extra grid.
                extraUserGrid.DataSource = dbaConnection.SelectData(spString);
            }
        }
    }
}
