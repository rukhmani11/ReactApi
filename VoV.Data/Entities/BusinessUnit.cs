using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VoV.Data.Entities
{
    public class BusinessUnit : BaseEntity
    {
        public BusinessUnit()
        {
            InverseParent = new HashSet<BusinessUnit>();
            Users = new HashSet<User>();
        }

        [MaxLength(200)]
        public string Name { get; set; } = null!;

        [MaxLength(50)]
        public string Code { get; set; } = null!;
        public bool Active { get; set; }

        //[ForeignKey("Company")]
        public Guid CompanyId { get; set; }

        //[ForeignKey("Parent")]
        public Guid? ParentId { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual BusinessUnit? Parent { get; set; }
        public virtual ICollection<BusinessUnit> InverseParent { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
