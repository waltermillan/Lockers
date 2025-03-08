using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("Customers")]
public class Customer : BaseEntity
{
    [Column("name")]
    public string Name { get; set; }
    [Column("phone")]
    public string Phone { get; set; }
    [Column("document")]
    public string Document { get; set; }
    [Column("address")]
    public string Address { get; set; }
    [Column("id_document")]
    public int IdDocument { get; set; }
}
