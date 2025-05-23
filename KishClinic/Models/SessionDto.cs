using System.ComponentModel.DataAnnotations;

namespace KishClinic.Models
{
    public class SessionDto
    {
        [Required]
        public int ClientID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime SessionDate { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan FinishTime { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Status must be 50 characters or fewer.")]
        public string Status { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string Notes { get; set; } = string.Empty;

        [Required]
        public int SessionTypeID { get; set; }
    }
}
