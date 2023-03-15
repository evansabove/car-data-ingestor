using System;

namespace CarDataIngestor.Response
{
    public class DriveResponse
    {
        public Guid Id { get; set; }
        public Uri GetUri { get; set; }
        public DateTime IngestedDate { get; set; }
    }
}
