using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Timers;
using Application.Game.Jobs;
using Application.Game.Storage;
using Application.Game.Technologies;
using Application.Store.Save;

namespace Application.Game
{
	public class Game
	{
		private readonly Timer _timer;

		public Game()
		{
			LastTick = DateTime.UtcNow;
			StorageHandler = new StorageHandler();
			Jobs = new ConcurrentQueue<Job>();
			_timer = new Timer(1000) {AutoReset = false};
			_timer.Elapsed += TimerOnElapsed;
		}

		public ConcurrentQueue<Job> Jobs { get; private set; }
		public StorageHandler StorageHandler { get; private set; }
		public bool Loaded { get; private set; }
		public DateTime LastTick { get; private set; }

		public bool Make(Technology tec)
		{
			if (StorageHandler.Make(tec, out _))
			{
				StorageHandler.Add(tec);
				return true;
			}

			return false;
		}

		public void Enqueue(Technology tec, decimal amount = 0)
		{
			for (decimal i = 0; i < amount; i++)
			{
				Jobs.Enqueue(new MakeJob(tec));
			}
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
				Storage = StorageHandler.ToSave(game.StorageHandler),
				Jobs = game.Jobs.ToList()
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
				StorageHandler = StorageHandler.FromSave(saveGame.Storage),
				Jobs = new ConcurrentQueue<Job>(saveGame.Jobs)
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