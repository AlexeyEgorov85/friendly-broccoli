using FriendlyBroccoli.API.Contracts;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Mime;

namespace FriendlyBroccoli.IntegrationTests;

public class OpenLoops : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public OpenLoops(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Get_OpenLoopsReturnSuccess()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("OpenLoops");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
    }

    [Fact]
    public async Task Post_OpenLoopsReturnSuccess()
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new CreateOpenLoopRequest() { Note = "my note" };
        var json = JsonSerializer.Serialize(request);

        var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

        // Act        
        var response = await client.PostAsync("OpenLoops", content);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
    }

        [Fact]
    public async Task Post_OpenLoopsReturnFailed()
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new CreateOpenLoopRequest() { Note = " " };
        var json = JsonSerializer.Serialize(request);

        var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

        // Act        
        var response = await client.PostAsync("OpenLoops", content);

        // Assert
        Assert.Throws<HttpRequestException>(() => response.EnsureSuccessStatusCode()); // Status Code 200-299
    }
}