using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAndControlWebApi.Models
{
    public class DataSet
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        public string URL { get; set; }

        public ICollection<ProfileDataSet> DataSetProfiles { get; set; }

        [InverseProperty("XComponentDataSet")]
        public ICollection<CompleteDataSet> XCompleteDataSets { get; set; }

        [InverseProperty("YComponentDataSet")]
        public ICollection<CompleteDataSet> YCompleteDataSets { get; set; }
    }
}
