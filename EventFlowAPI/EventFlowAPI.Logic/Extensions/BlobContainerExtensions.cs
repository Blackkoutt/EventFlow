using EventFlowAPI.Logic.Helpers.Enums;

namespace EventFlowAPI.Logic.Extensions
{
    public static class BlobContainerExtensions
    {
        public static string ToLowerString(this BlobContainer blobContainer) => blobContainer.ToString().ToLower();
    }
}
