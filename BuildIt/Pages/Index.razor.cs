using System;
using System.Linq;
using System.Timers;
using BuildIt.Application;
using BuildIt.Store;
using Microsoft.AspNetCore.Components;

namespace BuildIt.Pages
{
	public partial class Index
	{
		private static Timer _timer;

		private Game _state = new Game();

		public Index()
		{
			_timer = new Timer(2000) {AutoReset = false};
			_timer.Elapsed += Elapsed;
		}

		[Inject] protected IStateManager StateManager { get; set; }

		private void Elapsed(object sender, ElapsedEventArgs e)
		{
			InvokeAsync(StateHasChanged);

			_timer.Enabled = true;
		}

	}
}