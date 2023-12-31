namespace EduArk.Application.DTOs.UserDTOs
{
    public class UserDetailsDTO
    {
        public UserDetailsDTO()
        {
            Roles = new List<int>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string  UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Password { get; set; }
        public List<int> Roles { get; set; }

        public string? CreatedUser { get; set; }
        public string? CreatedDate { get; set; }
        public string? UpdatedUser { get; set; }
        public string? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public string? RoleName { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
