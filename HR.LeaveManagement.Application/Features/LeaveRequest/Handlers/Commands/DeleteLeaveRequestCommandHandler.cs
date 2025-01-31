using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Application.Features.LeaveRequest.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Handlers.Commands
{
    public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, BaseCommandResponse>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
        }
        public async Task<BaseCommandResponse> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var leaveRequest = await _leaveRequestRepository.DeleteAsync(request.DeleteLeaveRequestDto.Id);
            if(leaveRequest is null)
            {
                response.Success = false;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            response.Success = true;
            response.StatusCode = System.Net.HttpStatusCode.NoContent;
            return response;
        }
    }
}
