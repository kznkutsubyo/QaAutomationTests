using System.Text.Json.Serialization;

namespace QaAutomationTests.Models;

/// <summary>
/// Represents a post resource returned by the JSONPlaceholder API.
/// </summary>
public class Post
{
    [JsonPropertyName("userId")]
    public int UserId { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("body")]
    public string Body { get; set; } = string.Empty;
}
