using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;
using VoV.Data.Entities;
using static System.Net.WebRequestMethods;

namespace VoV.Data.Context
{
    public class VoVDbContext : DbContext
    {
        public VoVDbContext()
        {

        }
        public VoVDbContext(DbContextOptions<VoVDbContext> options)
            : base(options)
        {
        }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer("Data Source=SENTIENTPC-112\\SQLEXPRESS2014;Initial Catalog=VoV;Persist Security Info=True;User ID=sa;Password=Sentient@123");
        //            }
        //        }

        #region Tables
        public DbSet<Audit_DDL_Change> Audit_DDL_Changes { get; set; } = null!;
        public DbSet<BusinessSegment> BusinessSegments { get; set; } = null!;
        //public DbSet<StandardObservation> StandardObservations { get; set; } = null!;
        public DbSet<StandardOpportunity> StandardOpportunities { get; set; } = null!;
        public DbSet<StandardRisk> StandardRisks { get; set; } = null!;
        public DbSet<AccountType> AccountTypes { get; set; } = null!;
        public DbSet<AppSetting> AppSettings { get; set; } = null!;
        public DbSet<BusinessUnit> BusinessUnits { get; set; } = null!;
        public DbSet<Currency> Currencies { get; set; } = null!;
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<ClientAccount> ClientAccounts { get; set; } = null!;
        public DbSet<ClientBusinessUnit> ClientBusinessUnits { get; set; } = null!;
        public DbSet<ClientEmployee> ClientEmployees { get; set; } = null!;
        public DbSet<ClientFinancial> ClientFinancials { get; set; } = null!;

