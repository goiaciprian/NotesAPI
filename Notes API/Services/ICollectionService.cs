using System;
using System.Collections.Generic;

namespace Notes_API.Services
{
    public interface ICollectionService<T>
    {
        List<T> GetAll();
        T Get(Guid Id);
        T Create(T model);
        T Update(Guid Id, T model);
        T Delete(Guid Id);

    }
}
