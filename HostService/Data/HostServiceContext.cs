using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HostService.Models;

namespace HostService.Data
{
    public class HostServiceContext : DbContext
    {
        public HostServiceContext (DbContextOptions<HostServiceContext> options)
            : base(options)
        {
        }

        public DbSet<HostService.Models.HostLogin> HostLogin { get; set; } = default!;
    }
}
