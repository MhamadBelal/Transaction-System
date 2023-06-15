
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer
{
    /// <summary>
    /// User Table
    /// </summary>
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Server_DateTime { get { return DateTime.Now; } }
        public DateTime DateTime_UTC { get { return DateTime.UtcNow; } }
        public DateTime? Update_DateTime_UTC { get; set; }

        public DateTime? Last_Login_DateTime_UTC { get; set; }
        [Required, MaxLength(15)]
        public string First_Name { get; set; }
        [Required, MaxLength(15)]
        public string Last_Name { get; set; }

        public int? Status { get; set; }
        [Required]
        public string Gender { get; set; }
        public DateTime? Date_Of_Birth { get; set; }
        List<Account> accounts = new List<Account>();
        List<Transaction> transactions = new List<Transaction>();
        [ForeignKey("Role")]
        public int Role_ID { get { return 2; }}
        public Role Role { get; }
        public string? ImageBase64 { get; set; }


    }
}
