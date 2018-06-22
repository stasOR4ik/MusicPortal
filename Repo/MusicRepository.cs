using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
    public class MusicRepository<T> : IRepository<T>
        where T : class
    {
        MusicContext _context;
        DbSet<T> _db;

        public MusicRepository(MusicContext context)
        {
            _context = context;
            _db = context.Set<T>();
        }

        public IEnumerable<T> GetAll() => _db;

        public IEnumerable<T> GetAllBy(Expression<Func<T, bool>> predicate) => _db.Where(predicate);

        public void Create(T item) => _db.Add(item);

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            T item = _db.Find(predicate);
            if (item != null)
                _db.Remove(item);
        }

        public T GetBy(Expression<Func<T, bool>> predicate) => _db.FirstOrDefault(predicate);

        public void Save() => _context.SaveChanges();

        public void Update(T item) => _db.Update(item);
    }
}
