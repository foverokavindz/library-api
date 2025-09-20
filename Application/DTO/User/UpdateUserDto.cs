namespace library_api.Application.DTO.User
{

    // in update DTO all fields are optional because user can update any field
    // only the fields that are provided will be updated
    public class UpdateUserDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsLocked { get; set; }

    }
}
