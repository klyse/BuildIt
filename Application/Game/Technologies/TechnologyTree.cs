using System.Collections.Generic;

namespace Application.Game.Technologies
{
	public static class TechnologyTree
	{
		public static Stone Stone { get; } = new Stone();
		public static Wood Wood { get; } = new Wood();
		public static StoneAxe StoneAxe { get; } = new StoneAxe();
		public static Raft Raft { get; } = new Raft();

		public static ICollection<Technology> Technologies { get; } = new HashSet<Technology>
		{
			Wood,
			Stone,
			StoneAxe,
			Raft
		};
	}
}