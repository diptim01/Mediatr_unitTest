using System.Collections.Generic;
using System.Threading.Tasks;
using Application;
using Application.Contracts;
using Domain;
using Infrastructure.Utility;

namespace Infrastructure
{
    public class CommaLeads: IParseLeads
    {
        private readonly IFileHandler _handler;
        private readonly ILeadConverter _leadConverter;

        public CommaLeads(IFileHandler handler, ILeadConverter leadConverter)
        {
            _handler = handler;
            _leadConverter = leadConverter;
        }
        
        public async Task<IEnumerable<Lead>> FetchProcessedLeads()
        {
            var rawLeads =  _handler.FetchLeads("");
            return await _leadConverter.ParseRawLead(rawLeads, _handler.DelimiterCharacter);
        }
    }
}