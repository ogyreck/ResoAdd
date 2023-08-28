using System.ComponentModel.DataAnnotations;

namespace ResoAdd.ViewModels
{
   
    public class RegisterViewModel: IValidatableObject
    {
        [Required(ErrorMessage ="Требуется вести почту")]
        [EmailAddress(ErrorMessage ="Некоректный формат")]
        public string? Email { get; set; }

        [Required(ErrorMessage ="Требуется ввести пароль")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{10,}$", ErrorMessage="Пароль слишком простой")]
        public string? Password { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if(Password == "qwer1234")
            {
                yield return new ValidationResult("Пароль слишком простой, используйте другой пароль", new[] {"Password"});
            }
		}
	}
}
