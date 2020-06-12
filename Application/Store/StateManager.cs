using System.Threading.Tasks;
using Application.Services;
using Application.Store.Save;

namespace Application.Store
{
	public class StateManager : IStateManager
	{
		private readonly ISaveService _saveService;
		private Game.Game _game = new Game.Game();

		public StateManager(ISaveService saveService)
		{
			_saveService = saveService;
		}

		public async Task LoadAsync()
		{
			var saveGame = await _saveService.LoadAsync<SaveGame>();
			_game = Game.Game.FromSave(saveGame);
		}

		public async Task SaveAsync()
		{
			var save = Game.Game.ToSave(_game);
			await _saveService.SaveAsync(save);
		}

		public Game.Game Get()
		{
			return _game;
		}
	}
}