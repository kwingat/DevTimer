using AutoMapper;
using DevTimer.Core;
using DevTimer.Domain.Entities;

namespace DevTimer.Models
{
    public class ClientEditViewModel : IHasCustomMapping
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public void CreateMapping(IConfiguration configuration)
        {
            // To Model
            configuration.CreateMap<Client, ClientEditViewModel>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.ID, opt => opt.MapFrom(s => s.ID))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));

            // From Model
            configuration.CreateMap<ClientEditViewModel, Client>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.ID, opt => opt.MapFrom(s => s.ID))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));
        }
    }
}