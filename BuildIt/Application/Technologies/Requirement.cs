namespace BuildIt.Application.Technologies
{
	public class Requirement
	{
		public Requirement(Technology technology, decimal quantity)
		{
			Technology = technology;
			Quantity = quantity;
		}

		public Technology Technology { get; set; }
		public decimal Quantity { get; }
	}
}