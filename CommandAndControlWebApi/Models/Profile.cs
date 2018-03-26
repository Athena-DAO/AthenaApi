using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAndControlWebApi.Models
{
    public class Profile
    {
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string ProfilePicture { get; set; }

        [Required]
        public string CoverPicture { get; set; }

        public ICollection<ProfileDataSet> ProfileDataSets { get; set; }
    }
}
