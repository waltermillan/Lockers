using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("Documents")]
public class Document : BaseEntity
{
    [Column("description")]
    public string Description { get; set; }
}
