using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            //CreatedOn = DateTime.Now; //automapper when converts model to entity, this value is removed.. 
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid CreatedById { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedOn { get; set; }

        public Guid? UpdatedById { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdatedOn { get; set; }
    }
}
