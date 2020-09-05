using Application;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence
{
	public static class Extensions
	{
		public static IServiceCollection AddMongoDb(this IServiceCollection serviceCollection, string connectionString)
		{
			serviceCollection.AddScoped<IDbContext>(c => new DbContext(connectionString));

			return serviceCollection;
		}
	}
}