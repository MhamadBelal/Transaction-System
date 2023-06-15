using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPI.Model;

namespace WebMVC.Controllers
{
    public class AccountController : Controller
    {
        Uri Baseuri = new Uri("https://localhost:44389/api/AccountAPI");
        // GET: UserController
        public ActionResult Index(int? PageId)
        {
            AccountDataModel accounts= new AccountDataModel();
           // List<AccountViewModel> accounts = null;

            using (var client = new HttpClient())
            {
                var query = new Dictionary<string, string>()
                {
                    ["Page"] = PageId.HasValue ? PageId.Value.ToString() : "1",
                    ["PageSize"] = "10"
                };
                var uri = QueryHelpers.AddQueryString("https://localhost:44389/api/AccountAPI/GetAll", query);

                var result = client.GetAsync(uri).GetAwaiter().GetResult();

                if (result.IsSuccessStatusCode)
                {
                    accounts = result.Content.ReadAsAsync<AccountDataModel>().GetAwaiter().GetResult();
                }
                else //web api sent error response 
                {
                    //log response status here..
                    //accounts = Enumerable.Empty<AccountViewModel>().ToList();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            if (accounts.Count != 0)
            {
                if (accounts.Count % 10 == 0)
                    ViewBag.count = (accounts.Count / 10);
                else
                    ViewBag.count = (accounts.Count / 10) + 1;
            }
            return View(accounts.Data);
        }



        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountViewModel account)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44389/api/AccountAPI/SaveAccount");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var result = client.PostAsJsonAsync<AccountViewModel>("https://localhost:44389/api/AccountAPI/SaveAccount", account).GetAwaiter().GetResult();

                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                return View(account);
            }
            catch
            {
                return View();
            }
        }

    }
}