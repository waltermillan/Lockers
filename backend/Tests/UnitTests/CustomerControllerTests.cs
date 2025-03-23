using API.Controllers;
using AutoMapper;
using Core.Entities;
using Core.Interfases;
using Core.Services;
using Core.Services.Tests;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;

namespace Tests.UnitTests;
public class CustomerControllerTests
{
    private readonly CustomersController _controller;
    private readonly Mock<IUnitOfWork> _mockRepo;
    private readonly IMapper _mapper;
    private readonly CustomerDTOService _customerDTOService;

    public CustomerControllerTests()
    {
        _mockRepo = new Mock<IUnitOfWork>();
        _controller = new CustomersController(_mockRepo.Object, _mapper, _customerDTOService);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenCustomerDoesNotExist()
    {
        // Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Customers\GetById_ReturnsNotFound_WhenCustomerDoesNotExist.json");
        var json = File.ReadAllText(jsonFilePath);

        var customer = JsonConvert.DeserializeObject<Customer>(json);

        _mockRepo.Setup(repo => repo.Customers.GetByIdAsync(customer.Id)).ReturnsAsync((Customer)null);

        // Act
        var result = await _controller.Get(customer.Id);

        // Assert
        var actionResult = Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetById_ReturnsCustomer_WhenCustomerExists()
    {
        // Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Customers\GetById_ReturnsCustomer_WhenCustomerExists.json");
        var json = File.ReadAllText(jsonFilePath);

        var customer = JsonConvert.DeserializeObject<Customer>(json);

        var mockServerRepository = new Mock<ICustomerRepository>();
        mockServerRepository.Setup(repo => repo.GetByIdAsync(customer.Id)).ReturnsAsync(customer);

        var customerService = new CustomerTestService(mockServerRepository.Object);

        // Act
        var result = await customerService.GetCustomerById(customer.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(customer.Id, result.Id);
        Assert.Equal(customer.Name, result.Name);
        Assert.Equal(customer.Phone, result.Phone);
        Assert.Equal(customer.Address, result.Address);
        Assert.Equal(customer.IdDocument, result.IdDocument);
    }

    [Fact]
    public void AddCustomer_AddsCustomer_WhenCustomerIsValid()
    {
        // Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Customers\AddCustomer_AddsCustomer_WhenCustomerIsValid.json");
        var json = File.ReadAllText(jsonFilePath);

        var customer = JsonConvert.DeserializeObject<Customer>(json);

        var mockCustomerRepository = new Mock<ICustomerRepository>();
        mockCustomerRepository.Setup(repo => repo.Add(It.IsAny<Customer>()));

        var customerService = new CustomerTestService(mockCustomerRepository.Object);

        // Act
        customerService.AddCustomer(customer);

        // Assert
        mockCustomerRepository.Verify(repo => repo.Add(It.Is<Customer>(c => c.Name == customer.Name && c.Phone == customer.Phone && c.Address == customer.Address && c.IdDocument == customer.IdDocument)), Times.Once);
    }


    [Fact]
    public void AddCustomers_AddsCustomers_WhenCustomersIsValid()
    {
        //Arrange
        var mockCustomerRepository = new Mock<ICustomerRepository>();

        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Customers\AddCustomers_AddsCustomers_WhenCustomersIsValid.json");

        var customersJson = File.ReadAllText(jsonFilePath);
        var customers = JsonConvert.DeserializeObject<List<Customer>>(customersJson);

        mockCustomerRepository.Setup(repo => repo.Add(It.IsAny<Customer>()));

        var customerService = new CustomerTestService(mockCustomerRepository.Object);

        //Act
        foreach (var customer in customers)
            customerService.AddCustomer(customer);

        //Assert
        foreach (var customer in customers)
            mockCustomerRepository.Verify(repo => repo.Add(It.Is<Customer>(c => c.Name == customer.Name && c.Phone == customer.Phone && c.Address == customer.Address && c.IdDocument == customer.IdDocument)), Times.Once);
    }

    [Fact]
    public void UpdateCustomer_UpdatesCustomer_WhenCustomerIsValid()
    {
        //Arrange
        var mockCustomerRepository = new Mock<ICustomerRepository>();

        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Customers\UpdateCustomer_UpdatesCustomer_WhenCustomerIsValid.json");

        var customerJson = File.ReadAllText(jsonFilePath);
        var customer = JsonConvert.DeserializeObject<Customer>(customerJson);

        mockCustomerRepository.Setup(repo => repo.Add(It.IsAny<Customer>()));

        var customerService = new CustomerTestService(mockCustomerRepository.Object);

        //Act
        customerService.AddCustomer(customer);

        //Assert
        mockCustomerRepository.Verify(repo => repo.Add(It.Is<Customer>(c => c.Name == customer.Name && c.Phone == customer.Phone && c.Address == customer.Address && c.IdDocument == customer.IdDocument)), Times.Once);
    }

    [Fact]
    public void DeleteCustomer_DeletesCustomer_WhenCustomerExists()
    {
        //Arrange
        var mockCustomerRepository = new Mock<ICustomerRepository>();

        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Customers\DeleteCustomer_DeletesCustomer_WhenCustomerExists.json");

        var customerJson = File.ReadAllText(jsonFilePath);
        var customer = JsonConvert.DeserializeObject<Customer>(customerJson);

        mockCustomerRepository.Setup(repo => repo.GetByIdAsync(customer.Id)).ReturnsAsync(customer);
        mockCustomerRepository.Setup(repo => repo.Remove(customer));
        var customerService = new CustomerTestService(mockCustomerRepository.Object);

        //Act
        customerService.DeleteCustomer(customer);

        //Assert
        mockCustomerRepository.Verify(repo => repo.Remove(customer), Times.Once);
    }


    [Fact]
    public void UpdateCustomer_ThrowsException_WhenCustomerToUpdateDoesNotExist()
    {
        //Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Customers\UpdateCustomer_ThrowsException_WhenCustomerToUpdateDoesNotExist.json");

        var customerJson = File.ReadAllText(jsonFilePath);
        var customer = JsonConvert.DeserializeObject<Customer>(customerJson);

        var mockCustomerRepository = new Mock<ICustomerRepository>();
        mockCustomerRepository.Setup(repo => repo.GetByIdAsync(customer.Id)).ReturnsAsync((Customer)null);

        var customerService = new CustomerTestService(mockCustomerRepository.Object);

        //Act
        var exception = Assert.Throws<KeyNotFoundException>(() => customerService.UpdateCustomer(customer));

        //Assert
        Assert.Equal("Customer to update not found", exception.Message);
    }

    [Fact]
    public async void GetCustomerById_ThrowsException_WhenCustomerDoesNotExist()
    {
        //Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Customers\GetCustomerById_ThrowsException_WhenCustomerDoesNotExist.json");
        var customerJson = File.ReadAllText(jsonFilePath);
        var customer = JsonConvert.DeserializeObject<Customer>(customerJson);

        var mockCustomerRepository = new Mock<ICustomerRepository>();
        var customerId = customer.Id;

        mockCustomerRepository.Setup(repo => repo.GetByIdAsync(customerId)).ReturnsAsync((Customer)null);

        var customerService = new CustomerTestService(mockCustomerRepository.Object);

        //Act
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => customerService.GetCustomerById(customerId));

        //Assert
        Assert.Equal("Customer not found", exception.Message);
    }

}
