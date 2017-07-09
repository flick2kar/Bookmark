using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Entities;


namespace DALLayer
{
    public class WishListRepository
    {
        public DataTable GetWishList()
        {
            DataTable ds = new DataTable();
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "SELECT WishID,MemberID,BookID,AuthorName,BookName,Status,GrantDate FROM WishList order by WishID";
                ds = dbMgr.ExecuteDataTable(CommandType.Text, strQuery);
            }
            return ds;
        }

        public DataTable GetWishListID(int wishID)
        {
            DataTable ds = new DataTable();
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "SELECT WishID,MemberID,BookID,AuthorName,BookName,Status,GrantDate FROM WishList where wishID=" + wishID + " order by WishID";
                ds = dbMgr.ExecuteDataTable(CommandType.Text, strQuery);
            }
            return ds;
        }

        public DataTable SearchWishList(string strSearch="")
        {
            DataTable ds = new DataTable();
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "select WishList.BookID,BookName,Authorname,Status,GrantDate,WishList.WishID from (WishList inner join (select WishID as WishID from WishList where MemberID like '%" + strSearch + "%' and status=0 union select WishID as WishID from WishList where BookID like '%" + strSearch + "%' union select WishID as WishID from WishList where BookName like '%" + strSearch + "%' union select WishID as WishID from WishList where AuthorName like '%" + strSearch + "%') as temp on WishList.WishID =  temp.WishID  and WishList.Status=0) order by WishList.WishID";
                ds = dbMgr.ExecuteDataTable(CommandType.Text, strQuery);
            }
            return ds;
        }

        public DataTable GetGrantList(string strSearch = "")
        {
            DataTable ds = new DataTable();
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "select WishList.BookID,BookName,Authorname,Status,GrantDate,WishList.WishID from (WishList inner join (select WishID as WishID from WishList where MemberID like '%" + strSearch + "%' and status=0 union select WishID as WishID from WishList where BookID like '%" + strSearch + "%' union select WishID as WishID from WishList where BookName like '%" + strSearch + "%' union select WishID as WishID from WishList where AuthorName like '%" + strSearch + "%') as temp on WishList.WishID =  temp.WishID  and WishList.Status=1) order by WishList.WishID";
                ds = dbMgr.ExecuteDataTable(CommandType.Text, strQuery);
            }
            return ds;
        }

        public void InsertWishList(Entities.WishList wishObj)
        {
            Entities.LibEntities dbCtx = new Entities.LibEntities();
            dbCtx.WishLists.Add(wishObj);
            dbCtx.SaveChanges();
        }

        public void UpdateWishList(Entities.WishList wishObj, int WishID)
        {            
            Entities.LibEntities dbCtx = new Entities.LibEntities();
            Entities.WishList bkUpdate = dbCtx.WishLists.First(b => b.WishID == WishID);
            BaseRepository.CopyPropertyValues(wishObj, bkUpdate);
            bkUpdate.WishID = WishID;
            dbCtx.SaveChanges();
        }
    }
}
