using System.ComponentModel.DataAnnotations;

namespace KishClinic.Models
{
    public class SessionStatusUpdateDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "Status must be 50 characters or fewer.")]
        public string Status { get; set; } = string.Empty;
    }
} 