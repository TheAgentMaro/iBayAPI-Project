using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string apiUrl = "https://localhost:44378";

        await GetAllUsers(apiUrl);
        await GetUserById(apiUrl);
        await CreateUser(apiUrl);
        await UpdateUser(apiUrl);
        await DeleteUser(apiUrl);

        await GetAllProducts(apiUrl);
        await GetProductById(apiUrl, 1);
        await CreateProduct(apiUrl);
        await UpdateProduct(apiUrl, 1);
        await DeleteProduct(apiUrl, 1);

        await AddToCart(apiUrl);
        await RemoveFromCart(apiUrl, 1);
        await CheckoutCart(apiUrl);
    }

    /*------------------------------------------------User------------------------------------------------*/
    static async Task GetAllUsers(string apiUrl)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync($"{apiUrl}/api/user");
        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine("---------------------GetAllUsers-------------------");
        Console.WriteLine(responseBody);
        Console.WriteLine("---------------------------------------------------");
    }

    static async Task GetUserById(string apiUrl)
    {
        using var Client = new HttpClient();
        var response = await Client.GetAsync($"{apiUrl}/api/user/022312ba-7841-4735-a292-498791e8f5bb");
        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine("-------------------GetUserById---------------------");
        Console.WriteLine(responseBody);
        Console.WriteLine("---------------------------------------------------");


    }

    static async Task CreateUser(string apiUrl)
    {
        using var client = new HttpClient();
        var newUser = new
        {
            Email = "newuser@ibay.com", 
            Pseudo = "createduser", 
            Password = "123456",
            Role = "User" // Ou "Seller"
        };

        var json = JsonConvert.SerializeObject(newUser);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync($"{apiUrl}/api/User/register", content);
        Console.WriteLine("----------------CreateUser-----------------------");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        Console.WriteLine("---------------------------------------------------");
    }

    static async Task UpdateUser(string apiUrl)
    {
        using var client = new HttpClient();
        var updatedUser = new
        {
            Email = "updateduser@example.com",
            Pseudo = "samiupdated",
            Password = "updatedpassword",
            Role = "User" // Ou "Seller"
        };

        var json = JsonConvert.SerializeObject(updatedUser);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        string userId = "da22c6b7-f051-4b1f-a1c9-7bc0a93b0ff5"; 
        var response = await client.PutAsync($"{apiUrl}/api/users/{userId}", content);
        Console.WriteLine("----------------UpdateUser--------------------");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        Console.WriteLine("----------------------------------------------");
    }
    static async Task DeleteUser(string apiUrl)
    {
        using var client = new HttpClient();
        string userId = "022312ba-7841-4735-a292-498791e8f5bb";
        var response = await client.DeleteAsync($"{apiUrl}/api/users/{userId}");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        Console.WriteLine("---------------------------------------------------");
    }

    /*--------------------------------------------Product--------------------------------------------*/
    static async Task GetAllProducts(string apiUrl)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync($"{apiUrl}/api/products");
        Console.WriteLine("------------------GetAllProducts-------------------");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        Console.WriteLine("---------------------------------------------------");
    }

    static async Task GetProductById(string apiUrl, int productId)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync($"{apiUrl}/api/products/{productId}");
        Console.WriteLine("------------------GetProductById------------------");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        Console.WriteLine("---------------------------------------------------");
    }
    static async Task CreateProduct(string apiUrl)
    {
        using var client = new HttpClient();
        var newProduct = new
        {
            Name = "New Product",
            Price = 9.99,
        };

        var json = JsonConvert.SerializeObject(newProduct);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync($"{apiUrl}/api/products", content);
        Console.WriteLine("------------------CreateProduct-------------------");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        Console.WriteLine("---------------------------------------------------");
    }
    static async Task UpdateProduct(string apiUrl, int productId)
    {
        using var client = new HttpClient();
        var updatedProduct = new
        {
            Name = "Updated Product",
            Price = 14.99,
        };

        var json = JsonConvert.SerializeObject(updatedProduct);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PutAsync($"{apiUrl}/api/products/{productId}", content);
        Console.WriteLine("-----------------UpdateProduct--------------------");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        Console.WriteLine("---------------------------------------------------");
    }

    static async Task DeleteProduct(string apiUrl, int productId)
    {
        using var client = new HttpClient();
        var response = await client.DeleteAsync($"{apiUrl}/api/products/{productId}");
        Console.WriteLine("-----------------DeleteProduct--------------------");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        Console.WriteLine("---------------------------------------------------");
    }

    /*--------------------------------------------Cart--------------------------------------------*/
    static async Task AddToCart(string apiUrl)
    {
        using var client = new HttpClient();
        var item = new
        {
            ProductId = 1, // L'ID du produit à ajouter
            Quantity = 1   // La quantité à ajouter
        };

        var json = JsonConvert.SerializeObject(item);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync($"{apiUrl}/api/cart", content);
        Console.WriteLine("--------------------AddToCart---------------------");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        Console.WriteLine("---------------------------------------------------");
    }

    static async Task RemoveFromCart(string apiUrl, int itemId)
    {
        using var client = new HttpClient();
        var response = await client.DeleteAsync($"{apiUrl}/api/cart/{itemId}");
        Console.WriteLine("-----------------RemoveFromCart-------------------");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        Console.WriteLine("---------------------------------------------------");
    }

    static async Task CheckoutCart(string apiUrl)
    {
        using var client = new HttpClient();
        // Simuler un checkout en envoyant un objet vide, ou les détails de paiement si nécessaire
        var content = new StringContent("{}", Encoding.UTF8, "application/json");

        var response = await client.PostAsync($"{apiUrl}/api/cart/checkout", content);
        Console.WriteLine("----------------CheckoutCart----------------------");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        Console.WriteLine("---------------------------------------------------");
    }
}