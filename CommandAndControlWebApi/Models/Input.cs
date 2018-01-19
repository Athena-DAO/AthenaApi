using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAndControlWebApi.Models
{
    public class Input
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public InputOutputDataType DataType { get; set; }

        public Guid AlgorithmId { get; set; }
        public Algorithm Algorithm { get; set; }
    }
}
