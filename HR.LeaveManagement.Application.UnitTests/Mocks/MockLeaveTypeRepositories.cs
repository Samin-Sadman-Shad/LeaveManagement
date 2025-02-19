using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace HR.LeaveManagement.Application.UnitTests.Mocks
{
    /// <summary>
    /// One mock repository and multiple instances in there.
    /// Unit tests will run against features(handlers)
    /// Each mock respository will mock a respository from the Application layer
    /// </summary>
    public static class MockLeaveTypeRepositories
    {
        /// <summary>
        /// Allows to mock any type of repositoy
        /// </summary>
        /// <returns></returns>
        public static Mock<ILeaveTypeRepository> GetLeaveTypeRepository()
        {
            //list of objects that would be expected from database, sample data
            var leaveTypes = new List<LeaveType>
            {
                new LeaveType
                {
                    Id = 1,
                    LeaveTypeName = "Sick Leave",
                    AllocatedDays = 14
                },
                new LeaveType
                {
                    Id = 2,
                    LeaveTypeName = "Test",
                    AllocatedDays = 15
                }
            };

            var mockRepo = new Mock<ILeaveTypeRepository>();

            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(leaveTypes);

            mockRepo.Setup(repo => repo.GetAsync(1)).ReturnsAsync((LeaveType leaveType) =>
            {
                return leaveTypes.Find(leaveType => leaveType.Id == 1);
            });

            mockRepo.Setup(repo => repo.AddAsync(It.IsAny<LeaveType>())).ReturnsAsync((LeaveType leaveType) =>
            {
                leaveTypes.Add(leaveType);
                return leaveType;
            });

            return mockRepo;
        }
    }
}
