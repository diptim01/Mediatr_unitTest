using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Leads.Queries.DTOs;
using Domain;
using MediatR;

namespace Application.Leads.Commands
{
    public class PostLeadCommand : IRequest<LeadsBySortedParameters>
    {
        public string RawLead { get; set; }
    }

    public class PostLeadCommandHandler : IRequestHandler<PostLeadCommand, LeadsBySortedParameters>
    {
        private readonly IParseLeads _parseLeads;
        private readonly IFileHandler _handler;

        public PostLeadCommandHandler(IParseLeads parseLeads, IFileHandler handler)
        {
            _parseLeads = parseLeads;
            _handler = handler;
        }
        public Task<LeadsBySortedParameters> Handle(PostLeadCommand request, CancellationToken cancellationToken)
        {
            var isAdded = _handler.AddToLeads(request.RawLead);
            return ResolveResponse(isAdded);
        }

        private Task<LeadsBySortedParameters> ResolveResponse(bool isAdded)
        {
            var output = new LeadsBySortedParameters();
            
            if (!isAdded)
            {
                output.ResponseCode = "01";
                output.ResponseDescription = "Leads not added!-- Check the pipe request again";
                return Task.FromResult(output);
            }
            
            output.ResponseCode = "00";
            output.ResponseDescription = "(1) Lead Added successfully";
            return Task.FromResult(output);
        }
    }
}