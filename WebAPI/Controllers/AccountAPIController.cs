using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/AccountAPI")]
    [ApiController]
    public class AccountAPIController : ControllerBase
    {
        [Route("GetAll")]
        public IActionResult GetAllAccounts(int page = 1, int pageSize = 10)
        {
            AccountDataModel result = new AccountDataModel();
           

            using (var ctx = new ApplicationContext())
            {
                result.Count = ctx.Accounts.Count();
                result.Data = ctx.Accounts
                    .Skip(((page - 1) * pageSize))
                    .Take(pageSize)
                    .Select(s => new AccountViewModel()
                    {
                        Id = s.Id,
                        Account_Number = s.Account_Number,
                        ServerT = s.Server_DateTime,
                        UTC_T = s.DateTime_UTC,
                        Update_UTCT = s.Update_DateTime_UTC,
                        Status = s.Status,
                        Balance = s.Balance,
                        Available_Balance = s.Available_Balance,
                        Currency=s.Currency,
                        User_Id=s.User_ID
                    }).ToList();
            }

            if (result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }
        [Route("SaveAccount")]
        [HttpPost]
        public IActionResult PostNewAccount(AccountViewModel account)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new ApplicationContext())
            {
                ctx.Accounts.Add(new Account()
                {
                    User_ID = account.User_Id,
                    Account_Number = account.Account_Number,
                    Update_DateTime_UTC = account.Update_UTCT,
                    Status = account.Status,
                    Balance = account.Balance,
                    Available_Balance = account.Available_Balance,
                    Currency = account.Currency,
                }); ;

                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}
