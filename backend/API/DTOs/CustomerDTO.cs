namespace API.DTOs;

public class CustomerDTO
{
    public int Id { get; set; }                 //From table: Customer field: Id
    public string Name { get; set; }            //From table: Customer field: Name
    public string Phone { get; set; }           //From table: Customer field: Phone
    public string Document { get; set; }        //From table: Customer field: Document
    public string Address { get; set; }         //From table: Customer field: Address
    public int IdDocument { get; set; }         //From table: Customer field: IdDocument
    public string TypeDocument { get; set; }    //From table: Documents field: Description
}
