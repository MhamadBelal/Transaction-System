using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Model;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace WebAPI.Controllers
{
    [Route("api/UserAPI")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {

        [Route("GetAll")]
        public IActionResult GetAllUsers(int page = 1, int pageSize = 10)
        {

            UserDataModel result = new UserDataModel();
            using (var ctx = new ApplicationContext())
            {

                result.Count = ctx.Users.Count();


                result.Data = ctx.Users
                    .Skip(((page - 1) * pageSize))
                    .Take(pageSize)
                    .Select(s => new UserViewModel()
                    {
                        Id = s.Id,
                        FirstName = s.First_Name,
                        LastName = s.Last_Name,
                        ServerT = s.Server_DateTime,
                        UTC_T = s.DateTime_UTC,
                        Update_UTCT = s.Update_DateTime_UTC,
                        LastLoginT_UTC = s.Last_Login_DateTime_UTC,
                        Status = s.Status,
                        Gender = s.Gender,
                        BirthDate = s.Date_Of_Birth,
                        photoBase64 = s.ImageBase64
                    }).ToList();
                if (result.Count == 0)
                {
                    return NotFound();
                }

                return Ok(result);

            }

        }
        [Route("SaveUser")]
        [HttpPost]
        public IActionResult PostNewUser(UserViewModel user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");


            using (var ctx = new ApplicationContext())
            {
                ctx.Users.Add(new User()
                {
                    Id = user.Id,
                    First_Name = user.FirstName,
                    Last_Name = user.LastName,
                    Update_DateTime_UTC = user.Update_UTCT,
                    Last_Login_DateTime_UTC = user.LastLoginT_UTC,
                    Status = user.Status,
                    Gender = user.Gender,
                    Date_Of_Birth = user.BirthDate,
                    ImageBase64 = user.photoBase64,
                });

                ctx.SaveChanges();
            }

            return Ok();
        }
        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid User id");

            using (var ctx = new ApplicationContext())
            {
                var user = ctx.Users.FirstOrDefault(s => s.Id == id);

                ctx.Users.Remove(user);
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}
