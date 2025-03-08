using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class LockerDTO
    {
        public int Id { get; set; } //sale de la tabla Locker campo Id

        public int SerialNumber { get; set; } //sale de la tabla Locker campo SerialNumber

        public int IdLocation { get; set; } //sale de la tabla Location campo Id
        public string Location { get; set; } //sale de la tabla Location campo Address
        public int IdPrice { get; set; }//sale de la tabla Price campo Id
        public decimal Price { get; set; }//sale de la tabla Price campo Value

        public bool Rented { get; set; } //sale de la tabla Locker campo Rented
    }
}
