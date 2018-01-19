using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAndControlWebApi.Models
{
    public class ContainerExecutesAlgorithm
    {
        public Guid Id { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public string Log { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public bool? SuccessfullyComplted { get; set; }

        public Guid AlgorithmId { get; set; }
        public virtual Algorithm Algorithm { get; set; }

        public Guid ContainerId { get; set; }
        public virtual Conatiner Conatiner { get; set; }
    }
}
