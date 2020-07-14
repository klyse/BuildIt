using System.Collections.Generic;

namespace Application.Game.Items
{
	public static class Items
	{
		public static Stone Stone { get; } = new Stone();
		public static Wood Wood { get; } = new Wood();
		public static StoneAxe StoneAxe { get; } = new StoneAxe();
		public static Raft Raft { get; } = new Raft();

		public static ICollection<Item> Technologies { get; } = new HashSet<Item>
		{
			Wood,
			Stone,
			StoneAxe,
			Raft
		};
	}
}