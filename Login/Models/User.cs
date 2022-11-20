using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Login.Models
{
    public partial class User
    {
        [Key]
        public string user { get; set; }
        public string password { get; set; }
    }
}
