using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace NewSoft
{
    public class LibMembers : frmLibSoft
    {
        public void displayMembers(DataGridView gridMembers)
        {
            String strCount = "select max(sortid) from members";
            int memCount = returnInt(strCount, "members");
            int i = 0;
            for (i = 0; i <= memCount / 20; i++)
                LstNumbers.Items.Add(i + 1);
            //lstNumbers.Items.Add(i + 1);
            LstNumbers.SelectedIndex = 0;

            String strQuery = "select MemberID,MemberName,Address,Doj,Notes,Status,Mobile,Landline,Email,MemberType,Amount from members where sortid<20 order by sortid asc";

            DataSet ds = new DataSet();
            ds = returnDS(strQuery, "members");


            gridMembers.DataSource = ds;
            gridMembers.DataMember = "members";
        }

        public void Mem_Search(DataGridView gridMembers, string strMemSearch)
        {
            DataSet ds = new DataSet();
            //String strQuery = "select BookName from books where bookid in (select BookID from books where BookID='" + txtSearch.Text + "' union all select BookID from books where BookName like '%" + txtSearch.Text + "%')";

            String strQuery = "select members.MemberID,MemberName,Address,Doj,Notes,Status,Mobile,Landline,Email,MemberType,Amount from members inner join (select MemberID from members where memberid like '%" + strMemSearch + "%' union select MemberID from members where membername like '%" + strMemSearch + "%' union select MemberID from members where mobile like '%" + strMemSearch + "%' union select MemberID from members where membername like '%" + strMemSearch + "%' union select MemberID from members where landline like '%" + strMemSearch + "%') as temp on members.MemberID =  temp.MemberID";
            ds = returnDS(strQuery, "members");
            gridMembers.DataSource = ds;
            gridMembers.DataMember = "members";
        }

        public bool returnMemberStatus(string memberid)
        {
            String strQuery;
            int memStatus;
            strQuery = "select status from members where memberid='" + memberid + "'";
            memStatus = returnInt(strQuery, "members");
            if (memStatus == 1)
                return true;
            else
                return false;
        }
    }
}
