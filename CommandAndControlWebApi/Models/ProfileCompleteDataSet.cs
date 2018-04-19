using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAndControlWebApi.Models
{
    public class ProfileCompleteDataSet
    {
        public Guid ProfileId { get; set; }
        public Profile Profile { get; set; }

        public Guid CompleteDataSetId { get; set; }
        public CompleteDataSet CompleteDataSet { get; set; }
    }
}
