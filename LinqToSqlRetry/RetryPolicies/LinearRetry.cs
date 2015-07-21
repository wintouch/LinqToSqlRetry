using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetryLogic
{
    public class LinearRetry : IRetryPolicy
    {
        private static readonly TimeSpan DefaultInterval = TimeSpan.FromSeconds(2);
        private const int DefaultRetryCount = 3;


        internal static readonly int[] DefaultTransientErrors =
        {
            // From https://msdn.microsoft.com/pt-pt/library/azure/ff394106.aspx#bkmk_connection_errors 
            4060, // Cannot open database "%.*ls" requested by the login. The login failed.
            10928, // Resource ID: %d. The %s limit for the database is %d and has been reached. For more information, see http://go.microsoft.com/fwlink/?LinkId=267637.
            10929, // Resource ID: %d. The %s minimum guarantee is %d, maximum limit is %d and the current usage for the database is %d. However, the server is currently too busy to support requests greater than %d for this database. For more information, see http://go.microsoft.com/fwlink/?LinkId=267637. Otherwise, please try again later.
            40197, // The service has encountered an error processing your request. Please try again. Error code %d.
            40501, // The service is currently busy. Retry the request after 10 seconds. Incident ID: %ls. Code: %d.
            40613, // Database '%.*ls' on server '%.*ls' is not currently available. Please retry the connection later. If the problem persists, contact customer support, and provide them the session tracing ID of '%.*ls'.
            
            // https://msdn.microsoft.com/en-us/library/azure/ff394106.aspx#general
            40642, // The server is currently too busy. Please try again later.
            45168, // The SQL Azure system is under load, and is placing an upper limit on concurrent DB CRUD operations for a single server (e.g., create database). The server specified in the error message has exceeded the maximum number of concurrent connections. Try again later.
            45169  // The SQL azure system is under load, and is placing an upper limit on the number of concurrent server CRUD operations for a single subscription (e.g., create server). The subscription specified in the error message has exceeded the maximum number of concurrent connections, and the request was denied. Try again later.
        };

        private readonly TimeSpan _interval;
        private readonly int _retryCount;
        private readonly int[] _transientErrors;

        public LinearRetry()
            : this(DefaultInterval, DefaultRetryCount)
        {
        }

        public LinearRetry(TimeSpan interval, int retryCount)
            : this(interval, retryCount, DefaultTransientErrors)
        {
        }

        public LinearRetry(TimeSpan interval, int retryCount, int[] transientErrors)
        {
            _interval = interval;
            _retryCount = retryCount;
            _transientErrors = transientErrors;
        }

        public TimeSpan Interval
        {
            get { return _interval; }
        }

        public int RetryCount
        {
            get { return _retryCount; }
        }

        public int[] TransientErrors
        {
            get { return _transientErrors; }
        }

        public virtual TimeSpan? ShouldRetry(int retryCount, Exception exception)
        {
            SqlException sqlException = exception as SqlException;

            if (sqlException == null)
            {
                return retryCount < _retryCount ? (TimeSpan?)_interval : null;
            }
            else
            {
                return _transientErrors.Contains(sqlException.Number) && retryCount < _retryCount ? (TimeSpan?)_interval : null;
            }
        }
    }
}
