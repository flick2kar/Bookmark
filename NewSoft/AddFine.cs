using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Entities;
using DALLayer;

namespace NewSoft
{
    public partial class AddFine : Form
    {
        TransRepository transRep = new TransRepository();
        public AddFine()
        {
            InitializeComponent();
        }

        private void btnAddFinePop_Click(object sender, EventArgs e)
        {
            Entities.Fine fine = new Entities.Fine();
            fine.FineAmt = Int32.Parse(txtAddFine.Text);
            fine.MemberID = txtFineMemberID.Text;
            fine.FineDate = DateTime.Parse(dateAddFine.Text);
            fine.CreatedDate = DateTime.Now;
            int retValue=transRep.AddFine(fine);
            if (retValue > 0)
                this.Close();
        }
    }
}
