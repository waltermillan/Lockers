namespace API.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }                 // Table: User | Field: Id
        public string UserName { get; set; }        // Table: User | Field: UserName
        public int IdRole { get; set; }           // Table: Role | Field: IdPerfil
        public string Role { get; set; }          // Table: Role | Field: Description
    }
}
