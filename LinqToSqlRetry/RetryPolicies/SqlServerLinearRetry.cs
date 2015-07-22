using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetryLogic.RetryPolicies
{
    public class SqlServerLinearRetry : SqlServerRetryPolicies
    {
        private static readonly TimeSpan DefaultInterval = TimeSpan.FromSeconds(2);
        private const int DefaultRetryCount = 3;

        public SqlServerLinearRetry()
        {
            Interval = DefaultInterval;
            RetryCount = DefaultRetryCount;
            TransientErrors = DefaultTransientErrors;
        }

        public TimeSpan Interval { get; set; }
        public int RetryCount { get; set; }
        public int[] TransientErrors { get; set; }

        public override TimeSpan? ShouldRetry(int retryCount, Exception exception)
        {
            
            if (retryCount < RetryCount)
            {
                SqlException sqlException = exception as SqlException;

                if (sqlException == null) 
                {
                    return (TimeSpan?)Interval;
                }
                else 
                {
                    foreach (SqlError error in sqlException.Errors) {
                        if (TransientErrors.Contains(error.Number))
                        {
                            return (TimeSpan?)Interval;
                        }
                    }
                }
            }

            return null;

        }
    }
}