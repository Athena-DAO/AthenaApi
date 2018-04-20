using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAndControlWebApi.ViewModels
{
    public class PipelineParameterViewModel
    {
        public string Id { get; set; }
        public string ParameterName { get; set; }
        public string ParameterDescription { get; set; }
        public string Value { get; set; }
    }
}
