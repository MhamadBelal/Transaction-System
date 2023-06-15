using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer
{
    /// <summary>
    /// Account Table
    /// </summary>
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("User"),Required]
        public int User_ID { get; set; }
        public User User { get; set; }
        public DateTime Server_DateTime { get { return DateTime.Now; } }
        public DateTime DateTime_UTC { get { return DateTime.UtcNow; } }
        public DateTime? Update_DateTime_UTC { get; set; }
        [Required,MaxLength(10)]
        public string Account_Number { get; set; }
        [Required]
        public int Balance { get; set; }
        public int? Available_Balance { get; set; }
        [Required]
        public int Currency { get; set; }
        public int? Status { get; set; }
        List<Transaction> transactions = new List<Transaction>();

    }
}
