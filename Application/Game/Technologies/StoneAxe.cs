namespace Application.Game.Technologies
{
	public class StoneAxe : Technology
	{
		public StoneAxe() : base("Stone Axe",
			15000,
			new Requirement(TechnologyTree.Wood, 3),
			new Requirement(TechnologyTree.Stone, 5))
		{
		}
	}
}