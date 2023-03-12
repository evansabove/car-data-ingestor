using System;

namespace CarDataIngestor.Utilities
{
    internal static class ApiUriBuilder
    {
        public static Uri BuildFunctionUri(string path)
        {
            return new Uri($"https://{Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME")}/api/{path}");
        }
    }
}
