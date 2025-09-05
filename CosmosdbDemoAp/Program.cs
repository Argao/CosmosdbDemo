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


//Insert a new person
// PersonModel person = new() {FirstName = "Sue",LastName = "Storm" };
//
// ItemResponse<PersonModel> response = await container.CreateItemAsync(person);
//
// Console.WriteLine($"Created item in database with id: {response.Resource.Id}, Operation consumed {response.RequestCharge} RUs"); 


//Read one Record
// ItemResponse<PersonModel> response = await container.ReadItemAsync<PersonModel>("da0714e8-6a17-4ff1-84b8-8f6b4d1079a0", new PartitionKey("da0714e8-6a17-4ff1-84b8-8f6b4d1079a0"));
// Console.WriteLine($"Read item from database with id: {response.Resource.Id}, Operation consumed {response.RequestCharge} RUs");
// Console.WriteLine($"{response.Resource.FirstName} {response.Resource.LastName}");

//Read multiple records with a query
// FeedIterator<PersonModel> feedIterator = container.GetItemQueryIterator<PersonModel>(queryText:"SELECT * FROM c WHERE c.LastName = 'Storm'");
//
// while (feedIterator.HasMoreResults)
// {
//     FeedResponse<PersonModel> response = await feedIterator.ReadNextAsync();
//     Console.WriteLine($"Query returned {response.Count} items. Operation consumed {response.RequestCharge} RUs");
//
//     foreach (PersonModel person in response)
//     {
//         Console.WriteLine($"{person.FirstName} {person.LastName}");
//     }
// }


//Update a record
// ItemResponse<PersonModel> response = await container.ReadItemAsync<PersonModel>("da0714e8-6a17-4ff1-84b8-8f6b4d1079a0", new PartitionKey("da0714e8-6a17-4ff1-84b8-8f6b4d1079a0"));
//
// Console.WriteLine($"$Read item from database with id: {response.Resource.Id}, Operation consumed {response.RequestCharge} RUs");
//
// var user = response.Resource;
// user.FirstName = "Peter";
// response = await container.ReplaceItemAsync(user, user.Id, new PartitionKey(user.Id));
// Console.WriteLine($"Updated item in database with id: {response.Resource.Id}, Operation consumed {response.RequestCharge} RUs");


//Delete a record
ItemResponse<PersonModel> response = await container.DeleteItemAsync<PersonModel>("5cf1c240-14fe-47fb-b73e-9e85f3d51b38", new PartitionKey("5cf1c240-14fe-47fb-b73e-9e85f3d51b38"));
Console.WriteLine($"Deleted item from database , Operation consumed {response.RequestCharge} RUs");
