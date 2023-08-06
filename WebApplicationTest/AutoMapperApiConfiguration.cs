using AutoMapper;
using WebApplicationTest.Domain.Entities;
using WebApplicationTest.Service.Models;

namespace WebApplicationTest.Api
{
    public class AutoMapperApiConfiguration : Profile
    {
        public AutoMapperApiConfiguration()
        {
            CreateMap<ClientModel, ClientEntity>();
        }
    }
}
