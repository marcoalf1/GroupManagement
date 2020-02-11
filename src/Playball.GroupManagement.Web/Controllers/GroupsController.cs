using Microsoft.AspNetCore.Mvc;
//using Playball.GroupManagement.Web.Demo;
using Playball.GroupManagement.Web.Mappings;
using Playball.GroupManagement.Web.Models;
using PlayBall.GroupManagement.Business.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Playball.GroupManagement.Web.Controllers
{
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
        public async Task<IActionResult> IndexAsync(CancellationToken ct)
        {
            var result = await _groupsService.GetAllAsync(ct);
            return View(result.ToViewModel());
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> DetailsAsync(long id, CancellationToken ct)
        {
            var group = await _groupsService.GetByIdAsync(id,ct);

            if (group == null)
            {
                return NotFound();
            }

            return View(group.ToViewModel());
        }

        //[DemoActionFilter]
        [HttpPost]
        [Route("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(long id, GroupViewModel model, CancellationToken ct)
        {
            var group =  await _groupsService.UpdateAsync(model.ToServiceModel(),ct);

            if (group == null)
            {
                return NotFound();
            }

            return RedirectToAction("IndexAsync");
        }
        
        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        //[DemoActionFilter]
        [HttpPost]
        [Route("")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReallyAsync(GroupViewModel model, CancellationToken ct)
        {
            await _groupsService.AddAsync(model.ToServiceModel(),ct);

            return RedirectToAction("IndexAsync");
        }

    }
}