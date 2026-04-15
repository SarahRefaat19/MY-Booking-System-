using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Domain.Interfaces
{
    public  interface IGenericRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> Delete(int Id);


    }
}
