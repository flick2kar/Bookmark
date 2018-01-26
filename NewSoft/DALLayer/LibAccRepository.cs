using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Entities;

namespace DALLayer
{
    public class LibAccRepository
    {
        public DataTable GetAccountDetails(string startDate, string endDate)
        {
            DataTable ds = new DataTable();
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                //String strQuery = "SELECT sum(lendrate) lendrate,sum(fine) fine,sum(LibBal) LibBal,sum(MemBal) MemBal FROM booktrans bt where lenddate between '" + startDate + "' and '" + endDate + "'";
                dbMgr.AddParameters("@startDate", startDate);
                dbMgr.AddParameters("@endDate", endDate);
                

                //dbMgr.ExecuteScalar
                ds = dbMgr.ExecuteDataTable(CommandType.StoredProcedure, "GetAccountDetails");
                
            }
            return ds;
        }
    }
}
