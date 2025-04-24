using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services;

public class PriceTestService
{
    private readonly IPriceRepository _priceRepository;

    public PriceTestService(IPriceRepository priceRepository)
    {
        _priceRepository = priceRepository;
    }

    public async Task<Price> GetPriceById(int id)
    {
        var price = await _priceRepository.GetByIdAsync(id);

        if (price is null)
            throw new KeyNotFoundException("Price not found");

        return price;
    }

    public async Task<IEnumerable<Price>> GetAllPrices()
    {
        return await _priceRepository.GetAllAsync();
    }

    public void AddPrice(Price price)
    {
        _priceRepository.Add(price);
    }

    public void AddPrices(IEnumerable<Price> prices)
    {
        foreach (var price in prices)
            _priceRepository.Add(price);
    }

    public void UpdatePrice(Price price)
    {
        var exists = _priceRepository.GetByIdAsync(price.Id).Result;

        if (exists is null)
            throw new KeyNotFoundException("Price to update not found");

        _priceRepository.Update(price);
    }

    public void DeletePrice(Price price)
    {
        var exists = _priceRepository.GetByIdAsync(price.Id).Result;

        if (exists is null)
            throw new KeyNotFoundException("Price not found");

        _priceRepository.Remove(price);
    }
}
