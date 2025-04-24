using API.DTOs;
using Core.Interfaces;

namespace API.Services;

public class CustomerDTOService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IDocumentRepository _documentRepository;

    public CustomerDTOService(ICustomerRepository customerRepository,
                              IDocumentRepository documentRepository)
    {
        _customerRepository = customerRepository;
        _documentRepository = documentRepository;
    }

    public async Task<CustomerDTO> GetCustomerDTOAsync(int customerId)
    {
        var customer = await _customerRepository.GetByIdAsync(customerId);
        var document = await _documentRepository.GetByIdAsync(customer.IdDocument);

        if (customer is null || document is null)
            return null;

        var customerDTO = new CustomerDTO
        {
            Id = customer.Id,
            Name = customer.Name,
            Phone = customer.Phone,
            Address = customer.Address,
            Document = customer.Document,
            IdDocument = document.Id,
            TypeDocument = document.Description
        };

        return customerDTO;
    }

    public async Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync()
    {
        var customers = await _customerRepository.GetAllAsync();

        if (customers is null || !customers.Any())
            return Enumerable.Empty<CustomerDTO>();

        var customersDTO = new List<CustomerDTO>();

        foreach (var customer in customers)
        {
            var document = await _documentRepository.GetByIdAsync(customer.IdDocument);


            if (document is null || customer is null)
                continue;

            var customerDTO = new CustomerDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Document = customer.Document,
                Address = customer.Address,
                IdDocument = document.Id,
                TypeDocument = document.Description
            };

            customersDTO.Add(customerDTO);
        }

        return customersDTO;
    }
}
