using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocHub.Models;

namespace DocHub.Repositories
{
    public interface IUnitOfWork
    {
        DBContext Context { get; }
        IGenericRepository<Document> DocumentRepository { get; }
        IGenericRepository<ApplicationUser> UserRepository { get; }

        int SaveChanges();
        void Dispose();

    }
}
