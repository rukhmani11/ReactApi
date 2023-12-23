using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.Entities
{
    public class Company : BaseEntity
    {
        public Company()
        {
            this.Users = new HashSet<User>();
            this.AppSettings = new HashSet<AppSetting>();
            this.CompanyRisks = new HashSet<CompanyRisk>();
            //this.CompanyObservations = new HashSet<CompanyObservation>();
            this.CompanyOpportunities = new HashSet<CompanyOpportunity>();
            this.Locations = new HashSet<Location>();
            this.Designations = new HashSet<Designation>();
            this.BusinessUnits = new HashSet<BusinessUnit>();
            this.Clients = new HashSet<Client>();
            this.ClientGroups = new HashSet<ClientGroup>();
            this.Meetings = new HashSet<Meeting>();
        }

        [MaxLength(200)]
        public string Name { get; set; } = null!;
        [MaxLength(2000)]
        public string Address { get; set; } = null!;
        [MaxLength(100)]
        public string? Logo { get; set; }
        [MaxLength(100)]
        public string Email { get; set; } = null!;
        [MaxLength(200)]
        public string? Website { get; set; }
        public bool ADLoginYn { get; set; }
        public bool MobileIronYn { get; set; }
        public bool Active { get; set; }
        public string? ThemeLightHexCode { get; set; }
        public string? ThemeDarkHexCode { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<AppSetting> AppSettings { get; set; }
        public virtual ICollection<CompanyRisk> CompanyRisks { get; set; }
        public virtual ICollection<CompanyOpportunity> CompanyOpportunities { get; set; }
        //public virtual ICollection<CompanyObservation> CompanyObservations { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<Designation> Designations { get; set; }
        public virtual ICollection<BusinessUnit> BusinessUnits { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<ClientGroup> ClientGroups { get; set; }
        public virtual ICollection<Meeting> Meetings { get; set; }
    }
}
