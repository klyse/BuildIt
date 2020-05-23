using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using BuildIt.Store;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BuildIt
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("app");

			builder.Services.AddTransient(sp => new HttpClient
				{BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});

			AddServices(builder.Services);

			await builder.Build().RunAsync();
		}

		public static IServiceCollection AddServices(IServiceCollection services)
		{
			services.AddBlazoredLocalStorage();

			services.AddScoped<IStateManager, StateManager>();

			return services;
		}
	}
}