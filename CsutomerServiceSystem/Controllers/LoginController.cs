using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using CsutomerServiceSystem.DataAccess.Data;
using CsutomerServiceSystem.DataAccess.Repository;
using CustomerServiceSystem.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CsutomerServiceSystem.Controllers
{
    public class LoginController : Controller
    {
        

        private IAppRepository _appRepository;
        ApplicationDbContext _context = new ApplicationDbContext();
        private string code = null;

        public LoginController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login(User U)
        {
            var user = _context.User.Include(x => x.UserType).FirstOrDefault(x => x.UserName == U.UserName && x.Password == U.Password);

            if (user == null)
            {
                
                TempData["Message"] = "USERNAME OR PASSWORD IS WRONG !";
                return RedirectToAction("Index", "Login");
            }
            else 
            {
                HttpContext.Session.SetInt32("Id", user.Id);
                HttpContext.Session.SetString("fullname", user.Name + " " + user.Surname);

                if (user.UserTypeId == 1)
                {
                    return RedirectToAction("Index", "Customers");

                    
                }

                else if (user.UserTypeId == 2)
                {
                    return RedirectToAction("Index", "Officiers");
                    
                }
                else
                {
                    return RedirectToAction("Index", "Technicians");
                    
                }
            }


        }

        
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUP([Bind("Name,Surname,UserName","Phone","Email","Address","Password")]User U)
        {
            
            U.UserTypeId = 1;
            U.DepartmentId = 0;

            bool controlEmail = _appRepository.checkEmail(U.Email);
            bool controlUsername =  _appRepository.checkUsername(U.UserName);

            

            if (controlUsername == false)
            {
                TempData["RegisterErrorUsername"] = "USERNAME  IS ALREADY TAKEN";
                return RedirectToAction("SignUP", "Login");
            }

            else if (controlEmail == false)
            {
                TempData["RegisterErrorEmail"] = "EMAIL  IS ALREADY TAKEN";
                return RedirectToAction("SignUP", "Login");
            }

            else
            {
                _appRepository.Add<User>(U);

                TempData["RegisterMessage"] = "YOU ARE SIGN UP SUCCESSFULLY";
                return RedirectToAction("SignUP", "Login");

            }

        }

        public IActionResult ForgotPassword()
        {
            if (HttpContext.Session.GetInt32("id").HasValue)
            {
                return Redirect("/Home/Index");
            }
           
            return View();
        }

        public IActionResult ResetPassword()
        {
           if (HttpContext.Session.GetInt32("id").HasValue)
            {
                return Redirect("/Home/Index");
            }
            return View();
        }

        public IActionResult ResetPasswordCode(string code, string newPassword)
        {
            var passwordcode = _context.PasswordCode.FirstOrDefault(w => w.Code.Equals(code));
            if (passwordcode != null)
            {
                var user = _context.User.Find(passwordcode.UserId);
                user.Password = newPassword;
                _context.Update(user);
                _context.Remove(passwordcode);
                _context.SaveChanges();
                TempData["MessagePassword"] = "PASSWORD HAS BEEN CHANGED SUCCESSFULLY";
              
                return RedirectToAction("ResetPassword", "Login");
            }
           
            TempData["MessageErrorCode"] = "CODE IS WRONG";
            return RedirectToAction("ResetPassword", "Login");

        }

        public IActionResult SendCode(string email)
        {
            var user = _context.User.FirstOrDefault(x => x.Email.Equals(email));
            if (user != null)
            {
                _context.Add(new PasswordCode {UserId = user.Id, Code = getCode()});
                _context.SaveChanges();
                string text = "<h1>Your Code To Reset Password</h1>" + getCode() + " ";
                string subject = "PASSWORD RESET";
                MailMessage msg = new MailMessage("repair.service.gmbh@gmail.com", email, subject, text);
                msg.IsBodyHtml = true;
                SmtpClient sc = new SmtpClient("smtp.gmail.com", 587);
                sc.UseDefaultCredentials = false;
                NetworkCredential cre = new NetworkCredential("repair.service.gmbh@gmail.com", "GMBH3428");
                sc.Credentials = cre;
                sc.EnableSsl = true;
                sc.Send(msg);
                return Redirect("ResetPassword");
            }
            TempData["MessageReset"] = "INVALID EMAIL !";
            
            return RedirectToAction("ForgotPassword", "Login");
        }

        public string getCode()
        {
            if(code == null)
            {
                Random rand = new Random();
                code = "";
                for (int i = 0; i < 6; i++)
                {
                    char temp = Convert.ToChar(rand.Next(48, 58));
                    code += temp;
                }
            }
            return code;
        }
    }
}