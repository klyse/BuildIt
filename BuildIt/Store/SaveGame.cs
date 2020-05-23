using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BuildIt.Store
{
	public enum FactoryType
	{
		Coal,
		Iron
	}

	public class Factory
	{
		private readonly object _lock = new object();
		public FactoryType Type { get; set; }

		public int InternalStorage { get; set; }

		public double Throughput => (double) Employees / 1000;

		public int Employees { get; set; }

		public void Make()
		{
			InternalStorage++;
		}

		public void AddEmployee()
		{
			Employees++;
		}

		public int Collect(int maxTransportCapacity)
		{
			lock (_lock)
			{
				var min = Math.Min(maxTransportCapacity, InternalStorage);

				InternalStorage -= min;

				return min;
			}
		}

		public void Work(int ticks)
		{
			var producedStuff = (int) Math.Round(ticks * Throughput, 0);

			lock (_lock)
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