namespace Application.Game.Items
{
	public class Raft : Item
	{
		public Raft() : base("Raft", 30000, new Requirement(ItemsTree.Wood, 300), new Requirement(ItemsTree.StoneAxe, 1))
		{
		}
	}
}