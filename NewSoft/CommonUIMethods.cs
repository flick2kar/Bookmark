using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using DALLayer;

namespace NewSoft
{
    public class CommonUIMethods
    {
        public static void ClearControl(Control control)
        {
            if (control is TextBox)
            {
                TextBox txtbox = (TextBox)control;
                txtbox.Text = string.Empty;
            }
            else if (control is CheckBox)
            {
                CheckBox chkbox = (CheckBox)control;
                chkbox.Checked = false;
            }
            else if (control is RadioButton)
            {
                RadioButton rdbtn = (RadioButton)control;
                rdbtn.Checked = false;
            }
            else if (control is DateTimePicker)
            {
                DateTimePicker dtp = (DateTimePicker)control;
                dtp.Value = DateTime.Now;
            }
            else if (control is ComboBox)
            {
                ComboBox cmb = (ComboBox)control;
                if (cmb.DataSource != null)
                {
                    cmb.SelectedItem = string.Empty;
                    cmb.SelectedValue = 0;
                }
            }

            // repeat for combobox, listbox, checkbox and any other controls you want to clear
            if (control.HasChildren)
            {
                foreach (Control child in control.Controls)
                {
                    ClearControl(child);
                }
            }


        }

        public static void InitializeControls(Control control)
        {
            if (control is ComboBox)
            {
                ComboBox cmb = (ComboBox)control;
                if (cmb.DataSource != null && String.IsNullOrEmpty(cmb.Text))
                {
                    //cmb.SelectedItem = string.Empty;
                    cmb.SelectedValue = 0;
                    //cmb.Text = "Test";
                }
            }

            // repeat for combobox, listbox, checkbox and any other controls you want to clear
            if (control.HasChildren)
            {
                    foreach (Control child in control.Controls)
                    {
                        InitializeControls(child);
                    }                
            }
        }

        public static void setLstValue(ComboBox lstItems, String strValue)
        {
            int i = 0;
            //System.Data.DataRowView dr = new DataRowView();
            if (strValue.Equals(""))
                lstItems.SelectedIndex = 0;
            else
            {
                foreach (DataRowView strTemp in lstItems.Items)
                {
                    if (strTemp.Row[1].ToString().Equals(strValue))
                        lstItems.SelectedIndex = i;
                    i++;
                }
            }
        }

        public static void setBookLabel(Label lblBookLabel,string bookID)
        {
            DataTable ds = new DataTable();
            BookRepository bookRep = new BookRepository();
            ds = bookRep.GetBookIDDetails(bookID);
            lblBookLabel.Text = "Book not available";
            if (ds.Rows.Count > 0)
                lblBookLabel.Text = ds.Rows[0]["BookName"].ToString();
        }

        public static void setLendRate(TextBox txtLendRate, string bookID)
        {
            DataTable ds = new DataTable();
            BookRepository bookRep = new BookRepository();
            ds = bookRep.GetBookIDDetails(bookID);
            txtLendRate.Text = "0";
            if (ds.Rows.Count > 0)
                txtLendRate.Text = Math.Round((Int32.Parse(ds.Rows[0]["BookPrice"].ToString())*(0.12))).ToString();
        }

        public static DataTable ConvertGridToTable(DataGridView dsGV)
        {
            DataTable dt = new DataTable();
            DataColumn[] dcs = new DataColumn[] { };
            
            foreach (DataGridViewColumn c in dsGV.Columns)
            {
                if (c.HeaderText != "")
                {
                    DataColumn dc = new DataColumn();
                    dc.ColumnName = c.Name;
                    dc.DataType = c.ValueType;
                    dt.Columns.Add(dc);
                }

            }

            foreach (DataGridViewRow r in dsGV.Rows)
            {
                DataRow drow = dt.NewRow();

                foreach (DataGridViewCell cell in r.Cells)
                {
                    if(cell.Value!=null && cell.OwningColumn.HeaderText!="")
                        drow[cell.OwningColumn.Name] = cell.Value;
                }

                dt.Rows.Add(drow);
            }
            return dt;
        }

    }
}
