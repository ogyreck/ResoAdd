namespace ResoAdd.BL.Auth
{
	public interface ICurrentUser
	{
		Task<bool> ISLoggedIn();
	}
}
