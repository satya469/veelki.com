using System;
using System.Collections.Generic;
using System.Text;

namespace RB444.Model.Model
{
    public class FormDetails
    {
        public string formName { get; set; }
        public string tableName { get; set; }
    }

    public class ImportanceLevel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string colour_code { get; set; }
    }
}
