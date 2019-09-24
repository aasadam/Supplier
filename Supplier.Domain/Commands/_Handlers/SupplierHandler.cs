using Domain.Core.Bus;
using Domain.Core.Bus.Messages;
using Domain.Core.Bus.Messages.Events;
using Domain.Core.Persistence;
using MediatR;
using SupplierReg.Domain.Commands.SupplierContracts;
using SupplierReg.Domain.Models.Entities;
using SupplierReg.Domain.Models.ValueObjects;
using SupplierReg.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SupplierReg.Domain.Commands._Handlers
{
    public class SupplierHandler : CommandHandler<Supplier>,
                                    IRequestHandler<AddSupplierCommand, bool>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ICompanyRepository _companyRepository;

        public SupplierHandler(IMediatorHandler bus,
                                ISupplierRepository repository, 
                                INotificationHandler<DomainErrorNotification> notifications,
                                ICompanyRepository companyRepository) : base(bus, repository, notifications)
        {
            _supplierRepository = repository;
            _companyRepository = companyRepository;
        }

        public async Task<bool> Handle(AddSupplierCommand request, CancellationToken cancellationToken)
        {
            var company = _companyRepository.Find(request.CompanyID);

            if(company == null)
            {
                await _bus.RaiseEvent(new DomainErrorNotification("NotAllowed", "Company informed does not exists."));
                return false;
            }

            if(await _supplierRepository.ExistsSupplierWithCPFCNPJ(request.CpfCnpj))
            {
                await _bus.RaiseEvent(new DomainErrorNotification("Conflict", "Supplier with informed CPF/CNPJ already exists."));
                return false;
            }

            if(company.UF.ToString().Equals("PR") && CPF.IsCpf(request.CpfCnpj) && request.BirthDate.Value.Age() < 18)
            {
                await _bus.RaiseEvent(new DomainErrorNotification("NotAllowed", "Underage Supplier not permmited."));
                return false;
            }

            _repository.Add(new Supplier(request.Name, (CPFCNPJ)request.CpfCnpj, request.Phones, company, request.SupplierID, request.BirthDate, (RG)request.Rg));
            return Commit();
        }
    }
}
