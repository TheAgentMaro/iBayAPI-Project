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
        var newUser = new
        {
            Email = "newuser@example.com",
            Pseudo = "newusername",
            Password = "newpassword",
            Role = "User" // Ou "Seller", "Admin", etc., selon votre modèle
        };

        var json = JsonConvert.SerializeObject(newUser);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync($"{apiUrl}/api/users", content);
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        Console.WriteLine("---------------------------------------------------");
    }
    static async Task UpdateUser(string apiUrl, int userId)
    {
        var updatedUser = new
        {
            Email = "updateduser@example.com",
            Pseudo = "updatedusername",
            Password = "updatedpassword",
            Role = "User" // Ou "Seller", "Admin", etc., selon votre modèle
        };

        var json = JsonConvert.SerializeObject(updatedUser);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PutAsync($"{apiUrl}/api/users/{userId}", content);
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        Console.WriteLine("---------------------------------------------------");
    }

    static async Task DeleteUser(string apiUrl, int userId)
    {
        var response = await client.DeleteAsync($"{apiUrl}/api/users/{userId}");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        Console.WriteLine("---------------------------------------------------");
    }

    static async Task GetAllProducts(string apiUrl)
    {
        var response = await client.GetAsync($"{apiUrl}/api/products");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        Console.WriteLine("---------------------------------------------------");
    }
    static async Task GetProductById(string apiUrl, int productId)
    {
        var response = await client.GetAsync($"{apiUrl}/api/products/{productId}");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        Console.WriteLine("---------------------------------------------------");
    }

    static async Task CreateProduct(string apiUrl)
    {
        var newProduct = new
        {
            Name = "New Product",
            Price = 9.99,
        };

        var json = JsonConvert.SerializeObject(newProduct);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync($"{apiUrl}/api/products", content);
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        Console.WriteLine("---------------------------------------------------");
    }

    static async Task UpdateProduct(string apiUrl, int productId)
    {
        var updatedProduct = new
        {
            Name = "Updated Product",
            Price = 14.99,
        };

        var json = JsonConvert.SerializeObject(updatedProduct);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PutAsync($"{apiUrl}/api/products/{productId}", content);
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        Console.WriteLine("---------------------------------------------------");
    }
    static async Task DeleteProduct(string apiUrl, int productId)
    {
        var response = await client.DeleteAsync($"{apiUrl}/api/products/{productId}");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        Console.WriteLine("---------------------------------------------------");
    }

    static async Task AddToCart(string apiUrl)
    {
        var item = new
        {
            ProductId = 1, // L'ID du produit à ajouter
            Quantity = 1   // La quantité à ajouter
        };

        var json = JsonConvert.SerializeObject(item);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync($"{apiUrl}/api/cart", content);
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        Console.WriteLine("---------------------------------------------------");
    }
    static async Task RemoveFromCart(string apiUrl, int itemId)
    {
        var response = await client.DeleteAsync($"{apiUrl}/api/cart/{itemId}");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        Console.WriteLine("---------------------------------------------------");
    }


    static async Task CheckoutCart(string apiUrl)
    {
        // Simuler un checkout en envoyant un objet vide, ou les détails de paiement si nécessaire
        var content = new StringContent("{}", Encoding.UTF8, "application/json");

        var response = await client.PostAsync($"{apiUrl}/api/cart/checkout", content);
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        Console.WriteLine("---------------------------------------------------");
    }
}