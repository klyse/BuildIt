using System;
using System.Collections.Generic;
using Application.Game.Jobs;

namespace Application.Store.Save
{
	public class SaveGame
	{
		public DateTime LastTick { get; set; }
		public SaveStorageHandler Storage { get; set; } = new SaveStorageHandler();
		public IList<Job> Jobs { get; set; } = new List<Job>();
	}
}