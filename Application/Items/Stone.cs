using System;

namespace Application.Items
{
	public class Stone : Item
	{
		private Stone() : base("Stone", TimeSpan.FromMilliseconds(200))
		{
		}

		public static Stone Self { get; } = new Stone();
	}
}