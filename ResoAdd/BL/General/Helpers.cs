using System.Transactions;

namespace ResoAdd.BL.General
{
	public static class Helpers
	{
		public static TransactionScope CreateTransactionScope(int seconds = 6000)
		{
			return new TransactionScope(
				TransactionScopeOption.Required,
				new TimeSpan(0, 0, seconds),
				TransactionScopeAsyncFlowOption.Enabled);
		}
		public static int? StringToIntDef(string str, int? def)
		{
			int value;
			if (int.TryParse(str, out value))
				return value;
			return def;
		}

		public static Guid? StringToGuidDef(string str)
		{
			Guid value;
			if (Guid.TryParse(str, out value))
				return value;
			return null;
		}
	}
}
