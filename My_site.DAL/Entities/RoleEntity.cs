namespace My_site.DAL.Entities
{
    public class RoleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<PermissionEntity> Permissions { get; set; } = [];
        public virtual ICollection<UserEntity> Users { get; set; } = [];
    }
}
