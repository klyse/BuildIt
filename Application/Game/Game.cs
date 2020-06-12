using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Timers;
using Application.Game.Technologies;
using Application.Store.Save;

namespace Application.Game
{
	public class Game
	{
		private readonly Timer _timer;
		private DateTime? _lastTick;

		public Game()
		{
			_timer = new Timer(1000) {AutoReset = false};
			_timer.Elapsed += TimerOnElapsed;
			Storage = new ConcurrentDictionary<Technology, decimal>();
		}

		public ConcurrentDictionary<Technology, decimal> Storage { get; private set; }

		public bool Loaded { get; private set; }

		public bool CanMake(Technology tec)
		{
			foreach (var tecBuildRequirement in tec.BuildRequirements)
			{
				if (!Storage.ContainsKey(tecBuildRequirement.Technology))
					return false;

				if (Storage[tec] > tecBuildRequirement.Quantity)
					return false;
			}

			return true;
		}
		
		public void AddToStorage(Technology tec, decimal amount = 1)
		{
			Storage.AddOrUpdate(tec, t => amount, (t, a) => a + amount);
		}

		public DateTime LastTick
		{
			get => _lastTick ?? DateTime.UtcNow;
			set => _lastTick = value;
		}

		private void TimerOnElapsed(object sender, ElapsedEventArgs e)
		{
			var now = DateTime.UtcNow;
			var delta = (int) (DateTime.UtcNow - LastTick).TotalMilliseconds;

			LastTick = now;
			_timer.Enabled = true;

			Loaded = true;
		}

		public static SaveGame ToSave(Game game)
		{
			if (game is null)
				throw new ArgumentNullException(nameof(game));

			return new SaveGame
			{
				LastTick = game.LastTick,
				Storage = game.Storage.ToDictionary(c => c.Key.Identifier, c => c.Value)
			};
		}

		public static Game FromSave([AllowNull] SaveGame saveGame)
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
				Storage = new ConcurrentDictionary<Technology, decimal>(saveGame.Storage.ToDictionary(c => TechnologyTree.Technologies.First(r => r.Identifier == c.Key), c => c.Value))
			};
		}

		public void Launch()
		{
			if (Loaded)
				return;

			_timer.Enabled = true;
		}
	}
}