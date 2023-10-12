using ResoAdd.DAL.Models;

namespace ResoAdd.DAL
{
	public class UserTokenDAL : IUserTokenDAL
	{
		public async Task<Guid> Create(int userID)
		{
			Guid tockenid = Guid.NewGuid();

			string sql = @"insert into UserToken (UserTokenID, UserId, Created)
                    values (@tockenid, @userid, NOW())";

			 await DbHelpper.ExecuteScalarAsync(sql, new {userid = userID, tockenid = tockenid });
			return tockenid;
		}


		public async Task<int?> Get(Guid tokenID)
		{
			string sql = @"select UserId from UserToken where UserTokenID = @tockenid";
			return await DbHelpper.ExecuteScalarAsync(sql, new { tockenid = tokenID });
		}


	}
}
