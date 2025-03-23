using Core.Entities;
using Core.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Tests;

public class DocumentTestService
{
    private readonly IDocumentRepository _documentRepository;

    public DocumentTestService(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<Document> GetDocumentById(int id)
    {
        var document = await _documentRepository.GetByIdAsync(id);

        if (document is null)
            throw new KeyNotFoundException("Document not found");

        return document;
    }

    public async Task<IEnumerable<Document>> GetAllDocuments()
    {
        return await _documentRepository.GetAllAsync();
    }

    public void AddDocument(Document document)
    {
        _documentRepository.Add(document);
    }

    public void AddDocuments(IEnumerable<Document> documents)
    {
        foreach (var document in documents)
            _documentRepository.Add(document);
    }

    public void UpdateDocument(Document document)
    {
        var exists = _documentRepository.GetByIdAsync(document.Id).Result;

        if (exists is null)
            throw new KeyNotFoundException("Document to update not found");

        _documentRepository.Update(document);
    }

    public void DeleteDocument(Document document)
    {
        var exists = _documentRepository.GetByIdAsync(document.Id).Result;

        if (exists is null)
            throw new KeyNotFoundException("Document not found");

        _documentRepository.Remove(document);
    }
}
