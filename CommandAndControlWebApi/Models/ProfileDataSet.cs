using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAndControlWebApi.Models
{
    public class ProfileDataSet
    {
        public Guid ProfileId { get; set; }
        public Profile Profile { get; set; }

        public Guid DataSetId { get; set; }
        public DataSet DataSet { get; set; }
    }
}
