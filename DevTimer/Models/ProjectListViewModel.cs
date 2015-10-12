using AutoMapper;
using DevTimer.Core;
using DevTimer.Domain.Entities;

namespace DevTimer.Models
{
    public class ProjectListViewModel : IHasCustomMapping
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public void CreateMapping(IConfiguration configuration)
        {
            configuration.CreateMap<Project, ProjectListViewModel>()
                .ForMember(d => d.ID, opt => opt.MapFrom(s => s.ID))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description));
        }
    }
}