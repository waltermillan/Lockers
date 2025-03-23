using Core.Entities;
using Core.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Tests;

public class LocationTestService
{
    private readonly ILocationRepository _locationRepository;

    public LocationTestService(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<Location> GetLocationById(int id)
    {
        var location = await _locationRepository.GetByIdAsync(id);

        if (location is null)
            throw new KeyNotFoundException("Location not found");

        return location;
    }

    public async Task<IEnumerable<Location>> GetAllLocations()
    {
        return await _locationRepository.GetAllAsync();
    }

    public void AddLocation(Location location)
    {
        _locationRepository.Add(location);
    }

    public void AddLocations(IEnumerable<Location> locations)
    {
        foreach (var location in locations)
            _locationRepository.Add(location);
    }

    public void UpdateLocation(Location location)
    {
        var exists = _locationRepository.GetByIdAsync(location.Id).Result;

        if (exists is null)
            throw new KeyNotFoundException("Location to update not found");

        _locationRepository.Update(location);
    }

    public void DeleteLocation(Location location)
    {
        var exists = _locationRepository.GetByIdAsync(location.Id).Result;

        if (exists is null)
            throw new KeyNotFoundException("Location not found");

        _locationRepository.Remove(location);
    }
}
