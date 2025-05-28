using System.ComponentModel.DataAnnotations;

namespace KishClinic.Models
{
    public class UserProfileDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Email обов'язковий")]
        [EmailAddress(ErrorMessage = "Некоректний формат email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ім'я обов'язкове")]
        [StringLength(50, ErrorMessage = "Ім'я не може бути довшим за 50 символів")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Прізвище обов'язкове")]
        [StringLength(50, ErrorMessage = "Прізвище не може бути довшим за 50 символів")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Дата народження обов'язкова")]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Телефон обов'язковий")]
        [RegularExpression(@"^\+380\d{9}$", ErrorMessage = "Телефон повинен бути у форматі +380XXXXXXXXX")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Адреса обов'язкова")]
        [StringLength(200, ErrorMessage = "Адреса не може бути довшою за 200 символів")]
        public string Address { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Нотатки не можуть бути довшими за 500 символів")]
        public string Notes { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
}
