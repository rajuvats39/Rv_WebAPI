namespace Rv_WebAPI.Models
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? JoiningDate { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string? Gender { get; set; }
        public string? Skills { get; set; }
        public string? ResumePath { get; set; }
        public string? ProfileImage { get; set; }
        public bool? IsActive { get; set; }
        public decimal? Salary { get; set; }
        public float ExperienceYears { get; set; }
        public string? PasswordHash { get; set; }
        public string? Bio { get; set; }
    }
}
