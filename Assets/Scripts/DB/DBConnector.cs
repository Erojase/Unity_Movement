
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System;

public class DBConnector
{

    private readonly string apiUrl;

    public DBConnector()
    {
        apiUrl = AbstergoMenu.DBOptions.dbUrl;
    }

    public async Task<T> Get<T>(string endpoint)
    {
        try
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{apiUrl}/{endpoint}");
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<T>(responseJson);
            return data;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return default(T);
        }
    }

    public async Task<bool> Post<T>(string endpoint, T data)
    {
        try
        {
            using var client = new HttpClient();
            var requestData = new StringContent(JsonConvert.SerializeObject(data));
            requestData.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync($"{apiUrl}/{endpoint}", requestData);
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }

}
