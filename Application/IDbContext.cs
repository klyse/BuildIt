using System.Threading;
using System.Threading.Tasks;
using Domain;
using MongoDB.Driver;

namespace Application
{
	public interface IDbContext
	{
		IMongoCollection<User> Users { get; }

		public Task<IClientSessionHandle> StartSessionAsync(ClientSessionOptions? options = null, CancellationToken cancellationToken = default);
	}
}