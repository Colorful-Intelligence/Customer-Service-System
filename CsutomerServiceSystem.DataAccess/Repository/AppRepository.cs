using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsutomerServiceSystem.DataAccess.Data;
using CustomerServiceSystem.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace CsutomerServiceSystem.DataAccess.Repository
{
    public class AppRepository : IAppRepository
    {
        // Injection Operation
        ApplicationDbContext _context;
        

        public AppRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            var addedEntity = _context.Entry(entity);
            addedEntity.State = EntityState.Added;
            _context.SaveChanges();
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            _context.SaveChanges();
        }

        
        public List<User> GetCustomers()
        {
            // For Officiers
            var customers = _context.User.Where(x => x.UserTypeId == 1).ToList();
            return customers;
        }

        public List<Department> GetDepartments()
        {
            // For Officiers
            var departments = _context.Department.ToList();
            return departments;
        }

        public List<Registration> GetRegistrationByDepartment(int department_id)
        {
            // For Technicians
            var registrationByDepartment = _context.Registration.Include(x=>x.Statu).Include(x => x.Department).Include(X=>X.Statu).Where(x => x.DepartmentId == department_id).ToList();
            return registrationByDepartment;
        }

        public List<Registration> GetRegistrations()
        {
            // For Officiers
            var registration = _context.Registration.Include(x=>x.Department).Include(x=>x.Statu).Include(x=>x.User).ToList();
            return registration;
        }

        public List<Registration> GetRegistrationsByCustomer(int user_id)
        {
            // For each customers

            var registration = _context.Registration.Include(x => x.User).Include(x => x.Department).Include(x => x.Statu).Where(x => x.UserId == user_id).ToList();
            return registration;
        }

        public List<User> GetTechnicians(int User_Type)
        {
            // For Officiers
            var technicians = _context.User.Include(x => x.UserType).Where(x => x.UserTypeId == User_Type).ToList();
            return technicians;
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public object GetRegistrationByID(int id)
        {
            var registration = _context.Registration.Find(id);
            return registration;
        }

        public List<User> GetRegistrationDetail(int id)
        {
            var info = _context.User.Where(x => x.Id == id).ToList();
            return info;
        }

        public List<User> GetTechniciansByDepartment(int id)
        {
            var technicians = _context.User.Where(x => x.DepartmentId == id).ToList();

            return technicians;

        }

        public bool checkUsername(string username)
        {
            var checkUsername = _context.User.Where(x => x.UserName == username).FirstOrDefault();

            if (checkUsername != null)
            {
                return false;
            }

            return true;
        }

        public bool checkEmail(string email)
        {
            var checkPassword = _context.User.Where(x => x.Email == email).FirstOrDefault();
            if (checkPassword != null)
            {
                return false;
            }

            return true;
        }

        public bool checkCustomer(int id)
        {
            var customer = _context.User.Where(x => x.Id == id).FirstOrDefault();

            if (customer == null)
            {
                return false;
            }

            return true;
        }
    }
}
