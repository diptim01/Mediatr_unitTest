using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Application;
using Domain;

namespace Assessment.Console
{
    [ExcludeFromCodeCoverage]
    public class QualityLeadsManager
    {
        private readonly IParseLeads _leads;

        public QualityLeadsManager(IParseLeads leads)
        {
            _leads = leads;
        }
    
        public  Task<IEnumerable<Lead>> GetQualityLeads()
        {
            return _leads.FetchProcessedLeads();
        }

   
    }
}