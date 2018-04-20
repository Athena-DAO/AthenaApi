using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAndControlWebApi.ViewModels
{
    public class Pipeline
    {
        public string AlgorithmId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<PipelineParameter> Parameters { get; set; }
    }
}
