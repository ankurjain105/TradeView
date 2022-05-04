using System;

namespace AA.CommoditiesDashboard.Api.Core
{
    public class Clock : IClock
    {
        public DateTime UtcNow => new DateTime(2020, 06, 29);
        public DateTime Today => new DateTime(2020, 06, 29);
    }
}
