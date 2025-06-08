using System.Net;
using System.Text;
using System.Text.Json;

namespace ModelContextProtocol.Server.Integration;

public abstract class HttpService
{
    protected StringContent GetContent(object dado)
    {
        return new StringContent(JsonSerializer.Serialize(dado), Encoding.UTF8, "application/json");
    }

    protected async Task<T> DeserializarObjectResponse<T>(HttpResponseMessage responseMessage)
    {
        return JsonSerializer.Deserialize<T>(options: new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }, json: await responseMessage.Content.ReadAsStringAsync());
    }

    protected bool HandleErrorResponse(HttpResponseMessage response)
    {
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            return false;
        }

        response.EnsureSuccessStatusCode();
        return true;
    }
}