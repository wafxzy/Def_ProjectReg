namespace CommonReg.Common.Models
{
    public class UserForgotPasswordEntity
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid Code { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
