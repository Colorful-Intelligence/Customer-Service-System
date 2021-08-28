using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerServiceSystem.Model.Models
{
    public class PasswordCode
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; }
    }
}
