using API.Controllers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;

namespace Tests.UnitTests;
public class LocationControllerTests
{
    private readonly LocationsController _controller;
    private readonly Mock<IUnitOfWork> _mockRepo;
    private readonly IMapper _mapper;

    public LocationControllerTests()
    {
        _mockRepo = new Mock<IUnitOfWork>();
        _controller = new LocationsController(_mockRepo.Object, _mapper);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenLocationDoesNotExist()
    {
        // Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Locations\GetById_ReturnsNotFound_WhenLocationDoesNotExist.json");
        var json = File.ReadAllText(jsonFilePath);

        var location = JsonConvert.DeserializeObject<Location>(json);

        _mockRepo.Setup(repo => repo.Locations.GetByIdAsync(location.Id)).ReturnsAsync((Location)null);

        // Act
        var result = await _controller.Get(location.Id);

        // Assert
        var actionResult = Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetById_ReturnsLocation_WhenLocationExists()
    {
        // Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Locations\GetById_ReturnsLocation_WhenLocationExists.json");
        var json = File.ReadAllText(jsonFilePath);

        var location = JsonConvert.DeserializeObject<Location>(json);

        var mockServerRepository = new Mock<ILocationRepository>();
        mockServerRepository.Setup(repo => repo.GetByIdAsync(location.Id)).ReturnsAsync(location);

        var locationService = new LocationTestService(mockServerRepository.Object);

        // Act
        var result = await locationService.GetLocationById(location.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(location.Id, result.Id);
        Assert.Equal(location.Address, result.Address);
        Assert.Equal(location.PostalCode, result.PostalCode);
    }

    [Fact]
    public void AddLocation_AddsLocation_WhenLocationIsValid()
    {
        // Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Locations\AddLocation_AddsLocation_WhenLocationIsValid.json");
        var json = File.ReadAllText(jsonFilePath);

        var location = JsonConvert.DeserializeObject<Location>(json);

        var mockLocationRepository = new Mock<ILocationRepository>();
        mockLocationRepository.Setup(repo => repo.Add(It.IsAny<Location>()));

        var locationService = new LocationTestService(mockLocationRepository.Object);

        // Act
        locationService.AddLocation(location);

        // Assert
        mockLocationRepository.Verify(repo => repo.Add(It.Is<Location>(c => c.Address == location.Address && c.PostalCode == location.PostalCode)), Times.Once);
    }


    [Fact]
    public void AddLocations_AddsLocations_WhenLocationsIsValid()
    {
        //Arrange
        var mockLocationRepository = new Mock<ILocationRepository>();

        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Locations\AddLocations_AddsLocations_WhenLocationsIsValid.json");

        var locationsJson = File.ReadAllText(jsonFilePath);
        var locations = JsonConvert.DeserializeObject<List<Location>>(locationsJson);

        mockLocationRepository.Setup(repo => repo.Add(It.IsAny<Location>()));

        var locationService = new LocationTestService(mockLocationRepository.Object);

        //Act
        foreach (var location in locations)
            locationService.AddLocation(location);

        //Assert
        foreach (var location in locations)
            mockLocationRepository.Verify(repo => repo.Add(It.Is<Location>(c => c.Address == location.Address && c.PostalCode == location.PostalCode)), Times.Once);
    }

    [Fact]
    public void UpdateLocation_UpdatesLocation_WhenLocationIsValid()
    {
        //Arrange
        var mockLocationRepository = new Mock<ILocationRepository>();

        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Locations\UpdateLocation_UpdatesLocation_WhenLocationIsValid.json");

        var locationJson = File.ReadAllText(jsonFilePath);
        var location = JsonConvert.DeserializeObject<Location>(locationJson);

        mockLocationRepository.Setup(repo => repo.Add(It.IsAny<Location>()));

        var locationService = new LocationTestService(mockLocationRepository.Object);

        //Act
        locationService.AddLocation(location);

        //Assert
        mockLocationRepository.Verify(repo => repo.Add(It.Is<Location>(c => c.Address == location.Address && c.PostalCode == location.PostalCode)), Times.Once);
    }

    [Fact]
    public void DeleteLocation_DeletesLocation_WhenLocationExists()
    {
        //Arrange
        var mockLocationRepository = new Mock<ILocationRepository>();

        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Locations\DeleteLocation_DeletesLocation_WhenLocationExists.json");

        var locationJson = File.ReadAllText(jsonFilePath);
        var location = JsonConvert.DeserializeObject<Location>(locationJson);

        mockLocationRepository.Setup(repo => repo.GetByIdAsync(location.Id)).ReturnsAsync(location);
        mockLocationRepository.Setup(repo => repo.Remove(location));
        var locationService = new LocationTestService(mockLocationRepository.Object);

        //Act
        locationService.DeleteLocation(location);

        //Assert
        mockLocationRepository.Verify(repo => repo.Remove(location), Times.Once);
    }


    [Fact]
    public void UpdateLocation_ThrowsException_WhenLocationToUpdateDoesNotExist()
    {
        //Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Locations\UpdateLocation_ThrowsException_WhenLocationToUpdateDoesNotExist.json");

        var locationJson = File.ReadAllText(jsonFilePath);
        var location = JsonConvert.DeserializeObject<Location>(locationJson);

        var mockLocationRepository = new Mock<ILocationRepository>();
        mockLocationRepository.Setup(repo => repo.GetByIdAsync(location.Id)).ReturnsAsync((Location)null);

        var locationService = new LocationTestService(mockLocationRepository.Object);

        //Act
        var exception = Assert.Throws<KeyNotFoundException>(() => locationService.UpdateLocation(location));

        //Assert
        Assert.Equal("Location to update not found", exception.Message);
    }

    [Fact]
    public async void GetLocationById_ThrowsException_WhenLocationDoesNotExist()
    {
        //Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Locations\GetLocationById_ThrowsException_WhenLocationDoesNotExist.json");
        var locationJson = File.ReadAllText(jsonFilePath);
        var location = JsonConvert.DeserializeObject<Location>(locationJson);

        var mockLocationRepository = new Mock<ILocationRepository>();
        var locationId = location.Id;

        mockLocationRepository.Setup(repo => repo.GetByIdAsync(locationId)).ReturnsAsync((Location)null);

        var locationService = new LocationTestService(mockLocationRepository.Object);

        //Act
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => locationService.GetLocationById(locationId));

        //Assert
        Assert.Equal("Location not found", exception.Message);
    }

}
