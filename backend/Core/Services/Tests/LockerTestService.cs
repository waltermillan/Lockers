using Core.Entities;
using Core.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Tests;

public class LockerTestService
{
    private readonly ILockerRepository _lockerRepository;

    public LockerTestService(ILockerRepository userRepository)
    {
        _lockerRepository = userRepository;
    }

    public async Task<Locker> GetLockerById(int id)
    {
        var locker = await _lockerRepository.GetByIdAsync(id);

        if (locker is null)
            throw new KeyNotFoundException("Locker not found");

        return locker;
    }

    public async Task<IEnumerable<Locker>> GetAllLockers()
    {
        return await _lockerRepository.GetAllAsync();
    }

    public void AddLocker(Locker locker)
    {
        _lockerRepository.Add(locker);
    }

    public void AddLockers(IEnumerable<Locker> lockers)
    {
        foreach (var locker in lockers)
            _lockerRepository.Add(locker);
    }

    public void UpdateLocker(Locker locker)
    {
        var exists = _lockerRepository.GetByIdAsync(locker.Id).Result;

        if (exists is null)
            throw new KeyNotFoundException("Locker to update not found");

        _lockerRepository.Update(locker);
    }

    public void DeleteLocker(Locker locker)
    {
        var exists = _lockerRepository.GetByIdAsync(locker.Id).Result;

        if (exists is null)
            throw new KeyNotFoundException("Locker not found");

        _lockerRepository.Remove(locker);
    }
}
