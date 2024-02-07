using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string apiUrl = "https://localhost:44378";

        await GetAllUsers(apiUrl);
        await GetUserById(apiUrl, 1);
        await CreateUser(apiUrl);
        await UpdateUser(apiUrl, 1);
        await DeleteUser(apiUrl, 1);

        await GetAllProducts(apiUrl);
        await GetProductById(apiUrl, 1);
        await CreateProduct(apiUrl);
        await UpdateProduct(apiUrl, 1);
        await DeleteProduct(apiUrl, 1);

        await AddToCart(apiUrl);
        await RemoveFromCart(apiUrl, 1);
        await CheckoutCart(apiUrl);
    }

    static async Task GetAllUsers(string apiUrl)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync($"{apiUrl}/api/user");
        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseBody);
        Console.WriteLine("---------------------------------------------------");
    }

    static async Task GetUserById(string apiUrl, int userId)
    {
        using var Client = new HttpClient();
        var response = await Client.GetAsync($"{apiUrl}/api/user/022312ba-7841-4735-a292-498791e8f5bb");
        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseBody);
        Console.WriteLine("---------------------------------------------------");


    }

    static async Task CreateUser(string apiUrl)
    {
        // TODO: Appel à l'endpoint POST pour créer un nouvel utilisateur
    }

    static async Task UpdateUser(string apiUrl, int userId)
    {
        // TODO: Appel à l'endpoint PUT pour mettre à jour un utilisateur
    }

    static async Task DeleteUser(string apiUrl, int userId)
    {
        // TODO: Appel à l'endpoint DELETE pour supprimer un utilisateur
    }


    // Exemple pour les produits
    static async Task GetAllProducts(string apiUrl)
    {
        // TODO: Appel à l'endpoint GET pour récupérer tous les produits
    }

    static async Task GetProductById(string apiUrl, int productId)
    {
        // TODO: Appel à l'endpoint GET pour récupérer un produit par son ID
    }

    static async Task CreateProduct(string apiUrl)
    {
        // TODO: Appel à l'endpoint POST pour créer un nouveau produit
    }

    static async Task UpdateProduct(string apiUrl, int productId)
    {
        // TODO: Appel à l'endpoint PUT pour mettre à jour un produit
    }

    static async Task DeleteProduct(string apiUrl, int productId)
    {
        // TODO: Appel à l'endpoint DELETE pour supprimer un produit
    }

    // Exemple pour le panier
    static async Task AddToCart(string apiUrl)
    {
        // TODO: Appel à l'endpoint POST pour ajouter un produit au panier
    }

    static async Task RemoveFromCart(string apiUrl, int itemId)
    {
        // TODO: Appel à l'endpoint DELETE pour supprimer un produit du panier
    }

    static async Task CheckoutCart(string apiUrl)
    {
        // TODO: Appel à l'endpoint POST pour effectuer le paiement du panier
    }
}