using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BuildIt.Store
{
	public class Factory
	{
		public enum FactoryStatus
		{
			Offline,
			Online
		}

		public enum FactoryType
		{
			Coal,
			Iron
		}

		private readonly object _employeesLock = new object();

		private readonly object _internalStorageLock = new object();
		public FactoryType Type { get; set; }
		public FactoryStatus Status { get; set; }

		public int InternalStorage { get; set; }

		public double Throughput => Status == FactoryStatus.Offline ? 0 : (double) EmployeeCount / 1000;

		public int EmployeeCount { get; set; }

		public void Make()
		{
			InternalStorage++;
		}

		public void HireEmployee()
		{
			lock (_employeesLock)
			{
				EmployeeCount++;
			}
		}

		public void DismissEmployee()
		{
			lock (_employeesLock)
			{
				if (EmployeeCount > 0)
					EmployeeCount--;
			}
		}

		public int Collect(int maxTransportCapacity)
		{
			lock (_internalStorageLock)
			{
				var min = Math.Min(maxTransportCapacity, InternalStorage);

				InternalStorage -= min;

				return min;
			}
		}

		public void Work(int ticks)
		{
			var producedStuff = (int) Math.Round(ticks * Throughput, 0);

			lock (_internalStorageLock)
			{
				InternalStorage += producedStuff;
			}
		}
	}

	public class SaveGame
	{
		private DateTime? _lastTick;
		[JsonIgnore] public bool Loaded { get; set; }
		public ICollection<Factory> Factories { get; set; } = new List<Factory>();

		public DateTime LastTick
		{
			get => _lastTick ?? DateTime.UtcNow;
			set => _lastTick = value;
		}

		public int Coal { get; set; }

		public void AddFactory(Factory f)
		{
			Factories.Add(f);
		}
	}
}