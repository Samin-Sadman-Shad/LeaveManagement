using HR.LeaveManagement.Application.DTO.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Requests.Queries;
using HR.LeaveManagement.Application.Responses.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveAllocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<LeaveAllocationsController>
        [HttpGet]
        public async Task<ActionResult<BaseQueryListResponse<LeaveAllocationDto>>> Get()
        {
            //return new string[] { "value1", "value2" };
            var response = await _mediator.Send(new GetLeaveAllocationListRequest());
            if(response is null)
            {
                return StatusCode(500, "Unexpected error occured while fetching the data in handler");
            }
            switch (response.StatusCode)
            {
                case HttpStatusCode.InternalServerError:
                    return StatusCode(500, response.Message);
                    break;
                case HttpStatusCode.ExpectationFailed:
                    return StatusCode(417, response.Message);
                    break;
                case HttpStatusCode.NotFound:
                    return NotFound();
                    break;

            }
            return Ok(response);
        }

        // GET api/<LeaveAllocationsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseQueryResponse<LeaveAllocationDto>>> Get(int id)
        {
            //return "value";
            var response = new BaseQueryResponse<LeaveAllocationDto>();
            if(response is null)
            {
                return StatusCode(500, "Unexpected error occured while fetching the data in handler");
            }
            switch (response.StatusCode)
            {
                case HttpStatusCode.InternalServerError:
                    return StatusCode(500, response.Message);
                    break;
                case HttpStatusCode.ExpectationFailed:
                    return StatusCode(417, response.Message);
                    break;
                case HttpStatusCode.NotFound:
                    return NotFound();
                    break;

            }
            return Ok(response);
        }

        // POST api/<LeaveAllocationsController>
        [HttpPost]
        public async Task<ActionResult<CreateCommandResponse<CreateLeaveAllocationDto>>> Post([FromBody] CreateLeaveAllocationDto dto)
        {
            var leaveAllocationCommand = new CreateLeaveAllocationCommand();
            leaveAllocationCommand.dto = dto;
            var response = await _mediator.Send(leaveAllocationCommand);
            if(response is null)
            {
                return StatusCode(500, "Unexpected Error Occurred");
            }
            if(response.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(response.Errors);
            }

            return CreatedAtAction(nameof(Post), response.Message);

        }

        // PUT api/<LeaveAllocationsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LeaveAllocationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
