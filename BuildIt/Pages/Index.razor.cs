using System;
using System.Linq;
using System.Timers;
using BuildIt.Store;
using Microsoft.AspNetCore.Components;

namespace BuildIt.Pages
{
	public partial class Index
	{
		[Inject] protected IStateManager StateManager { get; set; }
		private static System.Timers.Timer aTimer;

		SaveGame _state = new SaveGame();

		public Index()
		{
			aTimer = new Timer(1000);
			aTimer.Enabled = true;
			aTimer.Elapsed += Elapsed;
		}

		private void Elapsed(object sender, ElapsedEventArgs e)
		{
			_state.Coal += _state.Factories.Sum(c => c.Collect());
			StateHasChanged();
		}
	}
}