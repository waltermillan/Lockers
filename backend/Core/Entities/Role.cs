using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    [Table("Roles")]
    public class Role : BaseEntity
    {
        [Column("description")]
        public string Description { get; set; }
    }
}
