using System.Collections.Generic;
using Veelki.Data.Entities;

namespace Veelki.Model.Model
{
    public class UserMyProfile
    {
    }

    public class UserRollingCommision
    {
    }

    public class UserAccountStatement
    {
    }

    public class UserBetsHistory : Pagination
    {
        public int UserId { get; set; }
        public int SportId { get; set; }
        public int IsSettlement { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool TodayHistory { get; set; }
        public string ColumnName { get; set; }
        public int OrderByColumn { get; set; }
    }

    public class UserBetPagination : CommonPagination
    {
        public List<Bets> betList { get; set; }
    }

    public class UserProfitAndLoss
    {
    }
}
