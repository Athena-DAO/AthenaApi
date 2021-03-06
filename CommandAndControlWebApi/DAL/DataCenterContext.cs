﻿using CommandAndControlWebApi.Models;
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
            modelBuilder.Entity<ProfileCompleteDataSet>().HasKey(pk => new { pk.CompleteDataSetId, pk.ProfileId });
            modelBuilder.Entity<ProfilePipeline>().HasKey(pk => new { pk.PipelineId, pk.ProfileId });
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<DataSet> DataSets { get; set; }
        public DbSet<ProfileDataSet> ProfilesDataSets { get; set; }
        public DbSet<CompleteDataSet> CompleteDataSets { get; set; }
        public DbSet<ProfileCompleteDataSet> ProfilesCompleteDataSets { get; set; }
        public DbSet<Algorithm> Algorithms { get; set; }
        public DbSet<AlgorithmParameters> AlgorithmParameters { get; set; }
        public DbSet<Pipeline> Pipelines { get; set; }
        public DbSet<PipelineParameter> PipelineParameters { get; set; }
        public DbSet<ProfilePipeline> ProfilePipeline { get; set; }
    }
}
