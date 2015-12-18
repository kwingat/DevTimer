using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AutoMapper;
using DevTimer.Core;
using DevTimer.Domain.Entities;

namespace DevTimer.Models
{
    public class WorkerEditViewModel : IHasCustomMapping
    {
        public int ID { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(64)]
        public string Address1 { get; set; }

        [StringLength(64)]
        public string Address2 { get; set; }

        [StringLength(32)]
        public string City { get; set; }

        public int? StateID { get; set; }

        [StringLength(10)]
        public string Zip { get; set; }

        [StringLength(32)]
        public string Phone { get; set; }

        [StringLength(32)]
        public string Fax { get; set; }

        public IEnumerable<SelectListItem> States { get; set; }

        public void CreateMapping(IConfiguration configuration)
        {
            configuration.CreateMap<Worker, WorkerEditViewModel>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.ID, opt => opt.MapFrom(s => s.ID))
                .ForMember(d => d.UserID, opt => opt.MapFrom(s => s.AspNetUser.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.AspNetUser.Email))
                .ForMember(d => d.Address1, opt => opt.MapFrom(s => s.Address1))
                .ForMember(d => d.Address2, opt => opt.MapFrom(s => s.Address2))
                .ForMember(d => d.City, opt => opt.MapFrom(s => s.City))
                .ForMember(d => d.StateID, opt => opt.MapFrom(s => s.StateID))
                .ForMember(d => d.Zip, opt => opt.MapFrom(s => s.Zip))
                .ForMember(d => d.Phone, opt => opt.MapFrom(s => s.Phone))
                .ForMember(d => d.Fax, opt => opt.MapFrom(s => s.Fax));

            configuration.CreateMap<IEnumerable<State>, WorkerEditViewModel>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.States, opt => opt.MapFrom(d => d));

            configuration.CreateMap<WorkerEditViewModel, Worker>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.ID, opt => opt.MapFrom(s => s.ID))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Address1, opt => opt.MapFrom(s => s.Address1))
                .ForMember(d => d.Address2, opt => opt.MapFrom(s => s.Address2))
                .ForMember(d => d.City, opt => opt.MapFrom(s => s.City))
                .ForMember(d => d.StateID, opt => opt.MapFrom(s => s.StateID))
                .ForMember(d => d.Zip, opt => opt.MapFrom(s => s.Zip))
                .ForMember(d => d.Phone, opt => opt.MapFrom(s => s.Phone))
                .ForMember(d => d.Fax, opt => opt.MapFrom(s => s.Fax));
                
        }
    }
}