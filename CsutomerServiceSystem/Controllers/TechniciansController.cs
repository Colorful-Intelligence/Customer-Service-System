using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using CsutomerServiceSystem.DataAccess.Data;
using CsutomerServiceSystem.DataAccess.Repository;
using CustomerServiceSystem.Model.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace CustomerServiceSystem.Controllers
{
    public class TechniciansController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        private IAppRepository _appRepository;

        public TechniciansController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        public IActionResult Index()
        {
            int sessionUserId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));

            var userObject = _context.User.Where(x => x.Id == sessionUserId).FirstOrDefault();

            var registrationsByDepartment = _appRepository.GetRegistrationByDepartment(userObject.DepartmentId).ToList();


            return View(registrationsByDepartment);
        }

        
        public IActionResult GetRegistrations(int id)
        {
            List<SelectListItem> statusT = (from i in _context.Statu.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.Title,
                                                Value = i.Id.ToString()
                                            }).ToList();

            ViewBag.statusT = statusT;

            var registration = _appRepository.GetRegistrationByID(id);
            

            return View("GetRegistrations", registration);


        }

        [HttpPost]
        public IActionResult UpdateRegistration(Registration r)
        {
            


            _appRepository.Update<Registration>(r);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

        public IActionResult EmailOperation()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EmailOperation(EmailModel model)
        {

            model.FromEmail = "repair.service.gmbh@gmail.com";
            model.FromPassword = "GMBH3428";


            try
            {
                using (MailMessage message = new MailMessage(model.FromEmail, model.To))
                {
                    message.Subject = model.Subject;
                    message.Body = model.Body;
                    message.IsBodyHtml = false;
                    if (model.Attachment.Length > 0)
                    {
                        string filename = Path.GetFileName(model.Attachment.FileName);
                        message.Attachments.Add(new Attachment(model.Attachment.OpenReadStream(), filename));
                    }

                    
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential cred = new NetworkCredential(model.FromEmail, model.FromPassword);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = cred;
                        smtp.Port = 587;
                        smtp.Send(message);
                        ViewBag.Message = "EMAIL HAS BEEN SEND SUCCESSFULLY";
                    }

                }
            }
            catch (Exception)
            {

                ViewBag.ErrorMessage = "SOMETHINGS WENT WRONG";
            }

            
            return View();
        }

        public IActionResult GetDepartments()
        {
            var departments = _appRepository.GetDepartments().ToList();
            return View(departments);
        }

        public IActionResult DepartmentDetail(int id)
        {
            var tech = _appRepository.GetTechniciansByDepartment(id).ToList();
            return View(tech);
        }

        public IActionResult RegistrationDetail(int id)
        {
            var detail = _appRepository.GetRegistrationDetail(id).ToList();
            return View(detail);
        }


    }
}