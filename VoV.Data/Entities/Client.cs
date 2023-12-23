using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.Entities
{
    public class Client : BaseEntity
    {
        public Client()
        {
            ClientAccounts = new HashSet<ClientAccount>();
            ClientBusinessUnits = new HashSet<ClientBusinessUnit>();
            ClientFinancials = new HashSet<ClientFinancial>();
            ClientEmployees = new HashSet<ClientEmployee>();
            Meetings = new HashSet<Meeting>();
        }
        [MaxLength(200)]
        public string Name { get; set; } = null!;
        [MaxLength(50)]
        public string CIFNo { get; set; } = null!;
        public Guid CompanyId { get; set; }
        public Guid ClientGroupId { get; set; }
        public byte VisitingFrequencyInMonth { get; set; }
        public bool Active { get; set; }

        public virtual ClientGroup ClientGroup { get; set; } = null!;
        public virtual Company Company { get; set; } = null!;
        public virtual ICollection<ClientAccount> ClientAccounts { get; set; }
        public virtual ICollection<ClientBusinessUnit> ClientBusinessUnits { get; set; }
        public virtual ICollection<ClientFinancial> ClientFinancials { get; set; }
        public virtual ICollection<ClientEmployee> ClientEmployees { get; set; }
        public virtual ICollection<Meeting> Meetings { get; set; }
    }
}
