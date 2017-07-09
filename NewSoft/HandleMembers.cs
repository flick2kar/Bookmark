using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Entities;

namespace NewSoft
{
    public partial class frmLibSoft
    {
        private void initializeMemPage()
        {
            DataTable dt = new DataTable();
            dt = memRep.GetMemberDetails();
            int memCount = memRep.GetMaxSortID();
            int i = 0;
            for (i = 0; i <= memCount / 20; i++)
                lstNumbers.Items.Add(i + 1);
            //lstNumbers.Items.Add(i + 1);
            lstNumbers.SelectedIndex = 0;
            gridMembers.DataSource = dt;
            initializeMemDetails(Int32.Parse(gridMembers["ID", 0].Value.ToString()));

        }

        private void initializeMemDetails(int ID)
        {
            DataTable ds = new DataTable();
            ds = memRep.GetMemberDetailsByID(ID);
            txtMemMemberid.Text = ds.Rows[0]["MemberID"].ToString(); 
            txtMemMemName.Text = ds.Rows[0]["MemberName"].ToString();
            txtAddress.Text = ds.Rows[0]["Address"].ToString(); 
            dateDoj.Text = ds.Rows[0]["Doj"].ToString(); 
            txtMemNotes.Text = ds.Rows[0]["Notes"].ToString(); 
            if (Int32.Parse(ds.Rows[0]["Status"].ToString()) == 0)
                lstStatus.SelectedIndex = 2;
            else
                lstStatus.SelectedIndex = 1;
            txtMemMob.Text = ds.Rows[0]["Mobile"].ToString();
            txtMemland.Text = ds.Rows[0]["Landline"].ToString();
            txtEmail.Text = ds.Rows[0]["Email"].ToString();
            switch (ds.Rows[0]["MemberType"].ToString())
            {
                case "1":
                    lstMemType.SelectedIndex = 1;
                    break;
                case "2":
                    lstMemType.SelectedIndex = 2;
                    break;
                case "3":
                    lstMemType.SelectedIndex = 3;
                    break;
                case "4":
                    lstMemType.SelectedIndex = 4;
                    break;
                case "5":
                    lstMemType.SelectedIndex = 1;
                    break;
                default: lstMemType.SelectedIndex = 0; break;
            }
            txtMemAmt.Text = ds.Rows[0]["Amount"].ToString();
        }

        private Entities.member UpdateMemDetails(Entities.member memObj)
        {
            if (txtMemMemberid.Text == "")
            {
                MessageBox.Show("Please provide a MemberID");
                return null;
            }
           
            else
            {
                int intStatus = 0;
                if (lstStatus.SelectedItem.ToString() == "Active")
                    intStatus = 1;
                String strMemType = lstMemType.SelectedItem.ToString();
                int intMemType = 0;
                switch (strMemType)
                {
                    case "Budget":
                        intMemType = 1;
                        break;
                    case "Children":
                        intMemType = 2;
                        break;
                    case "Family":
                        intMemType = 3;
                        break;
                    case "Individual":
                        intMemType = 4;
                        break;
                    case "Unlimited":
                        intMemType = 1;
                        break;
                    default: lstMemType.SelectedIndex = 0; break;
                }
                //Entities.member memObj = new member();

                memObj.MemberID = txtMemMemberid.Text;
                memObj.MemberName = txtMemMemName.Text;
                memObj.Address = txtAddress.Text;
                memObj.Notes = txtMemNotes.Text;
                memObj.Doj = DateTime.Parse(dateDoj.Text);
                memObj.Status = intStatus;
                memObj.Mobile = txtMemMob.Text;
                memObj.Landline = txtMemland.Text;
                memObj.Email = txtEmail.Text;
                memObj.MemberType = intMemType;
                memObj.Amount = Int32.Parse(txtMemAmt.Text);
                memObj.SortID = Int32.Parse(txtMemMemberid.Text.Substring(1));
                //memObj.ID = Int32.Parse(memRep.GetMemberIDDetails(txtMemMemberid.Text).Rows[0]["ID"].ToString()); 
            }
            return memObj;
        }

        private void btnAddMem_Click(object sender, EventArgs e)
        {
            Entities.member memObj = new member();
            if (memRep.GetMemberIDDetails(txtMemMemberid.Text).Rows.Count > 0)
            {
                MessageBox.Show("MemberID already exists");
                return;
            }
            memObj = UpdateMemDetails(memObj);
            if (memObj == null)
                return;            
                
            memRep.InsertMembers(memObj);
            gridMembers.DataSource = memRep.GetMemberDetails();               
            
        }

        private void btnMemInsert_Click(object sender, EventArgs e)
        {
            Entities.member memObj = new member();
            if (memRep.GetMemberIDDetails(txtMemMemberid.Text).Rows.Count <= 0)
            {
                MessageBox.Show("MemberID doesn't exists");
                return;
            }            
            memObj = UpdateMemDetails(memObj);
            //memObj.ID = null;
            if (memObj == null)
                return;
            else
            {
                memObj.ID = Int32.Parse(memRep.GetMemberIDDetails(txtMemMemberid.Text).Rows[0]["ID"].ToString()); 
                memRep.UpdateMembers(memObj, memObj.MemberID);
            }
            gridMembers.DataSource = memRep.GetMemberDetails();

        }

        private void btnDelMem_Click(object sender, EventArgs e)
        {
            //String strValBook = "select count(*) from members where memberid='" + txtMemMemberid.Text + "'";
            //int intCnt = returnInt(strValBook, "members");
            //if (intCnt > 0)
            //{
            //    String strQuery = "delete from members where memberid='" + txtMemMemberid.Text + "'";
            //    InsertUpdateQuery(strQuery);
            //    LibMembers lm = new LibMembers();
            //    lm.displayMembers(gridMembers);
            //}
            //else
            //{
            //    MessageBox.Show("MemberID doesn't exists");
            //    return;
            //}

        }

        private void gridMem_RowContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                initializeMemDetails(Int32.Parse(gridMembers["ID", e.RowIndex].Value.ToString()));
            }
        }

        private void btnMemSearch_Click(object sender, EventArgs e)
        {
            gridMembers.DataSource = memRep.GetMemberSearch(txtMemSearch.Text);
        }

        private void Mem_Search(object sender, KeyEventArgs e)
        {
            if (e.KeyData.Equals(Keys.Enter))
            {
                gridMembers.DataSource = memRep.GetMemberSearch(txtMemSearch.Text);
            }
        }

        private void btnMemAll_Click(object sender, EventArgs e)
        {
            gridMembers.DataSource = memRep.GetMemberDetails();
        }

        private void lstNumbers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstNumbers.SelectedItem != null)
            {
                gridMembers.DataSource = memRep.GetMemberDetailsBySortID(Int32.Parse(lstNumbers.SelectedItem.ToString()));
                
            }
        }
        
        private void btnClearMem_Click(object sender, EventArgs e)
        {
            CommonUIMethods.ClearControl(this);
        }
    }
}
