using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace Utils
{
    public class CommonMethods
    {
        public DataTable InitializeList(DataTable ds)
        {
            DataRow rw = ds.NewRow();
            rw[0] = 0;
            rw[1] = "--Select--";

            ds.Rows.InsertAt(rw, 0);

            return ds;
        }
        
    }
}
