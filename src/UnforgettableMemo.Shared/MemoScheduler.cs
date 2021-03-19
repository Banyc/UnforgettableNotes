using System;
using System.Linq;
using System.Collections.Generic;
using UnforgettableMemo.Shared.Models;
using UnforgettableMemo.Shared.Data;
using UnforgettableMemo.Shared.Energy;

namespace UnforgettableMemo.Shared
{
    public class MemoScheduler
    {
        private readonly IPersistence<List<Memo>> memoPersistence;
        private readonly IPersistence<MemoSchedulerSettings> settingsPersistence;
        public List<Memo> Memos { get; set; } = new List<Memo>();
        private readonly EnergyScheduler energyScheduler;
        private readonly MemoSchedulerSettings settings;

        public MemoScheduler(IPersistence<List<Memo>> memoPersistence, IPersistence<MemoSchedulerSettings> settingsPersistence, EnergyScheduler energyScheduler)
        {
            this.memoPersistence = memoPersistence;
            this.settingsPersistence = settingsPersistence;
            this.Memos = memoPersistence.Load() ?? new();
            this.settings = settingsPersistence.Load() ?? new();
            this.energyScheduler = energyScheduler;
        }

        public void OrderByRetrievability()
        {
            // ascending order
            this.Memos = this.Memos.OrderBy(xxxx => xxxx.Retrievability).ToList();
        }

        public Memo GetLeastRetrievedMemo()
        {
            if (this.settings.CoolingTimeSpan > DateTime.UtcNow - this.settings.LastGetLeastRetrievedMemoTime)
            {
                return null;
            }
            else if (this.Memos.Count == 0)
            {
                return null;
            }
            else if (this.energyScheduler.TryConsumeEnergy(this.settings.EnergyCost))
            {
                this.OrderByRetrievability();
                this.settings.LastGetLeastRetrievedMemoTime = DateTime.UtcNow;
                return this.Memos.First();
            }
            else
            {
                return null;
            }
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
            settingsPersistence.Save(this.settings);
            // energyScheduler.Save();
        }
    }
}
