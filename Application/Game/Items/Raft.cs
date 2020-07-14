namespace Application.Game.Items
{
	public class Raft : Item
	{
		public Raft() : base("Raft", 30000, new Requirement(Items.Wood, 300), new Requirement(Items.StoneAxe, 1))
		{
		}
	}
}