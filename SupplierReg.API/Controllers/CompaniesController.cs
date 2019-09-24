using AutoMapper;
using Domain.Core.Bus;
using Domain.Core.Bus.Messages.Events;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SupplierReg.API.Models.DTOs;
using SupplierReg.API.Models.Resources;
using SupplierReg.Domain.Commands.CompanyContracts;
using SupplierReg.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplierReg.API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class CompaniesController : CustomControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompaniesController(ICompanyRepository companyRepository,
                                    IMapper mapper,
                                    INotificationHandler<DomainErrorNotification> notifications,
                                    IMediatorHandler bus) : base(notifications, bus)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        [HttpGet("{companyID}", Name = nameof(GetCompanybyID))]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult<CompanyResource> GetCompanybyID(Guid companyID)
        {
            return _mapper.Map<CompanyResource>(_companyRepository.Find(companyID));
        }

        [HttpGet(Name = nameof(GetAllCompanies))]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<CompanyResource>>> GetAllCompanies()
        {
            return _mapper.Map<List<CompanyResource>>(await _companyRepository.GetAllCompanies());
        }


        [HttpPost(Name = nameof(CreateNewCompany))]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateNewCompany([FromBody]NewCompanyDTO newCompanyDTO)
        {
            var command = _mapper.Map<AddCompanyCommand>(newCompanyDTO);

            if (!(await SendCommand(command)))
            {
                return BadRequest(GetComandErrors());
            }

            return Created(Url.Link(nameof(GetCompanybyID), new { companyID = command.CompanyID }), new { companyID = command.CompanyID });
        }
    }
}
