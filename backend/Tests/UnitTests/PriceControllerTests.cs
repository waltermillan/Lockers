using API.Controllers;
using AutoMapper;
using Core.Entities;
using Core.Interfases;
using Core.Services.Tests;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;

namespace Tests.UnitTests;
public class PriceControllerTests
{
    private readonly PricesController _controller;
    private readonly Mock<IUnitOfWork> _mockRepo;
    private readonly IMapper _mapper;

    public PriceControllerTests()
    {
        _mockRepo = new Mock<IUnitOfWork>();
        _controller = new PricesController(_mockRepo.Object, _mapper);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenPriceDoesNotExist()
    {
        // Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Prices\GetById_ReturnsNotFound_WhenPriceDoesNotExist.json");
        var json = File.ReadAllText(jsonFilePath);

        var price = JsonConvert.DeserializeObject<Price>(json);

        _mockRepo.Setup(repo => repo.Prices.GetByIdAsync(price.Id)).ReturnsAsync((Price)null);

        // Act
        var result = await _controller.Get(price.Id);

        // Assert
        var actionResult = Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetById_ReturnsPrice_WhenPriceExists()
    {
        // Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Prices\GetById_ReturnsPrice_WhenPriceExists.json");
        var json = File.ReadAllText(jsonFilePath);

        var price = JsonConvert.DeserializeObject<Price>(json);

        var mockServerRepository = new Mock<IPriceRepository>();
        mockServerRepository.Setup(repo => repo.GetByIdAsync(price.Id)).ReturnsAsync(price);

        var priceService = new PriceTestService(mockServerRepository.Object);

        // Act
        var result = await priceService.GetPriceById(price.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(price.Id, result.Id);
        Assert.Equal(price.Value, result.Value);
    }

    [Fact]
    public void AddPrice_AddsPrice_WhenPriceIsValid()
    {
        // Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Prices\AddPrice_AddsPrice_WhenPriceIsValid.json");
        var json = File.ReadAllText(jsonFilePath);

        var price = JsonConvert.DeserializeObject<Price>(json);

        var mockPriceRepository = new Mock<IPriceRepository>();
        mockPriceRepository.Setup(repo => repo.Add(It.IsAny<Price>()));

        var priceService = new PriceTestService(mockPriceRepository.Object);

        // Act
        priceService.AddPrice(price);

        // Assert
        mockPriceRepository.Verify(repo => repo.Add(It.Is<Price>(c => c.Value == price.Value)), Times.Once);
    }


    [Fact]
    public void AddPrices_AddsPrices_WhenPricesIsValid()
    {
        //Arrange
        var mockPriceRepository = new Mock<IPriceRepository>();

        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Prices\AddPrices_AddsPrices_WhenPricesIsValid.json");

        var pricesJson = File.ReadAllText(jsonFilePath);
        var prices = JsonConvert.DeserializeObject<List<Price>>(pricesJson);

        mockPriceRepository.Setup(repo => repo.Add(It.IsAny<Price>()));

        var priceService = new PriceTestService(mockPriceRepository.Object);

        //Act
        foreach (var price in prices)
            priceService.AddPrice(price);

        //Assert
        foreach (var price in prices)
            mockPriceRepository.Verify(repo => repo.Add(It.Is<Price>(c => c.Value == price.Value)), Times.Once);
    }

    [Fact]
    public void UpdatePrice_UpdatesPrice_WhenPriceIsValid()
    {
        //Arrange
        var mockPriceRepository = new Mock<IPriceRepository>();

        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Prices\UpdatePrice_UpdatesPrice_WhenPriceIsValid.json");

        var priceJson = File.ReadAllText(jsonFilePath);
        var price = JsonConvert.DeserializeObject<Price>(priceJson);

        mockPriceRepository.Setup(repo => repo.Add(It.IsAny<Price>()));

        var priceService = new PriceTestService(mockPriceRepository.Object);

        //Act
        priceService.AddPrice(price);

        //Assert
        mockPriceRepository.Verify(repo => repo.Add(It.Is<Price>(c => c.Value == price.Value)), Times.Once);
    }

    [Fact]
    public void DeletePrice_DeletesPrice_WhenPriceExists()
    {
        //Arrange
        var mockPriceRepository = new Mock<IPriceRepository>();

        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Prices\DeletePrice_DeletesPrice_WhenPriceExists.json");

        var priceJson = File.ReadAllText(jsonFilePath);
        var price = JsonConvert.DeserializeObject<Price>(priceJson);

        mockPriceRepository.Setup(repo => repo.GetByIdAsync(price.Id)).ReturnsAsync(price);
        mockPriceRepository.Setup(repo => repo.Remove(price));
        var priceService = new PriceTestService(mockPriceRepository.Object);

        //Act
        priceService.DeletePrice(price);

        //Assert
        mockPriceRepository.Verify(repo => repo.Remove(price), Times.Once);
    }


    [Fact]
    public void UpdatePrice_ThrowsException_WhenPriceToUpdateDoesNotExist()
    {
        //Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Prices\UpdatePrice_ThrowsException_WhenPriceToUpdateDoesNotExist.json");

        var priceJson = File.ReadAllText(jsonFilePath);
        var price = JsonConvert.DeserializeObject<Price>(priceJson);

        var mockPriceRepository = new Mock<IPriceRepository>();
        mockPriceRepository.Setup(repo => repo.GetByIdAsync(price.Id)).ReturnsAsync((Price)null);

        var priceService = new PriceTestService(mockPriceRepository.Object);

        //Act
        var exception = Assert.Throws<KeyNotFoundException>(() => priceService.UpdatePrice(price));

        //Assert
        Assert.Equal("Price to update not found", exception.Message);
    }

    [Fact]
    public async void GetPriceById_ThrowsException_WhenPriceDoesNotExist()
    {
        //Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Prices\GetPriceById_ThrowsException_WhenPriceDoesNotExist.json");
        var priceJson = File.ReadAllText(jsonFilePath);
        var price = JsonConvert.DeserializeObject<Price>(priceJson);

        var mockPriceRepository = new Mock<IPriceRepository>();
        var priceId = price.Id;

        mockPriceRepository.Setup(repo => repo.GetByIdAsync(priceId)).ReturnsAsync((Price)null);

        var priceService = new PriceTestService(mockPriceRepository.Object);

        //Act
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => priceService.GetPriceById(priceId));

        //Assert
        Assert.Equal("Price not found", exception.Message);
    }

}
