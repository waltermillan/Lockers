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
        public int Id { get; set; }                     //From table: Locker field: Id
        public int SerialNumber { get; set; }           //From table: Locker field: SerialNumber
        public int IdLocation { get; set; }             //From table: Location field: Id
        public string Location { get; set; }            //From table: Location field: Address
        public int IdPrice { get; set; }                //From table: Price field: Id
        public decimal Price { get; set; }              //From table: Price field: Value
        public bool Rented { get; set; }                //From table: Locker field: Rented
    }
}
