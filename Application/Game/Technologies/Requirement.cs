namespace Application.Game.Technologies
{
	public class Requirement
	{
		public Requirement(Technology technology, decimal quantity)
		{
			Technology = technology;
			Quantity = quantity;
		}

		public Technology Technology { get; }
		public decimal Quantity { get; }
	}
}