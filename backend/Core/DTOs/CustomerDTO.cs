using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }                 //From table: Customer field: Id
        public string Name { get; set; }            //From table: Customer field: Name
        public string Phone { get; set; }           //From table: Customer field: Phone
        public string Document { get; set; }        //From table: Customer field: Document
        public string Address { get; set; }         //From table: Customer field: Address
        public int IdDocument { get; set; }         //From table: Customer field: IdDocument
        public string TypeDocument { get; set; }    //From table: Customer field: Description
    }
}
