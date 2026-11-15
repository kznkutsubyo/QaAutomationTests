using System.Net;
using System.Text.Json;
using QaAutomationTests.Models;

namespace QaAutomationTests.Tests;

/// <summary>
/// REST API functional tests against JSONPlaceholder endpoints.
/// </summary>
public class ApiTests
{
    private const string EndpointUrl = "https://jsonplaceholder.typicode.com/posts/1";

    [Fact]
    public async Task GetPostById_ShouldReturnValidPostWithStatus200()
    {
        // Arrange
        using var httpClient = new HttpClient();

        // Act
        using HttpResponseMessage response = await httpClient.GetAsync(EndpointUrl);
        string rawJson = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
        Assert.False(string.IsNullOrWhiteSpace(rawJson));

        Post? post = JsonSerializer.Deserialize<Post>(rawJson);

        Assert.NotNull(post);
        Assert.Equal(1, post.Id);
        Assert.False(string.IsNullOrWhiteSpace(post.Title));
        Assert.False(string.IsNullOrWhiteSpace(post.Body));
    }
}
