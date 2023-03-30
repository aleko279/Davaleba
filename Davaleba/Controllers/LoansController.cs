using Davaleba.Interface;
using Davaleba.Models;
using LoggerService;
using Microsoft.AspNetCore.Mvc;

namespace Davaleba.Controllers
{
    public class LoansController : Controller
    {
        private Iloans _loans;
        private ILoggerManager _logger;
        private readonly IJWTTokenServices _jwttokenservice;
        public LoansController(Iloans loans, IJWTTokenServices jWTTokenServices, ILoggerManager loggerManager = null)
        {
            _loans = loans;
            _jwttokenservice = jWTTokenServices;
            _logger = loggerManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInfo("Fetching all the Users from the Database Davaleba");
            var loans = _loans.GetLoanApplications();
            _logger.LogInfo($"Returning {loans.Count} loan applications.");
            return Ok(loans);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _logger.LogInfo("Fetching the loan application from the Database Davaleba");
            var loan = _loans.GetLoanApplication(id);
            _logger.LogInfo($"Returning {loan.Id} user.");
            return Ok(loan);
        }

        [HttpPost]
        public IActionResult Post(LoanCustomClass loan)
        {
            _logger.LogInfo("Adding the User to the Database Davaleba");
            _loans.AddLoanApplication(loan);
            _logger.LogInfo($"Adding {loan.Id} user.");
            return new JsonResult(new
            {
                message = "Loan Application Added",
                success = true // success status
            })
            {
                StatusCode = StatusCodes.Status200OK // Status code here 
            };
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInfo("Adding the Loan Application to the Database Davaleba");
            _loans.DeleteLoanApplication(id);
            return Ok(new { Message = "Loan Application Deleted" });
        }
    }
}
