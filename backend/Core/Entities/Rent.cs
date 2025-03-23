using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("Rents")]
public class Rent : BaseEntity
{
    [Column("id_customer")]
    public int IdCustomer { get; set; }
    [Column("id_locker")]
    public int IdLocker { get; set; }
    [Column("rental_date")]
    public DateTime RentalDate { get; set; }
    [Column("return_date")]
    public DateTime ReturnDate { get; set; }
    [Column("user_name")]
    public string UserName { get; set; }
}
