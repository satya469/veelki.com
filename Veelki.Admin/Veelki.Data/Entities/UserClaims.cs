namespace Veelki.Data.Entities
{
    public partial class UserClaims
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public virtual Workspace WorkSpace { get; set; }
    }
}
