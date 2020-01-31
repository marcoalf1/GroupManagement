using System.Collections.Generic;
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
            builder.RegisterType<InMemoryGroupsService>().Named<IGroupsService>("groupsService").SingleInstance();
            builder.RegisterDecorator<IGroupsService>((context, service) => new GroupServiceDecorator(service, context.Resolve<ILogger<GroupServiceDecorator>>()), "groupsService");
        }

        private class GroupServiceDecorator : IGroupsService
        {
            private readonly IGroupsService _inner;
            private readonly ILogger<GroupServiceDecorator> _logger;

            public GroupServiceDecorator(IGroupsService inner, ILogger<GroupServiceDecorator> logger)
            {
                _inner = inner;
                _logger = logger;
            }

            public IReadOnlyCollection<Group> GetAll()
            {
                //_logger.LogTrace("########## Hellooooo from {decoratedMethod} ##########", nameof(GetAll));

                using (var scope = _logger.BeginScope("Decorator scope: {decorator}", nameof(GroupServiceDecorator)))
                {
                    _logger.LogTrace("########## Hellooooo from {decoratedMethod} ##########", nameof(GetAll));
                    var result = _inner.GetAll();
                    _logger.LogTrace("########## Goodbyeee from {decoratedMethod} ##########", nameof(GetAll));
                    return result;
                }

                //return _inner.GetAll();
            }

            public Group GetById(long id)
            {
                _logger.LogTrace("########## Hellooooo from {decoratedMethod} ##########", nameof(GetById));
                return _inner.GetById(id);
            }

            public Group Update(Group group)
            {
                _logger.LogTrace("########## Hellooooo from {decoratedMethod} ##########", nameof(Update));
                return _inner.Update(group);
            }

            public Group Add(Group group)
            {
                _logger.LogTrace("########## Hellooooo from {decoratedMethod} ##########", nameof(Add));
                return _inner.Add(group);
            }
        }
    }
}
