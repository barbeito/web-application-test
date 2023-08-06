using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationTest.Domain.Entities;
using WebApplicationTest.Domain.Exceptions;
using WebApplicationTest.Domain.Interfaces.Repositories;
using WebApplicationTest.Domain.Interfaces.Services;
using WebApplicationTest.Service.Models;

namespace WebApplicationTest.Service
{
    public class ClientService : IClientService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClientService(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<int> Insert(ClientModel client)
        {
            ValidateClient(client);

            var id = await _clienteRepository.Insert(_mapper.Map<ClientEntity>(client));

            return id;
        }

        private void ValidateClient(ClientModel client)
        {
            if (string.IsNullOrWhiteSpace(client.Name) || client.Name.Length < 3 || InvalidName(client.Name))
                throw new ClientException($"Property {nameof(ClientModel.Name)} is required");

            if(string.IsNullOrWhiteSpace(client.Address) || client.Address.Length < 3)
                throw new ClientException($"Property {nameof(ClientModel.Address)} is required");

            if(string.IsNullOrWhiteSpace(client.Phone) || client.Phone.Length < 10)
                throw new ClientException($"Property {nameof(ClientModel.Phone)} is required");

            if (string.IsNullOrWhiteSpace(client.Email) || !EmailValid(client.Email))
                throw new ClientException($"Property {nameof(ClientModel.Phone)} is required");
        }

        private bool InvalidName(string name)
        {
            return name.Any(char.IsDigit);
        }

        private bool EmailValid(string email)
        {
            return !string.IsNullOrWhiteSpace(email) && email.Contains("@") && email.Contains(".com");
        }
    }
}
