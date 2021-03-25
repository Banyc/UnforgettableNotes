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
        private List<Memo> Memos { get; set; } = new List<Memo>();
        private readonly EnergyScheduler energyScheduler;
        private readonly MemoSchedulerSettings settings;
        private List<Memo> BlankMemos { get; set; } = new();

        public MemoScheduler(IPersistence<List<Memo>> memoPersistence, IPersistence<MemoSchedulerSettings> settingsPersistence, EnergyScheduler energyScheduler)
        {
            this.memoPersistence = memoPersistence;
            this.settingsPersistence = settingsPersistence;
            this.Memos = memoPersistence.Load() ?? new();
            this.settings = settingsPersistence.Load() ?? new();
            this.energyScheduler = energyScheduler;
        }

        private void OrderByRetrievability()
        {
            // ascending order
            this.Memos = this.Memos.OrderBy(xxxx => xxxx.Retrievability).ToList();
        }

        public Memo GetLeastRetrievedMemo()
        {
            if (this.settings.CoolingTimeSpan > DateTime.UtcNow - this.settings.LastCoolingStartTime)
            {
                return null;
            }
            else if (this.Memos.Count == 0)
            {
                return null;
            }
            else if (this.energyScheduler.TryConsumeEnergy(this.settings.EnergyCost))
            {
                this.OrganizeMemos();
                return this.Memos.First();
            }
            else
            {
                return null;
            }
        }

        public void StartCooling()
        {
            this.settings.LastCoolingStartTime = DateTime.UtcNow;
        }

        // get new memo that has just been added to the list
        public Memo GetBlankMemo()
        {
            Memo newMemo = new Memo();
            this.BlankMemos.Add(newMemo);
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

        private void OrganizeMemos()
        {
            MoveBlankMemos();
            MoveNonBlankMemos();
            OrderByRetrievability();
        }

        private void MoveBlankMemos()
        {
            int i;
            for (i = this.Memos.Count - 1; i >= 0; i--)
            {
                if (string.IsNullOrWhiteSpace(this.Memos[i].Content))
                {
                    this.BlankMemos.Add(this.Memos[i]);
                    this.Memos.RemoveAt(i);
                }
            }
        }

        private void MoveNonBlankMemos()
        {
            int i;
            for (i = this.BlankMemos.Count - 1; i >= 0; i--)
            {
                if (!string.IsNullOrWhiteSpace(this.BlankMemos[i].Content))
                {
                    this.Memos.Add(this.BlankMemos[i]);
                    this.BlankMemos.RemoveAt(i);
                }
            }
        }
    }
}
