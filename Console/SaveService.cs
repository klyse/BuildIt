using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Services;

namespace Console
{
	public class SaveService : ISaveService
	{
		public async Task<T> LoadAsync<T>()
		{
			if (File.Exists("save.json"))
			{
				var text = await File.ReadAllTextAsync("save.json");

				return JsonSerializer.Deserialize<T>(text);
			}

			return default;
		}

		public async Task SaveAsync<T>(T value)
		{
			var text = JsonSerializer.Serialize(value);

			await File.WriteAllTextAsync("save.json", text);
		}
	}
}