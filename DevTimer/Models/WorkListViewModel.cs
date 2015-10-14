using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DevTimer.Core;
using DevTimer.Domain.Entities;
using Microsoft.AspNet.Identity;

namespace DevTimer.Models
{
    public class WorkListViewModel
    {
        public int ID { get; set; }

        public int ProjectID { get; set; }

        public int WorkTypeID { get; set; }

        public string UserID { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public decimal? Hours { get; set; }

        public string AspNetUser { get; set; }

        public string Project { get; set; }

        public string WorkType { get; set; }

        //public void CreateMapping(IConfiguration configuration)
        //{
        //    #region To Model
        //    configuration.CreateMap<Work, WorkListViewModel>()
        //        .IgnoreAllUnmapped()
        //        .ForMember(d => d.ID, opt => opt.MapFrom(s => s.ID))
        //        .ForMember(d => d.ProjectID, opt => opt.MapFrom(s => s.ProjectID))
        //        //.ForMember(d => d.UserID, opt => opt.MapFrom(s => s.UserID))
        //        .ForMember(d => d.StartTime, opt => opt.MapFrom(s => s.StartTime))
        //        .ForMember(d => d.EndTime, opt => opt.MapFrom(s => s.EndTime))
        //        .ForMember(d => d.Hours, opt => opt.MapFrom(s => s.Hours));

        //    configuration.CreateMap<AspNetUser, WorkListViewModel>()
        //        .IgnoreAllUnmapped()
        //        .ForMember(d => d.UserID, opt => opt.MapFrom(s => s.Id))
        //        .ForMember(d => d.AspNetUser, opt => opt.MapFrom(s => s.UserName));

        //    configuration.CreateMap<Project, WorkListViewModel>()
        //        .IgnoreAllUnmapped()
        //        .ForMember(d => d.Project, opt => opt.MapFrom(s => s.Name));

        //    configuration.CreateMap<WorkType, WorkListViewModel>()
        //        .IgnoreAllUnmapped()
        //        .ForMember(d => d.WorkType, opt => opt.MapFrom(s => s.Name));
        //    #endregion

        //    #region From Model
        //    configuration.CreateMap<Work, WorkListViewModel>()
        //        .IgnoreAllUnmapped()
        //        .ForMember(d => d.ID, opt => opt.MapFrom(s => s.ID))
        //        .ForMember(d => d.ProjectID, opt => opt.MapFrom(s => s.ProjectID))
        //        .ForMember(d => d.UserID, opt => opt.MapFrom(s => s.UserID))
        //        .ForMember(d => d.StartTime, opt => opt.MapFrom(s => s.StartTime))
        //        .ForMember(d => d.EndTime, opt => opt.MapFrom(s => s.EndTime))
        //        .ForMember(d => d.Hours, opt => opt.MapFrom(s => s.Hours));
        //    #endregion

        //}
    }
}