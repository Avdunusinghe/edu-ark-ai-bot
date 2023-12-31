namespace EduArk.Domain.Entities.Tenant
{
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdatedByUserId { get; set; }
        public bool IsActive { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
        public virtual User CreatedByUser { get; set; }
        public virtual User UpdatedByUser { get; set; }
    }
}
