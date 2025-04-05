using HR.LeaveManagement.Application.DTO.LeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveType.Requests.Queries;
using HR.LeaveManagement.Application.Responses.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaveTypesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LeaveTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<LeaveTypesController>
        [HttpGet]
        public async Task<ActionResult<BaseQueryListResponse<LeaveTypeDto>>> Get()
        {
            //let mediator know about the request for this service
            //handler will manage all of the operation, querying via contracts, mapping
            //and returning the object(dto, vm) that is required
            //api will not interact with the actual domain objects from the database
            var leaveTypes = await _mediator.Send(new GetLeaveTypeListRequest());
            if (leaveTypes.Success)
            {
                return Ok(leaveTypes);
            }
            else
            {
                return BadRequest();
            }
            
        }

        // GET api/<LeaveTypesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseQueryResponse<LeaveTypeDto>>> Get([FromRoute] int id)
        {
            var leaveType = await _mediator.Send(new GetLeaveTypeDetailRequest { Id = id });
            return Ok(leaveType);
        }

        // POST api/<LeaveTypesController>
        [HttpPost]
        [Authorize(Roles ="Administrator")]
        public async Task<ActionResult<CreateCommandResponse<CreateLeaveTypeDto>>> Post([FromBody] CreateLeaveTypeDto leaveType)
        {
            var response = await _mediator.Send(new CreateLeaveTypeCommand { leaveTypeDto = leaveType });
            if(response == null)
            {
                return BadRequest();
            }
            if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest(ModelState);
            }
            return CreatedAtAction(nameof(Post), new {id = response.RecordId}, response.Record );
        }

        // PUT api/<LeaveTypesController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<BaseCommandResponse>> Put(int id, [FromBody] UpdateLeaveTypeDto leaveType)
        {
            var response = await _mediator.Send(new UpdateLeaveTypeCommand { leaveTypeDto = leaveType, Id = id});
            if (response == null)
            {
                return BadRequest();
            }
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }

        // DELETE api/<LeaveTypesController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<BaseCommandResponse>> Delete([FromRoute]int id)
        {
             var response = await _mediator.Send(new DeleteLeaveTypeCommand { Id = id});
            if(response == null)
            {
                return BadRequest();
            }
            if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
