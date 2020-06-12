using System.Threading.Tasks;

namespace Application.Services
{
	public interface ISaveService
	{
		Task<T> LoadAsync<T>();
		Task SaveAsync<T>(T value);
	}
}