        public DbSet<ClientFinancialFile> ClientFinancialFiles { get; set; } = null!;
        public DbSet<ClientGroup> ClientGroups { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        //public DbSet<CompanyObservation> CompanyObservations { get; set; } = null!;
        public DbSet<CompanyOpportunity> CompanyOpportunities { get; set; } = null!;
        public DbSet<CompanyRisk> CompanyRisks { get; set; } = null!;
        public DbSet<Designation> Designations { get; set; } = null!;
        public DbSet<FinancialYear> FinancialYears { get; set; } = null!;
        public DbSet<Location> Locations { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;

        //public DbSet<ScheduleMeeting> ScheduleMeetings { get; set; } = null!;
        //public DbSet<ScheduleMeetingObservationDetail> ScheduleMeetingObservationDetails { get; set; } = null!;
        //public DbSet<ScheduleMeetingOpportunityDetail> ScheduleMeetingOpportunityDetails { get; set; } = null!;
        //public DbSet<ScheduleMeetingRiskDetail> ScheduleMeetingRiskDetails { get; set; } = null!;
        //public DbSet<MeetingOtherMatter> MeetingOtherMatters { get; set; } = null!;
        //public DbSet<MeetingOther> MeetingOthers { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Meeting> Meetings { get; set; } = null!;
        public DbSet<MeetingObservationAndOtherMatter> MeetingObservationAndOtherMatters { get; set; } = null!;
        public DbSet<MeetingOpportunity> MeetingOpportunities { get; set; } = null!;
        public DbSet<MeetingRisk> MeetingRisks { get; set; } = null!;
        public DbSet<MeetingClientAttendee> MeetingClientAttendees { get; set; } = null!;
        public DbSet<MeetingCompanyAttendee> MeetingCompanyAttendees { get; set; } = null!;
        public DbSet<BusinessSubSegment> BusinessSubSegments { get; set; } = null!;

        #endregion Ends Tables
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Insert Data During Installation

            Guid createdById = new Guid("9BAD75F9-442E-447A-8385-4F7FE824FA90");
            // https://www.c-sharpcorner.com/article/code-first-approach-in-asp-net-core-mvc-with-ef-core-migration/

            modelBuilder.Entity<Role>().HasData(
              new Role() { Id = new Guid("1D0BF51C-3C8F-45B0-9A16-2732B251E88D"), Name = "SiteAdmin", CreatedById = createdById },
              new Role() { Id = new Guid("76CDE1F7-950E-4826-8666-BEA361D27772"), Name = "Admin", CreatedById = createdById },
                 new Role() { Id = new Guid("947D14F6-BAD9-49C6-8830-781FC9807963"), Name = "DomainUser", CreatedById = createdById },
              new Role() { Id = new Guid("633D5FD6-776F-4AF1-8F76-D52ADDE38D79"), Name = "User", CreatedById = createdById });

            modelBuilder.Entity<User>().HasData(
              new User() { Id = createdById, Name = "Sentient Admin", UserName = "SentientAdmin", Password = "2220tbYOIVYfrreLt4BsDg==", RoleId = new Guid("76CDE1F7-950E-4826-8666-BEA361D27772"), Mobile = "9969875308", Email = "sentient2008@gmail.com", Active = true, EmpCode = "VOV_SENTI_001", CompanyId = new Guid("CF1D1C0C-2F49-4941-BDA0-8F32232571B1"), LocationId = new Guid("3B988BB1-83FB-4FE9-8611-C6101940FFE3"), CreatedById = Guid.NewGuid() });

            modelBuilder.Entity<Location>().HasData(
           new Location() { Id = new Guid("3B988BB1-83FB-4FE9-8611-C6101940FFE3"), Name = "Goregaon", Code = "GRN", CompanyId = new Guid("CF1D1C0C-2F49-4941-BDA0-8F32232571B1"), CreatedById = createdById });

            modelBuilder.Entity<Company>().HasData(
           new Company() { Id = new Guid("CF1D1C0C-2F49-4941-BDA0-8F32232571B1"), Name = "Sentient", Address = "Goregaon(West)", Email = "sentient2008@gmail.com", CreatedById = createdById, ThemeLightHexCode = "#e8f1fa", ThemeDarkHexCode = "#0560b2" });

            modelBuilder.Entity<AppSetting>().HasData(
         new AppSetting() { Id = new Guid("345EF14C-F86F-4716-9659-55B1DEC2CA1A"), CompanyId = new Guid("CF1D1C0C-2F49-4941-BDA0-8F32232571B1") });

            modelBuilder.Entity<Designation>().HasData(
         new Designation() { Id = new Guid("CF1D1C0C-2F49-4941-BDA0-8F32232571B1"), CompanyId = new Guid("CF1D1C0C-2F49-4941-BDA0-8F32232571B1"), Name = "Relationship Officer", Code = "RO", ParentId = null, CreatedById = createdById },
              new Designation() { Id = new Guid("4870AF54-C766-4E0A-A4EF-9F5FBC2201FC"), CompanyId = new Guid("CF1D1C0C-2F49-4941-BDA0-8F32232571B1"), Name = "Team Lead", Code = "TL", ParentId = new Guid("CF1D1C0C-2F49-4941-BDA0-8F32232571B1"), CreatedById = createdById },
              new Designation() { Id = new Guid("290B0D1E-D796-4D36-B0FD-7679634F8C33"), CompanyId = new Guid("CF1D1C0C-2F49-4941-BDA0-8F32232571B1"), Name = "Unit Head", Code = "UH", ParentId = new Guid("4870AF54-C766-4E0A-A4EF-9F5FBC2201FC"), CreatedById = createdById });

            modelBuilder.Entity<BusinessUnit>().HasData(
         new BusinessUnit() { Id = new Guid("89D82C7B-95CE-4E37-970F-516F30A63522"), CompanyId = new Guid("CF1D1C0C-2F49-4941-BDA0-8F32232571B1"), Name = "Sales", Code = "S", CreatedById = createdById },
              new BusinessUnit() { Id = new Guid("13F6E635-5482-4F30-9336-C03E31C768DE"), CompanyId = new Guid("CF1D1C0C-2F49-4941-BDA0-8F32232571B1"), Name = "Purchase", Code = "P", CreatedById = createdById });

            //Business Segment Data
            modelBuilder.Entity<BusinessSegment>().HasData(
            new BusinessSegment() { Id = new Guid("583C79A8-EE74-46E4-A946-6ECC9BD715D0"), Name = "Aerospace industry", CreatedById = createdById },
            new BusinessSegment() { Id = new Guid("515655A2-4432-438A-B264-FA0399432E3C"), Name = "Agricultural industry", CreatedById = createdById },
            new BusinessSegment() { Id = new Guid("013CA040-E5E0-4563-B1A9-C3CDEDC4241B"), Name = "Automotive industry", CreatedById = createdById },
            new BusinessSegment() { Id = new Guid("35D20EFF-481D-4907-8A26-2DE3E479C91F"), Name = "Chemical industry", CreatedById = createdById },
            new BusinessSegment() { Id = new Guid("7A396F20-50C1-4F46-9383-E05ABF23D086"), Name = "Computer industry", CreatedById = createdById },
            new BusinessSegment() { Id = new Guid("C38DD507-AB7B-4DC6-8ACA-5EA584832CE1"), Name = "Construction industry", CreatedById = createdById },
            new BusinessSegment() { Id = new Guid("58BCB599-6860-43D4-8A0D-53739780EB69"), Name = "Defense industry", CreatedById = createdById },
            new BusinessSegment() { Id = new Guid("04E16FF4-7F76-4A21-9915-68C9CFD3F412"), Name = "Education industry", CreatedById = createdById },
            new BusinessSegment() { Id = new Guid("02AC7728-F244-45F4-9C48-784BEFB32A5A"), Name = "Entertainment industry", CreatedById = createdById },
            new BusinessSegment() { Id = new Guid("AA49D145-2F39-4384-9471-1B093FADEF00"), Name = "Fashion industry", CreatedById = createdById },
            new BusinessSegment() { Id = new Guid("E26CD5B5-AB62-4442-B335-455623379623"), Name = "Hotels industry", CreatedById = createdById });


            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Add<CascadeDeleteAttributeConvention>();
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            #endregion

            #region Entities Constraint, DefaultValue etc.
            modelBuilder.Entity<AppSetting>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Company)
                  .WithMany(p => p.AppSettings)
                  .HasForeignKey(d => d.CompanyId)
                  .OnDelete(DeleteBehavior.ClientSetNull);
            });

