using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAndControlWebApi.ViewModels
{
    public class PipelineViewModel
    {
        public string Id { get; set; }
        public string AlgorithmId { get; set; }
        public string AlgorithmName { get; set; }
        public string AlgorithmDescription { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfContainers { get; set; }
        public string Result { get; set; }
        public string DataSetId { get; set; }
        public ICollection<PipelineParameterViewModel> Parameters { get; set; }
    }
}
