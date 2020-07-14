using System.Collections.Generic;
using Application.Game.Items;

namespace Application.Game.Storage
{
	public class Reservation
	{
		private readonly StorageHandler _storage;
		private readonly IDictionary<Item, decimal> _technologies;

		public Reservation(StorageHandler storage)
		{
			_storage = storage;
			_technologies = new Dictionary<Item, decimal>();
		}

		/// <summary>
		///     Rolls back a certain transaction
		/// </summary>
		public void RollBack()
		{
			foreach (var keyValuePair in _technologies) _storage.Add(keyValuePair.Key, keyValuePair.Value);
		}

		/// <summary>
		///     Adds a reservation
		/// </summary>
		/// <returns>true if reservation was successful; false if reservation is unsuccessful</returns>
		public bool Add(Item tec, decimal quantity = 1)
		{
			if (_storage.Take(tec, quantity))
			{
				_technologies.Add(tec, quantity);
				return true;
			}

			return false;
		}
	}
}