using BarberMe.Models.Database;
using BarberMe.Models.Classes;
using BarberMe.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using BarberMe.Models.PagesModels;

namespace BarberMe.Controllers
{
    public class HomeController : Controller
    {
        private IServiceRepository repository;
        private UserManager<BarbershopUser> userManager;
        private SignInManager<BarbershopUser> signInManager;

        public async Task<BarbershopUser> GetCurrentUserAsync()
        {
            BarbershopUser user = await userManager.FindByNameAsync(User.Identity.Name);
            return user;
        }

        public HomeController(IServiceRepository repo, UserManager<BarbershopUser> userMgr,
                SignInManager<BarbershopUser> signInMgr)
        {
            repository = repo;
            userManager = userMgr;
            signInManager = signInMgr;
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                BarbershopUser user = await GetCurrentUserAsync();
            }
            return View();
        }

        [HttpGet]
        public ViewResult LoginPage(string returnUrl)
        {
            return View(new LoginModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpGet]
        public ViewResult RegistrationPage()
        {
            return View();
        }
    }
}
