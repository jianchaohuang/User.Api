using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api.model
{
    public class UserTag
    {
        public int AppUserId { get; set; }

        public string Tag { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
