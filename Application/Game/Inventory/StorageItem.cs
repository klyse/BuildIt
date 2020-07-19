using Application.Game.Items;

namespace Application.Game.Inventory
{
	public class StorageItem
	{
		public StorageItem(Item item, decimal count)
		{
			Item = item;
			Count = count;
		}

		public Item Item { get; }
		public decimal Count { get; }
	}
}