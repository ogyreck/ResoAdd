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
            using (var connection = new NpgsqlConnection(DbHelpper.ConnString))
            {
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<UserModel>(@"
                        SELECT UserID, Email,Password,Salt, Status 
                        from AppUser 
                        where Email = @email", new { email = email }) ?? new UserModel();
            }
        }
        /// <summary>
        /// Получения юзера по id из бд
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserModel> GetUser(int id)
        {
            using (var connection = new NpgsqlConnection(DbHelpper.ConnString))
            {
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<UserModel>(@"
                        SELECT UserID, Email,Password,Salt, Status 
                        From AppUser 
                        where UserId = @id", new { id = id }) ?? new UserModel();
            }
        }

        /// <summary>
        /// Создание пользователя в бд по переданной модели
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task<int> CreateUser(UserModel userModel)
		{
            using (var connection = new NpgsqlConnection(DbHelpper.ConnString))
            {
                await connection.OpenAsync();

                string sql = @"insert into AppUser(Email,Password,Salt, Status)
                                    values(@Email, @Password, @Salt, @Status) RETURNING userid";


                var res = await connection.QuerySingleAsync<int>(sql, userModel);

                return res;

            }
        }
    }
}
