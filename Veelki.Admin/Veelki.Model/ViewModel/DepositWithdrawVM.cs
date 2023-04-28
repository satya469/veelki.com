namespace Veelki.Model.ViewModel
{
    public class DepositWithdrawVM
    {
        public int UserId { get; set; }
        public bool IsDeposit { get; set; }
        public long Balance { get; set; }
        public string Remark { get; set; }
        public string Password { get; set; }
    }
}
