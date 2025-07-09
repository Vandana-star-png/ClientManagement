using AutoMapper;
using ClientManagement.Models;

namespace ClientManagement.Helper
{
    public class ClientMapper : Profile
    {
        public ClientMapper()
        {
            CreateMap<ClientRequest, Client>().ReverseMap();
        }
    }
}
