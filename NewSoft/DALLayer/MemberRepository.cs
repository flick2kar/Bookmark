using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Entities;


namespace DALLayer
{
    public class MemberRepository
    {
        public DataTable GetMemberDetails()
        {
            DataTable ds = new DataTable();
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "select MemberID,MemberName,Address,Doj,Notes,Status,Mobile,Landline,Email,MemberType,Amount,ID from members where sortid<20 order by sortid asc";
                ds = dbMgr.ExecuteDataTable(CommandType.Text, strQuery);
            }
            return ds;
        }

        public DataTable GetMemberDetailsByID(int ID)
        {
            DataTable ds = new DataTable();
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "select MemberID,MemberName,Address,Doj,Notes,Status,Mobile,Landline,Email,MemberType,Amount,ID from members where ID="+ID+" order by sortid asc";
                ds = dbMgr.ExecuteDataTable(CommandType.Text, strQuery);
            }
            return ds;
        }

        public DataTable GetMemberIDDetails(string MemberID="")
        {
            DataTable ds = new DataTable();
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "select MemberID,MemberName,Address,Doj,Notes,Status,Mobile,Landline,Email,MemberType,Amount,ID from members where MemberID='" + MemberID + "' order by sortid asc";
                ds = dbMgr.ExecuteDataTable(CommandType.Text, strQuery);
            }
            return ds;
        }

        public DataTable GetMemberSearch(string strMemSearch = "")
        {
            DataTable ds = new DataTable();
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "select members.MemberID,MemberName,Address,Doj,Notes,Status,Mobile,Landline,Email,MemberType,Amount,ID from members inner join (select MemberID from members where memberid like '%" + strMemSearch + "%' union select MemberID from members where membername like '%" + strMemSearch + "%' union select MemberID from members where mobile like '%" + strMemSearch + "%' union select MemberID from members where membername like '%" + strMemSearch + "%' union select MemberID from members where landline like '%" + strMemSearch + "%') as temp on members.MemberID =  temp.MemberID order by sortid asc";
                ds = dbMgr.ExecuteDataTable(CommandType.Text, strQuery);
            }
            return ds;
        }

        public DataTable GetMemberDetailsBySortID(int iCount)
        {
            DataTable ds = new DataTable();
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "select MemberID,MemberName,Address,Doj,Notes,Status,Mobile,Landline,Email,MemberType,Amount,ID from members where sortid between " + (iCount * 20 - 20 + 1) + " and " + (iCount * 20) + " order by sortid asc";
                ds = dbMgr.ExecuteDataTable(CommandType.Text, strQuery);
            }
            return ds;
        }

        public DataTable GetMemberNotes(string memberID)
        {
            DataTable ds = new DataTable();
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "select Notes from members where memberid='" + memberID+"'";
                ds = dbMgr.ExecuteDataTable(CommandType.Text, strQuery);
            }
            return ds;
        }



        public int GetMaxSortID()
        {
            int ds = 0;
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "select max(sortid) from members";
                ds = Int32.Parse(dbMgr.ExecuteScalar(CommandType.Text, strQuery).ToString());
            }
            return ds;
        }

        public void InsertMembers(Entities.member tranObj)
        {
            Entities.LibEntities dbCtx = new Entities.LibEntities();
            dbCtx.members.Add(tranObj);
            dbCtx.SaveChanges();
        }

        public void UpdateMembers(Entities.member btMem, string MemberID)
        {
            Entities.LibEntities dbCtx = new Entities.LibEntities();
            Entities.member bkUpdate = dbCtx.members.First(b => b.MemberID == MemberID);
            BaseRepository.CopyPropertyValues(btMem, bkUpdate); 
            dbCtx.SaveChanges();

        }
    }
}
