using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsutomerServiceSystem.DataAccess.Data;
using CsutomerServiceSystem.DataAccess.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CsutomerServiceSystem.Controllers
{
    public class CustomersController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        
        private  IAppRepository _appRepository;

        public CustomersController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
            
        }

        
        public IActionResult Index()
        {
            int sessionUserId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            
            var userObject = _context.User.Where(x => x.Id == sessionUserId).FirstOrDefault();

            var registrations = _appRepository.GetRegistrationsByCustomer(userObject.Id).ToList();
            

            return View(registrations);

        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}