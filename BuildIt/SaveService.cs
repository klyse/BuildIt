using System.Threading.Tasks;
using Application.Services;
using Blazored.LocalStorage;

namespace BuildIt
{
	public class SaveService : ISaveService
	{
		private readonly ILocalStorageService _localStorageService;

		public SaveService(ILocalStorageService localStorageService)
		{
			_localStorageService = localStorageService;
		}

		public async Task<T> LoadAsync<T>()
		{
			var saveGame = await _localStorageService.GetItemAsync<T>("game");

			return saveGame;
		}

		public async Task SaveAsync<T>(T value)
		{
			await _localStorageService.SetItemAsync("game", value);
		}
	}
}