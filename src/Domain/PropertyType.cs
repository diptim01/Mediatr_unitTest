using System.Text.Json.Serialization;

namespace Domain
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PropertyType
    {
        House = 1,
        Condo,
        Trailer
    }
    
}