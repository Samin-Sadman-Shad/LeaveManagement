using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Application.Features.LeaveType.Handlers.Queries;
using HR.LeaveManagement.Application.Profiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using HR.LeaveManagement.Application.Features.LeaveType.Requests.Queries;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Threading;
using Shouldly;
using HR.LeaveManagement.Application.Responses.Common;
using HR.LeaveManagement.Application.DTO.LeaveType;

namespace HR.LeaveManagement.Application.UnitTests.LeaveTypes.Queries
{
    /// <summary>
    /// Can not inject parameters in unit tests, as real parameters are real objects like database
    /// instantiate the mock objects instead
    /// </summary>
    public class GetLeaveTypeListRequestHandlerTests
    {
        private readonly IMapper _mapper;
        //need thr mock repository to interact with the respository
        private readonly Mock<ILeaveTypeRepository> _repository;

        public GetLeaveTypeListRequestHandlerTests()
        {
            _repository =  MockLeaveTypeRepositories.GetLeaveTypeRepository();

            var mappingConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public async Task GetLeaveTypeListTest()
        {
            var handler = new GetLeaveTypeListRequestHandler(_repository.Object, _mapper);
            var result = await handler.Handle(new GetLeaveTypeListRequest(), CancellationToken.None);

            result.ShouldBeOfType<BaseQueryListResponse<LeaveTypeDto>>();
            result.Records.Count.ShouldBe(2);
        }
    }
}
