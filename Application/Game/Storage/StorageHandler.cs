using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Application.Game.Technologies;
using Application.Store.Save;

namespace Application.Game.Storage
{
	public class StorageHandler
	{
		private ConcurrentDictionary<Technology, decimal> _storage;

		public StorageHandler()
		{
			_storage = new ConcurrentDictionary<Technology, decimal>();
		}

		public IDictionary<Technology, decimal> GetDictionary()
		{
			return _storage;
		}

		public void Add(Technology tec, decimal amount = 1)
		{
			if (amount <= 0)
				throw new Exception("Amount cannot be <= 0");
			_storage.AddOrUpdate(tec, t => amount, (t, a) => a + amount);
		}

		public bool Take(Technology tec, decimal amount = 1)
		{
			if (amount <= 0)
				throw new Exception("Amount cannot be <= 0");

			if (!_storage.ContainsKey(tec))
				return false;

			var neg = false;

			_storage.AddOrUpdate(tec, _ => throw new Exception("Cannot add on take"), (t, a) =>
			{
				if (a - amount >= 0) return a - amount;

				neg = true;
				return a;
			});

			return !neg;
		}

		public bool CanMake(Technology tec)
		{
			foreach (var tecBuildRequirement in tec.BuildRequirements)
			{
				if (!_storage.ContainsKey(tecBuildRequirement.Technology))
					return false;

				if (_storage[tecBuildRequirement.Technology] < tecBuildRequirement.Quantity)
					return false;
			}

			return true;
		}

		public bool Make(Technology tec, out Reservation res)
		{
			var reservation = new Reservation(this);
			res = reservation;
			foreach (var req in tec.BuildRequirements)
				if (!reservation.Add(req.Technology, req.Quantity))
				{
					// add reservation back to storage
					reservation.RollBack();
					return false;
				}

			return true;
		}

		public static StorageHandler FromSave(SaveStorageHandler storageHandler)
		{
			var sh = new StorageHandler();

			sh._storage = new ConcurrentDictionary<Technology, decimal>(storageHandler.Storage.ToDictionary(c => TechnologyTree.Technologies.First(r => r.Identifier == c.Key), c => c.Value));

			return sh;
		}

		public static SaveStorageHandler ToSave(StorageHandler storageHandler)
		{
			return new SaveStorageHandler
			{
				Storage = storageHandler._storage.ToDictionary(c => c.Key.Identifier, c => c.Value)
			};
		}
	}
}