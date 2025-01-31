using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Persistance.Configurations.Entities
{
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.HasData(
                new LeaveType
                {
                    Id = 1,
                    AllocatedDays = 10,
                    LeaveTypeName = "Vacation"
                },
                new LeaveType
                {
                    Id = 2,
                    AllocatedDays = 14,
                    LeaveTypeName = "Sick Leaves"
                }
         );
        }
    }
}
