using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAndControlWebApi.Models
{
    public class ProfilePipeline
    {
        public Guid PipelineId { get; set; }
        public Pipeline Pipeline { get; set; }

        public Guid ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}
