using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using DevTimer.Core;
using DevTimer.Domain.Entities;

namespace DevTimer.Models
{
    public class ProjectEditViewModel : IHasCustomMapping
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ClientID { get; set; }

        public IEnumerable<SelectListItem> Clients { get; set; }

        public void CreateMapping(IConfiguration configuration)
        {
            // To Model
            configuration.CreateMap<Project, ProjectEditViewModel>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.ID, opt => opt.MapFrom(s => s.ID))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.ClientID, opt => opt.MapFrom(s => s.ClientID));

            configuration.CreateMap<IEnumerable<Client>, ProjectEditViewModel>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.Clients, opt => opt.MapFrom(s => s));


            // From Model
            configuration.CreateMap<ProjectEditViewModel, Project>()
                .ForMember(d => d.ID, opt => opt.MapFrom(s => s.ID))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.ClientID, opt => opt.MapFrom(s => s.ClientID));
        }
    }
}