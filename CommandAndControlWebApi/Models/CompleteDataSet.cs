using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAndControlWebApi.Models
{
    public class CompleteDataSet
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        public DataSet XComponentDataSet { get; set; }

        public DataSet YComponentDataSet { get; set; }

        public ICollection<ProfileCompleteDataSet> ProfileCompleteDataSet { get; set; }
    }
}
