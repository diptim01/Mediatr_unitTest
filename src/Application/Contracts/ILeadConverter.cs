using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Application.Contracts
{
    public interface ILeadConverter
    {
        Task<IEnumerable<Lead>> ParseRawLead(string leads, char delimiter);
    }
}