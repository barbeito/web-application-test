using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationTest.Domain.Interfaces.Repositories;
using WebApplicationTest.Service;

namespace WebApplicationTest.Test
{
    public class ClientServiceTest
    {
        private IMock<IClienteRepository> _clientRepositoryMock;
        private IMock<IMapper> _mapper;

        public ClientServiceTest()
        {
            _clientRepositoryMock = new Mock<IClienteRepository>();
            _mapper = new Mock<IMapper>();
        }

        [Theory]
        [InlineData("")]
        [InlineData("")]
        public async void Insert_NameError(string name)
        {
            var service = new ClientService(_clientRepositoryMock.Object, _mapper.Object);
        }
    }
}
