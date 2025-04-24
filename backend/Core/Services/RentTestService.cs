using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services;

public class RentTestService
{
    private readonly IRentRepository _rentRepository;

    public RentTestService(IRentRepository rentRepository)
    {
        _rentRepository = rentRepository;
    }

    public async Task<Rent> GetRentById(int id)
    {
        var rent = await _rentRepository.GetByIdAsync(id);

        if (rent is null)
            throw new KeyNotFoundException("Rent not found");

        return rent;
    }

    public async Task<IEnumerable<Rent>> GetAllRents()
    {
        return await _rentRepository.GetAllAsync();
    }

    public void AddRent(Rent rent)
    {
        _rentRepository.Add(rent);
    }

    public void AddRents(IEnumerable<Rent> rents)
    {
        foreach (var rent in rents)
            _rentRepository.Add(rent);
    }

    public void UpdateRent(Rent rent)
    {
        var exists = _rentRepository.GetByIdAsync(rent.Id).Result;

        if (exists is null)
            throw new KeyNotFoundException("Rent to update not found");

        _rentRepository.Update(rent);
    }

    public void DeleteRent(Rent rent)
    {
        var exists = _rentRepository.GetByIdAsync(rent.Id).Result;

        if (exists is null)
            throw new KeyNotFoundException("Rent not found");

        _rentRepository.Remove(rent);
    }
}
