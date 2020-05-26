using System.Threading.Tasks;

namespace BuildIt.Store
{
	public interface IStateManager
	{
		Task LoadAsync();
		Task SaveAsync();

		Game Get();
	}
}