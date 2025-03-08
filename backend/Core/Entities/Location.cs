using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("Locations")]
public class Location : BaseEntity
{
    [Column("address")]
    public string Address { get; set; }
    [Column("postal_code")]
    public int PostalCode { get; set; }
}
