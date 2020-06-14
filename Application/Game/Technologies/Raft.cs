namespace Application.Game.Technologies
{
	public class Raft : Technology
	{
		public Raft() : base("Raft", 30000, new Requirement(TechnologyTree.Wood, 300), new Requirement(TechnologyTree.StoneAxe, 1))
		{
		}
	}
}