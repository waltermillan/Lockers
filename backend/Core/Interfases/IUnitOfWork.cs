using Core.Interfases;

namespace Core.Interfases;

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
