using MediatR;
using SupplierReg.Domain.Models.Entities;
using SupplierReg.Domain.Commands.CompanyContracts;
using System.Threading;
using System.Threading.Tasks;
using Domain.Core.Bus.Messages;
using Domain.Core.Bus;
using SupplierReg.Domain.Repositories;
using Domain.Core.Bus.Messages.Events;
using SupplierReg.Domain.Models.ValueObjects;

namespace SupplierReg.Domain.Commands._Handlers
{
    public class CompanyHandler : CommandHandler<Company>,
                                    IRequestHandler<AddCompanyCommand, bool>
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyHandler(IMediatorHandler bus, 
                                ICompanyRepository repository, 
                                INotificationHandler<DomainErrorNotification> notifications) : base(bus, repository, notifications)
        {
            _companyRepository = repository;
        }

        public async Task<bool> Handle(AddCompanyCommand request, CancellationToken cancellationToken)
        {
            if (await _companyRepository.ExistsCompanyWithCNPJ(request.CNPJ))
            {
                await _bus.RaiseEvent(new DomainErrorNotification("Conflict", "Company with CNPJ already exists."));
                return false;
            }

            _repository.Add(new Company(request.Name, (UF)request.UF, (CNPJ)request.CNPJ, request.CompanyID));
            return Commit();
        }
    }
}
