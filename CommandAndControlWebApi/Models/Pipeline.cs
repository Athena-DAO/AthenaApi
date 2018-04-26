using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAndControlWebApi.Models
{
    public class Pipeline
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int NumberOfContainers { get; set; }

        public string Status { get; set; }

        public string Result { get; set; }

        public Algorithm Algorithm { get; set; }

        public ICollection<PipelineParameter> PipelineParameters { get; set; }

        public ICollection<ProfilePipeline> ProfilePipeline { get; set; }
    }
}
