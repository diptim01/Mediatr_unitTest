using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Leads.Queries.DTOs;
using Domain;
using MediatR;

namespace Application.Leads.Queries
{
    public class FetchLeadsByStartDate:  IRequest<LeadsBySortedParameters>
    {
    }
    
    public class FetchLeadsByStartDateHandler:  IRequestHandler<FetchLeadsByStartDate, LeadsBySortedParameters>
    {
        private readonly IParseLeads _parseLeads;

        public FetchLeadsByStartDateHandler(IParseLeads parseLeads)
        {
            _parseLeads = parseLeads;
        }
        
        public async Task<LeadsBySortedParameters> Handle(FetchLeadsByStartDate request, CancellationToken cancellationToken)
        {
            var leads = await _parseLeads.FetchProcessedLeads();
            return ResolveResponse(leads);
        }

        private LeadsBySortedParameters ResolveResponse(IEnumerable<Lead> leads)
        {
            var output = new LeadsBySortedParameters();
            
            if (leads.Any())
            {
                output.ResponseCode = "01";
                output.ResponseDescription = "Leads not retrieved!";
            }

            output.Leads = leads.SortedByStartDateAsc();
            output.ResponseCode = "00";
            output.ResponseDescription = "StartDate Leads retrieval completed successfully";

            
            return output;
        }
    }
}