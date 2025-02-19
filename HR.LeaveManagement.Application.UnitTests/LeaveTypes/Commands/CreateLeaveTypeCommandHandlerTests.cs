using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Infrastrcuture;
using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Application.Features.LeaveRequest.Handlers.Commands;
using HR.LeaveManagement.Application.Features.LeaveType.Handlers.Commands;
using HR.LeaveManagement.Application.Profiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using HR.LeaveManagement.Application.Features.LeaveType.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Threading;
using HR.LeaveManagement.Application.DTO.LeaveType;
using Moq;
using Shouldly;
using HR.LeaveManagement.Application.Responses.Common;

namespace HR.LeaveManagement.Application.UnitTests.LeaveTypes.Commands
{
    public class CreateLeaveTypeCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILeaveTypeRepository> _repository;
        private readonly CreateLeaveTypeDto _leaveTypeDto;
        private readonly CreateLeaveTypeCommandHandler _handler;

        public CreateLeaveTypeCommandHandlerTests()
        {
            _repository = MockLeaveTypeRepositories.GetLeaveTypeRepository();
            var mapperConfig = new MapperConfiguration(exp =>
            {
                exp.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _handler = new CreateLeaveTypeCommandHandler(_repository.Object, _mapper);


            _leaveTypeDto = new CreateLeaveTypeDto
            {
                LeaveTypeName = "Test Dto",
                AllocatedDays = 15
            };
        }

        [Fact]
        public async Task Create_ValidLeaveType_CommandHanlderTest()
        {
            //var handler = new CreateLeaveTypeCommandHandler(_repository.Object, _mapper);
            var result = await _handler.Handle(new CreateLeaveTypeCommand() { leaveTypeDto = _leaveTypeDto },
                CancellationToken.None);
            var leaveTypes = await _repository.Object.GetAllAsync();

            result.ShouldBeOfType<CreateCommandResponse<CreateLeaveTypeDto>>();

            leaveTypes.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Create_InvalidLeaveType_CommandHandlerTest()
        {
            _leaveTypeDto.AllocatedDays = -1;
            var result = await _handler.Handle(new CreateLeaveTypeCommand() { leaveTypeDto = _leaveTypeDto }, CancellationToken.None);

            result.ShouldBeOfType<CreateCommandResponse<CreateLeaveTypeDto>>();
            result.Success.ShouldBe(false);
            result.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);

            var leaveTypes = await _repository.Object.GetAllAsync();
            leaveTypes.Count.ShouldBe(3);
        }
    }
}
