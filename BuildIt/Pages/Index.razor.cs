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
			_timer = new Timer(1000);
			_timer.AutoReset = false;
			_timer.Elapsed += Elapsed;
		}

		[Inject] protected IStateManager StateManager { get; set; }

		private void Elapsed(object sender, ElapsedEventArgs e)
		{
			var now = DateTime.UtcNow;
			var delta = (int) (DateTime.UtcNow - _state.LastTick).TotalMilliseconds;
			var totalPriority = _state.Factories.Sum(c => c.Priority);

			foreach (var factory in _state.Factories)
			{
				factory.Work(delta);

				if (totalPriority > 0)
				{
					var itemsToCollect = (int) Math.Round((double) factory.Priority / totalPriority * _state.TransportRobotThroughput * delta, 0);
					Console.WriteLine($"Collect: {itemsToCollect}");
					var cnt = factory.Collect(itemsToCollect);
					_state.AddToStorage(factory.Type, cnt);
				}
			}

			_state.LastTick = now;


			InvokeAsync(StateHasChanged);

			_timer.Enabled = true;
		}

		private void AddFactory()
		{
			_state.Factories.Add(new Factory());
		}
	}
}