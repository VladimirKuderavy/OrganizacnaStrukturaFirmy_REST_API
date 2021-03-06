using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrganizacnaStrukturaFirmy_REST_API.Models;

namespace OrganizacnaStrukturaFirmy_REST_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Zamestnanec> zamestnanci { get; set; }
        public DbSet<UzolOrganizacnejStruktury> organizacna_struktura { get; set; }
    }
}
