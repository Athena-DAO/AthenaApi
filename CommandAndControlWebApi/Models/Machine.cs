using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAndControlWebApi.Models
{
    public class Conatiner
    {
        public Guid Id { get; set; }
        public string IpAddress { get; set; }
        public int PortNumber { get; set; }
    }
}
