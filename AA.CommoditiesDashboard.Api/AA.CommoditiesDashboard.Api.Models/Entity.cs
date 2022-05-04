using System;

namespace AA.CommoditiesDashboard.Api.Models
{
    public abstract class Entity<T>
    {
        public T Id { get; set; }
        
        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public string LastModifiedBy { get; set; }
    }
}
