using System;
using ResoAdd.DAL.Models;
using Dapper;
using Npgsql;
using Microsoft.AspNetCore.Hosting.Server;

namespace ResoAdd.DAL
{
    /// <summary>
    ///  Класс для реализации слоенной архитектуры. Слой базы данных
    /// </summary>
    public class AuthDAL: IAuthDAL
    {
        /// <summary>
        /// Получения юзера по почте из бд
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<UserModel> GetUser(string email)
        {
			var result = await DbHelpper.QueryAsync<UserModel>(@"
                        SELECT UserID, Email,Password,Salt, Status 
                        From AppUser 
                         where Email = @email", new { email = email });
			return result.FirstOrDefault() ?? new UserModel();
        }
        /// <summary>
        /// Получения юзера по id из бд
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserModel> GetUser(int id)
        {
            var result = await DbHelpper.QueryAsync<UserModel>(@"
                        SELECT UserID, Email,Password,Salt, Status 
                        From AppUser 
                        where UserId = @id", new { id = id });
            return result.FirstOrDefault() ?? new UserModel();
            
        }

        /// <summary>
        /// Создание пользователя в бд по переданной модели
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task<int> CreateUser(UserModel userModel)
		{
			string sql = @"insert into AppUser(Email,Password,Salt, Status)
                                    values(@Email, @Password, @Salt, @Status) RETURNING userid";
            var result = await DbHelpper.QueryAsync<int>(sql, userModel);  
            return result.First();
           
        }
    }
}
