using Azure.Storage.Blobs;
using EventFlowAPI.DB.Context;
using EventFlowAPI.Logic.Services.OtherServices.Services;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Extensions
{
    public static class BuilderExtensionsDB
    {
        public static void AddConnectionToDB(this WebApplicationBuilder builder, string connectionString)
        {
            builder.Services.AddDbContext<APIContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString(connectionString));
            });
        }
        public static void AddConnectionToAzureBlobStorage(this WebApplicationBuilder builder, string connectionString)
        {
            var accountKey = builder.Configuration.GetSection("Authentication:Azure")["AccountKey"]!;
            var fullConnectionString = $"{builder.Configuration.GetConnectionString(connectionString)}AccountKey={accountKey};";
            builder.Services.AddSingleton(x => new BlobServiceClient(fullConnectionString));
        }
    }
}
