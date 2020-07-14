namespace Application.Game.Items
{
	public class StoneAxe : Item
	{
		public StoneAxe() : base("Stone Axe",
			15000,
			new Requirement(Items.Wood, 3),
			new Requirement(Items.Stone, 5))
		{
		}
	}
}