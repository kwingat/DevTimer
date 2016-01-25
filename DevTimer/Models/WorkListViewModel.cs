using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using DevTimer.Core;
using DevTimer.Domain.Entities;

namespace DevTimer.Models
{
    public class WorkListViewModel : IHasCustomMapping
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<SelectListItem> Projects { get; set; }
        public IEnumerable<Work> Works { get; set; }

        public void CreateMapping(IConfiguration configuration)
        {
            // To Model
            configuration.CreateMap<IEnumerable<Project>, WorkListViewModel>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.Projects, opt => opt.MapFrom(d => d));

            configuration.CreateMap<IEnumerable<Work>, WorkListViewModel>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.Works, opt => opt.MapFrom(s => s));
        }
    }
}