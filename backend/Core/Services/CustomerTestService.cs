using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services;

public class CustomerTestService
{
    private readonly ICustomerRepository _customertRepository;

    public CustomerTestService(ICustomerRepository customertRepository)
    {
        _customertRepository = customertRepository;
    }

    public async Task<Customer> GetCustomerById(int id)
    {
        var customer = await _customertRepository.GetByIdAsync(id);

        if (customer is null)
            throw new KeyNotFoundException("Customer not found");

        return customer;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomers()
    {
        return await _customertRepository.GetAllAsync();
    }

    public void AddCustomer(Customer customer)
    {
        _customertRepository.Add(customer);
    }

    public void AddCustomers(IEnumerable<Customer> customers)
    {
        foreach (var customer in customers)
            _customertRepository.Add(customer);
    }

    public void UpdateCustomer(Customer customer)
    {
        var exists = _customertRepository.GetByIdAsync(customer.Id).Result;

        if (exists is null)
            throw new KeyNotFoundException("Customer to update not found");

        _customertRepository.Update(customer);
    }

    public void DeleteCustomer(Customer customer)
    {
        var exists = _customertRepository.GetByIdAsync(customer.Id).Result;

        if (exists is null)
            throw new KeyNotFoundException("Customer not found");

        _customertRepository.Remove(customer);
    }
}
