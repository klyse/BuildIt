using System.Threading.Tasks;
using BuildIt.Application;

namespace BuildIt.Store
{
	public interface IStateManager
	{
		Task LoadAsync();
		Task SaveAsync();

		Game Get();
	}
}