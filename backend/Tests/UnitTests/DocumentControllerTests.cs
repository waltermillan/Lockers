using API.Controllers;
using AutoMapper;
using Core.Entities;
using Core.Interfases;
using Core.Services.Tests;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;

namespace Tests.UnitTests;
public class DocumentControllerTests
{
    private readonly DocumentsController _controller;
    private readonly Mock<IUnitOfWork> _mockRepo;
    private readonly IMapper _mapper;

    public DocumentControllerTests()
    {
        _mockRepo = new Mock<IUnitOfWork>();
        _controller = new DocumentsController(_mockRepo.Object, _mapper);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenDocumentDoesNotExist()
    {
        // Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Documents\GetById_ReturnsNotFound_WhenDocumentDoesNotExist.json");
        var json = File.ReadAllText(jsonFilePath);

        var document = JsonConvert.DeserializeObject<Document>(json);

        _mockRepo.Setup(repo => repo.Documents.GetByIdAsync(document.Id)).ReturnsAsync((Document)null);

        // Act
        var result = await _controller.Get(document.Id);

        // Assert
        var actionResult = Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetById_ReturnsDocument_WhenDocumentExists()
    {
        // Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Documents\GetById_ReturnsDocument_WhenDocumentExists.json");
        var json = File.ReadAllText(jsonFilePath);

        var document = JsonConvert.DeserializeObject<Document>(json);

        var mockServerRepository = new Mock<IDocumentRepository>();
        mockServerRepository.Setup(repo => repo.GetByIdAsync(document.Id)).ReturnsAsync(document);

        var documentService = new DocumentTestService(mockServerRepository.Object);

        // Act
        var result = await documentService.GetDocumentById(document.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(document.Id, result.Id);
        Assert.Equal(document.Description, result.Description);
    }

    [Fact]
    public void AddDocument_AddsDocument_WhenDocumentIsValid()
    {
        // Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Documents\AddDocument_AddsDocument_WhenDocumentIsValid.json");
        var json = File.ReadAllText(jsonFilePath);

        var document = JsonConvert.DeserializeObject<Document>(json);

        var mockDocumentRepository = new Mock<IDocumentRepository>();
        mockDocumentRepository.Setup(repo => repo.Add(It.IsAny<Document>()));

        var documentService = new DocumentTestService(mockDocumentRepository.Object);

        // Act
        documentService.AddDocument(document);

        // Assert
        mockDocumentRepository.Verify(repo => repo.Add(It.Is<Document>(c => c.Description == document.Description)), Times.Once);
    }


    [Fact]
    public void AddDocuments_AddsDocuments_WhenDocumentsIsValid()
    {
        //Arrange
        var mockDocumentRepository = new Mock<IDocumentRepository>();

        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Documents\AddDocuments_AddsDocuments_WhenDocumentsIsValid.json");

        var documentsJson = File.ReadAllText(jsonFilePath);
        var documents = JsonConvert.DeserializeObject<List<Document>>(documentsJson);

        mockDocumentRepository.Setup(repo => repo.Add(It.IsAny<Document>()));

        var documentService = new DocumentTestService(mockDocumentRepository.Object);

        //Act
        foreach (var document in documents)
            documentService.AddDocument(document);

        //Assert
        foreach (var document in documents)
            mockDocumentRepository.Verify(repo => repo.Add(It.Is<Document>(c => c.Description == document.Description)), Times.Once);
    }

    [Fact]
    public void UpdateDocument_UpdatesDocument_WhenDocumentIsValid()
    {
        //Arrange
        var mockDocumentRepository = new Mock<IDocumentRepository>();

        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Documents\UpdateDocument_UpdatesDocument_WhenDocumentIsValid.json");

        var documentJson = File.ReadAllText(jsonFilePath);
        var document = JsonConvert.DeserializeObject<Document>(documentJson);

        mockDocumentRepository.Setup(repo => repo.Add(It.IsAny<Document>()));

        var documentService = new DocumentTestService(mockDocumentRepository.Object);

        //Act
        documentService.AddDocument(document);

        //Assert
        mockDocumentRepository.Verify(repo => repo.Add(It.Is<Document>(c => c.Description == document.Description)), Times.Once);
    }

    [Fact]
    public void DeleteDocument_DeletesDocument_WhenDocumentExists()
    {
        //Arrange
        var mockDocumentRepository = new Mock<IDocumentRepository>();

        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Documents\DeleteDocument_DeletesDocument_WhenDocumentExists.json");

        var documentJson = File.ReadAllText(jsonFilePath);
        var document = JsonConvert.DeserializeObject<Document>(documentJson);

        mockDocumentRepository.Setup(repo => repo.GetByIdAsync(document.Id)).ReturnsAsync(document);
        mockDocumentRepository.Setup(repo => repo.Remove(document));
        var documentService = new DocumentTestService(mockDocumentRepository.Object);

        //Act
        documentService.DeleteDocument(document);

        //Assert
        mockDocumentRepository.Verify(repo => repo.Remove(document), Times.Once);
    }


    [Fact]
    public void UpdateDocument_ThrowsException_WhenDocumentToUpdateDoesNotExist()
    {
        //Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Documents\UpdateDocument_ThrowsException_WhenDocumentToUpdateDoesNotExist.json");

        var documentJson = File.ReadAllText(jsonFilePath);
        var document = JsonConvert.DeserializeObject<Document>(documentJson);

        var mockDocumentRepository = new Mock<IDocumentRepository>();
        mockDocumentRepository.Setup(repo => repo.GetByIdAsync(document.Id)).ReturnsAsync((Document)null);

        var documentService = new DocumentTestService(mockDocumentRepository.Object);

        //Act
        var exception = Assert.Throws<KeyNotFoundException>(() => documentService.UpdateDocument(document));

        //Assert
        Assert.Equal("Document to update not found", exception.Message);
    }

    [Fact]
    public async void GetDocumentById_ThrowsException_WhenDocumentDoesNotExist()
    {
        //Arrange
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Documents\GetDocumentById_ThrowsException_WhenDocumentDoesNotExist.json");
        var documentJson = File.ReadAllText(jsonFilePath);
        var document = JsonConvert.DeserializeObject<Document>(documentJson);

        var mockDocumentRepository = new Mock<IDocumentRepository>();
        var documentId = document.Id;

        mockDocumentRepository.Setup(repo => repo.GetByIdAsync(documentId)).ReturnsAsync((Document)null);

        var documentService = new DocumentTestService(mockDocumentRepository.Object);

        //Act
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => documentService.GetDocumentById(documentId));

        //Assert
        Assert.Equal("Document not found", exception.Message);
    }

}
