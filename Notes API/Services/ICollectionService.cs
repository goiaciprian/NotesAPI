using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes_API.Services
{
    public interface ICollectionService<T>
    {
        Task<List<T>> GetAll();
        Task<T> Get(Guid Id);
        Task<T> Create(T model);
        Task<T> Update(Guid Id, T model);
        Task<T> Delete(Guid Id);

    }
}