            //modelBuilder.Entity<Audit_DDL_Change>(entity =>
            //{
            //    entity.Property(e => e.ObjectId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            //});
            modelBuilder.Entity<AccountType>(entity =>
          {
              entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

              entity.Property(e => e.Active).HasDefaultValue(true);

              entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

              entity.HasIndex(e => e.Name).IsUnique();
          });

            modelBuilder.Entity<BusinessSegment>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
                entity.Property(e => e.Active).HasDefaultValue(true);
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
                entity.HasIndex(a => a.Code).IsUnique();
                entity.Property(e => e.Code).HasDefaultValue("1");

            });
            modelBuilder.Entity<BusinessUnit>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Active).HasDefaultValue(true);

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasIndex(a => new { a.CompanyId, a.Name }).IsUnique();

                entity.HasIndex(a => new { a.CompanyId, a.Code }).IsUnique();

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.BusinessUnits)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BusinessUnits_Company");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_BusinessUnits_BusinessUnits");
            });
            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Active).HasDefaultValue(true);

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasIndex(a => new { a.CompanyId, a.CIFNo }).IsUnique();

                entity.HasIndex(e => new { e.CompanyId, e.Name }).IsUnique();

                entity.HasOne(d => d.ClientGroup)
                 .WithMany(p => p.Clients)
                 .HasForeignKey(d => d.ClientGroupId)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_Clients_ClientGroup");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Clients_Company");
            });
            modelBuilder.Entity<ClientAccount>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasIndex(e => new { e.ClientId, e.AccountNo, e.AccountTypeId }).IsUnique();

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.AccountType)
                 .WithMany(p => p.ClientAccounts)
                 .HasForeignKey(d => d.AccountTypeId)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_ClientAccounts_AccountType");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientAccounts)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientAccounts_Client");

                entity.HasOne(d => d.Currency)
                  .WithMany(p => p.ClientAccounts)
                  .HasForeignKey(d => d.CurrencyCode)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_ClientAccounts_Currency");
            });

            modelBuilder.Entity<ClientBusinessUnit>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasIndex(e => new { e.ClientId, e.Name }).IsUnique();

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Client)
                  .WithMany(p => p.ClientBusinessUnits)
                  .HasForeignKey(d => d.ClientId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_ClientBusinessUnit_Client");

                entity.HasOne(d => d.BusinessSegment)
                .WithMany(p => p.ClientBusinessUnits)
                .HasForeignKey(d => d.BusinessSegmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClientBusinessUnit_BusinessSegment");

                entity.HasOne(d => d.BusinessSubSegment)
               .WithMany(p => p.ClientBusinessUnits)
               .HasForeignKey(d => d.BusinessSubSegmentId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_ClientBusinessUnit_BusinessSubSegment");

                entity.HasOne(d => d.RoUser)
                  .WithMany(p => p.ClientBusinessUnits)
                  .HasForeignKey(d => d.RoUserId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_ClientBusinessUnit_User");
            });

            modelBuilder.Entity<ClientEmployee>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasIndex(e => new { e.ClientId, e.Mobile }).IsUnique();

                entity.HasIndex(e => new { e.ClientId, e.Email }).IsUnique();

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ClientBusinessUnit)
                   .WithMany(x => x.ClientEmployees)
                   .HasForeignKey(d => d.ClientBusinessUnitId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_ClientEmployees_ClientBusinessUnit");

                entity.HasOne(d => d.Client)
                    .WithMany(x => x.ClientEmployees)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientEmployees_Client");
            });

            modelBuilder.Entity<ClientFinancial>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Client)
                     .WithMany(p => p.ClientFinancials)
                     .HasForeignKey(d => d.ClientId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_ClientFinancials_Client");

                entity.HasOne(d => d.FinancialYear)
                    .WithMany(p => p.ClientFinancials)
                    .HasForeignKey(d => d.FinancialYearId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientFinancials_FinancialYear");

                entity.HasOne(d => d.Currency)
                  .WithMany(p => p.ClientFinancials)
                  .HasForeignKey(d => d.CurrencyCode)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_ClientFinancials_Currency");
            });

            modelBuilder.Entity<ClientFinancialFile>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasIndex(e => new { e.ClientFinancialId, e.FileName }).IsUnique();

                entity.HasOne(d => d.ClientFinancial)
                     .WithMany(p => p.ClientFinancialFiles)
                     .HasForeignKey(d => d.ClientFinancialId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_ClientFinancialFiles_ClientFinancial");
            });


            modelBuilder.Entity<ClientGroup>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasIndex(e => new { e.GroupName, e.CompanyId }).IsUnique();

                entity.HasOne(d => d.Company)
                .WithMany(p => p.ClientGroups)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasIndex(e => e.Name).IsUnique();
            });
            
            modelBuilder.Entity<CompanyOpportunity>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Active).HasDefaultValue(true);

                entity.HasIndex(e => new { e.CompanyId, e.Name }).IsUnique();

                entity.HasOne(d => d.BusinessSegment)
                  .WithMany(p => p.CompanyOpportunities)
                  .HasForeignKey(d => d.BusinessSegmentId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_CompanyOpportunities_BusinessSegment");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyOpportunities)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyOpportunities_Company");

            });
            modelBuilder.Entity<CompanyRisk>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Active).HasDefaultValue(true);

                entity.HasIndex(e => new { e.CompanyId, e.Name }).IsUnique();

                entity.HasOne(d => d.BusinessSegment)
                   .WithMany(p => p.CompanyRisks)
                   .HasForeignKey(d => d.BusinessSegmentId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_CompanyRisks_BusinessSegment");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyRisks)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyRisks_Company");
            });
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasIndex(e => e.Code).IsUnique();
                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<Designation>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.Active).HasDefaultValue(true);
                entity.HasIndex(e => new { e.CompanyId, e.Name }).IsUnique();

                entity.HasOne(d => d.Company)
                   .WithMany(p => p.Designations)
                   .HasForeignKey(d => d.CompanyId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_Designations_Company");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Designations_Designations");
            });

            modelBuilder.Entity<FinancialYear>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasIndex(e => e.Abbr).IsUnique();
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasIndex(e => new { e.CompanyId, e.Code }).IsUnique();
                entity.HasIndex(e => new { e.CompanyId, e.Name }).IsUnique();

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Location_Company");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Location_Location");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<StandardOpportunity>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Active).HasDefaultValue(true);

                entity.HasIndex(e => new { e.BusinessSegmentId, e.Name }).IsUnique();

                entity.HasOne(d => d.BusinessSegment)
                   .WithMany(p => p.StandardOpportunities)
                   .HasForeignKey(d => d.BusinessSegmentId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<StandardRisk>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Active).HasDefaultValue(true);

                entity.HasIndex(e => new { e.BusinessSegmentId, e.Name }).IsUnique();

                entity.HasOne(x => x.BusinessSegment)
                .WithMany(x => x.StandardRisks)
                .HasForeignKey(x => x.BusinessSegmentId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Active).HasDefaultValue(true);

                entity.HasIndex(e => new { e.CompanyId, e.UserName }).IsUnique();
                entity.HasIndex(e => new { e.CompanyId, e.EmpCode }).IsUnique();

                entity.HasOne(d => d.BusinessUnit)
                     .WithMany(p => p.Users)
                     .HasForeignKey(d => d.BusinessUnitId)
                     .HasConstraintName("FK_Users_BusinessUnit");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Company");

                entity.HasOne(d => d.Designation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.DesignationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Designation");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Location");

                entity.HasOne(d => d.ReportingToUser)
                    .WithMany(p => p.InverseReportingToUser)
                    .HasForeignKey(d => d.ReportingToUserId)
                    .HasConstraintName("FK_Users_ReportingUsers");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Role");
            });

            modelBuilder.Entity<Meeting>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SrNo).HasDefaultValue(0);

                entity.Property(e => e.MeetingStatus)
                  .IsUnicode(false)
                  .HasComputedColumnSql("(case [MeetingStatusId] when (0) then 'Pending' when (1) then 'Expired' when (2) then 'Closed' when (3) then 'Cancelled' when (4) then 'OnHold' end)", false);

                entity.Property(e => e.MeetingStatusId)
                .HasDefaultValue(0);

                entity.HasOne(d => d.Client)
                  .WithMany(p => p.Meetings)
                  .HasForeignKey(d => d.ClientId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_Meeting_Client");

                entity.HasOne(d => d.Company)
                .WithMany(p => p.Meetings)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Meeting_Company");

                entity.HasOne(d => d.CompanyUser)
                .WithMany(p => p.MeetingsCompanyUser)
                .HasForeignKey(d => d.CompanyUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Meeting_CompanyUser");

                entity.HasOne(d => d.ClientEmployee)
                .WithMany(p => p.MeetingsClientEmployee)
                .HasForeignKey(d => d.ClientEmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Meeting_ClientEmployee");

                entity.HasOne(d => d.ClientBusinessUnit)
               .WithMany(p => p.MeetingsClientBusinessUnit)
               .HasForeignKey(d => d.ClientBusinessUnitId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_Meeting_ClientBusinessUnit");
                /////
                //entity.HasOne(d => d.VisitedClientBusinessUnit)
                //    .WithMany(p => p.MeetingsVisitedClientBusinessUnit)
                //    .HasForeignKey(d => d.VisitedClientBusinessUnitId)
                //    .HasConstraintName("FK_Meeting_VisitedClientBusinessUnit");

                //entity.HasOne(d => d.VisitedClientEmployee)
                //    .WithMany(p => p.MeetingsVisitedClientEmployee)
                //    .HasForeignKey(d => d.VisitedClientEmployeeId)
                //    .HasConstraintName("FK_Meeting_VisitedClientEmployee");

                //entity.HasOne(d => d.VisitedCompanyUser)
                //    .WithMany(p => p.MeetingsVisitedCompanyUser)
                //    .HasForeignKey(d => d.VisitedCompanyUserId)
                //    .HasConstraintName("FK_Meeting_VisitedCompanyUser");

                entity.HasOne(d => d.VisitedCompanyUser)
               .WithMany(p => p.MeetingsVisitedCompanyUser)
               .HasForeignKey(d => d.VisitedCompanyUserId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_Meeting_VisitedCompanyUser");

                entity.HasOne(d => d.VisitedClientEmployee)
                .WithMany(p => p.MeetingsVisitedClientEmployee)
                .HasForeignKey(d => d.VisitedClientEmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Meeting_VisitedClientEmployee");

                entity.HasOne(d => d.VisitedClientBusinessUnit)
               .WithMany(p => p.MeetingsVisitedClientBusinessUnit)
               .HasForeignKey(d => d.VisitedClientBusinessUnitId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_Meeting_VisitedClientBusinessUnit");
            });

            modelBuilder.Entity<MeetingObservationAndOtherMatter>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsCritical).HasDefaultValue(false);

                entity.HasOne(d => d.Meeting)
                 .WithMany(p => p.MeetingObservations)
                 .HasForeignKey(d => d.MeetingId)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_MeetingObservationAndOtherMatter_Meeting");

                entity.HasOne(d => d.AssignedToUser)
                .WithMany(p => p.MeetingObservations)
                .HasForeignKey(d => d.AssignedToUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MeetingObservationAndOtherMatter_User");

                //entity.HasOne(d => d.CompanyObservation)
                //.WithMany(p => p.MeetingObservations)
                //.HasForeignKey(d => d.CompanyObservationId)
                //.OnDelete(DeleteBehavior.ClientSetNull)
                //.HasConstraintName("FK_MeetingObservationAndOtherMatter_CompanyObservation");
            });

            modelBuilder.Entity<MeetingOpportunity>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsCritical).HasDefaultValue(false);

                entity.HasOne(d => d.Meeting)
                 .WithMany(p => p.MeetingOpportunities)
                 .HasForeignKey(d => d.MeetingId)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_MeetingOpportunity_Meeting");

                entity.HasOne(d => d.AssignedToUser)
                .WithMany(p => p.MeetingOpportunites)
                .HasForeignKey(d => d.AssignedToUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MeetingOpportunity_User");

                entity.HasOne(d => d.CompanyOpportunity)
                .WithMany(p => p.MeetingOpportunites)
                .HasForeignKey(d => d.CompanyOpportunityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MeetingOpportunity_CompanyOpportunity");
            });

            //modelBuilder.Entity<MeetingOther>(entity =>
            //{
            //    entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            //    entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            //    entity.Property(e => e.IsCritical).HasDefaultValue(false);

            //    entity.HasOne(d => d.Meeting)
            //     .WithMany(p => p.MeetingOthers)
            //     .HasForeignKey(d => d.MeetingId)
            //     .OnDelete(DeleteBehavior.ClientSetNull)
            //     .HasConstraintName("FK_MeetingOther_Meeting");

            //    entity.HasOne(d => d.AssignedToUser)
            //    .WithMany(p => p.MeetingOthers)
            //    .HasForeignKey(d => d.AssignedToUserId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_MeetingOther_User");
            //});

            modelBuilder.Entity<MeetingRisk>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsCritical).HasDefaultValue(false);

                entity.HasOne(d => d.Meeting)
                 .WithMany(p => p.MeetingRisks)
                 .HasForeignKey(d => d.MeetingId)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_MeetingRisk_Meeting");

                entity.HasOne(d => d.AssignedToUser)
                .WithMany(p => p.MeetingRisks)
                .HasForeignKey(d => d.AssignedToUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MeetingRisk_User");

                entity.HasOne(d => d.CompanyRisk)
                .WithMany(p => p.MeetingRisks)
                .HasForeignKey(d => d.CompanyRiskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MeetingRisk_CompanyRisk");
            });

            //modelBuilder.Entity<MeetingOtherMatter>(entity =>
            //{
            //    entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            //    entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            //    entity.Property(e => e.IsCritical).HasDefaultValue(false);

            //    entity.HasOne(d => d.Meeting)
            //     .WithMany(p => p.MeetingOtherMatters)
            //     .HasForeignKey(d => d.MeetingId)
            //     .OnDelete(DeleteBehavior.ClientSetNull)
            //     .HasConstraintName("FK_MeetingOtherMatter_Meeting");

            //    entity.HasOne(d => d.AssignedToUser)
            //    .WithMany(p => p.MeetingOtherMatters)
            //    .HasForeignKey(d => d.AssignedToUserId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_MeetingOtherMatter_User");

            //});

            modelBuilder.Entity<MeetingClientAttendee>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Meeting)
                 .WithMany(p => p.MeetingClientAttendees)
                 .HasForeignKey(d => d.MeetingId)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_MeetingClientAttendee_Meeting");

                entity.HasOne(d => d.ClientEmployee)
                .WithMany(p => p.MeetingClientAttendees)
                .HasForeignKey(d => d.ClientEmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MeetingClientAttendee_ClientEmployee");

            });

            modelBuilder.Entity<MeetingCompanyAttendee>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Meeting)
                 .WithMany(p => p.MeetingCompanyAttendees)
                 .HasForeignKey(d => d.MeetingId)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_MeetingCompanyAttendee_Meeting");

                entity.HasOne(d => d.CompanyUser)
                .WithMany(p => p.MeetingCompanyAttendees)
                .HasForeignKey(d => d.CompanyUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MeetingCompanyAttendee_User");
            });

            modelBuilder.Entity<BusinessSubSegment>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Active).HasDefaultValue(true);

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasIndex(e => new { e.BusinessSegmentId, e.Name }).IsUnique();

                entity.HasOne(d => d.BusinessSegment)
               .WithMany(p => p.BusinessSubSegments)
               .HasForeignKey(d => d.BusinessSegmentId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_BusinessSubSegment_BusinessSegment");
            });

            //modelBuilder.Entity<StandardObservation>(entity =>
            //{
            //    entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            //    entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            //    entity.Property(e => e.Active).HasDefaultValue(true);

            //    entity.HasIndex(e => new { e.BusinessSegmentId, e.Name }).IsUnique();

            //    entity.HasOne(d => d.BusinessSegment)
            //       .WithMany(p => p.StandardObservations)
            //       .HasForeignKey(d => d.BusinessSegmentId)
            //       .OnDelete(DeleteBehavior.ClientSetNull);
            //});

            //modelBuilder.Entity<CompanyObservation>(entity =>
            //{
            //    entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            //    entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            //    entity.Property(e => e.Active).HasDefaultValue(true);

            //    entity.HasIndex(e => new { e.CompanyId, e.Name }).IsUnique();

            //    entity.HasOne(d => d.BusinessSegment)
            //        .WithMany(p => p.CompanyObservations)
            //        .HasForeignKey(d => d.BusinessSegmentId)
            //        .HasConstraintName("FK_CompanyObservations_BusinessSegment");

            //    entity.HasOne(d => d.Company)
            //        .WithMany(p => p.CompanyObservations)
            //        .HasForeignKey(d => d.CompanyId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_CompanyObservations_Company");
            //});
            #endregion
        }
    }
}
