using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationTest.Api;
using WebApplicationTest.Domain.Entities;
using WebApplicationTest.Domain.Exceptions;
using WebApplicationTest.Domain.Interfaces.Repositories;
using WebApplicationTest.Service;
using WebApplicationTest.Service.Models;

namespace WebApplicationTest.Test
{
    public class ClientServiceTest
    {
        private readonly Mock<IClienteRepository> _clientRepositoryMock;
        private readonly IMapper _mapper;

        public ClientServiceTest()
        {
            _clientRepositoryMock = new Mock<IClienteRepository>();

            var mapperConfig = new MapperConfiguration(mapper => mapper.AddProfile(new AutoMapperApiConfiguration()));
            var mapper = mapperConfig.CreateMapper();

            _mapper = mapper;
        }

        #region Error validation tests

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("AB")]
        [InlineData("ABC 123")]
        public void Insert_NameError(string name)
        {
            // Arrange
            var service = new ClientService(_clientRepositoryMock.Object, _mapper);
            var clientModelRequest = new ClientModel
            {
                Name = name,
            };
            var expectedMessage = $"Property {nameof(ClientModel.Name)} is required";

            // Assert
            var exception = Assert.ThrowsAsync<ClientException>(async () => await service.Insert(clientModelRequest));
            Assert.Equal(exception.Result.Message, expectedMessage);
            _clientRepositoryMock.Verify(repo => repo.Insert(It.IsAny<ClientEntity>()), Times.Never);
            _clientRepositoryMock.Verify(repo => repo.Upadte(It.IsAny<ClientEntity>()), Times.Never);
            _clientRepositoryMock.Verify(repo => repo.Delete(It.IsAny<ClientEntity>()), Times.Never);
            _clientRepositoryMock.Verify(repo => repo.Get(It.IsAny<int>()), Times.Never);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("AB")]
        [InlineData("ABC 123")]
        [InlineData("123456789101")]
        [InlineData("1234567891")]
        public void Insert_CpfError(string cpf)
        {
            // Arrange
            var service = new ClientService(_clientRepositoryMock.Object, _mapper);
            var clientModelRequest = new ClientModel
            {
                Name = "Jose da Silva",
                Cpf = cpf
            };
            var expectedMessage = $"Property {nameof(ClientModel.Cpf)} is required";

            // Assert
            var exception = Assert.ThrowsAsync<ClientException>(async () => await service.Insert(clientModelRequest));
            Assert.Equal(exception.Result.Message, expectedMessage);
            _clientRepositoryMock.Verify(repo => repo.Insert(It.IsAny<ClientEntity>()), Times.Never);
            _clientRepositoryMock.Verify(repo => repo.Upadte(It.IsAny<ClientEntity>()), Times.Never);
            _clientRepositoryMock.Verify(repo => repo.Delete(It.IsAny<ClientEntity>()), Times.Never);
            _clientRepositoryMock.Verify(repo => repo.Get(It.IsAny<int>()), Times.Never);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("AB")]
        [InlineData("AB D")]
        public void Insert_AddressError(string address)
        {
            // Arrange
            var service = new ClientService(_clientRepositoryMock.Object, _mapper);
            var clientModelRequest = new ClientModel
            {
                Name = "João da Silva",
                Address = address,
            };
            var expectedMessage = $"Property {nameof(ClientModel.Address)} is required";

            // Assert
            var exception = Assert.ThrowsAsync<ClientException>(async () => await service.Insert(clientModelRequest));
            Assert.Equal(exception.Result.Message, expectedMessage);
            
            _clientRepositoryMock.Verify(repo => repo.Insert(It.IsAny<ClientEntity>()), Times.Never);
            _clientRepositoryMock.Verify(repo => repo.Upadte(It.IsAny<ClientEntity>()), Times.Never);
            _clientRepositoryMock.Verify(repo => repo.Delete(It.IsAny<ClientEntity>()), Times.Never);
            _clientRepositoryMock.Verify(repo => repo.Get(It.IsAny<int>()), Times.Never);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("ABC")]
        [InlineData("AB D")]
        [InlineData("12 C")]
        [InlineData("(31) 9999-AB12")]
        [InlineData("(31) 999-1234")]
        public void Insert_PhoneError(string phone)
        {
            // Arrange
            var service = new ClientService(_clientRepositoryMock.Object, _mapper);
            var clientModelRequest = new ClientModel
            {
                Name = "João da Silva",
                Address = "Rua Sem Nome, n. 45",
                Phone = phone,
            };
            var expectedMessage = $"Property {nameof(ClientModel.Phone)} is required";

            // Assert
            var exception = Assert.ThrowsAsync<ClientException>(async () => await service.Insert(clientModelRequest));
            Assert.Equal(exception.Result.Message, expectedMessage);

            _clientRepositoryMock.Verify(repo => repo.Insert(It.IsAny<ClientEntity>()), Times.Never);
            _clientRepositoryMock.Verify(repo => repo.Upadte(It.IsAny<ClientEntity>()), Times.Never);
            _clientRepositoryMock.Verify(repo => repo.Delete(It.IsAny<ClientEntity>()), Times.Never);
            _clientRepositoryMock.Verify(repo => repo.Get(It.IsAny<int>()), Times.Never);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("ABC")]
        [InlineData("AB D")]
        [InlineData("email@teste")]
        [InlineData("email.teste.com")]
        //[InlineData("email@teste.com")]
        public void Insert_EmailError(string email)
        {
            // Arrange
            var service = new ClientService(_clientRepositoryMock.Object, _mapper);
            var clientModelRequest = new ClientModel
            {
                Name = "João da Silva",
                Address = "Rua Sem Nome, n. 45",
                Phone = "(31) 3333-3333",
                Email = email
            };
            var expectedMessage = $"Property {nameof(ClientModel.Email)} is required";

            // Assert
            var exception = Assert.ThrowsAsync<ClientException>(async () => await service.Insert(clientModelRequest));
            Assert.Equal(exception.Result.Message, expectedMessage);

            _clientRepositoryMock.Verify(repo => repo.Insert(It.IsAny<ClientEntity>()), Times.Never);
            _clientRepositoryMock.Verify(repo => repo.Upadte(It.IsAny<ClientEntity>()), Times.Never);
            _clientRepositoryMock.Verify(repo => repo.Delete(It.IsAny<ClientEntity>()), Times.Never);
            _clientRepositoryMock.Verify(repo => repo.Get(It.IsAny<int>()), Times.Never);
        }

