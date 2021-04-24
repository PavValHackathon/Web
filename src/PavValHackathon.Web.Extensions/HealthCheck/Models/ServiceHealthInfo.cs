using System;

namespace PavValHackathon.Web.Extensions.HealthCheck.Models
{
    public record ServiceHealthInfo
    {
        public TimeSpan Duration { get; init; }
        public string Key { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string Status { get; init; } = string.Empty;
        public string? Error { get; init; }
    }
}