namespace Core.Constants
{
    public class Constants
    {

        #region Messages from CUSTOMER entity
        public static string MSG_CUSTOMER_ADDED_SUCCESS { get; set; } = "Customer registered - ID {0} - Name: {1} - Phone: {2} - Address: {3}";
        public static string MSG_CUSTOMER_ADDED_ERROR { get; set; } = "Error registering a customer";
        public static string MSG_CUSTOMER_UPDATED_ERROR { get; set; } = "Error updating a customer";
        public static string MSG_CUSTOMER_UPDATED_SUCCESS { get; set; } = "Customer updated - ID {0} - Name: {1} - Phone: {2} - Address: {3}";
        public static string MSG_CUSTOMER_DELETED_ERROR { get; set; } = "Error deleting a customer";
        public static string MSG_CUSTOMER_DELETED_SUCCESS { get; set; } = "Customer deleted - ID {0} - Name: {1} - Phone: {2} - Address: {3}";
        #endregion

        #region Messages from DOCUMENT entity
        public static string MSG_DOCUMENT_ADDED_SUCCESS { get; set; } = "Document registered - Id {0} - Description: {1}";
        public static string MSG_DOCUMENT_ADDED_ERROR { get; set; } = "Error registering a document";
        public static string MSG_DOCUMENT_UPDATED_ERROR { get; set; } = "Error updating a document";
        public static string MSG_DOCUMENT_UPDATED_SUCCESS { get; set; } = "Document updated - Id {0} - Description: {1}";
        public static string MSG_DOCUMENT_DELETED_ERROR { get; set; } = "Error deleting a document";
        public static string MSG_DOCUMENT_DELETED_SUCCESS { get; set; } = "Document deleted - Id {0} - Description: {1}";
        #endregion

        #region Messages from LOCATION entity
        public static string MSG_LOCATION_ADDED_SUCCESS { get; set; } = "Location registered - Id {0} - Description: {1}";
        public static string MSG_LOCATION_ADDED_ERROR { get; set; } = "Error registering a location";
        public static string MSG_LOCATION_UPDATED_ERROR { get; set; } = "Error updating a location";
        public static string MSG_LOCATION_UPDATED_SUCCESS { get; set; } = "Location updated - Id {0} - Description: {1}";
        public static string MSG_LOCATION_DELETED_ERROR { get; set; } = "Error deleting a location";
        public static string MSG_LOCATION_DELETED_SUCCESS { get; set; } = "Location deleted - Id {0} - Description: {1}";
        #endregion

        #region Messages from LOCKER entity
        public static string MSG_LOCKER_ADDED_SUCCESS { get; set; } = "Locker registered - Id {0} - Serial Number: {1} - Id Location: {2} - Id Price: {3} - Rented: {4}";
        public static string MSG_LOCKER_ADDED_ERROR { get; set; } = "Error registering a locker";
        public static string MSG_LOCKER_UPDATED_ERROR { get; set; } = "Error updating a locker";
        public static string MSG_LOCKER_UPDATED_SUCCESS { get; set; } = "Locker updated  - Id {0} - Serial Number: {1} - Id Location: {2} - Id Price: {3} - Rented: {4}";
        public static string MSG_LOCKER_DELETED_ERROR { get; set; } = "Error deleting a locker";
        public static string MSG_LOCKER_DELETED_SUCCESS { get; set; } = "Locker deleted  - Id {0} - Serial Number: {1} - Id Location: {2} - Id Price: {3} - Rented: {4}";
        #endregion

        #region Messages from PRICE entity
        public static string MSG_PRICE_ADDED_SUCCESS { get; set; } = "Price registered - Id {0} - Value: {1}";
        public static string MSG_PRICE_ADDED_ERROR { get; set; } = "Error registering a price";
        public static string MSG_PRICE_UPDATED_ERROR { get; set; } = "Error updating a price";
        public static string MSG_PRICE_UPDATED_SUCCESS { get; set; } = "Price updated  - Id {0} - Value: {1}";
        public static string MSG_PRICE_DELETED_ERROR { get; set; } = "Error deleting a price";
        public static string MSG_PRICE_DELETED_SUCCESS { get; set; } = "Price deleted  - Id {0} - Value: {1}";
        #endregion

        #region Messages from RENT entity
        public static string MSG_RENT_ADDED_SUCCESS { get; set; } = "Rent registered - Id {0} - Id Customer: {1} - Id Locker: {2} - Rental Date: {3} - Return Date: {4}";
        public static string MSG_RENT_ADDED_ERROR { get; set; } = "Error registering a rent";
        public static string MSG_RENT_UPDATED_ERROR { get; set; } = "Error updating a rent";
        public static string MSG_RENT_UPDATED_SUCCESS { get; set; } = "Rent updated  - Id {0} - Id Customer: {1} - Id Locker: {2} - Rental Date: {3} - Return Date: {4}";
        public static string MSG_RENT_DELETED_ERROR { get; set; } = "Error deleting a rent";
        public static string MSG_RENT_DELETED_SUCCESS { get; set; } = "Rent deleted  - Id {0} - Id Customer: {1} - Id Locker: {2} - Rental Date: {3} - Return Date: {4}";
        #endregion

        #region Messages from ROLE entity
        public static string MSG_ROLE_ADDED_SUCCESS { get; set; } = "Role registered - Id {0} - Description: {1}";
        public static string MSG_ROLE_ADDED_ERROR { get; set; } = "Error registering a role";
        public static string MSG_ROLE_UPDATED_ERROR { get; set; } = "Error updating a role";
        public static string MSG_ROLE_UPDATED_SUCCESS { get; set; } = "Role updated  - Id {0} - Description: {1}";
        public static string MSG_ROLE_DELETED_ERROR { get; set; } = "Error deleting a role";
        public static string MSG_ROLE_DELETED_SUCCESS { get; set; } = "Role deleted  - Id {0} - Description: {1}";
        #endregion

        #region Messages from USER entity
        public static string MSG_USER_ADDED_SUCCESS { get; set; } = "User registered - Id {0} - User Name: {1} - ID Perfil: {2}";
        public static string MSG_USER_ADDED_ERROR { get; set; } = "Error registering a user";
        public static string MSG_USER_UPDATED_ERROR { get; set; } = "Error updating a user";
        public static string MSG_USER_UPDATED_SUCCESS { get; set; } = "User updated  - Id {0} - User Name: {1} - ID Perfil: {2}";
        public static string MSG_USER_DELETED_ERROR { get; set; } = "Error deleting a user";
        public static string MSG_USER_DELETED_SUCCESS { get; set; } = "User deleted  - Id {0} - User Name: {1} - ID Perfil: {2}";
        #endregion
    }
}
