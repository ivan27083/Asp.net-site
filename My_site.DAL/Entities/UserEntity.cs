namespace My_site.DAL.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public virtual ICollection<RoleEntity> Roles { get; set; }
        private UserEntity(string username, string passwordHash, string email)
        {
            Username = username;
            PasswordHash = passwordHash;
            Email = email;
        }
        public static UserEntity Create(string username, string passwordHash, string email)
        {
            return new UserEntity(username, passwordHash, email);
        }
    }
}
