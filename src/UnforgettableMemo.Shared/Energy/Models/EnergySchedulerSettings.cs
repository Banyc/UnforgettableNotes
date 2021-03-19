using System;

namespace UnforgettableMemo.Shared.Energy.Models
{
    public class EnergySchedulerSettings
    {
        public double LastEnergy { get; set; } = 100;
        public int MaxEnergy { get; set; } = 100;
        public DateTime LastUpdateTime { get; set; } = DateTime.UtcNow;
    }
}
