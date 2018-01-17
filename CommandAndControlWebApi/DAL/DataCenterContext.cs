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

        public DbSet<Machine> Machines { get; set; }
    }
}
