using AutoMapper;
using System;
using System.Linq;
using System.Text.RegularExpressions;
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

            if (string.IsNullOrWhiteSpace(client.Cpf) || client.Cpf.Length != 11)
                throw new ClientException($"Property {nameof(ClientModel.Cpf)} is required");

            if (string.IsNullOrWhiteSpace(client.Address) || client.Address.Length < 5)
                throw new ClientException($"Property {nameof(ClientModel.Address)} is required");

            if(string.IsNullOrWhiteSpace(client.Phone) || client.Phone.Length < 10 || !IsValidPhone(client.Phone))
                throw new ClientException($"Property {nameof(ClientModel.Phone)} is required");

            if (string.IsNullOrWhiteSpace(client.Email) || InvalidEmail(client.Email))
                throw new ClientException($"Property {nameof(ClientModel.Email)} is required");
        }

        private bool InvalidName(string name)
        {
            return name.Any(char.IsDigit);
        }

        /// <summary>
        /// Valid format phone:
        ///     - (99) 9999-9999
        ///     - (99) 99999-9999
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        private bool IsValidPhone(string phone)
        {
            return Regex.IsMatch(phone, "^\\([0-9]{2}\\) ([0-9]{4}|[0-9]{5})-[0-9]{4}");
        }

        private bool InvalidEmail(string email)
        {
            return email.Contains(' ') || !(email.Contains('@') && email.Contains(".com"));
        }
    }
}
