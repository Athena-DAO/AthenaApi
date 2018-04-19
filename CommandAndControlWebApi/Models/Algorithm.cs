using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAndControlWebApi.Models
{
    public class Algorithm
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        public string Cover { get; set; }

        [Required]
        public string MasterImage { get; set; }

        [Required]
        public string SlaveImage { get; set; }

        public ICollection<AlgorithmParameters> AlgorithmParameters { get; set; }
    }
}
