using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAPICalls<T> where T: class
    {
        Task<IEnumerable<T>?> GetAll(string url);
    }
}