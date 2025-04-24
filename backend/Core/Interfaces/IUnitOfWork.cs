using Core.Interfaces;

namespace Core.Interfaces;

public interface IUnitOfWork
{
	IUserRepository Users { get; }
    ICustomerRepository Customers { get; }
    IDocumentRepository Documents { get; }
    ILocationRepository Locations { get; }
    ILockerRepository Lockers { get; }
    IPriceRepository Prices { get; }
    IRoleRepository Roles { get; }
    IRentRepository Rents { get; }

    void Dispose();
    Task<int> SaveAsync();
}
