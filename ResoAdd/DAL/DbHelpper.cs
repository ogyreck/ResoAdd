using Dapper;
using Npgsql;
using ResoAdd.DAL.Models;

namespace ResoAdd.DAL
{
    public class DbHelpper
    {
        public static string ConnString = "User ID=postgres;Password=password;Host=localhost;Port=5432;Database=test";



        public static async Task<int> ExecuteScalarAsync(string sql, object model)
        {
			using (var connection = new NpgsqlConnection(DbHelpper.ConnString))
			{
				await connection.OpenAsync();

				return await connection.ExecuteAsync(sql, model);
			}
		}
		public static async Task<IEnumerable<T>> QueryAsync<T>(string sql, object model)
		{
			using (var connection = new NpgsqlConnection(DbHelpper.ConnString))
			{
				await connection.OpenAsync();

				return await connection.QueryAsync<T>(sql, model);
			}
		}
    }
}
