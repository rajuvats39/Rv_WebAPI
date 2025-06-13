namespace BackEnd.Models
{
    public class CustomerModel
    {
        #region Properties
        public int CustomerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public DateTime? RegistrationDate { get; set; }
        #endregion
    }
}
