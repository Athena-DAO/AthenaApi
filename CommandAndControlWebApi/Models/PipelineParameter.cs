using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAndControlWebApi.Models
{
    public class PipelineParameter
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Value { get; set; }

        public AlgorithmParameters AlgorithmParameter { get; set; }

        public Pipeline Pipeline { get; set; }
    }
}
