using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPI.Model;
namespace WebMVC.Controllers
{
    public class UserController : Controller
    {
        Uri Baseuri = new Uri("https://localhost:44389/api/UserAPI");
        // GET: UserController
        public ActionResult Index(int? PageId)
        {

            UserDataModel users = new UserDataModel();
            // genralResult  =>
            // IList<UserViewModel>
            // countAll

            using (var client = new HttpClient())
            {
                var query = new Dictionary<string, string>()
                {
                    ["Page"] = PageId.HasValue ? PageId.Value.ToString() : "1",
                    ["PageSize"] = "10"
                };
                var uri = QueryHelpers.AddQueryString("https://localhost:44389/api/UserAPI/GetAll", query);
                var result = client.GetAsync(uri).GetAwaiter().GetResult();

                if (result.IsSuccessStatusCode)
                {
                    users = result.Content.ReadAsAsync<UserDataModel>().GetAwaiter().GetResult();
                }
                else //web api sent error response 
                {
                    //log response status here
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            if (users.Count != 0)
            {
                if (users.Count % 10 == 0)
                    ViewBag.count = (users.Count / 10);
                else
                    ViewBag.count = (users.Count / 10) + 1;
            }

            return View(users.Data);
        }



        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        public  ActionResult create(UserViewModel user, IFormFile photo)
        {
            using (var client = new HttpClient())
            {

                if (user.photo != null)
                    using (var ms = new MemoryStream())
                    {
                        user.photo.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        user.photoBase64 = Convert.ToBase64String(fileBytes);
                        user.photo = null;
                    }
             

                client.BaseAddress = new Uri("https://localhost:44389/api/UserAPI/SaveUser");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var result = client.PostAsJsonAsync<UserViewModel>("https://localhost:44389/api/UserAPI/SaveUser", user).GetAwaiter().GetResult();

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(user);
        }
        [HttpPost, HttpGet]
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:44389/api/");

                //HTTP DELETE
                var result = client.DeleteAsync("https://localhost:44389/api/UserAPI/Delete?id=" + id.ToString()).GetAwaiter().GetResult();

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("index");
                }
            }

            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<ActionResult> delSelected(List<string> ids)
        {

            foreach (var item in ids)
            {
                using (var client = new HttpClient())
                {
                    //HTTP DELETE
                    var result = await client.DeleteAsync("https://localhost:44389/api/UserAPI/Delete?id=" + item.ToString());
                }
            }
            return Ok(ids.Count);
        }
    }
}