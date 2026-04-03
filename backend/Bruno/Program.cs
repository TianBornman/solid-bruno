using Bruno.Features.Customer.Api;
using Bruno.Features.Customer.State;
using Bruno.Features.Booking.Api;
using Bruno.Features.Booking.State;
using Bruno.Features.Vehicle.Api;
using Bruno.Features.Vehicle.State;
using Bruno.Shared.Api;
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

			builder.Services.AddScoped<IApiClient, ApiClient>();
			builder.Services.AddScoped<ICustomerApi, CustomerApi>();
			builder.Services.AddScoped<CustomerState>();
			builder.Services.AddScoped<IVehicleApi, VehicleApi>();
			builder.Services.AddScoped<VehicleState>();
			builder.Services.AddScoped<IBookingApi, BookingApi>();
			builder.Services.AddScoped<BookingState>();

			builder.Services.AddMudServices();

			await builder.Build().RunAsync();
		}
	}
}
