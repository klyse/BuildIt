using System;
using System.Linq;
using System.Timers;
using BuildIt.Store;
using Microsoft.AspNetCore.Components;

namespace BuildIt.Pages
{
	public partial class Index
	{
		private static Timer _timer;

		private SaveGame _state = new SaveGame();

		public Index()
		{
			_timer = new Timer(1000);
			_timer.Elapsed += Elapsed;
		}

		[Inject] protected IStateManager StateManager { get; set; }

		private void Elapsed(object sender, ElapsedEventArgs e)
		{
			InvokeAsync(() =>
			{
				var now = DateTime.UtcNow;
				var delta = (int) (DateTime.UtcNow - _state.LastTick).TotalMilliseconds;

				foreach (var factory in _state.Factories) factory.Work(delta);

				_state.Coal += _state.Factories.Sum(c => c.Collect(10));
				_state.LastTick = now;
				StateHasChanged();
			});
		}

		private void AddFactory()
		{
			_state.Factories.Add(new Factory());
		}
	}
}