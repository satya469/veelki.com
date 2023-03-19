using System;
using System.Collections.Generic;
using System.Text;

namespace Veelki.Data.Entities
{
    public class MatchSetting
    {
        public int Id { get; set; }
        public string Match_Name { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> Action { get; set; }
    }
}
