﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IEXTrading.Models;

namespace IEXTrading.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<Rec> Companies { get; set; }
        public DbSet<Equity> Equities { get; set; }
        public DbSet<Signup> Customer { get; set; }
    }
}
