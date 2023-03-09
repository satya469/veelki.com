using System;

namespace Veelki.Data.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
    }
}