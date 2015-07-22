using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetryLogic.RetryPolicies
{
    public class ExponentialRetry : IRetryPolicy
    {
        private static readonly TimeSpan DefaultInitialInterval = TimeSpan.FromSeconds(10);
        private static readonly TimeSpan DefaultIntervalDelta = TimeSpan.FromSeconds(5);
        private const int DefaultRetryCount = 3;

        public ExponentialRetry()
        {
            InitialInterval = DefaultInitialInterval;
            IntervalDelta = DefaultIntervalDelta;
            RetryCount = DefaultRetryCount;
        }

        public TimeSpan InitialInterval { get; set; }
        public TimeSpan IntervalDelta { get; set; }
        public int RetryCount { get; set; }


        public virtual TimeSpan? ShouldRetry(int retryCount, Exception exception)
        {
            return retryCount < RetryCount ? (TimeSpan?)InitialInterval.Add(TimeSpan.FromMilliseconds(IntervalDelta.TotalMilliseconds * retryCount)) : null;
        }
    }
}
