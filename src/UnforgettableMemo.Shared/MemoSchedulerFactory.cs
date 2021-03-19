using System.Collections.Generic;
using UnforgettableMemo.Shared.Data;
using UnforgettableMemo.Shared.Energy;
using UnforgettableMemo.Shared.Energy.Models;
using UnforgettableMemo.Shared.Models;

namespace UnforgettableMemo.Shared
{
    public class MemoSchedulerFactory
    {
        public string PersistenceDirectory { get; set; }
        public MemoSchedulerFactory(string persistenceDirectory)
        {
            this.PersistenceDirectory = persistenceDirectory;
        }

        // with JSON persistence
        public MemoScheduler GetMemoScheduler()
        {
            (MemoScheduler scheduler, _) = GetSchedulers();
            return scheduler;
        }

        public (MemoScheduler, EnergyScheduler) GetSchedulers()
        {
            JsonPersistence<List<Memo>> memoPersistence =
                new JsonPersistence<List<Memo>>(this.PersistenceDirectory, "memos.json");
            JsonPersistence<MemoSchedulerSettings> memoSchedulerSettingsPersistence =
                new JsonPersistence<MemoSchedulerSettings>(this.PersistenceDirectory, "memoSchedulerSettings.json");
            JsonPersistence<EnergySchedulerSettings> energySchedulerSettingsPersistence =
                new JsonPersistence<EnergySchedulerSettings>(this.PersistenceDirectory, "energySchedulerSettings.json");
            EnergyScheduler energyScheduler = new(energySchedulerSettingsPersistence);
            MemoScheduler memoScheduler = new MemoScheduler(memoPersistence, memoSchedulerSettingsPersistence, energyScheduler);
            return (memoScheduler, energyScheduler);
        }
    }
}
