using CommandAndControlWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandAndControlWebApi.DAL
{
    public class DataCenterContext : DbContext
    {
        public DataCenterContext(DbContextOptions<DataCenterContext> options) : base(options)
        {
        }

        public DbSet<Profile> Profiles { get; set; }
    }
}
