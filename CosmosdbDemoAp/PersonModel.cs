using Newtonsoft.Json;

namespace CosmosdbDemoAp;

public class PersonModel
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}