using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.Entities
{
    public class FinancialYear : BaseEntity
    {
        public FinancialYear()
        {
            this.ClientFinancials = new HashSet<ClientFinancial>();
        }
        [Column(TypeName = "date")]
        public DateTime FromDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime ToDate { get; set; }
        [MaxLength(15)]
        public string Abbr { get; set; } = null!;

        public virtual ICollection<ClientFinancial> ClientFinancials { get; set; }
    }
}
