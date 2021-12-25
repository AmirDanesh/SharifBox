using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SharifBox.Repository
{
    public interface IUnitOfWork
    {
        //public C Context { get; }

        Task CommitAsync();
    }

    public class UnitOfWork<C> : IUnitOfWork where C : DbContext
    {
        protected C Context { get; }

        public UnitOfWork(C context)
        {
            Context = context;
        }

        public async Task CommitAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}