using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAndControlWebApi.ViewModels
{
    public class PipelineViewModel
    {
        public string AlgorithmId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<PipelineParameterViewModel> Parameters { get; set; }
    }
}
