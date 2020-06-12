using System.Threading.Tasks;

namespace Application.Store
{
	public interface IStateManager
	{
		Task LoadAsync();
		Task SaveAsync();

		Game.Game Get();
	}
}