using System.Linq.Expressions;

namespace BusinessMotors.Infrastructure.Repositories
{
     public class Repository<T> : IDisposable where T : class
    {
        protected CManagerDBContext _context;
        protected DbSet<T> _dbSet;
        public Repository(CManagerDBContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public void Edit(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<T> FindAll()
        {
            return _context.Set<T>();
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}