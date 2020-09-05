using System;

namespace Application.Items
{
	public class Iron : Item
	{
		private Iron() : base("Iron", TimeSpan.FromMilliseconds(200))
		{
		}

		public static Iron Self { get; } = new Iron();
	}
}