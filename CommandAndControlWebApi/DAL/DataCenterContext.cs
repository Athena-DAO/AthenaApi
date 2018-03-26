using CommandAndControlWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandAndControlWebApi.DAL
{
    public class DataCenterContext : DbContext
    {
        public DataCenterContext(DbContextOptions<DataCenterContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfileDataSet>().HasKey(pk => new { pk.DataSetId, pk.ProfileId });
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<DataSet> DataSets { get; set; }
        public DbSet<ProfileDataSet> ProfilesDataSets { get; set; }
    }
}
