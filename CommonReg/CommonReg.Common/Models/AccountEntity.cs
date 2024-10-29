namespace CommonReg.Common.Models
{
    public class AccountEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public Guid PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public Guid ActivationCode { get; set; }
        public byte[] Avatar { get; set; }
        public List<RoleEntity> Roles { get; set; } = new List<RoleEntity>();
    }
}
