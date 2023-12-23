using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.Entities
{
    public class Location : BaseEntity
    {
        public Location()
        {
            this.Users = new HashSet<User>();
            InverseParent = new HashSet<Location>();
        }
        [MaxLength(200)]
        public string Name { get; set; } = null!;
        [MaxLength(50)]
        public string Code { get; set; } = null!;
        public bool Active { get; set; }
        public Guid CompanyId { get; set; }
        public Guid? ParentId { get; set; }
        public virtual Company Company { get; set; } = null!;
        public virtual Location? Parent { get; set; }
        public virtual ICollection<Location> InverseParent { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
