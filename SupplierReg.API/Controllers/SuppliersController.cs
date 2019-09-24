using AutoMapper;
using Domain.Core.Bus;
using Domain.Core.Bus.Messages.Events;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SupplierReg.API.Models.DTOs;
using SupplierReg.API.Models.Resources;
using SupplierReg.Domain.Commands.SupplierContracts;
using SupplierReg.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplierReg.API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class SuppliersController : CustomControllerBase
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public SuppliersController(ISupplierRepository supplierRepository,
                                    IMapper mapper,
                                    INotificationHandler<DomainErrorNotification> notifications,
                                    IMediatorHandler bus) : base(notifications, bus)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        [HttpGet("{supplierID}", Name = nameof(GetSupplierbyID))]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult<SupplierResource> GetSupplierbyID(Guid supplierID)
        {
            return _mapper.Map<SupplierResource>(_supplierRepository.Find(supplierID));
        }

        [HttpGet(Name = nameof(FilterAllSuppliers))]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<SupplierResource>>> FilterAllSuppliers([FromQuery]string name,
                                                                                            [FromQuery]string cpfcnpj,
                                                                                            [FromQuery]DateTimeOffset? creationdate)
        {
            return _mapper.Map<List<SupplierResource>>(await _supplierRepository.FilterAllSupliers(name, cpfcnpj, creationdate));
        }

        [HttpPost(Name = nameof(CreateNewSupplier))]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateNewSupplier([FromBody]NewSupplierDTO newSupplierDTO)
        {
            var command = _mapper.Map<AddSupplierCommand>(newSupplierDTO);

            if (!(await SendCommand(command)))
            {
                return BadRequest(GetComandErrors());
            }

            return Created(Url.Link(nameof(GetSupplierbyID), new { supplierID = command.SupplierID }), new { supplierID = command.SupplierID });
        }
    }
}
