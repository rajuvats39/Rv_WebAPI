namespace Rv_WebAPI.Models
{
    public class UserDetailsModel
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Number { get; set; }
        public DateTime DOB { get; set; }
        public string? Gender { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public bool AcceptTerms { get; set; }
        public string? Feedback { get; set; }
        public string? Image { get; set; }
        public int Score { get; set; }
        public string? CountryName { get; set; }
        public string? StateName { get; set; }
        public string? CityName { get; set; }
    }
}
