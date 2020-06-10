using System.Collections.Generic;

namespace BuildIt.Application.Technologies
{
	public class TechnologyTree
	{
		public static Wood Wood { get; } = new Wood();
		public static Stone Stone { get; } = new Stone();
		public static StoneAxe StoneAxe { get; } = new StoneAxe();

		public TechnologyTree()
		{
			Technologies = new HashSet<Technology>
			{
				Wood,
				Stone,
				StoneAxe
			};
		}

		public ICollection<Technology> Technologies { get; }
	}
}