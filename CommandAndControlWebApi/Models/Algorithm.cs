using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAndControlWebApi.Models
{
    public class Algorithm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MasterExecutableFileLocation { get; set; }
        public string SlaveExecutableFileLocation { get; set; }

        public virtual ICollection<ContainerExecutesAlgorithm> Executions { get; set; }

        public virtual ICollection<Input> Inputs { get; set; }
    }
}
