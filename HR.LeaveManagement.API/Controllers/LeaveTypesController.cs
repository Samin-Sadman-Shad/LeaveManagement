using HR.LeaveManagement.Application.DTO.LeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Handlers.Commands;
using HR.LeaveManagement.Application.Features.LeaveType.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveType.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LeaveTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<LeaveTypesController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveTypeDto>>> Get()
        {
            //let mediator know about the request for this service
            //handler will manage all of the operation, querying via contracts, mapping
            //and returning the object(dto, vm) that is required
            //api will not interact with the actual domain objects from the database
            var leaveTypes = await _mediator.Send(new GetLeaveTypeListRequest());
            return Ok(leaveTypes);
        }

        // GET api/<LeaveTypesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveTypeDto>> Get([FromRoute] int id)
        {
            var leaveType = await _mediator.Send(new GetLeaveTypeDetailRequest { Id = id });
            return Ok(leaveType);
        }

        // POST api/<LeaveTypesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateLeaveTypeDto leaveType)
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
        public async Task<IActionResult> Put(int id, [FromBody] UpdateLeaveTypeDto leaveType)
        {
            var response = await _mediator.Send(new UpdateLeaveTypeCommand());
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
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
             var response = await _mediator.Send(new UpdateLeaveTypeCommand());
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
