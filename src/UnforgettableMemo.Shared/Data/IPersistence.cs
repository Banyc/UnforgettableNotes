using System.Collections.Generic;
using UnforgettableMemo.Shared.Models;

namespace UnforgettableMemo.Shared.Data
{
    public interface IPersistence<T>
    {
        void Save(T data);
        T Load();
    }
}
