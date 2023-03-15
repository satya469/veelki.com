using System.Transactions;

namespace Veelki.Data.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TransactionScope _transaction;

        private bool _isDisposed;

        public UnitOfWork(bool async = false)
        {
            _transaction = async
                ? new TransactionScope(TransactionScopeAsyncFlowOption.Enabled)
                : new TransactionScope();
        }

        public void Commit() => _transaction?.Complete();
        public void Rollback() => _transaction?.Dispose();

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (isDisposing)
            {
                _transaction?.Dispose();
            }

            _isDisposed = true;
        }
    }
}
