using System;

namespace Application.Items
{
	public class SteelAxe : Item
	{
		private SteelAxe() : base("Steel Axe", TimeSpan.FromSeconds(1), new Requirement(Iron.Self, 2), new Requirement(Wood.Self, 2))
		{
		}

		public static SteelAxe Self { get; } = new SteelAxe();
	}
}