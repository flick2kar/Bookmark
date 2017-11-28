using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Entities;

namespace DALLayer
{
    
    public class BookRepository
    {
        public DataTable Book_Search(string strBookSearch)
        {
            DataTable ds=new DataTable();
            using(DBManager dbMgr=new DBManager(DataProvider.SqlClient,DBConnection.Connstring))
            {
                String strQuery = "select books.BookID,BookName,authors.Authorname,BookPrice,OrgPrice,Soldprice,category.CategoryName,SubCategory.Name from (((books inner join (select BookID as bookid from books where BookID like '%" + strBookSearch + "%' union select BookID as bookid from books where BookName like '%" + strBookSearch + "%' union select BookID as bookid from books inner join authors on authors.authorid=books.authorid where authors.authorname like '%" + strBookSearch + "%') as temp on books.bookid =  temp.bookid) inner join category on category.categoryid=books.categoryid) left join authors on authors.authorid=books.authorid) Left join Subcategory on Subcategory.ID=books.SubCategoryID order by books.BookID";
                ds = dbMgr.ExecuteDataTable(CommandType.Text, strQuery);
            }
            return ds;
        }

        public DataTable GetBookDetails(string bookAlpha="a")
        {
            DataSet ds = new DataSet();
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "select BookID,BookName,Authorname,OrgPrice,Soldprice,CategoryName,Name from ((books left join authors on books.authorid=authors.authorid) inner join category on category.CategoryID=books.CategoryID) Left join Subcategory on Subcategory.ID=books.SubCategoryID where bookid like '"+bookAlpha+"%' order by bookid;";
                ds = dbMgr.ExecuteDataSet(CommandType.Text, strQuery);
            }
            return ds.Tables[0];
        }

