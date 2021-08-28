using CustomerServiceSystem.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;


namespace CsutomerServiceSystem.DataAccess.Repository
{
    public interface IAppRepository
    {
        void Add<T>(T entity) where T:class;
        void Delete<T>(T entity) where T:class;
        void Update<T>(T entity) where T : class;

        bool SaveAll();
        bool checkUsername(string username);
        bool checkEmail(string email);
        bool checkCustomer(int id);

        object GetRegistrationByID(int id);
       

        List<Registration> GetRegistrations();
        List<User> GetCustomers();
        List<User> GetTechnicians(int User_Type);
        List<Registration> GetRegistrationByDepartment(int department_id);
        List<Department> GetDepartments();
        List<Registration> GetRegistrationsByCustomer(int user_id);
        List<User> GetRegistrationDetail(int id);
        List<User> GetTechniciansByDepartment(int id);
        
    }
}
