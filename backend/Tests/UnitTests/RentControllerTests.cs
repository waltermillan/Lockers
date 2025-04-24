using API.Controllers;
using API.Services;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;

namespace Tests.UnitTests;
public class RentControllerTests
{
    private readonly RentsController _controller;
    private readonly Mock<IUnitOfWork> _mockRepo;
    private readonly IMapper _mapper;
    private readonly RentDTOService _rentDTOService;

    public RentControllerTests()
    {
        _mockRepo = new Mock<IUnitOfWork>();
        _controller = new RentsController(_mockRepo.Object, _mapper, _rentDTOService);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenRentDoesNotExist()
    {
        // Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Rents\GetById_ReturnsNotFound_WhenRentDoesNotExist.json");
        var json = File.ReadAllText(jsonFilePath);

        var role = JsonConvert.DeserializeObject<Rent>(json);

        _mockRepo.Setup(repo => repo.Rents.GetByIdAsync(role.Id)).ReturnsAsync((Rent)null);

        // Act
        var result = await _controller.Get(role.Id);

        // Assert
        var actionResult = Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetById_ReturnsRent_WhenRentExists()
    {
        // Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Rents\GetById_ReturnsRent_WhenRentExists.json");
        var json = File.ReadAllText(jsonFilePath);

        var role = JsonConvert.DeserializeObject<Rent>(json);

        var mockServerRepository = new Mock<IRentRepository>();
        mockServerRepository.Setup(repo => repo.GetByIdAsync(role.Id)).ReturnsAsync(role);

        var roleService = new RentTestService(mockServerRepository.Object);

        // Act
        var result = await roleService.GetRentById(role.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(role.Id, result.Id);
        Assert.Equal(role.IdCustomer, result.IdCustomer);
        Assert.Equal(role.IdLocker, result.IdLocker);
        Assert.Equal(role.RentalDate, result.RentalDate);
        Assert.Equal(role.ReturnDate, result.ReturnDate);
        Assert.Equal(role.UserName, result.UserName);
    }

    [Fact]
    public void AddRent_AddsRent_WhenRentIsValid()
    {
        // Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Rents\AddRent_AddsRent_WhenRentIsValid.json");
        var json = File.ReadAllText(jsonFilePath);

        var role = JsonConvert.DeserializeObject<Rent>(json);

        var mockRentRepository = new Mock<IRentRepository>();
        mockRentRepository.Setup(repo => repo.Add(It.IsAny<Rent>()));

        var roleService = new RentTestService(mockRentRepository.Object);

        // Act
        roleService.AddRent(role);

        // Assert
        mockRentRepository.Verify(repo => repo.Add(It.Is<Rent>(c => c.IdCustomer == role.IdCustomer && c.IdLocker == role.IdLocker && c.RentalDate == role.RentalDate && c.ReturnDate == role.ReturnDate && c.UserName == role.UserName)), Times.Once);
    }


    [Fact]
    public void AddRents_AddsRents_WhenRentsIsValid()
    {
        //Arrange
        var mockRentRepository = new Mock<IRentRepository>();

        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Rents\AddRents_AddsRents_WhenRentsIsValid.json");

        var rolesJson = File.ReadAllText(jsonFilePath);
        var roles = JsonConvert.DeserializeObject<List<Rent>>(rolesJson);

        mockRentRepository.Setup(repo => repo.Add(It.IsAny<Rent>()));

        var roleService = new RentTestService(mockRentRepository.Object);

        //Act
        foreach (var role in roles)
            roleService.AddRent(role);

        //Assert
        foreach (var role in roles)
            mockRentRepository.Verify(repo => repo.Add(It.Is<Rent>(c => c.IdCustomer == role.IdCustomer && c.IdLocker == role.IdLocker && c.RentalDate == role.RentalDate && c.ReturnDate == role.ReturnDate && c.UserName == role.UserName)), Times.Once);
    }

    [Fact]
    public void UpdateRent_UpdatesRent_WhenRentIsValid()
    {
        //Arrange
        var mockRentRepository = new Mock<IRentRepository>();

        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Rents\UpdateRent_UpdatesRent_WhenRentIsValid.json");

        var roleJson = File.ReadAllText(jsonFilePath);
        var role = JsonConvert.DeserializeObject<Rent>(roleJson);

        mockRentRepository.Setup(repo => repo.Add(It.IsAny<Rent>()));

        var roleService = new RentTestService(mockRentRepository.Object);

        //Act
        roleService.AddRent(role);

        //Assert
        mockRentRepository.Verify(repo => repo.Add(It.Is<Rent>(c => c.IdCustomer == role.IdCustomer && c.IdLocker == role.IdLocker && c.RentalDate == role.RentalDate && c.ReturnDate == role.ReturnDate && c.UserName == role.UserName)), Times.Once);
    }

    [Fact]
    public void DeleteRent_DeletesRent_WhenRentExists()
    {
        //Arrange
        var mockRentRepository = new Mock<IRentRepository>();

        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Rents\DeleteRent_DeletesRent_WhenRentExists.json");

        var roleJson = File.ReadAllText(jsonFilePath);
        var role = JsonConvert.DeserializeObject<Rent>(roleJson);

        mockRentRepository.Setup(repo => repo.GetByIdAsync(role.Id)).ReturnsAsync(role);
        mockRentRepository.Setup(repo => repo.Remove(role));
        var roleService = new RentTestService(mockRentRepository.Object);

        //Act
        roleService.DeleteRent(role);

        //Assert
        mockRentRepository.Verify(repo => repo.Remove(role), Times.Once);
    }


    [Fact]
    public void UpdateRent_ThrowsException_WhenRentToUpdateDoesNotExist()
    {
        //Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Rents\UpdateRent_ThrowsException_WhenRentToUpdateDoesNotExist.json");

        var roleJson = File.ReadAllText(jsonFilePath);
        var role = JsonConvert.DeserializeObject<Rent>(roleJson);

        var mockRentRepository = new Mock<IRentRepository>();
        mockRentRepository.Setup(repo => repo.GetByIdAsync(role.Id)).ReturnsAsync((Rent)null);

        var roleService = new RentTestService(mockRentRepository.Object);

        //Act
        var exception = Assert.Throws<KeyNotFoundException>(() => roleService.UpdateRent(role));

        //Assert
        Assert.Equal("Rent to update not found", exception.Message);
    }

    [Fact]
    public async void GetRentById_ThrowsException_WhenRentDoesNotExist()
    {
        //Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Rents\GetRentById_ThrowsException_WhenRentDoesNotExist.json");
        var roleJson = File.ReadAllText(jsonFilePath);
        var role = JsonConvert.DeserializeObject<Rent>(roleJson);

        var mockRentRepository = new Mock<IRentRepository>();
        var roleId = role.Id;

        mockRentRepository.Setup(repo => repo.GetByIdAsync(roleId)).ReturnsAsync((Rent)null);

        var roleService = new RentTestService(mockRentRepository.Object);

        //Act
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => roleService.GetRentById(roleId));

        //Assert
        Assert.Equal("Rent not found", exception.Message);
    }

}
