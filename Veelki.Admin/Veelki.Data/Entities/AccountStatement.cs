using System;

namespace Veelki.Data.Entities
{
    public partial class AccountStatement
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public double Deposit { get; set; }
        public double Withdraw { get; set; }
        public double Balance { get; set; }
        public string Remark { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public int ToUserRoleId { get; set; }
        public bool IsAccountStatement { get; set; }
    }
}
