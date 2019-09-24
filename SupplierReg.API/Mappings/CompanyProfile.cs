using AutoMapper;
using SupplierReg.API.Models.DTOs;
using SupplierReg.API.Models.Resources;
using SupplierReg.Domain.Commands.CompanyContracts;
using SupplierReg.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplierReg.API.Mappings
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyResource>(MemberList.Destination)
                .ForMember(dest => dest.CompanyID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CNPJ, opt => opt.MapFrom(src => (string)src.CNPJ))
                .ForMember(dest => dest.UF, opt => opt.MapFrom(src => (string)src.UF));

            CreateMap<NewCompanyDTO, AddCompanyCommand>(MemberList.Source);
        }
    }
}
