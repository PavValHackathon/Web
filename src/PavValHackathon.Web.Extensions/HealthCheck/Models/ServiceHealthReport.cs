using System;
using System.Collections.Generic;

namespace PavValHackathon.Web.Extensions.HealthCheck.Models
{
    public record ServiceHealthReport
    {
        public TimeSpan Duration { get; init; } = TimeSpan.Zero;
        public string Status { get; init; } = string.Empty;
        public ICollection<ServiceHealthInfo> Info { get; init; } = Array.Empty<ServiceHealthInfo>();
    }
}