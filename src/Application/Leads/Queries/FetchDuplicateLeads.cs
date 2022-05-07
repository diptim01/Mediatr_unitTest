using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Leads.Queries.DTOs;
using Domain;
using MediatR;

namespace Application.Leads.Queries
{
    public class FetchDuplicateLeads : IRequest<LeadsBySortedParameters>
    {
    }
    
    public class FetchDuplicateLeadsHandler: IRequestHandler<FetchDuplicateLeads, LeadsBySortedParameters>
    {
        private readonly IParseLeads _parseLeads;
        private readonly IAPICalls<APiLead> _apiCalls;
        private readonly IConfigurationManager _configurationManager;

        public FetchDuplicateLeadsHandler(IParseLeads parseLeads, IAPICalls<APiLead> apiCalls, 
            IConfigurationManager configurationManager)
        {
            _parseLeads = parseLeads;
            _apiCalls = apiCalls;
            _configurationManager = configurationManager;
        }

        public async Task<LeadsBySortedParameters> Handle(FetchDuplicateLeads request, CancellationToken token)
        {
            var apiLeads = await _apiCalls.GetAll(_configurationManager.RetrieveAppSettingsValue("LeadsAPIURL"));
            var storedleads = await _parseLeads.FetchProcessedLeads();
            
            return ResolveResponseDuplicates(apiLeads, storedleads);
        }

        private LeadsBySortedParameters ResolveResponseDuplicates(IEnumerable<APiLead>? apiLeads, IEnumerable<Lead> storedleads)
        {
            var output = new LeadsBySortedParameters();

            var duplicates = storedleads.Where(item => apiLeads.Any(l => l.Phone == item.PhoneNumber)).ToList();

            output.ResponseCode = "00";
            
            if (duplicates.Any())
            {
                output.Leads = duplicates;
                output.ResponseDescription = $"{duplicates.Count} Duplicate(s) found";
                return output;
            }
            

            output.ResponseDescription = "No Duplicate found";
            return output;
        }
        
    }
}