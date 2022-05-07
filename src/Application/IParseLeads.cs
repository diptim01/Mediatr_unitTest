using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Application
{
    public interface IParseLeads
    {
        Task<IEnumerable<Lead>> FetchProcessedLeads();
    }
}