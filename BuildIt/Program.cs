using System;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Services;
using Application.Store;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Radzen;

namespace BuildIt
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("app");

			builder.Services.AddTransient(sp => new HttpClient
			{
				BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
			});

			AddServices(builder.Services);

			await builder.Build().RunAsync();
		}

		public static IServiceCollection AddServices(IServiceCollection services)
		{
			services.AddBlazoredLocalStorage();

			services.AddScoped<IStateManager, StateManager>()
				.AddScoped<ISaveService, SaveService>()
				.AddScoped<DialogService>()
				.AddScoped<NotificationService>();


			return services;
		}
	}
}