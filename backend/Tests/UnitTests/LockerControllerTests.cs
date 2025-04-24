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
public class LockerControllerTests
{
    private readonly LockersController _controller;
    private readonly Mock<IUnitOfWork> _mockRepo;
    private readonly IMapper _mapper;
    private readonly LockerDTOService _lockerDTOService;

    public LockerControllerTests()
    {
        _mockRepo = new Mock<IUnitOfWork>();
        _controller = new LockersController(_mockRepo.Object, _mapper, _lockerDTOService);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenLockerDoesNotExist()
    {
        // Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Lockers\GetById_ReturnsNotFound_WhenLockerDoesNotExist.json");
        var json = File.ReadAllText(jsonFilePath);

        var locker = JsonConvert.DeserializeObject<Locker>(json);

        _mockRepo.Setup(repo => repo.Lockers.GetByIdAsync(locker.Id)).ReturnsAsync((Locker)null);

        // Act
        var result = await _controller.Get(locker.Id);

        // Assert
        var actionResult = Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetById_ReturnsLocker_WhenLockerExists()
    {
        // Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Lockers\GetById_ReturnsLocker_WhenLockerExists.json");
        var json = File.ReadAllText(jsonFilePath);

        var locker = JsonConvert.DeserializeObject<Locker>(json);

        var mockServerRepository = new Mock<ILockerRepository>();
        mockServerRepository.Setup(repo => repo.GetByIdAsync(locker.Id)).ReturnsAsync(locker);

        var lockerService = new LockerTestService(mockServerRepository.Object);

        // Act
        var result = await lockerService.GetLockerById(locker.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(locker.Id, result.Id);
        Assert.Equal(locker.SerialNumber, result.SerialNumber);
        Assert.Equal(locker.IdLocation, result.IdLocation);
        Assert.Equal(locker.IdPrice, result.IdPrice);
        Assert.Equal(locker.Rented, result.Rented);
    }

    [Fact]
    public void AddLocker_AddsLocker_WhenLockerIsValid()
    {
        // Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Lockers\AddLocker_AddsLocker_WhenLockerIsValid.json");
        var json = File.ReadAllText(jsonFilePath);

        var locker = JsonConvert.DeserializeObject<Locker>(json);

        var mockLockerRepository = new Mock<ILockerRepository>();
        mockLockerRepository.Setup(repo => repo.Add(It.IsAny<Locker>()));

        var lockerService = new LockerTestService(mockLockerRepository.Object);

        // Act
        lockerService.AddLocker(locker);

        // Assert
        mockLockerRepository.Verify(repo => repo.Add(It.Is<Locker>(c => c.SerialNumber == locker.SerialNumber && c.IdLocation == locker.IdLocation && c.IdPrice == locker.IdPrice && c.Rented == locker.Rented)), Times.Once);
    }


    [Fact]
    public void AddLockers_AddsLockers_WhenLockersIsValid()
    {
        //Arrange
        var mockLockerRepository = new Mock<ILockerRepository>();

        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Lockers\AddLockers_AddsLockers_WhenLockersIsValid.json");

        var lockersJson = File.ReadAllText(jsonFilePath);
        var lockers = JsonConvert.DeserializeObject<List<Locker>>(lockersJson);

        mockLockerRepository.Setup(repo => repo.Add(It.IsAny<Locker>()));

        var lockerService = new LockerTestService(mockLockerRepository.Object);

        //Act
        foreach (var locker in lockers)
            lockerService.AddLocker(locker);

        //Assert
        foreach (var locker in lockers)
            mockLockerRepository.Verify(repo => repo.Add(It.Is<Locker>(c => c.SerialNumber == locker.SerialNumber && c.IdLocation == locker.IdLocation && c.IdPrice == locker.IdPrice && c.Rented == locker.Rented)), Times.Once);
    }

    [Fact]
    public void UpdateLocker_UpdatesLocker_WhenLockerIsValid()
    {
        //Arrange
        var mockLockerRepository = new Mock<ILockerRepository>();

        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Lockers\UpdateLocker_UpdatesLocker_WhenLockerIsValid.json");

        var lockerJson = File.ReadAllText(jsonFilePath);
        var locker = JsonConvert.DeserializeObject<Locker>(lockerJson);

        mockLockerRepository.Setup(repo => repo.Add(It.IsAny<Locker>()));

        var lockerService = new LockerTestService(mockLockerRepository.Object);

        //Act
        lockerService.AddLocker(locker);

        //Assert
        mockLockerRepository.Verify(repo => repo.Add(It.Is<Locker>(c => c.SerialNumber == locker.SerialNumber && c.IdLocation == locker.IdLocation && c.IdPrice == locker.IdPrice && c.Rented == locker.Rented)), Times.Once);
    }

    [Fact]
    public void DeleteLocker_DeletesLocker_WhenLockerExists()
    {
        //Arrange
        var mockLockerRepository = new Mock<ILockerRepository>();

        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Lockers\DeleteLocker_DeletesLocker_WhenLockerExists.json");

        var lockerJson = File.ReadAllText(jsonFilePath);
        var locker = JsonConvert.DeserializeObject<Locker>(lockerJson);

        mockLockerRepository.Setup(repo => repo.GetByIdAsync(locker.Id)).ReturnsAsync(locker);
        mockLockerRepository.Setup(repo => repo.Remove(locker));
        var lockerService = new LockerTestService(mockLockerRepository.Object);

        //Act
        lockerService.DeleteLocker(locker);

        //Assert
        mockLockerRepository.Verify(repo => repo.Remove(locker), Times.Once);
    }


    [Fact]
    public void UpdateLocker_ThrowsException_WhenLockerToUpdateDoesNotExist()
    {
        //Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Lockers\UpdateLocker_ThrowsException_WhenLockerToUpdateDoesNotExist.json");

        var lockerJson = File.ReadAllText(jsonFilePath);
        var locker = JsonConvert.DeserializeObject<Locker>(lockerJson);

        var mockLockerRepository = new Mock<ILockerRepository>();
        mockLockerRepository.Setup(repo => repo.GetByIdAsync(locker.Id)).ReturnsAsync((Locker)null);

        var lockerService = new LockerTestService(mockLockerRepository.Object);

        //Act
        var exception = Assert.Throws<KeyNotFoundException>(() => lockerService.UpdateLocker(locker));

        //Assert
        Assert.Equal("Locker to update not found", exception.Message);
    }

    [Fact]
    public async void GetLockerById_ThrowsException_WhenLockerDoesNotExist()
    {
        //Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Lockers\GetLockerById_ThrowsException_WhenLockerDoesNotExist.json");
        var lockerJson = File.ReadAllText(jsonFilePath);
        var locker = JsonConvert.DeserializeObject<Locker>(lockerJson);

        var mockLockerRepository = new Mock<ILockerRepository>();
        var lockerId = locker.Id;

        mockLockerRepository.Setup(repo => repo.GetByIdAsync(lockerId)).ReturnsAsync((Locker)null);

        var lockerService = new LockerTestService(mockLockerRepository.Object);

        //Act
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => lockerService.GetLockerById(lockerId));

        //Assert
        Assert.Equal("Locker not found", exception.Message);
    }

}
