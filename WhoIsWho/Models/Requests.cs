using System.ComponentModel.DataAnnotations;

namespace WhoIsWho.Models
{
    public class PostBookingModel
    {
        [Required]
        public DateTime Date { get; set; }
        [MaxLength(256, ErrorMessage = "Длиной дo {0}")]
        public string Text { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        public string Email { get; set; }

        [Compare("PasswordConfirmation", ErrorMessage ="miss match")]
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
