using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserServices.Models;

namespace UserServices.Data
{
    public class UserServicesContext : DbContext
    {
        public UserServicesContext (DbContextOptions<UserServicesContext> options)
            : base(options)
        {
        }

        public DbSet<UserServices.Models.PensionStatus> PensionStatus { get; set; } = default!;

        public DbSet<UserServices.Models.Request>? Request { get; set; }

        public DbSet<UserServices.Models.User>? User { get; set; }
    }
}
