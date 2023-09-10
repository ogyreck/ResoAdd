 using ResoAdd.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace ResoAdd.BL.Auth
{
    public interface IAuthBL
    {
        Task<int> CreateUser(UserModel userModel);

        Task<int> Authenticate(string email, string password, bool rememberMe);
        Task<ValidationResult?> ValidateEmail(string email);

	}
}
