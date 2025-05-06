using System.ComponentModel.DataAnnotations;

namespace Web_API_Test.Dtos.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be at least 5 characters long.")]
        [MaxLength(280, ErrorMessage = "Title must be at most 280 characters long.")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Title must be at least 5 characters long.")]
        [MaxLength(280, ErrorMessage = "Title must be at most 280 characters long.")]
        public string Content { get; set; } = string.Empty;

    }
}
