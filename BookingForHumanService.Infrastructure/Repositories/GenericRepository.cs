using BookingForHumanService.Domain.Interfaces;
using BookingForHumanService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingForHumanService.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly BookingDbContext _context;

        public GenericRepository(
            BookingDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>>GetAllAsync()
            => await _context.Set<T>().AsNoTracking().ToListAsync();


        public async Task<T?>GetByIdAsync(int id)
            => await _context.Set<T>().FindAsync(id);

        public async Task<T>AddAsync(T entity)
        {
            await _context.Set<T>()
                .AddAsync(entity);

            return entity;
        }

        public Task<T>UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);

            return Task.FromResult(entity);
        }

        public async Task<bool>DeleteAsync(int id)
        {
            var entity =await _context.Set<T>()
                    .FindAsync(id);

            if (entity == null)
                return false;

            _context.Set<T>().Remove(entity);

            return true;
        }
    }
}
