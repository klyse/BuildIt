namespace Application.Items
{
	public class Requirement
	{
		public Requirement(Item item, decimal amount)
		{
			Item = item;
			Amount = amount;
		}

		public Item Item { get; }
		public decimal Amount { get; }
	}
}