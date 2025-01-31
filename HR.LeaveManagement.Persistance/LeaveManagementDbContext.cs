using HR.LeaveManagement.Domain.Common;
using HR.LeaveManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistance
{
    public class LeaveManagementDbContext:DbContext
    {
        public LeaveManagementDbContext(DbContextOptions<LeaveManagementDbContext> options) : base(options)
        {
            
        }

        public DbSet<LeaveAllocation> leaveAllocations { get; set; }
        public DbSet<LeaveRequest> leaveRequests { get; set; }
        public DbSet<LeaveType> leaveTypes { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var entry in ChangeTracker.Entries<BaseDomainEntity>())
            {
                entry.Entity.LastModifiedDate = DateTime.Now;
                if(entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(LeaveManagementDbContext).Assembly);
        }
    }
}
