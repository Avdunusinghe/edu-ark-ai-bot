namespace EduArk.Domain.Entities.Master
{
    public class MasterUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }   
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string PasswordHash { get; set; }
        public string? ProfilePicture { get; set; }
        public bool IsActive { get; set; }         

    }
}
