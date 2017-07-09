using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSoft
{
       
    public class bookTrans
    {
        public String memberId,bookId,dateFrom,dateTo,dateReturn,strFine,strTransRate,strLibBal,strMemBal,strRenewal;
        

        public bookTrans()
        {
            memberId = "";
            bookId = "";
            dateFrom = "";
            dateTo = "";
            dateReturn="";
            strFine=strTransRate=strLibBal=strMemBal="";            
        }
        
    }
    class memDetails
    {        
        public String memberId, memberName, memAddress, dateDoj, memNotes;
        public int memStatus;

        public memDetails()
        {
            memberId="";memberName="";memAddress="";dateDoj=""; memNotes="";
            memStatus=0;
        }
    }
}
