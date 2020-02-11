using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Playball.GroupManagement.Web.Models
{
    public class GroupViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
