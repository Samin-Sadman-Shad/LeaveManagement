using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Domain.Common
{
    public class BaseDomainEntity
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string? CreatedById { get; set; } 
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedById { get; set; }
    }
}
