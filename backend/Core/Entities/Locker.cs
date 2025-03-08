using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("Lockers")]
public class Locker : BaseEntity
{
    [Column("serial_number")]
    public int SerialNumber { get; set; }

    [Column("id_location")]
    public int IdLocation { get; set; }

    [Column("id_price")]
    public int IdPrice { get; set; }

    [Column("rented")]
    public bool Rented { get; set; }
}
