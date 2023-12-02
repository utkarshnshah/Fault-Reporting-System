using FaultReportingSystem.Data;
using FaultReportingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Diagnostics;

namespace FaultReportingSystem.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
        public IActionResult Index()
        {
            if (IsUserLoggedIn())
            {
                SetViewDataFromSession();
                return View();
            }
            else
            {
                ClearUserSession();
                return RedirectToAction("Login", "Home");
            }
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
        public IActionResult Login()
        {
            int? SessionUserId = HttpContext.Session.GetInt32("SessionUserId");

            if (SessionUserId.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
        [HttpPost]
        public IActionResult LoginCustomer(Customer model)
        {
            if (ModelState.IsValid)
            {
                var customer = _dbContext.Customers.FirstOrDefault(c => c.CustomerEmail == model.CustomerEmail && c.CustomerPassword == model.CustomerPassword);

                if (customer != null)
                {
                    string UserType = "Customer";
                    SetUserSession(customer.CustomerId, customer.CustomerFirstName, customer.CustomerLastName, customer.CustomerEmail, UserType);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Email or Password!");
                    TempData["LoginErrorMessage"] = "Invalid Email or Password!";
                }
            }
            return RedirectToAction("Login", "Home");
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
        public IActionResult CustomerRegistration()
        {
            int? SessionUserId = HttpContext.Session.GetInt32("SessionUserId");

            if (SessionUserId.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
        [HttpPost]
        public IActionResult RegisterCustomer(Customer model, string ConfirmPassword)
        {
            if (ModelState.IsValid)
            {
                if (_dbContext.Customers.Any(r => r.CustomerEmail == model.CustomerEmail))
                {
                    ModelState.AddModelError("CustomerEmail", "Email is already registered.");
                }
                else if (model.CustomerPassword.Length < 8)
                {
                    ModelState.AddModelError("CustomerPassword", "Password must be at least 8 characters long.");
                }
                else if (model.CustomerPassword != ConfirmPassword)
                {
                    ModelState.AddModelError(string.Empty, "Password and Confirm Password do not match.");
                }
                else
                {
                    _dbContext.Customers.Add(model);
                    _dbContext.SaveChanges();

                    TempData["RegistrationSuccessMessage"] = "Registration successful! ";

                    return RedirectToAction("CustomerRegistration", "Home");
                }
            }

            return View("CustomerRegistration", model);
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
        public IActionResult DeveloperLogin()
        {
            int? SessionUserId = HttpContext.Session.GetInt32("SessionUserId");

            if (SessionUserId.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
        [HttpPost]
        public IActionResult LoginDeveloper(Developer model)
        {
            if (ModelState.IsValid)
            {
                var developer = _dbContext.Developers.FirstOrDefault(d => d.DeveloperEmail == model.DeveloperEmail && d.DeveloperPassword == model.DeveloperPassword);

                if (developer != null)
                {
                    string UserType = "Developer";
                    SetUserSession(developer.DeveloperId, developer.DeveloperFirstName, developer.DeveloperLastName, developer.DeveloperEmail, UserType);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Email or Password!");
                    TempData["LoginErrorMessage"] = "Invalid Email or Password!";
                }
            }
            return View("DeveloperLogin", model);
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
        public IActionResult ManagerLogin()
        {
            int? SessionUserId = HttpContext.Session.GetInt32("SessionUserId");

            if (SessionUserId.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
        [HttpPost]
        public IActionResult LoginManager(Manager model)
        {
            if (ModelState.IsValid)
            {
                var manager = _dbContext.Managers.FirstOrDefault(m => m.ManagerEmail == model.ManagerEmail && m.ManagerPassword == model.ManagerPassword);

                if (manager != null)
                {
                    string UserType = "Manager";
                    SetUserSession(manager.ManagerId, manager.ManagerFirstName, manager.ManagerLastName, manager.ManagerEmail, UserType);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Email or Password!");
                    TempData["LoginErrorMessage"] = "Invalid Email or Password!";
                }
            }
            return View("ManagerLogin", model);
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
        public IActionResult HelpDeskStaffLogin()
        {
            int? SessionUserId = HttpContext.Session.GetInt32("SessionUserId");

            if (SessionUserId.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
        [HttpPost]
        public IActionResult LoginHelpDeskStaff(HelpDesk model)
        {
            if (ModelState.IsValid)
            {
                var helpdesk = _dbContext.HelpDesks.FirstOrDefault(h => h.HelpDeskEmail == model.HelpDeskEmail && h.HelpDeskPassword == model.HelpDeskPassword);

                if (helpdesk != null)
                {
                    string UserType = "HelpDesk";
                    SetUserSession(helpdesk.HelpDeskId, helpdesk.HelpDeskFirstName, helpdesk.HelpDeskLastName, helpdesk.HelpDeskEmail, UserType);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Email or Password!");
                    TempData["LoginErrorMessage"] = "Invalid Email or Password!";
                }
            }
            return View("HelpDeskStaffLogin", model);
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
        public IActionResult SystemAdministratorLogin()
        {
            int? SessionUserId = HttpContext.Session.GetInt32("SessionUserId");

            if (SessionUserId.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
        [HttpPost]
        public IActionResult LoginSystemAdministrator(SystemAdministrator model)
        {
            if (ModelState.IsValid)
            {
                var systemadministrator = _dbContext.SystemAdministrators.FirstOrDefault(s => s.Email == model.Email && s.Password == model.Password);

                if (systemadministrator != null)
                {
                    string UserType = "SystemAdministrator";
                    SetUserSession(systemadministrator.SystemAdministratorId, systemadministrator.FirstName, systemadministrator.LastName, systemadministrator.Email, UserType);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Email or Password!");
                    TempData["LoginErrorMessage"] = "Invalid Email or Password!";
                }
            }
            return View("SystemAdministratorLogin", model);
        }

        [HttpPost]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
        public IActionResult Exit()
        {
            ClearUserSession();
            Response.Headers["Cache-Control"] = "no-cache, no-store";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "-1";
            return RedirectToAction("Login", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
