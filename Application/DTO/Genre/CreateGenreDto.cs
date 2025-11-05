using System.ComponentModel.DataAnnotations;

namespace library_api.Application.DTO.Genre
{
    public class CreateGenreDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters")]
        public string Name { get; set; } = string.Empty;
    }
}
