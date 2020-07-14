namespace Application.Game.Items
{
	public class Requirement
	{
		public Requirement(Item item, decimal quantity)
		{
			Item = item;
			Quantity = quantity;
		}

		public Item Item { get; }
		public decimal Quantity { get; }
	}
}