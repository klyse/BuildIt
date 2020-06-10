using System;
using System.Timers;
using BuildIt.Application.Technologies;
using BuildIt.Store.Save;

namespace BuildIt.Application
{
	public class Game
	{
		private readonly Timer _timer;
		private DateTime? _lastTick;
		public TechnologyTree TechnologyTree { get; }

		public Game()
		{
			_timer = new Timer(1000) {AutoReset = false};
			_timer.Elapsed += TimerOnElapsed;
			TechnologyTree = new TechnologyTree();
		}

		public bool Loaded { get; private set; }

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
			};
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