using System.Linq;
using System.Collections.Generic;
using UnforgettableMemo.Shared.Models;
using UnforgettableMemo.Shared.Data;

namespace UnforgettableMemo.Shared
{
    public class MemoScheduler
    {
        private readonly IMemoPersistence memoPersistence;
        public List<Memo> Memos { get; set; } = new List<Memo>();
        public MemoScheduler(IMemoPersistence memoPersistence)
        {
            this.memoPersistence = memoPersistence;
        }

        public void OrderByRetrievability()
        {
            this.Memos = this.Memos.OrderBy(xxxx => xxxx.Retrievability).ToList();
        }

        public void Save()
        {
            memoPersistence.Save(this.Memos);
        }

        public void Load()
        {
            this.Memos = memoPersistence.Load();
        }
    }
}
