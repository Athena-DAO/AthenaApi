﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAndControlWebApi.Models
{
    public class AlgorithmParameters
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string DataType { get; set; }

        public Algorithm Algorithm { get; set; }

        public ICollection<PipelineParameter> PipelineParameters { get; set; }
    }
}
