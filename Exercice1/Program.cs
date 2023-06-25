using Newtonsoft.Json;

using(HttpClient client = new HttpClient())
{
    string url = "https://ipapi.co/json";
    HttpResponseMessage response = await client.GetAsync(url);

    if (response.IsSuccessStatusCode)
    {
        string responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine("API: " + responseBody);

        // Deserializar a resposta JSON para um objeto ErroAPI
        ErroAPI countryData = JsonConvert.DeserializeObject<ErroAPI>(responseBody);

        // Acessar os dados e exibi-los no console
        Console.WriteLine($"\nErro: {countryData?.Error}");
        Console.WriteLine($"Razão: {countryData?.Reason}");
        Console.WriteLine($"Mensagem: {countryData?.Message} \n");
    }
    else
    {
        Console.WriteLine($"A solicitação falhou com o código de status: {response.StatusCode}");
    }
}

public class ErroAPI
{
    public string? Error { get; set; }
    public string? Reason { get; set; }
    public string? Message { get; set; }
}

