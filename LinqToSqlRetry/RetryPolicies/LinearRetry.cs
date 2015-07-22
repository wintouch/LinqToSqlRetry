using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetryLogic.RetryPolicies
{
    public class LinearRetry : IRetryPolicy
    {
        private static readonly TimeSpan DefaultInterval = TimeSpan.FromSeconds(1);
        private const int DefaultRetryCount = 3;

        public LinearRetry()
        {
            Interval = DefaultInterval;
            RetryCount = DefaultRetryCount;
        }

        public TimeSpan Interval { get; set; }
        public int RetryCount { get; set; }

        public virtual TimeSpan? ShouldRetry(int retryCount, Exception exception)
        {
            return retryCount < RetryCount ? (TimeSpan?)Interval : null;
        }
    }
}

