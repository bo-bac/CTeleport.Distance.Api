using System.Text.Json.Serialization;

namespace CTeleport.Distance.Api
{
    public class Airport
    {
        [JsonPropertyName("location")]
        public GeoLocation Location { get; set; }
    }
}
