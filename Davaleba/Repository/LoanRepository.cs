using AutoMapper;
using Davaleba.Helpers;
using Davaleba.Interface;
using Davaleba.Models;

namespace Davaleba.Repository
{
    public class LoanRepository : Iloans
    {
        private DavalebaContext _davalebaContext;
        private readonly IMapper _mapper;
        public LoanRepository(DavalebaContext davalebaContext, IMapper mapper)
        {
            _davalebaContext = davalebaContext;
            _mapper = mapper;
        }
        //public List<User> GetLoanApplications()
        //{
        //    return _davalebaContext.LoanApplications.ToList();
        //}
        public List<LoanApplication> GetLoanApplications()
        {
            return _davalebaContext.LoanApplications.ToList(); ;
        }
        public LoanApplication GetLoanApplication(int id)
        {
            var loan = _davalebaContext.LoanApplications.Find(id);
            if (loan == null)
            {
                throw new AppException("loan Application Not Found");
            }
            return loan;
        }
        public void AddLoanApplication(LoanCustomClass loanCustomClass)
        {
            //var userExist = _davalebaContext.LoanApplications.Any(m => m.UserName == userCustomClass.UserName);
            //if (userExist == true)
            //{
            //    throw new AppException("User UserName: " + userCustomClass.UserName + " Already Exist");
            //}
            var loan = _mapper.Map<User>(loanCustomClass);
            _davalebaContext.Add(loan);
            _davalebaContext.SaveChanges();
        }
        public void UpdateLoanApplication(LoanCustomClass loan)
        {

            //_davalebaContext.LoanApplications.Update(user);
            _davalebaContext.Entry(loan).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public void DeleteLoanApplication(int id)
        {
            var loan = _davalebaContext.LoanApplications.Find(id);
            if (loan == null)
            {
                throw new AppException("loan Application Not Found");
            }
            _davalebaContext.LoanApplications.Remove(loan);
            _davalebaContext.SaveChanges();
        }
    }
}
