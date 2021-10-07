namespace Votinger.AuthServer.Core.Entities
{
    public class User : BaseEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public int? RefreshTokenId { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
