
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Model
{
    public class AccountDataModel
    {
        public int Count { get; set; }
        public List<AccountViewModel> Data { get; set; } = new List<AccountViewModel>();
    }
    public class AccountViewModel
    {
        public int Id { get; set; }
        public DateTime ServerT { get; set; }
        public DateTime UTC_T { get; set; }
        public DateTime? Update_UTCT { get; set; }
        public string Account_Number { get; set; }
        public int Balance { get; set; }
        public int? Available_Balance { get; set; }
        public int Currency { get; set; }
        public int? Status { get; set; }
        public int User_Id { get; set; }
    }
}
