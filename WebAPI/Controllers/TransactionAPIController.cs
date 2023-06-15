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
    [Route("api/TransactionAPI")]
    [ApiController]
    public class TransactionAPIController : ControllerBase
    {
        // GET: api/<TransactioAPIController>
        [Route("GetAll")]
        public IActionResult GetAllTransactions(int page = 1, int pageSize = 10)
        {
            TransactionDataModel result = new TransactionDataModel();


            using (var ctx = new ApplicationContext())
            {
                result.Count = ctx.Transactions.Count();
                result.Data = ctx.Transactions
                    .Skip(((page - 1) * pageSize))
                    .Take(pageSize)
                    .Select(s => new TransactionViewModel()
                    {
                        Id = s.Id,
                        User_ID = s.User_ID,
                        Account_ID=s.Account_ID,
                        ServerT = s.Server_DateTime,
                        UTC_T = s.DateTime_UTC,
                        Is_Credit=s.Is_Credit,
                        Amount=s.Amount,
                        Currency=s.Currency
                    }).ToList();
            }

            if (result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }



        [Route("SaveTransaction")]
        [HttpPost]
        public IActionResult PostNewTransaction(TransactionViewModel Transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new ApplicationContext())
            {
                ctx.Transactions.Add(new Transaction()
                {
                    User_ID = Transaction.User_ID,
                    Account_ID=Transaction.Account_ID,
                    Is_Credit=Transaction.Is_Credit,
                    Amount=Transaction.Amount,
                    Currency = Transaction.Currency,
                }); ;

                ctx.SaveChanges();
            }

            return Ok();
        }




    }
}
