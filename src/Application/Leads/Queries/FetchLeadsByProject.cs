using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Leads.Queries.DTOs;
using Domain;
using MediatR;

namespace Application.Leads.Queries
{
    public class FetchLeadsByProject: IRequest<LeadsBySortedParameters>
    {
    }
    
    public class FetchLeadsByProjectHandler: IRequestHandler<FetchLeadsByProject, LeadsBySortedParameters>
    {
        private readonly IParseLeads _parseLeads;
        
        public FetchLeadsByProjectHandler(IParseLeads parseLeads)
        {
            _parseLeads = parseLeads;
        }

        public async Task<LeadsBySortedParameters> Handle(FetchLeadsByProject request, CancellationToken cancellationToken)
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
                output.ResponseDescription = "Sorted Project Leads not retrieved!";
            }

            output.Leads = leads.SortedByProjects();
            output.ResponseCode = "00";
            output.ResponseDescription = "Sorted Project Leads retrieval completed successfully";
            
            return output;
        }
    }
}