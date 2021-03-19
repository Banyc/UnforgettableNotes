using System;

namespace UnforgettableMemo.Shared.Energy.Models
{
    public class EnergySchedulerSettings
    {
        public int LastEnergy { get; set; } = 100;
        public DateTime LastUpdateTime { get; set; } = DateTime.UtcNow;
    }
}
