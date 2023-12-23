using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.DTOs
{
    public class spTestResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Sequence { get; set; }
    }

    public class spGetCompanyRisksOfClientEmployeeResult
    {
        public Guid RiskID { get; set; }
        public string RiskName { get; set; } = null!;
        public string RiskDescription { get; set; } = null!;
    }

    public class spGetCompanyOpportunitiesOfClientEmployeeResult
    {
        public Guid OpportunityID { get; set; }
        public string OpportunityName { get; set; } = null!;
        public string OpportunityDescription { get; set; } = null!;
    }
}
