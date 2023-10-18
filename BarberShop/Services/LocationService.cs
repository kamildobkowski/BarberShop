using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;
using Azure.Core.GeoJson;
using BarberShop.Entities.BarberShop;
using BarberShop.Services.Interfaces;

namespace BarberShop.Services;

public class LocationService : ILocationService
{
	private readonly string? _apiKey;

	public LocationService(IConfiguration configuration)
	{
		_apiKey = configuration["LocationApiKey:Key"];
		if (_apiKey is null) throw new JsonException();
	}
	public async Task GetCoordinates(Address address)
	{
		var client = new HttpClient();
		var addressToString = Uri.EscapeDataString($"{address.Street} {address.Number} {address.City}");
		var requestUrl =
			$"https://us1.locationiq.com/v1/search?key={_apiKey}&q={addressToString}&format=json&";
		var request = await client.GetAsync(requestUrl);
		if (request is null) throw new JsonException();
		var requestBody = await request.Content.ReadAsStringAsync();
		
		
		try
		{
			var json = JsonNode.Parse(requestBody).AsArray();
			var item = json[0];
			var lat = item["lat"];
			var lon = item["lon"];
			address.Latitude = double.Parse(lat.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
			address.Longitude = double.Parse(lon.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
		}
		catch (ArgumentNullException e)
		{
			await Console.Out.WriteAsync("couldn't parse location!");
		}
	}

	public double GetDistance(Address point1, Address point2)
	{
		var d1 = point1.Latitude * (Math.PI / 180.0);
		var num1 = point1.Longitude * (Math.PI / 180.0);
		var d2 = point2.Latitude * (Math.PI / 180.0);
		var num2 = point2.Longitude * (Math.PI / 180.0) - num1;
		var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
		         Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
		return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
	}
	
	
}