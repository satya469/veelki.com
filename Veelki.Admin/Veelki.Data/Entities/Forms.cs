namespace Veelki.Data.Entities
{
    public partial class Forms
    {
        public int id { get; set; }
        public string name { get; set; }
        public int? parentid { get; set; }
        public bool isactive { get; set; }
    }
}
