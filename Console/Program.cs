using System.Threading.Tasks;
using Application.Game.Technologies;
using Application.Services;
using Application.Store;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Console
{
	public class Program
	{
		private static ServiceProvider _sp;

		private static string Ask(string q, params string[] options)
		{
			System.Console.WriteLine();
			var cnt = 0;
			foreach (var option in options)
			{
				System.Console.WriteLine($"{cnt,2}: {option}");
				cnt++;
			}

			System.Console.Write($"{q}: ");

			string readLine;

			if (options.Length < 10)
				readLine = System.Console.ReadKey().KeyChar.ToString();
			else
				readLine = System.Console.ReadLine();

			System.Console.WriteLine();
			return readLine;
		}

		private static async Task Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Verbose()
				.WriteTo.Console()
				.CreateLogger();

			System.Console.WriteLine("BuildIt!");

			var sc = new ServiceCollection()
				.AddScoped<IStateManager, StateManager>()
				.AddScoped<ISaveService, SaveService>();

			_sp = sc.BuildServiceProvider();

			var stateManager = _sp.CreateScope().ServiceProvider.GetRequiredService<IStateManager>();

			await stateManager.LoadAsync();
			var game = stateManager.Get();
			game.Launch();

			while (true)
				switch (Ask("Input", "Quit", "Show Storage", "Make"))
				{
					case "0":
						await stateManager.SaveAsync();

						return;
					case "1":
						System.Console.Clear();
						foreach (var keyValuePair in game.StorageHandler.GetDictionary()) System.Console.WriteLine($"{keyValuePair.Key.Name}: {keyValuePair.Value}");

						break;
					case "2":
						var loop = true;
						while (loop)
							switch (Ask("Make", "Stop", "Stone"))
							{
								case "0":
									loop = false;
									break;
								case "1":
									game.StorageHandler.Add(TechnologyTree.Stone);
									break;
							}

						break;
				}
		}
	}
}