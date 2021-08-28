using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CustomerServiceSystem.Model.Models
{
    public class UserType
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public List<User> User { get; set; }
    }
}
