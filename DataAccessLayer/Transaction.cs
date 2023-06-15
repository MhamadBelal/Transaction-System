using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer
{
    /// <summary>
    /// Transaction Table
    /// </summary>
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("User"),Required]
        public int User_ID { get; set; }
        public User User { get; set; }
        [ForeignKey("Account"),Required]
        public int Account_ID { get; set; }
        public Account Account { get; set; }
        public DateTime Server_DateTime { get { return DateTime.Now; } }
        public DateTime DateTime_UTC { get { return DateTime.UtcNow; } }
        [Required]
        public bool Is_Credit { get; set; }
        public int Amount { get; set; }
        [Required]
        public int Currency { get; set; }
    }
}




