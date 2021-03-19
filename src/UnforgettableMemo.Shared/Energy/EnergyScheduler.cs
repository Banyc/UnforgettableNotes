using System;
using UnforgettableMemo.Shared.Data;
using UnforgettableMemo.Shared.Energy.Models;

namespace UnforgettableMemo.Shared.Energy
{
    public class EnergyScheduler
    {
        private readonly IPersistence<EnergySchedulerSettings> settingsPersistence;
        public int Energy
        {
            get
            {
                UpdateLastEnergy();
                return (int)this.settings.LastEnergy;
            }
        }

        private readonly EnergySchedulerSettings settings;

        public EnergyScheduler(IPersistence<EnergySchedulerSettings> settingsPersistence)
        {
            this.settingsPersistence = settingsPersistence;
            this.settings = settingsPersistence.Load() ?? new();
        }

        public bool TryConsumeEnergy(int energy)
        {
            if (this.Energy < energy)
            {
                return false;
            }
            UpdateLastEnergy();
            this.settings.LastEnergy -= energy;
            return true;
        }

        private void UpdateLastEnergy()
        {
            this.settings.LastEnergy =
                Math.Min(
                    this.settings.LastEnergy + GetEnergy(DateTime.UtcNow - settings.LastUpdateTime),
                    this.settings.MaxEnergy);
            this.settings.LastUpdateTime = DateTime.UtcNow;
        }

        private static double GetEnergy(TimeSpan timeSpan)
        {
            return timeSpan.TotalMinutes / 5;
        }

        public void Save()
        {
            settingsPersistence.Save(this.settings);
        }
    }
}
