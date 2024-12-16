using System.ComponentModel.DataAnnotations;

namespace BestReads.InputModels.Auth
{
    public class RegisterInputModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "The username should be between {0} and {1} characters long.", MinimumLength=5)]
        [Display(Name = "Потребителско име")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Имейл")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength=6)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потвърди паролата")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
