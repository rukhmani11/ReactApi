using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;

namespace VoV.Data.Entities
{
    public class ClientGroup : BaseEntity
    {
        public ClientGroup()
        {
            Clients = new HashSet<Client>();
        }

        [MaxLength(200)]
        public string GroupName { get; set; } = null!;
        [MaxLength(50)]
        public string GroupCIFNo { get; set; } = null!;
        public bool Active { get; set; }
        public Guid CompanyId { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        public virtual Company Company { get; set; }
    }
}
