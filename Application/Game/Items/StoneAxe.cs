namespace Application.Game.Items
{
	public class StoneAxe : Item
	{
		public StoneAxe() : base("Stone Axe",
			15000,
			new Requirement(ItemsTree.Wood, 3),
			new Requirement(ItemsTree.Stone, 5))
		{
		}
	}
}