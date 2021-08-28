using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CustomerServiceSystem.Model.Models
{
    public class Registration
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DepartmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public int StatuId { get; set; }

        public List<User> User { get; set; }
        public Statu Statu { get; set; }
        public Department Department { get; set; }
    }
}
