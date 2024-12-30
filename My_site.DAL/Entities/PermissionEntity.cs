namespace My_site.DAL.Entities
{
    public class PermissionEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<RoleEntity> Roles { get; set; } = [];
    }
}
