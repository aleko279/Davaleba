using Davaleba.Models;

namespace Davaleba.Interface
{
    public interface Iloans
    {
        //public List<User> GetUsers();
        List<LoanApplication> GetLoanApplications();
        public LoanApplication GetLoanApplication(int id);
        public void AddLoanApplication(LoanCustomClass user);
        public void UpdateLoanApplication(LoanCustomClass user);
        public void DeleteLoanApplication(int id);
    }
}
