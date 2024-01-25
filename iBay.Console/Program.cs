using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Base URL de votre API
        string apiUrl = "https://localhost:7190/api";

        await GetProducts(apiUrl);
    }

    static async Task GetProducts(string apiUrl)
    {
        using (HttpClient client = new HttpClient())
        {
            // Appel à votre endpoint GET (par exemple, pour obtenir la liste des produits)
            HttpResponseMessage response = await client.GetAsync($"{apiUrl}/product");

            // Vérification de la réussite de la requête
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine($"Erreur : {response.StatusCode}");
            }
        }
    }

    // Autres méthodes pour tester différentes fonctionnalités de l'API
}