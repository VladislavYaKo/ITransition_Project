using System.ComponentModel.DataAnnotations;

namespace ITransitionProject.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordRepeat { get; set; }
    }
}
