using Azure.Storage.Blobs;
using EventFlowAPI.DB.Context;
using EventFlowAPI.Enums;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Extensions
{
    public static class BuilderExtensionsDB
    {
        public static void AddConnectionToDB(this WebApplicationBuilder builder, ConnectionString connectionString)
        {
            builder.Services.AddDbContext<APIContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString(connectionString.ToString()));
            });
        }
        public static void AddConnectionToAzureBlobStorage(this WebApplicationBuilder builder, ConnectionString connectionString)
        {
            var accountKey = builder.Configuration.GetSection("Authentication:Azure")["AccountKey"]!;
            var fullConnectionString = $"{builder.Configuration.GetConnectionString(connectionString.ToString())}AccountKey={accountKey};";
          
            builder.Services.AddSingleton(x => new BlobServiceClient(fullConnectionString));
        }
    }
}
