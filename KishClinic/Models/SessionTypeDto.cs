using System.ComponentModel.DataAnnotations;

namespace KishClinic.Models
{
    public class SessionTypeDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Description must be 100 characters or fewer.")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Fee must be a positive number.")]
        public decimal DefaultFee { get; set; }

        [Required]
        [Range(1, 1440, ErrorMessage = "Duration must be between 1 and 1440 minutes.")]
        public int DefaultDuration { get; set; }
    }
}
