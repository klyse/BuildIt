using System.Threading;
using System.Threading.Tasks;
using Application;
using Domain;
using MongoDB.Driver;

namespace Persistence
{
	public class DbContext : IDbContext
	{
		private readonly MongoClient _client;

		public DbContext(string connectionString)
		{
			var url = MongoUrl.Create(connectionString);
			var databaseName = url.DatabaseName;
			_client = new MongoClient(connectionString);
			var database = _client.GetDatabase(databaseName);

			Users = database.GetCollection<User>("Users");
		}

		public IMongoCollection<User> Users { get; }

		public async Task<IClientSessionHandle> StartSessionAsync(ClientSessionOptions? options = null, CancellationToken cancellationToken = default)
		{
			return await _client.StartSessionAsync(options, cancellationToken);
		}
	}
}