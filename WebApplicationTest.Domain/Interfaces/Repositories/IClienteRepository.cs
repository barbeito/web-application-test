using System.Threading.Tasks;
using WebApplicationTest.Domain.Entities;

namespace WebApplicationTest.Domain.Interfaces.Repositories
{
    public interface IClienteRepository
    {
        Task<int> Insert(ClientEntity client);
        Task<bool> Delete(ClientEntity client);
        Task<int> Upadte(ClientEntity client);
        Task<ClientEntity> Get(int id);
    }
}
