using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;
using BarberShop.Domain.Entites;
using BarberShop.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;

namespace BarberShop.Infrastructure.ExternalServices;

public interface ILocationService
{
	Task GetLocation(Address address);
}

public class LocationService : ILocationService
{
	private readonly string? _apiKey;
	private static readonly HttpClient? Client = new HttpClient();

	public LocationService(IConfiguration configuration)
	{
		_apiKey = configuration["LocationApiKey:Key"];
	}
	public async Task GetLocation(Address address)
	{
		var addressToString = Uri.EscapeDataString($"{address.Street} {address.Number} {address.PostalCode} {address.City}");
		var requestUrl =
			$"https://us1.locationiq.com/v1/search?key={_apiKey}&q={addressToString}&format=json&";
		var request = await Client?.GetAsync(requestUrl)!;
		if (request is null) throw new JsonException();
		var requestBody = await request.Content.ReadAsStringAsync();
			var json = JsonNode.Parse(requestBody)?.AsArray();
			if (json is null)
				throw new Exception();
			var item = json[0];
			var lat = item?["lat"];
			var lon = item?["lon"];
			if (lat is null || lon is null)
				throw new HttpRequestException();
			address.Latitude = double.Parse(lat.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
			address.Longitude = double.Parse(lon.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
	}
}