        #endregion

        #region Success Tests

        [Fact]
        public async Task Insert_Sucess()
        {
            // Arrange
            var service = new ClientService(_clientRepositoryMock.Object, _mapper);
            var clientModelRequest = new ClientModel
            {
                Name = "João da Silva",
                Address = "Rua Sem Nome, n. 45",
                Phone = "(31) 3333-3333",
                Email = "email@teste.com",
                Cpf = "12345678910"
            };
            _clientRepositoryMock.Setup(repo => repo.Insert(It.IsAny<ClientEntity>())).Returns(Task.FromResult(10));

            // Act
            var idClient = await service.Insert(clientModelRequest);

            // Assert
            Assert.True(idClient > 0);
            Assert.True(idClient == 10);
            _clientRepositoryMock.Verify(repo => repo.Insert(It.IsAny<ClientEntity>()), Times.Once);
            _clientRepositoryMock.Verify(repo => repo.Upadte(It.IsAny<ClientEntity>()), Times.Never);
            _clientRepositoryMock.Verify(repo => repo.Delete(It.IsAny<ClientEntity>()), Times.Never);
            _clientRepositoryMock.Verify(repo => repo.Get(It.IsAny<int>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(ClientData))]
        public async Task Insert_Sucess2(string name, string address, string phone, string email, string cpf, int expextedId)
        {
            // Arrange
            var service = new ClientService(_clientRepositoryMock.Object, _mapper);
            var clientModelRequest = new ClientModel
            {
                Name = name,
                Address = address,
                Phone = phone,
                Email = email,
                Cpf = cpf
            };
            _clientRepositoryMock.Setup(repo => repo.Insert(It.IsAny<ClientEntity>())).Returns(Task.FromResult(expextedId));

            // Act
            var idClient = await service.Insert(clientModelRequest);

            // Assert
            Assert.True(idClient > 0);
            Assert.True(idClient == expextedId);
            _clientRepositoryMock.Verify(repo => repo.Insert(It.IsAny<ClientEntity>()), Times.Once);
            _clientRepositoryMock.Verify(repo => repo.Upadte(It.IsAny<ClientEntity>()), Times.Never);
            _clientRepositoryMock.Verify(repo => repo.Delete(It.IsAny<ClientEntity>()), Times.Never);
            _clientRepositoryMock.Verify(repo => repo.Get(It.IsAny<int>()), Times.Never);
        }

        public static IEnumerable<object[]> ClientData => new List<object[]>
        {
            new object[] { "José da Silva", "Rua ABC, n 30", "(31) 3333-3333", "email@test.com", "12345678910", 2 },
            new object[] { "Maria Souza", "Rua XPTO, n 100", "(31) 93333-3333", "email@test.com", "12345678910", 5 },
        };

        #endregion
    }
}
