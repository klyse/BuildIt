using System;
using System.Collections.Generic;

namespace BuildIt.Store.Save
{
	public class SaveGame
	{
		public int TransportRobotCount { get; set; }
		public Dictionary<string, int> Storage { get; set; } = new Dictionary<string, int>();
		public ICollection<SaveFactory> Factories { get; set; } = new List<SaveFactory>();
		public DateTime LastTick { get; set; }
	}
}