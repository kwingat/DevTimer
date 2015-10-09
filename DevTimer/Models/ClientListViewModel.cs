using AutoMapper;
using DevTimer.Core;
using DevTimer.Domain.Entities;

namespace DevTimer.Models
{
    public class ClientListViewModel : IHasCustomMapping
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public void CreateMapping(IConfiguration configuration)
        {
            configuration.CreateMap<Client, ClientListViewModel>()
                .ForMember(d => d.ID, opt => opt.MapFrom(s => s.ID))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));
        }
    }
}