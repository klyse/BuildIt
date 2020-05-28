using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using BuildIt.Store.Save;

namespace BuildIt.Application
{
	public class Game
	{
		private static Timer _timer;
		private DateTime? _lastTick;

		public Game()
		{
			_timer = new Timer(1000) {AutoReset = false};
			_timer.Elapsed += TimerOnElapsed;
		}

		public bool Loaded { get; private set; }
		public int TransportRobotCount { get; private set; }
		public double TransportRobotThroughput => (double) TransportRobotCount / 1000;
		public ICollection<Factory> Factories { get; private set; }
		public IDictionary<string, int> Storage { get; private set; }

		public DateTime LastTick
		{
			get => _lastTick ?? DateTime.UtcNow;
			set => _lastTick = value;
		}

		private void TimerOnElapsed(object sender, ElapsedEventArgs e)
		{
			var now = DateTime.UtcNow;
			var delta = (int) (DateTime.UtcNow - LastTick).TotalMilliseconds;
			lock (Factories)
			{
				var totalPriority = Factories.Sum(c => c.Priority);

				foreach (var factory in Factories)
				{
					factory.Work(delta);

					if (totalPriority > 0)
					{
						var itemsToCollect = (int) Math.Round((double) factory.Priority / totalPriority * TransportRobotThroughput * delta, 0);

						if (itemsToCollect <= 0)
							continue;

						var cnt = factory.Collect(itemsToCollect);
						AddToStorage(factory.Type.ToString(), cnt);
					}
				}

				LastTick = now;
				_timer.Enabled = true;

				Loaded = true;
			}
		}

		public static SaveGame ToSave(Game game)
		{
			if (game is null)
				throw new ArgumentNullException(nameof(game));
			lock (game.Factories)
			{
				return new SaveGame
				{
					LastTick = game.LastTick,
					TransportRobotCount = game.TransportRobotCount,
					Storage = game.Storage.ToDictionary(c => c.Key.ToString(), c => c.Value),
					Factories = game.Factories.Select(Factory.ToSave).ToList()
				};
			}
		}

		public static Game FromSave(SaveGame saveGame)
		{
			if (saveGame is null)
				return new Game
				{
					LastTick = DateTime.UtcNow
				};

			return new Game
			{
				LastTick = saveGame.LastTick,
				TransportRobotCount = saveGame.TransportRobotCount,
				Storage = saveGame.Storage.ToDictionary(c => c.Key, c => c.Value),
				Factories = saveGame.Factories.Select(Factory.FromSave).ToList()
			};
		}

		public void Launch()
		{
			if (Loaded)
				return;

			_timer.Enabled = true;
		}

		public void HireTransportRobot()
		{
			TransportRobotCount++;
		}

		public void DismissTransportRobot()
		{
			if (TransportRobotCount > 0)
				TransportRobotCount--;
		}

		public void AddToStorage(string type, int cnt)
		{
			lock (Storage)
			{
				if (!Storage.ContainsKey(type))
					Storage.Add(type, cnt);
				else
					Storage[type] += cnt;
			}
		}

		public void AddFactory(Factory f)
		{
			lock (Factories)
				Factories.Add(f);
		}

		public void RemoveFactory(Factory f)
		{
			lock (Factories)
				Factories.Remove(f);
		}
	}
}