using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Playball.GroupManagement.Web.Mappings;
using Playball.GroupManagement.Web.Models;
using PlayBall.GroupManagement.Business.Services;

namespace Playball.GroupManagement.Web.Controllers
{
    // localhost:5000/groups
    [Route("groups")]
    public class GroupsController : Controller
    {
        private static long currentGroupId = 1;
        private static List<GroupViewModel> groups = new List<GroupViewModel> { new GroupViewModel { Id = 1, Name = "Sample Group" } };

        private readonly IGroupsService _groupsService;

        public GroupsController(IGroupsService groupsService )
        {
            this._groupsService = groupsService;
        }

        [HttpGet]
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
        public IActionResult Create(GroupViewModel model)
        {
            _groupsService.Add(model.ToServiceModel());

            return RedirectToAction("Index");
        }

    }
}