using AutoMapper;
using AutoMapper.QueryableExtensions;
using BethanysPieShop.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShop.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Repository(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<bool> Create<TInput>(TInput input) where TInput : class
        {
            bool result;

            try
            {
                _appDbContext.Set<T>().Add(_mapper.Map<T>(input));
                await _appDbContext.SaveChangesAsync();

                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public async Task<bool> Delete<TInput>(TInput input) where TInput : class
        {
            bool result;

            try
            {
                _appDbContext.Set<T>().Remove(_mapper.Map<T>(input));
                await _appDbContext.SaveChangesAsync();

                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public async Task<bool> Exists<TOutput>(TOutput output) where TOutput : class
        {
            return await _appDbContext.Set<T>().ContainsAsync(_mapper.Map<T>(output));
        }

        public async Task<TOutput> GetById<TOutput>(Expression<Func<TOutput, bool>> expression) where TOutput : class
        {
            return await _appDbContext.Set<T>()
                .ProjectTo<TOutput>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<bool> Update<TInput>(TInput input) where TInput : class
        {
            bool result;

            try
            {
                _appDbContext.Set<T>().Update(_mapper.Map<T>(input));
                await _appDbContext.SaveChangesAsync();

                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
    }
}
