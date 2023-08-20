using ResoAdd.DAL.Models;

namespace ResoAdd.DAL
{
    public interface IAuthDAL
    {
        Task<UserModel> GetUser(string email);
        Task<UserModel> GetUser(int id);
        Task<int> CreateUser(UserModel userModel);
    }
}
