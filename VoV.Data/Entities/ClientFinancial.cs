using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.Entities
{
    public class ClientFinancial : BaseEntity
    {
        public ClientFinancial()
        {
            this.ClientFinancialFiles = new HashSet<ClientFinancialFile>();
        }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Turnover { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Profit { get; set; }
        public Guid ClientId { get; set; }
        public Guid FinancialYearId { get; set; }

        [Column(TypeName = "nchar(3)")]
        public string? CurrencyCode { get; set; }
        public virtual Client Client { get; set; } = null!;
        public virtual FinancialYear FinancialYear { get; set; } = null!;
        public virtual ICollection<ClientFinancialFile> ClientFinancialFiles { get; set; }
        public virtual Currency? Currency { get; set; }
    }
}
