using Newtonsoft.Json;

public class Program
{
    private static readonly HttpClient client = new HttpClient();

    public static async Task Main()
    {
        int escolha;
        string url;
        try
        {
            Console.Write(
                "Moedas\n" +
                "1 - Por Moeda\n" +
                "2 - Por Continente\n" +
                "(apenas numeros): "
            );
            escolha = int.Parse(Console.ReadLine());
            if( escolha == 1)
            {
                Console.Write("Nome da moeda: ");            
                url = $"https://restcountries.com/v3.1/currency/{Console.ReadLine()}?fields=name,capital,currencies";
            }
            else
            {
                Console.Write("Nome do país ou região: ");
                url = $"https://restcountries.com/v3.1/region/{Console.ReadLine()}?fields=name,capital,currencies";
            }
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserializar a resposta JSON para um objeto dynamic
                dynamic countryData = JsonConvert.DeserializeObject(responseBody);

                // Acessar os dados e exibi-los no console
                foreach (var currency in countryData)
                {
                    Console.WriteLine(
                        $"[\n" +
                        $"\tNome: {currency.name.common} \n"+
                        $"\tCapital: {currency.capital[0]}\n"+
                        $"\tSigla: {currency.currencies}" +
                        $"\n]"
                        );
                }
            }
            else
            {
                Console.WriteLine($"A solicitação falhou com o código de status: {response.StatusCode}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Apenas numeros são validos");
        }
    }
}