using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AutoMapper;
using DevTimer.Core;
using DevTimer.Domain.Entities;

namespace DevTimer.Models
{
    public class WorkEditViewModel : IHasCustomMapping
    {
        public int ID { get; set; }

        public int ProjectID { get; set; }

        public int WorkTypeID { get; set; }

        public string UserID { get; set; }

        public string Comment { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        public DateTime? StartTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        public DateTime? EndTime { get; set; }

        public decimal? Hours { get; set; }

        public string AspNetUser { get; set; }

        public string Project { get; set; }

        public string WorkType { get; set; }

        public IEnumerable<SelectListItem> Projects { get; set; }
        public IEnumerable<SelectListItem> WorkTypes { get; set; }

        public void CreateMapping(IConfiguration configuration)
        {
            #region To Model
            configuration.CreateMap<Work, WorkEditViewModel>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.ID, opt => opt.MapFrom(s => s.ID))
                .ForMember(d => d.ProjectID, opt => opt.MapFrom(s => s.ProjectID))
                .ForMember(d => d.WorkTypeID, opt => opt.MapFrom(s => s.WorkTypeID))
                .ForMember(d => d.UserID, opt => opt.MapFrom(s => s.UserID))
                .ForMember(d => d.Comment, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.Date, opt => opt.MapFrom(s => s.StartTime))
                .ForMember(d => d.StartTime, opt => opt.MapFrom(s => s.StartTime))
                .ForMember(d => d.EndTime, opt => opt.MapFrom(s => s.EndTime))
                .ForMember(d => d.Hours, opt => opt.MapFrom(s => s.Hours));

            configuration.CreateMap<AspNetUser, WorkEditViewModel>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.AspNetUser, opt => opt.MapFrom(s => s.UserName));

            configuration.CreateMap<Project, WorkEditViewModel>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.Project, opt => opt.MapFrom(s => s.Name));

            configuration.CreateMap<WorkType, WorkEditViewModel>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.WorkType, opt => opt.MapFrom(s => s.Name));

            configuration.CreateMap<IEnumerable<Project>, WorkEditViewModel>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.Projects, opt => opt.MapFrom(d => d));

            configuration.CreateMap<IEnumerable<WorkType>, WorkEditViewModel>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.WorkTypes, opt => opt.MapFrom(d => d));
            #endregion

            #region From Model
            configuration.CreateMap<WorkEditViewModel, Work>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.ID, opt => opt.MapFrom(s => s.ID))
                .ForMember(d => d.ProjectID, opt => opt.MapFrom(s => s.ProjectID))
                .ForMember(d => d.WorkTypeID, opt => opt.MapFrom(s => s.WorkTypeID))
                .ForMember(d => d.UserID, opt => opt.MapFrom(s => s.UserID))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Comment))
                .ForMember(d => d.StartTime, opt => opt.MapFrom(s => s.StartTime))
                .ForMember(d => d.EndTime, opt => opt.MapFrom(s => s.EndTime))
                .ForMember(d => d.Hours, opt => opt.MapFrom(s => s.Hours));
            #endregion

        }
    }
}