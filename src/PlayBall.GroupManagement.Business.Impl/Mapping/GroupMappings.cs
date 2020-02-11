
using PlayBall.GroupManagement.Business.Models;
using PlayBall.GroupManagement.Data.Entities;
using System.Collections.Generic;

namespace PlayBall.GroupManagement.Business.Impl.Mapping
{
    internal static class GroupMappings
    {
        public static Group ToService(this GroupEntity entity)
        {
            return entity != null ? new Group { Id = entity.Id, Name = entity.Name, RowVersion = entity.RowVersion } : null;
        }

        public static GroupEntity ToEntity(this Group model)
        {
            //if (!byte.TryParse(model.RowVersion, out var rowVersion))
            //{
            //    rowVersion = 0;
            //}

            return model != null ? new GroupEntity { Id = model.Id, Name = model.Name, RowVersion = model.RowVersion } : null;
        }

        public static IReadOnlyCollection<Group> ToService(this IReadOnlyCollection<GroupEntity> entities) 
            => entities.MapCollection(ToService);
     }
}
