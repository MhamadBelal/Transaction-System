using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Model
{
    public class UserDataModel
    {
        public int Count { get; set; }
        public List<UserViewModel> Data { get; set; } = new List<UserViewModel>();
    }

    public class UserViewModel
    {
        public int Id { get; set; }
        public DateTime ServerT { get; set; }
        public DateTime UTC_T { get; set; }
        public DateTime? Update_UTCT { get; set; }

        public DateTime? LastLoginT_UTC { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? Status { get; set; }

        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public IFormFile photo { get; set; }
        public string photoBase64 { get; set; }

    }
}
