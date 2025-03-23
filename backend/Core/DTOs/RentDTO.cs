using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class RentDTO
    {
        public int Id { get; set; }                     //Fron table Rent field: Id.
        public string Customer { get; set; }            //Fron table Customer field: Name
        public int IdCustomer { get; set; }             //Fron table Rent field: IdCustomer
        public int IdLocker { get; set; }               //Fron table Locker field: Id
        public string Locker { get; set; }              //Fron table Locker field: SerialNumber + Locatiom  field: Address
        public DateTime RentalDate { get; set; }        //Fron table Rent field: RentalDate
        public DateTime ReturnDate { get; set; }        //Fron table Rent field: ReturnDate
        public string UserName { get; set; }            //Fron table Rent field: UserName
    }
}
