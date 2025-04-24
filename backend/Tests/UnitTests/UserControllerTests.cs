using API.Controllers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;

namespace Tests.UnitTests;
public class UserControllerTests
{
    private readonly UsersController _controller;
    private readonly Mock<IUnitOfWork> _mockRepo;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;

    public UserControllerTests()
    {
        _mockRepo = new Mock<IUnitOfWork>();
        _controller = new UsersController(_mockRepo.Object, _mapper, _passwordHasher);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Users\GetById_ReturnsNotFound_WhenUserDoesNotExist.json");
        var json = File.ReadAllText(jsonFilePath);

        var user = JsonConvert.DeserializeObject<User>(json);

        _mockRepo.Setup(repo => repo.Users.GetByIdAsync(user.Id)).ReturnsAsync((User)null);

        // Act
        var result = await _controller.Get(user.Id);

        // Assert
        var actionResult = Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetById_ReturnsUser_WhenUserExists()
    {
        // Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Users\GetById_ReturnsUser_WhenUserExists.json");
        var json = File.ReadAllText(jsonFilePath);

        var user = JsonConvert.DeserializeObject<User>(json);

        var mockServerRepository = new Mock<IUserRepository>();
        mockServerRepository.Setup(repo => repo.GetByIdAsync(user.Id)).ReturnsAsync(user);

        var userService = new UserTestService(mockServerRepository.Object);

        // Act
        var result = await userService.GetUserById(user.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result.Id);
        Assert.Equal(user.UserName, result.UserName);
        Assert.Equal(user.Password, result.Password);
        Assert.Equal(user.IdPerfil, result.IdPerfil);
    }

    [Fact]
    public void AddUser_AddsUser_WhenUserIsValid()
    {
        // Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Users\AddUser_AddsUser_WhenUserIsValid.json");
        var json = File.ReadAllText(jsonFilePath);

        var user = JsonConvert.DeserializeObject<User>(json);

        var mockUserRepository = new Mock<IUserRepository>();
        mockUserRepository.Setup(repo => repo.Add(It.IsAny<User>()));

        var userService = new UserTestService(mockUserRepository.Object);

        // Act
        userService.AddUser(user);

        // Assert
        mockUserRepository.Verify(repo => repo.Add(It.Is<User>(c => c.UserName == user.UserName && c.IdPerfil == user.IdPerfil)), Times.Once);
    }


    [Fact]
    public void AddUsers_AddsUsers_WhenUsersIsValid()
    {
        //Arrange
        var mockUserRepository = new Mock<IUserRepository>();

        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Users\AddUsers_AddsUsers_WhenUsersIsValid.json");

        var usersJson = File.ReadAllText(jsonFilePath);
        var users = JsonConvert.DeserializeObject<List<User>>(usersJson);

        mockUserRepository.Setup(repo => repo.Add(It.IsAny<User>()));

        var userService = new UserTestService(mockUserRepository.Object);

        //Act
        foreach (var user in users)
            userService.AddUser(user);

        //Assert
        foreach (var user in users)
            mockUserRepository.Verify(repo => repo.Add(It.Is<User>(c => c.UserName == user.UserName && c.IdPerfil == user.IdPerfil)), Times.Once);
    }

    [Fact]
    public void UpdateUser_UpdatesUser_WhenUserIsValid()
    {
        //Arrange
        var mockUserRepository = new Mock<IUserRepository>();

        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Users\UpdateUser_UpdatesUser_WhenUserIsValid.json");

        var userJson = File.ReadAllText(jsonFilePath);
        var user = JsonConvert.DeserializeObject<User>(userJson);

        mockUserRepository.Setup(repo => repo.Add(It.IsAny<User>()));

        var userService = new UserTestService(mockUserRepository.Object);

        //Act
        userService.AddUser(user);

        //Assert
        mockUserRepository.Verify(repo => repo.Add(It.Is<User>(c => c.UserName == user.UserName && c.IdPerfil == user.IdPerfil)), Times.Once);
    }

    [Fact]
    public void DeleteUser_DeletesUser_WhenUserExists()
    {
        //Arrange
        var mockUserRepository = new Mock<IUserRepository>();

        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Users\DeleteUser_DeletesUser_WhenUserExists.json");

        var userJson = File.ReadAllText(jsonFilePath);
        var user = JsonConvert.DeserializeObject<User>(userJson);

        mockUserRepository.Setup(repo => repo.GetByIdAsync(user.Id)).ReturnsAsync(user);
        mockUserRepository.Setup(repo => repo.Remove(user));
        var userService = new UserTestService(mockUserRepository.Object);

        //Act
        userService.DeleteUser(user);

        //Assert
        mockUserRepository.Verify(repo => repo.Remove(user), Times.Once);
    }


    [Fact]
    public void UpdateUser_ThrowsException_WhenUserToUpdateDoesNotExist()
    {
        //Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Users\UpdateUser_ThrowsException_WhenUserToUpdateDoesNotExist.json");

        var userJson = File.ReadAllText(jsonFilePath);
        var user = JsonConvert.DeserializeObject<User>(userJson);

        var mockUserRepository = new Mock<IUserRepository>();
        mockUserRepository.Setup(repo => repo.GetByIdAsync(user.Id)).ReturnsAsync((User)null);

        var userService = new UserTestService(mockUserRepository.Object);

        //Act
        var exception = Assert.Throws<KeyNotFoundException>(() => userService.UpdateUser(user));

        //Assert
        Assert.Equal("User to update not found", exception.Message);
    }

    [Fact]
    public async void GetUserById_ThrowsException_WhenUserDoesNotExist()
    {
        //Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Users\GetUserById_ThrowsException_WhenUserDoesNotExist.json");
        var userJson = File.ReadAllText(jsonFilePath);
        var user = JsonConvert.DeserializeObject<User>(userJson);

        var mockUserRepository = new Mock<IUserRepository>();
        var userId = user.Id;

        mockUserRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync((User)null);

        var userService = new UserTestService(mockUserRepository.Object);

        //Act
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => userService.GetUserById(userId));

        //Assert
        Assert.Equal("User not found", exception.Message);
    }

}
