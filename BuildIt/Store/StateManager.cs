using System.Threading.Tasks;
using Blazored.LocalStorage;
using BuildIt.Store.Save;

namespace BuildIt.Store
{
	public class StateManager : IStateManager
	{
		private readonly ILocalStorageService _localStorageService;
		private Game _game;

		public StateManager(ILocalStorageService localStorageService)
		{
			_localStorageService = localStorageService;
		}

		public async Task LoadAsync()
		{
			var saveGame = await _localStorageService.GetItemAsync<SaveGame>("SaveGame") ?? new SaveGame();
			_game = Game.FromSave(saveGame);
		}

		public async Task SaveAsync()
		{
			var save = Game.ToSave(_game);
			await _localStorageService.SetItemAsync("SaveGame", save);
		}

		public Game Get()
		{
			return _game;
		}
	}
}