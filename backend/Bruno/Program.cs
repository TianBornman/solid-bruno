using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System.Net.Http.Headers;

namespace Bruno
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");
			builder.RootComponents.Add<HeadOutlet>("head::after");

			builder.Services.AddHttpClient("Api", client =>
			{
				client.BaseAddress = new Uri(builder.Configuration["Api:BaseUrl"]!);
				client.DefaultRequestHeaders.Accept.Add(
					new MediaTypeWithQualityHeaderValue("application/json"));
			});

			builder.Services.AddScoped(sp =>
			{
				var factory = sp.GetRequiredService<IHttpClientFactory>();
				return factory.CreateClient("Api");
			});

			builder.Services.AddMudServices();

			await builder.Build().RunAsync();
		}
	}
}
