using System.ComponentModel.DataAnnotations;

namespace PlayBall.GroupManagement.Data.Entities
{
    public class GroupEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
