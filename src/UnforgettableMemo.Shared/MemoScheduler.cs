using System;
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
            // ascending order
            this.Memos = this.Memos.OrderBy(xxxx => xxxx.Retrievability).ToList();
        }

        // get new memo that has just been added to the list
        public Memo GetNewMemo()
        {
            Memo newMemo = new Memo();
            this.Memos.Add(newMemo);
            return newMemo;
        }

        public void RemoveMemo(Memo memoToRemove)
        {
            this.Memos.RemoveAll(xxxx => xxxx.Id == memoToRemove.Id);
        }

        public void Save()
        {
            // remove blank memos
            List<Memo> cleanMemos = this.Memos.Where(xxxx => !string.IsNullOrWhiteSpace(xxxx.Content)).ToList();
            memoPersistence.Save(cleanMemos);
        }

        public void Load()
        {
            this.Memos = memoPersistence.Load();
        }
    }
}
