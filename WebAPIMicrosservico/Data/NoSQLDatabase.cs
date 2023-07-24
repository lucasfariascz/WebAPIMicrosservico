using WebAPIMicrosservico.Config.DataBase;
using Microsoft.Azure.Cosmos;

namespace CosmosDBExemple.Data
{
    public class NoSQLDatabase<T>
    {
        private static readonly string EndpointUri = AppSettings.CosmosDdEndpointUri;
        private static readonly string PrimaryKey = AppSettings.CosmosDdPrimaryKey;
        private readonly string databaseId = "WebAPIMicroDB";

        public async Task Add(string containerId, T data, string id)
        {
            CosmosClient cosmosClient = new(EndpointUri, PrimaryKey);

            Database database = cosmosClient.GetDatabase(databaseId);
            Container container = database.GetContainer(containerId);

            await container.CreateItemAsync<T>(data, new PartitionKey(id));
        }
    }
}