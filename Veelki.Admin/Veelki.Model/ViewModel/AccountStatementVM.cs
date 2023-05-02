using System;

namespace Veelki.Model.ViewModel
{
    public class AccountStatementVM
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public double Deposit { get; set; }
        public double Withdraw { get; set; }
        public double Balance { get; set; }
        public string Remark { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
    }
}
