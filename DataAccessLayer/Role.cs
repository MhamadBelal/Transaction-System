using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer
{
    /// <summary>
    /// Role Table
    /// </summary>
    public class Role
    {
        public int Id { get; set; }
        public string Role_Name { get; set; }
    }
}
