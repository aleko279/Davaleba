namespace Davaleba.Models
{
    public class UserCustomClass
    {
        public int Id { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public int? PersonalNumber { get; set; }

        public DateTime? BirthDate { get; set; }

        public virtual ICollection<LoanApplication> LoanApplications { get; } = new List<LoanApplication>();
    }
}
