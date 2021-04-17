using System;
using System.Data.Common;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PavValHackathon.Web.Common;

namespace PavValHackathon.Web.Extensions.HealthCheck.Checks
{
    public class SqlServerHealthCheck : IHealthCheck
    {
        private const string SelectSql = "SELECT 1;";

        private static readonly TimeSpan DegradedTime = TimeSpan.FromSeconds(1);

        private readonly string _connectionString;

        public SqlServerHealthCheck(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException(nameof(connectionString));

            _connectionString = connectionString;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(context, nameof(context));
            cancellationToken.ThrowIfCancellationRequested();

            var timeWatch = Stopwatch.StartNew();

            try
            {
                await using var sqlConnection = new SqlConnection(_connectionString);
                await sqlConnection.OpenAsync(cancellationToken);
                await using var command = sqlConnection.CreateCommand();
                command.CommandText = SelectSql;

                await command.ExecuteScalarAsync(cancellationToken);
            }
            catch (DbException exception)
            {
                return HealthCheckResult.Unhealthy(exception: exception);
            }
            timeWatch.Stop();

            return timeWatch.Elapsed >= DegradedTime
                ? HealthCheckResult.Degraded($"Too slow DB request. Degraded time: {DegradedTime.TotalSeconds} seconds.")
                : HealthCheckResult.Healthy("Ok.");
        }
    }
}