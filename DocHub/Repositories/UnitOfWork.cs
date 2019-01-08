using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocHub.Models;

namespace DocHub.Repositories
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private DBContext context = null;
        private IGenericRepository<Document> documentRepository;
        private IGenericRepository<ApplicationUser> userRepository;

        public UnitOfWork()
        {
            context = new DBContext();
        }

        public DBContext Context
        {
            get { return context; }
        }

        public IGenericRepository<Document> DocumentRepository
        {
            get
            {
                return documentRepository = documentRepository ?? new GenericRepository<Document>(context);
            }
        }

        public IGenericRepository<ApplicationUser> UserRepository
        {
            get
            {
                return userRepository = userRepository ?? new GenericRepository<ApplicationUser>(context);
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

    }
}