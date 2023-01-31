namespace UniversityApiBackend.Models
{
    public class UserTokens
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; } = string.Empty;
        public TimeSpan Validaty { get; set; }
        public string RefreshToken { get; set; } 
        public string EmailId { get; set; }
        public DateTime ExpiredTime { get; set; }
        public Guid GuidId { get; internal set; }
    }
}
