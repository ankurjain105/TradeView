using System;

namespace AA.CommoditiesDashboard.Api.Core
{
    public interface IClock
    {
        DateTime UtcNow { get; }
        DateTime Today { get; }
    }
}