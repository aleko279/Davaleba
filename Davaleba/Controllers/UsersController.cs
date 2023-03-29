using Davaleba.Interface;
using Davaleba.Models;
using Microsoft.AspNetCore.Mvc;
using LoggerService;
using Davaleba.Helpers;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
namespace Davaleba.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsers _users;
        private ILoggerManager _logger;
        private readonly IJWTTokenServices _jwttokenservice;
        public UsersController(IUsers users, IJWTTokenServices jWTTokenServices, ILoggerManager loggerManager = null)
        {
            _users = users;
            _jwttokenservice = jWTTokenServices;
            _logger = loggerManager;
        }

        //        [HttpGet, Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInfo("Fetching all the Users from the Database Davaleba");
            var users = _users.GetUsers();
            //throw new AppException("Exception while fetching all the Users from the Database Davaleba.");
            _logger.LogInfo($"Returning {users.Count} users.");
            return Ok(users);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _logger.LogInfo("Fetching the User from the Database Davaleba");
            var user = _users.GetUser(id);
            _logger.LogInfo($"Returning {user.Id} user.");
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post(UserCustomClass user)
        {
            _logger.LogInfo("Adding the User to the Database Davaleba");
            _users.AddUser(user);
            _logger.LogInfo($"Adding {user.Id} user.");
            // return Ok(new { Message = "User Added" });
            return new JsonResult(new
            {
                message = "User Added",
                success = true // success status
            })
            {
                StatusCode = StatusCodes.Status200OK // Status code here 
            };
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInfo("Adding the User to the Database Davaleba");
            _users.DeleteUser(id);
            return Ok(new { Message = "User Deleted" });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Authenticate")]
        public IActionResult Authenticate(User users)
        {
            var token = _jwttokenservice.Authenticate(users);

            if (token == null)
            {
                return Ok(new { Message = "Unauthorized" });
            }

            return Ok(token);
        }
    }
}
