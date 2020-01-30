using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Logging;
using PlayBall.GroupManagement.Business.Impl.Services;
using PlayBall.GroupManagement.Business.Models;
using PlayBall.GroupManagement.Business.Services;

namespace Playball.GroupManagement.Web.IoC
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryGroupsService>().As<IGroupsService>().SingleInstance();
            builder.RegisterDecorator<IGroupsService>((context, service) => new GroupServiceDecorator(service, context.Resolve<ILogger<GroupServiceDecorator>>()), "groupsService");
        }

        private class GroupServiceDecorator : IGroupsService
        {
            private readonly IGroupsService _inner;
            private readonly ILogger<GroupServiceDecorator> _logger;

            public GroupServiceDecorator(IGroupsService inner, ILogger<GroupServiceDecorator> logger)
            {
                this._inner = inner;
                this._logger = logger;
            }
            public Group Add(Group group)
            {
                Console.WriteLine($"########## Hellooooo from {nameof(Add)} ##########");
                return _inner.Add(group);
            }

            public IReadOnlyCollection<Group> GetAll()
            {
                Console.WriteLine($"########## Hellooooo from {nameof(GetAll)} ##########");
                return _inner.GetAll();
            }

            public Group GetById(long id)
            {
                Console.WriteLine($"########## Hellooooo from {nameof(GetById)} ##########");
                return _inner.GetById(id);
            }

            public Group Update(Group group)
            {
                Console.WriteLine($"########## Hellooooo from {nameof(Update)} ##########");
                return _inner.Update(group);
            }
        }
    }
}
