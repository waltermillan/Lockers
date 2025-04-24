using API.Controllers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;

namespace Tests.UnitTests;
public class RoleControllerTests
{
    private readonly RolesController _controller;
    private readonly Mock<IUnitOfWork> _mockRepo;
    private readonly IMapper _mapper;

    public RoleControllerTests()
    {
        _mockRepo = new Mock<IUnitOfWork>();
        _controller = new RolesController(_mockRepo.Object, _mapper);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenRoleDoesNotExist()
    {
        // Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Roles\GetById_ReturnsNotFound_WhenRoleDoesNotExist.json");
        var json = File.ReadAllText(jsonFilePath);

        var role = JsonConvert.DeserializeObject<Role>(json);

        _mockRepo.Setup(repo => repo.Roles.GetByIdAsync(role.Id)).ReturnsAsync((Role)null);

        // Act
        var result = await _controller.Get(role.Id);

        // Assert
        var actionResult = Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetById_ReturnsRole_WhenRoleExists()
    {
        // Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Roles\GetById_ReturnsRole_WhenRoleExists.json");
        var json = File.ReadAllText(jsonFilePath);

        var role = JsonConvert.DeserializeObject<Role>(json);

        var mockServerRepository = new Mock<IRoleRepository>();
        mockServerRepository.Setup(repo => repo.GetByIdAsync(role.Id)).ReturnsAsync(role);

        var roleService = new RoleTestService(mockServerRepository.Object);

        // Act
        var result = await roleService.GetRoleById(role.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(role.Id, result.Id);
        Assert.Equal(role.Description, result.Description);
    }

    [Fact]
    public void AddRole_AddsRole_WhenRoleIsValid()
    {
        // Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Roles\AddRole_AddsRole_WhenRoleIsValid.json");
        var json = File.ReadAllText(jsonFilePath);

        var role = JsonConvert.DeserializeObject<Role>(json);

        var mockRoleRepository = new Mock<IRoleRepository>();
        mockRoleRepository.Setup(repo => repo.Add(It.IsAny<Role>()));

        var roleService = new RoleTestService(mockRoleRepository.Object);

        // Act
        roleService.AddRole(role);

        // Assert
        mockRoleRepository.Verify(repo => repo.Add(It.Is<Role>(c => c.Description == role.Description)), Times.Once);
    }


    [Fact]
    public void AddRoles_AddsRoles_WhenRolesIsValid()
    {
        //Arrange
        var mockRoleRepository = new Mock<IRoleRepository>();

        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Roles\AddRoles_AddsRoles_WhenRolesIsValid.json");

        var rolesJson = File.ReadAllText(jsonFilePath);
        var roles = JsonConvert.DeserializeObject<List<Role>>(rolesJson);

        mockRoleRepository.Setup(repo => repo.Add(It.IsAny<Role>()));

        var roleService = new RoleTestService(mockRoleRepository.Object);

        //Act
        foreach (var role in roles)
            roleService.AddRole(role);

        //Assert
        foreach (var role in roles)
            mockRoleRepository.Verify(repo => repo.Add(It.Is<Role>(c => c.Description == role.Description)), Times.Once);
    }

    [Fact]
    public void UpdateRole_UpdatesRole_WhenRoleIsValid()
    {
        //Arrange
        var mockRoleRepository = new Mock<IRoleRepository>();

        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Roles\UpdateRole_UpdatesRole_WhenRoleIsValid.json");

        var roleJson = File.ReadAllText(jsonFilePath);
        var role = JsonConvert.DeserializeObject<Role>(roleJson);

        mockRoleRepository.Setup(repo => repo.Add(It.IsAny<Role>()));

        var roleService = new RoleTestService(mockRoleRepository.Object);

        //Act
        roleService.AddRole(role);

        //Assert
        mockRoleRepository.Verify(repo => repo.Add(It.Is<Role>(c => c.Description == role.Description)), Times.Once);
    }

    [Fact]
    public void DeleteRole_DeletesRole_WhenRoleExists()
    {
        //Arrange
        var mockRoleRepository = new Mock<IRoleRepository>();

        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Roles\DeleteRole_DeletesRole_WhenRoleExists.json");

        var roleJson = File.ReadAllText(jsonFilePath);
        var role = JsonConvert.DeserializeObject<Role>(roleJson);

        mockRoleRepository.Setup(repo => repo.GetByIdAsync(role.Id)).ReturnsAsync(role);
        mockRoleRepository.Setup(repo => repo.Remove(role));
        var roleService = new RoleTestService(mockRoleRepository.Object);

        //Act
        roleService.DeleteRole(role);

        //Assert
        mockRoleRepository.Verify(repo => repo.Remove(role), Times.Once);
    }


    [Fact]
    public void UpdateRole_ThrowsException_WhenRoleToUpdateDoesNotExist()
    {
        //Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Roles\UpdateRole_ThrowsException_WhenRoleToUpdateDoesNotExist.json");

        var roleJson = File.ReadAllText(jsonFilePath);
        var role = JsonConvert.DeserializeObject<Role>(roleJson);

        var mockRoleRepository = new Mock<IRoleRepository>();
        mockRoleRepository.Setup(repo => repo.GetByIdAsync(role.Id)).ReturnsAsync((Role)null);

        var roleService = new RoleTestService(mockRoleRepository.Object);

        //Act
        var exception = Assert.Throws<KeyNotFoundException>(() => roleService.UpdateRole(role));

        //Assert
        Assert.Equal("Role to update not found", exception.Message);
    }

    [Fact]
    public async void GetRoleById_ThrowsException_WhenRoleDoesNotExist()
    {
        //Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Roles\GetRoleById_ThrowsException_WhenRoleDoesNotExist.json");
        var roleJson = File.ReadAllText(jsonFilePath);
        var role = JsonConvert.DeserializeObject<Role>(roleJson);

        var mockRoleRepository = new Mock<IRoleRepository>();
        var roleId = role.Id;

        mockRoleRepository.Setup(repo => repo.GetByIdAsync(roleId)).ReturnsAsync((Role)null);

        var roleService = new RoleTestService(mockRoleRepository.Object);

        //Act
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => roleService.GetRoleById(roleId));

        //Assert
        Assert.Equal("Role not found", exception.Message);
    }

}
