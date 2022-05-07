using System.Collections.Generic;
using Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Application.Leads.Queries.DTOs
{
    public class LeadsBySortedParameters : GenericResponse
    {
        public LeadsBySortedParameters()
        {
            Leads = new List<Lead>();
        }
        public IEnumerable<Lead> Leads { get; set; }
    }
}