using System;

namespace CarDataIngestor.Utilities
{
    internal static class UriBuilder
    {
        public static Uri BuildFunctionUri(string path)
        {
            return new Uri($"https://{Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME")}/api/{path}");
        }
    }
}
