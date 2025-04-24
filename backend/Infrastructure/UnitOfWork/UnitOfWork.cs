using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
namespace Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly Context _context;

    private IUserRepository _users;
    private ICustomerRepository _customers;
    private IDocumentRepository _documents;
    private ILocationRepository _locations;
    private ILockerRepository _lockers;
    private IPriceRepository _prices;
    private IRentRepository _rents;
    private IRoleRepository _roles;

    public UnitOfWork(Context context)
    {
        _context = context;
    }

	public IUserRepository Users
	{
		get
		{
			if (_users is null)
				_users = new UserRepository(_context);

			return _users;
		}
	}

	public ICustomerRepository Customers
    {
        get
        {
            if (_customers is null)
                _customers = new CustomerRepository(_context);

            return _customers;
        }
    }

    public IDocumentRepository Documents
    {
        get
        {
            if (_documents is null)
                _documents = new DocumentRepository(_context);

            return _documents;
        }
    }

    public ILocationRepository Locations
    {
        get
        {
            if (_locations is null)
                _locations = new LocationRepository(_context);

            return _locations;
        }
    }



    public ILockerRepository Lockers
    {
        get
        {
            if (_lockers is null)
                _lockers = new LockerRepository(_context);

            return _lockers;
        }
    }
    public IPriceRepository Prices
    {
        get
        {
            if (_prices is null)
                _prices = new PriceRepository(_context);

            return _prices;
        }
    }

    public IRentRepository Rents
    {
        get
        {
            if (_rents is null)
                _rents = new RentRepository(_context);

            return _rents;
        }
    }

    public IRoleRepository Roles
    {
        get
        {
            if (_roles is null)
                _roles = new RoleRepository(_context);

            return _roles;
        }
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
