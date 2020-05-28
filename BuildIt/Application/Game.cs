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
			_timer = new Timer(1000);
			_timer.AutoReset = false;
		}

		public bool Loaded { get; private set; }
		public int TransportRobotCount { get; private set; }
		public double TransportRobotThroughput => (double) TransportRobotCount / 1000;
		public ICollection<Factory> Factories { get; private set; }
		public Dictionary<FactoryType, int> Storage { get; private set; }

		public DateTime LastTick
		{
			get => _lastTick ?? DateTime.UtcNow;
			set => _lastTick = value;
		}

		public static SaveGame ToSave(Game game)
		{
			if (game is null)
				throw new ArgumentNullException(nameof(game));

			return new SaveGame
			{
				LastTick = game.LastTick,
				TransportRobotCount = game.TransportRobotCount,
				Storage = game.Storage.ToDictionary(c => c.Key.ToString(), c => c.Value),
				Factories = game.Factories.Select(Factory.ToSave).ToList()
			};
		}

		public static Game FromSave(SaveGame saveGame)
		{
			if (saveGame is null)
				return new Game
				{
					Loaded = true,
					LastTick = DateTime.UtcNow
				};

			return new Game
			{
				Loaded = true,
				LastTick = saveGame.LastTick,
				TransportRobotCount = saveGame.TransportRobotCount,
				Storage = saveGame.Storage.ToDictionary(c => Enum.Parse<FactoryType>(c.Key), c => c.Value),
				Factories = saveGame.Factories.Select(Factory.FromSave).ToList()
			};
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

		public void AddToStorage(FactoryType type, int cnt)
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
			Factories.Add(f);
		}
	}
}