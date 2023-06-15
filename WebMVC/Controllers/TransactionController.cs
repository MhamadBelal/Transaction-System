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
    public class TransactionController : Controller
    {
        Uri Baseuri = new Uri("https://localhost:44389/api/TransactionAPI");
        // GET: TransactionController
        public ActionResult Index(int? PageId)
        {
            TransactionDataModel transactions = new TransactionDataModel();
            // List<AccountViewModel> accounts = null;

            using (var client = new HttpClient())
            {
                var query = new Dictionary<string, string>()
                {
                    ["Page"] = PageId.HasValue ? PageId.Value.ToString() : "1",
                    ["PageSize"] = "10"
                };
                var uri = QueryHelpers.AddQueryString("https://localhost:44389/api/TransactionAPI/GetAll", query);

                var result = client.GetAsync(uri).GetAwaiter().GetResult();

                if (result.IsSuccessStatusCode)
                {
                    transactions = result.Content.ReadAsAsync<TransactionDataModel>().GetAwaiter().GetResult();
                }
                else //web api sent error response 
                {
                    //log response status here..
                    
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            
                if (transactions.Count != 0)
                {
                    if (transactions.Count % 10 == 0)
                        ViewBag.count = (transactions.Count / 10);
                    else
                        ViewBag.count = (transactions.Count / 10) + 1;
                }
            
            return View(transactions.Data);
        }



        // GET: TransactionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransactionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransactionViewModel transaction)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44389/api/TransactionAPI/SaveTransaction");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var result = client.PostAsJsonAsync<TransactionViewModel>("https://localhost:44389/api/TransactionAPI/SaveTransaction", transaction).GetAwaiter().GetResult();

                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                return View(transaction);
            }
            catch
            {
                return View();
            }
        }

    }
}
