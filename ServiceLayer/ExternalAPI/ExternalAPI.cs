using System.Net.Http;
using System.Threading.Tasks;

public class ExternalApiService
{
    private readonly HttpClient _httpClient;

    public ExternalApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Pobierz surowe dane JSON jako string
    public async Task<string> GetRawDataAsync(string url)
    {
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    // Pobierz dane i zdeserializuj do konkretnego typu (np. lista firm)
    public async Task<T?> GetDataAsync<T>(string url)
    {
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return System.Text.Json.JsonSerializer.Deserialize<T>(json);
    }
}
