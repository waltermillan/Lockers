using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("Prices")]
public class Price : BaseEntity
{
    [Column("value")]
    public decimal Value { get; set; }
}
