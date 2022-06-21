using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BethanysPieShop.Application.Interfaces
{
    public interface IRepository<T>
    {
        Task<bool> Delete<TInput>(TInput input) where TInput : class;
        Task<bool> Update<TInput>(TInput input) where TInput : class;
        Task<bool> Create<TInput>(TInput input) where TInput : class;
        Task<TOutput> GetById<TOutput>(Expression<Func<TOutput, bool>> expression) where TOutput : class;
        Task<bool> Exists<TOutput>(TOutput output) where TOutput : class;
    }
}
