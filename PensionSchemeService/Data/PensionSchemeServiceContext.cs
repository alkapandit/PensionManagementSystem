using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PensionSchemeService.Models;

namespace PensionSchemeService.Data
{
    public class PensionSchemeServiceContext : DbContext
    {
        public PensionSchemeServiceContext (DbContextOptions<PensionSchemeServiceContext> options)
            : base(options)
        {
        }

        public DbSet<PensionSchemeService.Models.PensionScheme> PensionScheme { get; set; } = default!;
    }
}
