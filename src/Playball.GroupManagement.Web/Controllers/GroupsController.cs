using Microsoft.AspNetCore.Mvc;
using Playball.GroupManagement.Web.Demo;
using Playball.GroupManagement.Web.Mappings;
using Playball.GroupManagement.Web.Models;
using PlayBall.GroupManagement.Business.Services;

namespace Playball.GroupManagement.Web.Controllers
{
    // localhost:5000/groups
    [Route("groups")]
    public class GroupsController : Controller
    {
        private readonly IGroupsService _groupsService;
        private readonly SomeRootConfiguration _config;
        private readonly DemoSecretsConfiguration _secrets;

        public GroupsController(IGroupsService groupsService, SomeRootConfiguration config, DemoSecretsConfiguration secrets)
        {
            _groupsService = groupsService;
            _config = config;
            _secrets = secrets;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View(_groupsService.GetAll().ToViewModel());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Details(long id)
        {
            var group = _groupsService.GetById(id);

            if (group == null)
            {
                return NotFound();
            }

            return View(group.ToViewModel());
        }

        [HttpPost]
        [Route("{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, GroupViewModel model)
        {
            var group = _groupsService.Update(model.ToServiceModel());

            if (group == null)
            {
                return NotFound();
            }

            group.Name = model.Name;

            return RedirectToAction("Index");

        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreateReally(GroupViewModel model)
        {
            _groupsService.Add(model.ToServiceModel());

            return RedirectToAction("Index");
        }

    }
}