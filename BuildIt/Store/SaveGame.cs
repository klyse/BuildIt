using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BuildIt.Store
{
	public enum FactoryType
	{
		Coal,
		Iron,
	}
	public class Factory
	{
		public FactoryType Type { get; set; }
		public int Count { get; set; }

		public void AddManual()
		{
			Count++;
		}

		public int Collect()
		{
			var cnt = Count;
			Count = 0;
			return cnt;
		}
	}

	public class SaveGame
	{
		[JsonIgnore]
		public bool Loaded { get; set; }
		public ICollection<Factory> Factories { get; set; } = new List<Factory>();
		public int Coal { get; set; }

		public void AddFactory(Factory f)
		{
			Factories.Add(f);
		}
	}
}