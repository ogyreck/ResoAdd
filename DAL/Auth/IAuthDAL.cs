using ResoAdd.DAL.Models;

namespace ResoAdd.DAL.Auth
{
    public interface IAuthDAL
    {
        Task<UserModel> GetUser(string email);
        Task<UserModel> GetUser(int id);
        Task<int> CreateUser(UserModel userModel);
    }
}
