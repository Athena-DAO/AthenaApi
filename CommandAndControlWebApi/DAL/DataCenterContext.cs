using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using CommandAndControlWebApi.Models;

namespace CommandAndControlWebApi.DAL
{
    public class DataCenterContext : DbContext
    {
        public DataCenterContext(DbContextOptions<DataCenterContext> options) : base(options)
        {
        }

        public DbSet<Conatiner> Containers { get; set; }
        public DbSet<Algorithm> Algorithms { get; set; }
        public DbSet<ContainerExecutesAlgorithm> ContainerExecutesAlgorithms { get; set; }
        public DbSet<Input> Inputs { get; set; }
    }
}
