namespace CommonReg.Common.Models
{
    public class UserSessionsEntity    {

        public int SessionId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public Guid RefreshToken { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime ExpireRefreshDate { get; set; }
        public string UserAgent { get; set; } = string.Empty;
    }
}