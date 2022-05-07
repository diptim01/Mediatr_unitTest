using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Domain;

namespace Infrastructure.Utility
{
    public class LeadsConverter : ILeadConverter
    {
        public Task<IEnumerable<Lead>> ParseRawLead(string leads, char delimiter)
        {
            var parsedListsLeads = SplitByNewLines(leads);
            var rawArrayLeads = SplitMultipleLeads(parsedListsLeads, delimiter);
           return Task.FromResult(PopulateRecords(rawArrayLeads).AsEnumerable());
        }

        private string[] SplitByNewLines(string leads)
        {
            return leads.Split('\n');
        }
        
        private IEnumerable<string[]> SplitMultipleLeads(string[] parsedListsLeads, char delimiter)
        {
            return  parsedListsLeads.Select(item =>
                item.Split(new[] {delimiter.ToString()}, StringSplitOptions.RemoveEmptyEntries));
        }
        
        private List<Lead> PopulateRecords(IEnumerable<string[]> rawArrayLeads)
        {
            return rawArrayLeads.Select(parsedLeads => new Lead()
            {
                LastName = parsedLeads[0].Trim(),
                FirstName = parsedLeads[1].Trim(),
                PropertyType = (PropertyType) Enum.Parse(typeof(PropertyType), parsedLeads[2].Trim()),
                Project = parsedLeads[3].Trim(),
                StartDate = DateTime.Parse(parsedLeads[4].Trim()).ToString("M/d/yyyy"),
                PhoneNumber = parsedLeads[5].Trim()
            }).ToList();
        }
    }
}