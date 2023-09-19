 using ResoAdd.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace ResoAdd.BL.Auth
{
    public interface IAuth
    {
        Task<int> CreateUser(UserModel userModel);

        Task<int> Authenticate(string email, string password, bool rememberMe);
        Task ValidateEmail(string email);
        Task Register(UserModel userModel);

	}
}
