using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLayerApp.Core.Interfaces;
using NLayerApp.Core.Repositories;
using NLayerApp.Core.Services;
using NLayerApp.Core.UnitOfWorks;
using NLayerApp.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Service.Services
{
    public class Service<T> : IService<T> where T : class, IEntity, new()
    {
        private readonly IGenericRepository<T> _repository;
        protected readonly IUnitOfWork UnitOfWork;

        public Service(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            UnitOfWork = unitOfWork;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _repository.AddRangeAsync(entities);
            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            var result = await _repository.AnyAsync(expression);
            return result;
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            var result = await _repository.CountAsync(expression);
            return result > 0 ? result : 0;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await _repository.GetAll().ToListAsync();
            return result;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null) throw new NotFoundException($"{typeof(T).Name}({id}) not found");

            return result;
        }

        public async Task RemoveAsync(T entity)
        {
            await Task.Run(() => { _repository.Remove(entity); });
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            await Task.Run(() => { _repository.RemoveRange(entities); });

        }

        public async Task UpdateAsync(T entity)
        {
            await Task.Run(() => { _repository.Update(entity); });

        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _repository.Where(expression);
        }
    }
}
