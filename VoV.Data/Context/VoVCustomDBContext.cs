using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Data.Context
{
    public class VoVCustomDBContext : VoVDbContext
    {
        public VoVCustomDBContext()
        {
        }

        public VoVCustomDBContext(DbContextOptions<VoVDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<spTestResult> spTest { get; set; } = null!;
        public virtual DbSet<spGetCompanyRisksOfClientEmployeeResult> spGetCompanyRisksOfClientEmployee { get; set; }
        public virtual DbSet<spGetCompanyOpportunitiesOfClientEmployeeResult> spGetCompanyOpportunitiesOfClientEmployee { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<spTestResult>(e => e.HasNoKey());
            modelBuilder.Entity<spGetCompanyRisksOfClientEmployeeResult>(e => e.HasNoKey());
            modelBuilder.Entity<spGetCompanyOpportunitiesOfClientEmployeeResult>(e => e.HasNoKey());
        }
    }
}
