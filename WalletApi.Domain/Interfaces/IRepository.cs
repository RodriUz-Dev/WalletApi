﻿
namespace WalletApi.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
