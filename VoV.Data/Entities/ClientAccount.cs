using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.Entities
{
    public class ClientAccount : BaseEntity
    {
        [MaxLength(100)]
        public string AccountNo { get; set; } = null!;
        public DateTime BalanceAsOn { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }
        public Guid ClientId { get; set; }
        public Guid AccountTypeId { get; set; }
        [Column(TypeName = "nchar(3)")]
        public string? CurrencyCode { get; set; }
        public virtual AccountType AccountType { get; set; } = null!;
        public virtual Client Client { get; set; } = null!;
        public virtual Currency? Currency { get; set; }
    }
}
