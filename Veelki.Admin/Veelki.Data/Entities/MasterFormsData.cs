namespace Veelki.Data.Entities
{
    public partial class MasterFormsData
    {
        public int id { set; get; }
        public int form_id { set; get; }
        public string action { set; get; }
        public string action_value { set; get; }
        public int isactive { get; set; }
    }
}
