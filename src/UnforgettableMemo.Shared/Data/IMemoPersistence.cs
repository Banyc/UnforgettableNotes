using System.Collections.Generic;
using UnforgettableMemo.Shared.Models;

namespace UnforgettableMemo.Shared.Data
{
    public interface IMemoPersistence
    {
        void Save(List<Memo> memos);
        List<Memo> Load();
    }
}
