using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DojoSecrets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace DojoSecrets.Controllers
{
    public class HomeController : Controller
    {
        private DojoSecretsContext _context;
    
        public HomeController(DojoSecretsContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index(Users myUser)
        {
            return View("Index");
        }
        [HttpPost("AddUser")]
        public IActionResult AddUser(UsersValidate uservalidator)
        {
            if(ModelState.IsValid)
            {

                var emailvalidation = _context.users.SingleOrDefault(p => p.email == uservalidator.email);
                if(emailvalidation == null)
                {
                    PasswordHasher<UsersValidate> Hasher = new PasswordHasher<UsersValidate>();
                    uservalidator.password = Hasher.HashPassword(uservalidator, uservalidator.password);
                    Users myUser = new Users();
                    myUser.first_name = uservalidator.first_name;
                    myUser.last_name = uservalidator.last_name;
                    myUser.email = uservalidator.email;
                    myUser.password = uservalidator.password;
                    myUser.created_at = DateTime.Now;
                    myUser.updated_at = DateTime.Now;
                    _context.Add(myUser);
                    _context.SaveChanges();

                    HttpContext.Session.SetInt32("UserID", myUser.id);
                    int? UserID = HttpContext.Session.GetInt32("UserID");
                    ViewBag.UserID = UserID;
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    TempData["uniqueemail"] = "This email belongs to a registered user. Please use another email address";
                    return View("Index");
                }
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost("LoginProcess")]
        public IActionResult LoginProcess(LoginValidate myLogin)
        {
            if(ModelState.IsValid)
            {
                Users loginData = _context.users.SingleOrDefault(p => p.email == myLogin.login_email);
                if(loginData == null)
                {
                    ModelState.AddModelError("login_email", "Email Address is not registered");
                }
                else if(loginData != null && myLogin.login_password != null)
                {
                    var Hasher = new PasswordHasher<Users>();
                    // Pass the user object, the hashed password, and the PasswordToCheck
                    if(0 != Hasher.VerifyHashedPassword(loginData, loginData.password, myLogin.login_password))
                    {
                        HttpContext.Session.SetInt32("UserID", loginData.id);
                        int? UserID = HttpContext.Session.GetInt32("UserID");
                        ViewBag.UserID = UserID;
                        return RedirectToAction("Dashboard");
                    }
                }
                return View("Index");
            }
            // ViewBag.error = "LOL, Nice try!";
            // TempData["error"] = "LOL, try again!";
            return View("Index");
        }
        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }
        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            int? UserID = HttpContext.Session.GetInt32("UserID");
            ViewBag.userInfo = _context.users.Where(p => p.id == UserID).SingleOrDefault();
            ViewBag.allSecrets = _context.secrets.Include(p => p.Like).ToList();
            return View("Dashboard");
        }
        [HttpGet("Popular")]
        public IActionResult Popular()
        {
            int? UserID = HttpContext.Session.GetInt32("UserID");
            ViewBag.userInfo = _context.users.Where(p => p.id == UserID).SingleOrDefault();
            ViewBag.allSecrets = _context.secrets.OrderByDescending(p => p.created_at).Include(p => p.Like).ToList();
            return View("Popular");
        }
        [HttpPost("AddMessage")]
        public IActionResult AddMessage(string message)
        {
            int? UserID = HttpContext.Session.GetInt32("UserID");
            var userInfo = _context.users.Where(p => p.id == UserID).SingleOrDefault();
            Secrets mySecret = new Secrets();
            mySecret.message = message;
            mySecret.usersid = (int)UserID;
            mySecret.created_at = DateTime.Now;
            mySecret.updated_at = DateTime.Now;
            _context.Add(mySecret);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("SecretID", mySecret.id);
            int? SecretID = HttpContext.Session.GetInt32("SecretID");
            return RedirectToAction("Dashboard");
        }
        [HttpPost("DeleteMessage")]
        public IActionResult DeleteMessage(int secretID)
        {
            int? UserID = HttpContext.Session.GetInt32("UserID");
            var DelLike = _context.likes.Where(p => p.secretsid == secretID).ToList();
            var DelSecret = _context.secrets.Where(p => p.id == secretID).SingleOrDefault();
            foreach (var like in DelLike)
            {
                _context.likes.Remove(like);
            }
            _context.secrets.Remove(DelSecret);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpPost("DeleteLike")]
        public IActionResult DeleteLike(int secretID)
        {
            int? UserID = HttpContext.Session.GetInt32("UserID");
            var DelLike = _context.likes.Where(p => p.secretsid == secretID).Where(p => p.usersid == UserID).SingleOrDefault();
            _context.likes.Remove(DelLike);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpPost("AddLike")]
        public IActionResult AddLike(int secretID)
        {
            int? UserID = HttpContext.Session.GetInt32("UserID");
            Likes myLike = new Likes();
            myLike.created_at = DateTime.Now;
            myLike.updated_at = DateTime.Now;
            myLike.usersid = (int)UserID;
            myLike.secretsid = secretID;
            _context.likes.Add(myLike);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
    }
}
