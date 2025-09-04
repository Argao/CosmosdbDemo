using CosmosdbDemoAp;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true)
    .AddUserSecrets<Program>();
    
IConfiguration config = builder.Build();

CosmosClient client = new(
    config.GetValue<string>("CosmosDB:Endpoint"),
    config.GetValue<string>("CosmosDB:PrimaryKey")
    );

Database database = client.GetDatabase(config.GetValue<string>("CosmosDB:Database"));

Container container = database.GetContainer("People");


//Isert a new person
PersonModel person = new() {FirstName = "Tim",LastName = "Corey" };

ItemResponse<PersonModel> response = await container.CreateItemAsync(person);

Console.WriteLine($"Created item in database with id: {response.Resource.Id}, Operation consumed {response.RequestCharge} RUs"); 