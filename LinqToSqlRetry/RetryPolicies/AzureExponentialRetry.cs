using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetryLogic.RetryPolicies
{
    public class AzureExponentialRetry : AzureRetryPolicies
    {
        private static readonly TimeSpan DefaultInitialInterval = TimeSpan.FromSeconds(10);
        private static readonly TimeSpan DefaultIntervalDelta = TimeSpan.FromSeconds(5);
        private const int DefaultRetryCount = 3;

        public AzureExponentialRetry()
        {
            InitialInterval = DefaultInitialInterval;
            IntervalDelta = DefaultIntervalDelta;
            RetryCount = DefaultRetryCount;
            TransientErrors = DefaultTransientErrors;
        }

        public TimeSpan InitialInterval { get; set; }
        public TimeSpan IntervalDelta { get; set; }
        public int RetryCount { get; set; }
        public int[] TransientErrors { get; set; }

        public override TimeSpan? ShouldRetry(int retryCount, Exception exception)
        {
            if (retryCount < RetryCount)
            {
                SqlException sqlException = exception as SqlException;

                if (sqlException == null)
                {
                    return computeTimeSpan(retryCount);
                }
                else
                {
                    foreach (SqlError error in sqlException.Errors)
                    {
                        if (TransientErrors.Contains(error.Number))
                        {
                            return computeTimeSpan(retryCount);
                        }
                    }
                }
            } 

            return null;
        }

        private TimeSpan? computeTimeSpan(int retryCount)
        {
            return InitialInterval.Add(TimeSpan.FromMilliseconds(IntervalDelta.TotalMilliseconds * retryCount));
        }
    }
}