        public DataTable GetBookIDDetails(string bookId)
        {
            DataSet ds = new DataSet();
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "select books.BookID,BookName,authors.Authorname,BookPrice,OrgPrice,Soldprice,category.CategoryName,ISBN,SubCategory.Name,Series.SeriesName,books.CreatedDate from (((books inner join category on category.categoryid=books.categoryid) left join authors on authors.authorid=books.authorid) Left join Subcategory on Subcategory.ID=books.SubCategoryID)Left join Series on Series.SeriesID=books.SeriesID where BookID='" + bookId + "'";
                ds = dbMgr.ExecuteDataSet(CommandType.Text, strQuery);
            }
            return ds.Tables[0];
        }

        public DataTable GetAuthorDetails()
        {
            DataTable ds = new DataTable();
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "select AuthorId,Authorname from authors order by Authorname";
                ds = dbMgr.ExecuteDataTable(CommandType.Text, strQuery);
            }
            return ds;
        }

        public DataTable GetAuthorDetails(string strAuthor)
        {
            DataTable ds = new DataTable();
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "select AuthorId,Authorname from authors where authorname='" + strAuthor + "'";
                ds = dbMgr.ExecuteDataTable(CommandType.Text, strQuery);
            }
            return ds;
        }

        public int GetMaxID(string strTableName,string strColumn)
        {
            int maxID = 0;
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "select MAX("+strColumn+") from "+strTableName;
                maxID = Int32.Parse(dbMgr.ExecuteScalar(CommandType.Text, strQuery).ToString());
            }
            return maxID;
        }

        public DataTable GetSeriesDetails()
        {
            DataTable ds = new DataTable();
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "select SeriesId,Seriesname from Series order by Seriesname";
                ds = dbMgr.ExecuteDataTable(CommandType.Text, strQuery);
            }
            return ds;
        }

        public DataTable GetSeriesDetails(string strSeries)
        {
            DataTable ds = new DataTable();
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "select SeriesId,Seriesname from Series where SeriesName='"+strSeries+"'";
                ds = dbMgr.ExecuteDataTable(CommandType.Text, strQuery);
            }
            return ds;
        }
        public DataTable GetCategoryDetails()
        {
            DataTable ds = new DataTable();
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "select CategoryID,CategoryName from Category order by CategoryName";
                ds = dbMgr.ExecuteDataTable(CommandType.Text, strQuery);
            }
            return ds;
        }

        public DataTable GetSubCategoryDetails()
        {
            DataTable ds = new DataTable();
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "select ID,Name from SubCategory order by Name";
                ds = dbMgr.ExecuteDataTable(CommandType.Text, strQuery);
            }
            return ds;
        }

        public DataTable GetSubCategoryDetails(string strSubCat)
        {
            DataTable ds = new DataTable();
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "select ID,Name from SubCategory where Name='" + strSubCat + "'";
                ds = dbMgr.ExecuteDataTable(CommandType.Text, strQuery);
            }
            return ds;
        }

        public String GetSalePrice(string bookID)
        {
            DataTable ds = new DataTable();
            String saleprice = "";
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {                
                dbMgr.AddParameters("@bookid", bookID);
                //dbMgr.ExecuteScalar
                saleprice = dbMgr.ExecuteScalar(CommandType.StoredProcedure, "GetSalesPrice").ToString();
            }
            return saleprice;
        }

        public int GetBookstatus(string bookID)
        {
            int status = 1;
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "select status from books where bookid='" + bookID +"'";
                status = Int32.Parse(dbMgr.ExecuteScalar(CommandType.Text, strQuery).ToString());
            }
            return status;
        }

        public void InsertBooks(Entities.book bkObj)
        {
            //NewsoftEntities dbCtx = new NewsoftEntities();         
            Entities.LibEntities dbCtx = new Entities.LibEntities();
            dbCtx.books.Add(bkObj);
            dbCtx.SaveChanges();            
        }

        public void UpdateBooks(Entities.book bkObj,string bookID)
        {            
            Entities.LibEntities dbCtx = new Entities.LibEntities();            
            Entities.book bkUpdate = dbCtx.books.First(b => b.BookID==bookID);
            BaseRepository.CopyPropertyValues(bkObj, bkUpdate);          
            
            dbCtx.SaveChanges();

        }

        public void DeleteBooks(string bookID)
        {
            Entities.LibEntities dbCtx = new Entities.LibEntities();
            Entities.book bkUpdate = dbCtx.books.First(b => b.BookID == bookID);
            dbCtx.books.Remove(bkUpdate);

            dbCtx.SaveChanges();

        }

        public void InsertAuthors(Entities.author objAuth)
        {
            //NewsoftEntities dbCtx = new NewsoftEntities();         
            Entities.LibEntities dbCtx = new Entities.LibEntities();
            dbCtx.authors.Add(objAuth);
            dbCtx.SaveChanges();
        }

        public void DeleteAuthors(int authorID)
        {
            Entities.LibEntities dbCtx = new Entities.LibEntities();
            Entities.author bkUpdate = dbCtx.authors.First(b => b.AuthorID == authorID);
            dbCtx.authors.Remove(bkUpdate);
            dbCtx.SaveChanges();

        }

        public void InsertSubCat(Entities.SubCategory objSubCat)
        {
            //NewsoftEntities dbCtx = new NewsoftEntities();         
            Entities.LibEntities dbCtx = new Entities.LibEntities();
            dbCtx.SubCategories.Add(objSubCat);
            dbCtx.SaveChanges();
        }

        public void DeleteSubCat(int subcatID)
        {
            Entities.LibEntities dbCtx = new Entities.LibEntities();
            Entities.SubCategory bkUpdate = dbCtx.SubCategories.First(b => b.Id == subcatID);
            dbCtx.SubCategories.Remove(bkUpdate);
            dbCtx.SaveChanges();

        }

        public void InsertSeries(Entities.Series objSeries)
        {                 
            Entities.LibEntities dbCtx = new Entities.LibEntities();
            dbCtx.Series.Add(objSeries);
            dbCtx.SaveChanges();
        }

        public void DeleteSeries(int seriesID)
        {
            Entities.LibEntities dbCtx = new Entities.LibEntities();
            Entities.Series bkUpdate = dbCtx.Series.First(b => b.SeriesID == seriesID);
            dbCtx.Series.Remove(bkUpdate);
            dbCtx.SaveChanges();
        }
    }
}
