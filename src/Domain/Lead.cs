using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Domain
{
    public class Lead
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public PropertyType PropertyType { get; set; }
        public string StartDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Project { get; set; }
    }
}