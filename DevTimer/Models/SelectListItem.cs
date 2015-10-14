using System.Web.Mvc;
using AutoMapper;
using DevTimer.Core;
using DevTimer.Domain.Entities;

namespace DevTimer.Models
{
    public class SelectListItemMapping : IHasCustomMapping
    {
        public void CreateMapping(IConfiguration configuration)
        {
            configuration.CreateMap<Client, SelectListItem>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.Text, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Value, opt => opt.MapFrom(s => s.ID));

            configuration.CreateMap<Project, SelectListItem>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.Text, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Value, opt => opt.MapFrom(s => s.ID));

            configuration.CreateMap<WorkType, SelectListItem>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.Text, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Value, opt => opt.MapFrom(s => s.ID));
        }
    }
}