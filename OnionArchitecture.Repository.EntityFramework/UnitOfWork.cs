using System;
using OnionArchitecture.Core.Infrastructure.Repositories;

namespace OnionArchitecture.Repository.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;
        private readonly IDbContext _context;

        public UnitOfWork(IDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.Commit();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }
    }
}
