using AutoMapper;
using SupplierReg.API.Models.DTOs;
using SupplierReg.API.Models.Resources;
using SupplierReg.Domain.Commands.SupplierContracts;
using SupplierReg.Domain.Models.Entities;
using SupplierReg.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplierReg.API.Mappings
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<Supplier, SupplierResource>(MemberList.Destination)
                .ForMember(dest => dest.CPFCNPJ, opt => opt.MapFrom(src => (string)src.CPFCNPJ));

            CreateMap<NewSupplierDTO, AddSupplierCommand>(MemberList.Source)
                .ForMember(dest => dest.Phones, opt => opt.MapFrom(src => src.Phones.Select(p => new Phone(p.IsResidential, p.PhoneNumber.Take(2).ToString(), p.PhoneNumber.Skip(2).ToString(), "55"))));
        }
    }
}
