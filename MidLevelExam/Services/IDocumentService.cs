using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocHub.Models;

namespace DocHub.Services
{
    public interface IDocumentService
    {
        bool Update(int ID, string name, string text);
        bool Add(string name, string text, string createdBy);
        Document GetDocumentById(int ID);
        IEnumerable<Document> GetDocuments();

    }
}