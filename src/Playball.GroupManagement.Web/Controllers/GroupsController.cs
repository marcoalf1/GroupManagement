using Microsoft.AspNetCore.Mvc;
using Playball.GroupManagement.Web.Mappings;
using Playball.GroupManagement.Web.Models;
using PlayBall.GroupManagement.Business.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Playball.GroupManagement.Web.Controllers
{
    //[ApiController]
    [Route("groups")]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupsService _groupsService;

        public GroupsController(IGroupsService groupsService)
        {
            _groupsService = groupsService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllAsync(CancellationToken ct)
        {
            var result = await _groupsService.GetAllAsync(ct);
            return Ok(result.ToModel());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync(long id, CancellationToken ct)
        {
            var group = await _groupsService.GetByIdAsync(id,ct);

            if (group == null)
            {
                return NotFound();
            }

            return Ok(group.ToModel());
        }

        [HttpPut]
        [Route("{id}")]
        //public async Task<IActionResult> UpdateAsync(long id, [FromQuery]GroupModel model, CancellationToken ct)
        public async Task<IActionResult> UpdateAsync(long id, GroupModel model, CancellationToken ct)
        {
            model.Id = id;

            var group = await _groupsService.UpdateAsync(model.ToServiceModel(), ct);

            return Ok(group.ToModel());
        }

        [HttpPost]
        [HttpPut]
        [Route("")]
        //public async Task<IActionResult> AddAsync([FromBody]GroupModel model, CancellationToken ct)
        public async Task<IActionResult> AddAsync(GroupModel model, CancellationToken ct)
        {
            model.Id = 0;
            var group = await _groupsService.AddAsync(model.ToServiceModel(),ct);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = group.Id }, group);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> RemoveAsync(long id, CancellationToken ct)
        {
            await _groupsService.RemoveAsync(id, ct);
            
            return NoContent();
        }


    }
}