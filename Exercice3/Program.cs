using Newtonsoft.Json;

using (HttpClient client = new HttpClient())
{
    Console.Write("Digite o currencies das moedas (Ex:M1-M2): ");
    string moedas = Console.ReadLine();
    string url = $"https://economia.awesomeapi.com.br/last/{moedas}";
    HttpResponseMessage response = await client.GetAsync(url);

    if (response.IsSuccessStatusCode)
    {
        string responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine("API: " + responseBody);
        var json = JsonConvert.DeserializeObject<Dictionary<string, Cotacao>>(responseBody);

        var moeda = moedas.Split('-');
        Console.WriteLine(
            $"[\n" +
            $"Name: {json[moeda[0] + moeda[1]].name}\n" +
            $"High: {json[moeda[0] + moeda[1]].high}\n" +
            $"Low: {json[moeda[0] + moeda[1]].low}\n"
        );
    }
}
public class Cotacao
{
    public string code { get; set; }
    public string codein { get; set; }
    public string name { get; set; }
    public string high { get; set; }
    public string low { get; set; }
    public string varBid { get; set; }
    public string pctChange { get; set; }
    public string var { get; set; }
    public string bid { get; set; }
    public string ask { get; set; }
    public string timestamp { get; set; }
    public DateTime create_date { get; set; }
}

