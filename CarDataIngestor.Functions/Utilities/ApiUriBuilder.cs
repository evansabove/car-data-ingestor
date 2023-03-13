using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;

namespace CarDataIngestor.Utilities
{
    internal static class ApiUriBuilder
    {
        public static Uri BuildFunctionUri(HttpRequest request, string path)
        {
            var queryParams = request.GetQueryParameterDictionary();

            var builder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.Host,
                Port = request.Host.Port ?? (request.Scheme == "https" ? 443 : 80),
                Path = $"/api/{path}"
            };

            if(queryParams.ContainsKey("code"))
            {
                var apiKey = queryParams["code"];
                builder.Query = $"code={apiKey}";
            }

            return builder.Uri;
        }
    }
}
