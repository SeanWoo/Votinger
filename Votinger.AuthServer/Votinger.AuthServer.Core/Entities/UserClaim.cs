namespace Votinger.AuthServer.Core.Entities
{
    public class UserClaim : BaseEntity
    {
        public int? UserId { get; set; }
        public User User { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
