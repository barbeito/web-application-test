using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationTest.Domain.Entities;
using WebApplicationTest.Domain.Interfaces.Repositories;

namespace WebApplicationTest.Infrastructure
{
    public class ClientRepository : IClienteRepository
    {
        private IList<ClientEntity> Clientes;

        public ClientRepository()
        {
            Clientes = new List<ClientEntity>();
        }

        public Task<bool> Delete(ClientEntity client)
        {
            throw new NotImplementedException();
        }

        public Task<ClientEntity> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Insert(ClientEntity client)
        {
            return await Task.Run(() =>
            {
                client.Id = Clientes.Count + 1;
                Clientes.Add(client);

                return client.Id;
            });
        }

        public Task<int> Upadte(ClientEntity client)
        {
            throw new NotImplementedException();
        }
    }
}
