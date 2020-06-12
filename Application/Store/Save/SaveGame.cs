using System;
using System.Collections.Generic;

namespace Application.Store.Save
{
	public class SaveGame
	{
		public DateTime LastTick { get; set; }
		public Dictionary<string, decimal> Storage { get; set; } = new Dictionary<string, decimal>();
	}
}