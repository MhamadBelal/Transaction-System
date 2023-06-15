using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace WebAPI.Model
{
    
        public class TransactionDataModel
        {
            public int Count { get; set; }
            public List<TransactionViewModel> Data { get; set; } = new List<TransactionViewModel>();
        }
        public class TransactionViewModel
    {
        public int Id { get; set; }
        
        public int User_ID { get; set; }
        
        public int Account_ID { get; set; }
        public DateTime ServerT { get; set; }
        public DateTime UTC_T { get; set; }
        
        public bool Is_Credit { get; set; }
        public int Amount { get; set; }
        public int Currency { get; set; }
    }
}
