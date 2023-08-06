using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationTest.Service.Models;

namespace WebApplicationTest.Domain.Interfaces.Services
{
    public interface IClientService
    {
        Task<int> Insert(ClientModel client);
    }
}
