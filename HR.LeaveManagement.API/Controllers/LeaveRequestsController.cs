using HR.LeaveManagement.Application.DTO.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveRequest.Requests.Queries;
using HR.LeaveManagement.Application.Responses.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<LeaveRequestsController>
        [HttpGet]
        public async Task<ActionResult<BaseQueryListResponse<LeaveRequestListDto>>> Get()
        {
            //return new string[] { "value1", "value2" };
            var response = await _mediator.Send(new GetLeaveRequestListRequest());
            if (response is null)
            {
                return StatusCode(500,
                    "Unexpected error occurred while fetching leave requests.");
            }
            if (response.Records.Count == 0)
            {
                return NotFound();
            }
            return Ok(response);
        }

        // GET api/<LeaveRequestsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseQueryResponse<LeaveRequestDto>>> Get(int id)
        {
            //return "value";
            var response = await _mediator.Send(new GetLeaveRequestDetailRequest());
            if (response is null)
            {
                return StatusCode(500,
                     "Unexpected error occurred while fetching leave requests.");
            }
            if (response.Record is null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        // POST api/<LeaveRequestsController>
        [HttpPost]
        public async Task<ActionResult<CreateCommandResponse<CreateLeaveRequestDto>>> Post([FromBody] CreateLeaveRequestDto leaveRequest)
        {
            var response = await _mediator.Send(new CreateLeaveRequestCommand
            {
                CreateLeaveRequestDto = leaveRequest
            });
            if (response is null)
            {
                return StatusCode(500,
                     "Unexpected error occurred while fetching leave requests.");
            }
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(response.Errors);
            }
            return CreatedAtAction(nameof(Post), new { id = response.RecordId }, response.Record);
        }

        // PUT api/<LeaveRequestsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<BaseCommandResponse>> Put(int id, [FromBody] UpdateLeaveRequestDto leaveRequest)
        {
            var response = await _mediator.Send(new UpdateLeaveRequestCommand
            {
                Id = id,
                UpdateLeaveRequestDto = leaveRequest
            });
            if (response is null)
            {
                return StatusCode(500,
                     "Unexpected error occurred while fetching leave requests.");
            }
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(response.Errors);
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPatch("change-approval/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> ChangeLeaveRequestApproval(int id, 
            [FromBody] ChangeLeaveRequestApprovalDto leaveRequestApproval)
        {
            var response = await _mediator.Send(new UpdateLeaveRequestCommand
            {
                Id = id,
                ChangeLeaveRequestApprovalDto = leaveRequestApproval
            });
            if (response is null)
            {
                return StatusCode(500,
                     "Unexpected error occurred while fetching leave requests.");
            }
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(response.Errors);
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE api/<LeaveRequestsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseCommandResponse>> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteLeaveRequestCommand
            {
                Id = id
            });
            if (response is null)
            {
                return StatusCode(500,
                     "Unexpected error occurred while fetching leave requests.");
            }
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(response.Errors);
            }
            if(response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
