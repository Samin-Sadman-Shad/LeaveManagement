using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Domain.Common
{
    public class BaseDomainEntity
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; } = "admin";
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
