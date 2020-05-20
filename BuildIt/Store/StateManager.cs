using System;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace BuildIt.Store
{
	public interface IStateManager
	{
		Task LoadAsync();
		Task SaveAsync();

		SaveGame Get();
	}

	public class StateManager : IStateManager
	{
		private readonly ILocalStorageService _localStorageService;
		private SaveGame _saveGame;

		public StateManager(ILocalStorageService localStorageService)
		{
			_localStorageService = localStorageService;
		}

		public async Task LoadAsync()
		{
			_saveGame = await _localStorageService.GetItemAsync<SaveGame>("SaveGame");
			_saveGame.Loaded = true;
		}

		public async Task SaveAsync()
		{
			await _localStorageService.SetItemAsync("SaveGame", _saveGame);
		}

		public SaveGame Get()
		{
			return _saveGame;
		}
	}
}