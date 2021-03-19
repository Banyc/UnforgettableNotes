using System;
using System.Text.Json.Serialization;

namespace UnforgettableMemo.Shared.Models
{
    public class MemoSchedulerSettings
    {
        public double CoolingTimeSpanSeconds { get; set; } = TimeSpan.FromSeconds(0).TotalSeconds;
        [JsonIgnore]
        public TimeSpan CoolingTimeSpan
        {
            get
            {
                return TimeSpan.FromSeconds(this.CoolingTimeSpanSeconds);
            }
            set
            {
                this.CoolingTimeSpanSeconds = value.TotalSeconds;
            }
        }
        public DateTime LastGetLeastRetrievedMemoTime { get; set; }
        public int EnergyCost { get; set; } = 3;  // per memo retrieving
    }
}
