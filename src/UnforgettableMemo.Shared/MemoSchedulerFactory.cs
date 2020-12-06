using UnforgettableMemo.Shared.Data;

namespace UnforgettableMemo.Shared
{
    public class MemoSchedulerFactory
    {
        public string PersistenceDirectory { get; set; }
        public MemoSchedulerFactory(string persistenceDirectory)
        {
            this.PersistenceDirectory = persistenceDirectory;
        }

        public MemoScheduler GetMemoScheduler()
        {
            JsonPersistence persistence = new JsonPersistence(this.PersistenceDirectory);
            MemoScheduler scheduler = new MemoScheduler(persistence);
            return scheduler;
        }
    }
}
