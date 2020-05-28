using System;
using BuildIt.Store.Save;

namespace BuildIt.Application
{
	public class Factory
	{
		private readonly object _internalStorageLock = new object();
		public FactoryType Type { get; set; }
		public FactoryStatus Status { get; set; }

		public int Priority { get; set; }
		public int InternalStorage { get; private set; }

		public double Throughput => Status == FactoryStatus.Offline ? 0 : (double) EmployeeCount / 1000;

		public int EmployeeCount { get; private set; }

		public static Factory FromSave(SaveFactory factory)
		{
			return new Factory
			{
				Priority = factory.Priority,
				Status = (FactoryStatus) factory.Status,
				Type = (FactoryType) factory.Type,
				EmployeeCount = factory.EmployeeCount,
				InternalStorage = factory.InternalStorage
			};
		}

		public static SaveFactory ToSave(Factory factory)
		{
			return new SaveFactory
			{
				Priority = factory.Priority,
				Status = (int) factory.Status,
				Type = (int) factory.Type,
				EmployeeCount = factory.EmployeeCount,
				InternalStorage = factory.InternalStorage
			};
		}

		public void Make()
		{
			lock (_internalStorageLock)
			{
				InternalStorage++;
			}
		}

		public void HireEmployee()
		{
			EmployeeCount++;
		}

		public void DismissEmployee()
		{
			if (EmployeeCount > 0)
				EmployeeCount--;
		}

		public void ResetStorage()
		{
			lock (_internalStorageLock)
			{
				InternalStorage = 0;
			}
		}

		public int Collect(int maxTransportCapacity)
		{
			if (maxTransportCapacity <= 0)
				throw new ArgumentOutOfRangeException(nameof(maxTransportCapacity), "max transport capacity cannot be smaller or equal 0");

			lock (_internalStorageLock)
			{
				var min = Math.Min(maxTransportCapacity, InternalStorage);

				InternalStorage -= min;

				return min;
			}
		}

		public void Work(int ticks)
		{
			if (ticks <= 0)
				return;

			var producedStuff = (int) Math.Round(ticks * Throughput, 0);

			lock (_internalStorageLock)
			{
				InternalStorage += producedStuff;
				Console.WriteLine($"Prod: {producedStuff}");
			}
		}
	}
}