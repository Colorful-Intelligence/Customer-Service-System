using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsutomerServiceSystem.DataAccess.Data;
using CsutomerServiceSystem.DataAccess.Repository;
using CustomerServiceSystem.Model.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CustomerServiceSystem.Controllers
{
    public class OfficiersController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();

        private IAppRepository _appRepository;

        public OfficiersController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        public IActionResult Index(int id)
        {


            int sessionUserId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));

            /*if (id != null)
            {
                return View(_context.Registration.Where(x=>x.Id == id).ToList());
            }*/
           

            var registrations = _appRepository.GetRegistrations().ToList();

            return View(registrations);
        }

        public IActionResult GetCustomers(string p)
        {
            if (!string.IsNullOrEmpty(p))
            {
                return View(_context.User.Where(x => x.Name.ToUpper().Contains(p.ToUpper())).ToList());
            }

            var customers = _appRepository.GetCustomers().ToList();
            return View(customers);
        }

        public IActionResult GetDepartments()
        {
            var departments = _appRepository.GetDepartments().ToList();
            return View(departments);
        }

        [HttpGet]
        public IActionResult AddRegistration()
        {
            List<SelectListItem> departments = (from i in _context.Department.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = i.Title,
                                                    Value = i.Id.ToString()

                                                }).ToList();
            List<SelectListItem> status = (from i in _context.Statu.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Title,
                                               Value = i.Id.ToString()
                                           }).ToList();

            ViewBag.status = status;
            ViewBag.departments = departments;



            return View();
        }

        [HttpPost]
        public IActionResult AddRegistration(Registration r)
        {
            bool customerControl = _appRepository.checkCustomer(r.Id);

            if (customerControl == false)
            {
                TempData["CustomerErrorMessage"] = "Customer not found";
                return RedirectToAction("AddRegistration");
            }

            else
            {
                _appRepository.Add<Registration>(r);

                return RedirectToAction("Index");
            }

            

        }

        public IActionResult RegistrationDetail(int id)
        {
            var detail = _appRepository.GetRegistrationDetail(id).ToList();
            return View(detail);
        }

        public IActionResult DepartmentDetail(int id)
        {
            var tech = _appRepository.GetTechniciansByDepartment(id).ToList();
            return View(tech);
        }



        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}