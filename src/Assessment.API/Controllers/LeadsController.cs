using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Application.Leads.Commands;
using Application.Leads.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.API.Controllers
{
    [ExcludeFromCodeCoverage]
    [ApiController]
    [Route("[controller]")]
    public class LeadsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeadsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet , Route("PropertyType")]
        public async Task<IActionResult> GetByPropertyType()
        {
            var response = await _mediator.Send(new FetchLeadsByPropertyType());
            return Ok(response);
        }
        
        [HttpGet , Route("Project")]
        public async Task<IActionResult> GetByProjects()
        {
            var response = await _mediator.Send(new FetchLeadsByProject());
            return Ok(response);
        }
        
        [HttpGet , Route("StartDate")]
        public async Task<IActionResult> GetByStartDate()
        {
            var response = await _mediator.Send(new FetchLeadsByStartDate());
            return Ok(response);
        }
        
        [HttpPost , Route("AddLead")]
        public async Task<IActionResult> AddLead([FromBody] PostLeadCommand request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        
        [HttpGet , Route("Duplicateleads")]
        public async Task<IActionResult> GetDuplicateLeads()
        {
            var response = await _mediator.Send(new FetchDuplicateLeads());
            return Ok(response);
        }
        
        
    }
}