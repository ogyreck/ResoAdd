using System;
using ResoAdd.DAL.Models;
using Dapper;
using Npgsql;

namespace ResoAdd.DAL.Auth
{
    public class AuthDAL: IAuthDAL
    {
        

        public async Task<UserModel> GetUser(string email)
        {
            using (var connection = new NpgsqlConnection(DbHelpper.ConnString))
            {
                connection.Open();

                return await connection.QueryFirstOrDefaultAsync<UserModel>(@"
                        SELECT UserID, Email,Password,Salt, Status 
                        FORM AppUser 
                        where Email = @email", new { email = email }) ?? new UserModel();
            }
        }

        public async Task<UserModel> GetUser(int id)
        {
            using (var connection = new NpgsqlConnection(DbHelpper.ConnString))
            {
                connection.Open();

                return await connection.QueryFirstOrDefaultAsync<UserModel>(@"
                        SELECT UserID, Email,Password,Salt, Status 
                        FORM AppUser 
                        where UserId = @id", new { id = id }) ?? new UserModel();
            }
        }

        public async Task<int> CreateUser(UserModel userModel)
        {
            using (var connection = new NpgsqlConnection(DbHelpper.ConnString))
            {
                connection.Open();

                string sql = @"insert into AppUser(Email,Password,Salt, Status)
                                    values(@Email, @Password, @Salt, @Status)";

                return await connection.ExecuteAsync(sql, userModel);

            }
        }
    }
}
