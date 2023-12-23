using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.Entities
{
    public class Currency
    {
        public Currency()
        {
            this.ClientFinancials = new HashSet<ClientFinancial>();
            this.ClientAccounts= new HashSet<ClientAccount>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "nchar(3)")]
        public string Code { get; set; } = null!;

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; } = null!;

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedOn { get; set; }
        public Guid CreatedById { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedById { get; set; }

        public virtual ICollection<ClientFinancial> ClientFinancials { get; set; }
        public virtual ICollection<ClientAccount> ClientAccounts { get; set; }
    }
}
