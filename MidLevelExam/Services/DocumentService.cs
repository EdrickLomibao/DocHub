using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocHub.Repositories;
using DocHub.Models;

namespace DocHub.Services
{
    public class DocumentService : IDocumentService
    {
        private IUnitOfWork unitOfWork;

        public DocumentService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = new UnitOfWork();
        }

        public bool Update(int ID,string name, string text)
        {
            var doc = new Document();
            doc.ID = ID;
            doc.FileName = name;
            doc.Text = text;
            doc.UpdatedDate = DateTime.Now;

            unitOfWork.DocumentRepository.Update(doc);
            return unitOfWork.SaveChanges() > 0;

        }

        public bool Add(string name, string text, string createdBy)
        {
            var doc = new Document();
            doc.FileName = name;
            doc.Text = text;
            doc.CreatedBy = createdBy;
            doc.CreatedDate = DateTime.Now;

            unitOfWork.DocumentRepository.Add(doc);
            return unitOfWork.SaveChanges() > 0;

        }

        public Document GetDocumentById(int ID)
        {
            var result = unitOfWork.DocumentRepository.Find(x => x.ID == ID);
            return result;
        }

        public IEnumerable<Document> GetDocuments()
        {
            var result = unitOfWork.DocumentRepository.SelectAll().ToList();
            return result;
        }

    }
}