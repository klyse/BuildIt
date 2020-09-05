using System;

namespace Application.Items
{
	public class Wood : Item
	{
		private Wood() : base("Wood", TimeSpan.FromMilliseconds(100))
		{
		}

		public static Wood Self { get; } = new Wood();
	}
}