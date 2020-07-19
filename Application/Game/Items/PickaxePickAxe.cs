namespace Application.Game.Items
{
	public class PickaxePickAxe : Item
	{
		public PickaxePickAxe() : base("Stone Pickaxe",
			createTick: 15000,
			researchTick: 0,
			durabilityCount: 100,
			new Requirement(ItemsTree.Wood, 3),
			new Requirement(ItemsTree.Stone, 5))
		{
		}
	}
}