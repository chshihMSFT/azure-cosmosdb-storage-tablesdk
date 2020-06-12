using System;
using System.Linq;
using Microsoft.Azure.Cosmos.Table;

namespace azure_cosmosdb_storage_tablesdk
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                String BlobConnectionString = "EndpointofBlobTable";
                String CosmosConnectionString = "EndpointofCosmosTable";
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(BlobConnectionString);
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

                //Create Table
                var table = tableClient.GetTableReference("Table001");
                table.CreateIfNotExists();

                //Query
                var condition = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "demo001");
                var list = table.ExecuteQuery<TableEntity>(new TableQuery<TableEntity>().Where(condition).Take(1));
                var first = list.FirstOrDefault();

                Console.WriteLine($"Item: { first?.PartitionKey }");

            }
            catch (Exception ce) {
                Console.WriteLine("Error: " + ce.Message.ToString());
            }
        }

    }
}
