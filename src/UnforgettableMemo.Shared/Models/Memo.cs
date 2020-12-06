using System.Runtime.Serialization;
using System;
using System.Text.Json.Serialization;

namespace UnforgettableMemo.Shared.Models
{
    // Forgetting curve: https://supermemo.guru/wiki/Forgetting_curve
    [Serializable]
    public class Memo
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Content { get; set; }
        public DateTime Creation { get; set; } = DateTime.Now;
        public DateTime LastCheck { get; set; } = DateTime.Now;
        [JsonIgnore]
        public TimeSpan Time
        {
            get
            {
                return DateTime.Now - this.LastCheck;
            }
        }
        public double Stability { get; set; } = 1;
        public double Retrievability
        {
            get
            {
                return Math.Exp(-this.Time.TotalDays / this.Stability);
            }
        }
        public double StabilityIncrementMax { get; set; } = 26.31;
        private double gain = 2.96;
        public double Gain
        {
            get
            {
                return this.gain;
            }
            set => this.gain = Math.Max(-Math.Log(1d / this.StabilityIncrementMax), value);
        }
        public double StabilityIncrement
        {
            get
            {
                return this.StabilityIncrementMax * Math.Exp(-this.Gain * this.Retrievability);
            }
        }

        public void Review()
        {
            this.Stability += this.StabilityIncrement;
            this.LastCheck = DateTime.Now;
        }
    }
}
