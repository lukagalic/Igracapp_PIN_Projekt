using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Igracapp.Models;

namespace Igracapp.Data
{
    public class KontekstBaze : DbContext
    {
        public KontekstBaze (DbContextOptions<KontekstBaze> options)
            : base(options)
        {
        }

        public DbSet<Igracapp.Models.Igrac> Igrac { get; set; }

        public DbSet<Igracapp.Models.Opsirnije> Opsirnije { get; set; }
    }
}
