
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoV.Data.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class BusinessSegment : BaseEntity
    {
        public BusinessSegment()
        {
            Active = true;

            this.StandardRisks = new HashSet<StandardRisk>();
            this.StandardOpportunities = new HashSet<StandardOpportunity>();
            //this.StandardObservations = new HashSet<StandardObservation>();
            this.CompanyRisks = new HashSet<CompanyRisk>();
            this.ClientBusinessUnits = new HashSet<ClientBusinessUnit>();
            //this.CompanyObservations = new HashSet<CompanyObservation>();
            this.CompanyOpportunities = new HashSet<CompanyOpportunity>();
            this.BusinessSubSegments = new HashSet<BusinessSubSegment>();
        }

        [MaxLength(200)]
        public string Name { get; set; } = null!;
        public bool Active { get; set; }

        [MaxLength(10)]
        public string Code { get; set; } = null!;

        public virtual ICollection<StandardRisk> StandardRisks { get; set; }
        public virtual ICollection<StandardOpportunity> StandardOpportunities { get; set; }
        //public virtual ICollection<StandardObservation> StandardObservations { get; set; }
        public virtual ICollection<CompanyRisk> CompanyRisks { get; set; }
        public virtual ICollection<ClientBusinessUnit> ClientBusinessUnits { get; set; }
        //public virtual ICollection<CompanyObservation> CompanyObservations { get; set; }
        public virtual ICollection<CompanyOpportunity> CompanyOpportunities { get; set; }
        public virtual ICollection<BusinessSubSegment> BusinessSubSegments { get; set; }
    }
}
