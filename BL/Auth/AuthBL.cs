using ResoAdd.DAL.Models;
using ResoAdd.DAL;
using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;

namespace ResoAdd.BL.Auth
{
    /// <summary>
    /// Реализация BL уровня
    /// </summary>
    public class AuthBL : IAuthBL
    {
        /// <summary>
        /// Зависимость от authDAL
        /// </summary>
        private readonly IAuthDAL _authDAL;
        private readonly IEncrypt _encrypt;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthBL(IAuthDAL authDAL, IEncrypt encrypt, IHttpContextAccessor httpContextAccessor)
        {
            _authDAL = authDAL;
			_encrypt = encrypt;
			_httpContextAccessor = httpContextAccessor;

		}

		public async Task<int> Authenticate(string email, string password, bool rememberMe)
		{
			UserModel user = await _authDAL.GetUser(email);
            string encriptPass = _encrypt.HashPassword(password, user.Salt);

            if(user.Password == encriptPass)
            {
                Login(user.UserId?? 0);
                return user.UserId ?? 0;
            }
            return 0;
		}

		/// <summary>
		///  Создание юзера на уровне BL
		/// </summary>
		/// <param name="userModel"></param>
		/// <returns></returns>
		public async Task<int> CreateUser(UserModel userModel)
        {
            userModel.Salt = Guid.NewGuid().ToString();
            userModel.Password = _encrypt.HashPassword(userModel.Password, userModel.Salt);
            int id =  await _authDAL.CreateUser(userModel);
            Login(id);
            return id;
        }
        public void Login(int id)
        {
            _httpContextAccessor.HttpContext?.Session.SetInt32(AuthConstant.AUTH_SESSION_PARAM_KEY, id);

		}

        public async Task<ValidationResult?> ValidateEmail(string email)
        {
            var user = await _authDAL.GetUser(email);

            if(user.UserId != null)
            {
                return new ValidationResult("Email уже существует");
            }
            return null;
        }
    }
}
