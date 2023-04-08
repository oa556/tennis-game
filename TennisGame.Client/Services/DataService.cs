using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json;
using System.Text;
using TennisGame.Shared;
using Newtonsoft.Json.Serialization;

namespace TennisGame.Client.Services;

internal class DataService : IDataService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseAddress;
    private readonly string _url;

    public DataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _baseAddress = DeviceInfo.Platform == DevicePlatform.Android
            ? "http://10.0.2.2:7234"
            : "http://localhost:7234";
        _url = $"{_baseAddress}/api";
    }

    public async Task<PlayerDto> GetPlayerAsync(int id)
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            Debug.WriteLine("No internet connection");
            return null;
        }

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/players/{id}");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PlayerDto>(content);
            }
            else
            {
                Debug.WriteLine("Response not successful");
                return null;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Something went wrong: {ex.Message}");
            return null;
        }
    }

    public async Task<PlayerDto[]> GetPlayersAsync()
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            Debug.WriteLine("No internet connection");
            return null;
        }

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/players");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PlayerDto[]>(content);
            }
            else
            {
                Debug.WriteLine("Response not successful");
                return Array.Empty<PlayerDto>();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Something went wrong: {ex.Message}");
            return Array.Empty<PlayerDto>();
        }
    }

    public async Task CreateMatchOutcomeAsync(CreateMatchOutcomeRequest request)
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            Debug.WriteLine("No internet connection");
        }

        try
        {
            string requestJson = JsonConvert.SerializeObject(
                request,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
            );
            StringContent content = new (requestJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/matches", content);

            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Request not successful");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Something went wrong: {ex.Message}");
        }
    }
}
