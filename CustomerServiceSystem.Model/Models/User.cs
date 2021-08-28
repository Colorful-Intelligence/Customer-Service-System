using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CustomerServiceSystem.Model.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public int UserTypeId { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        

        public UserType UserType { get; set; }
        public Registration Registration { get; set; }
        
    }
}
