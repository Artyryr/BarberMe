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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace BarberMe.Controllers
{
    public class HomeController : Controller
    {
        private IServiceRepository repository;
        private readonly IHostingEnvironment hostingEnvironment;
        private UserManager<BarbershopUser> userManager;
        private SignInManager<BarbershopUser> signInManager;

        public async Task<BarbershopUser> GetCurrentUserAsync()
        {
            BarbershopUser user = await userManager.FindByNameAsync(User.Identity.Name);
            return user;
        }

        public HomeController(IServiceRepository repo, UserManager<BarbershopUser> userMgr,
                SignInManager<BarbershopUser> signInMgr, IHostingEnvironment host)
        {
            repository = repo;
            userManager = userMgr;
            signInManager = signInMgr;
            hostingEnvironment = host;
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

        [HttpPost]
        public async Task<ActionResult> RegistrationPage(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Password.Equals(model.ConfirmationPassword))
                    {
                        BarbershopUser user = await userManager.FindByEmailAsync(model.Email);
                        if (user == null)
                        {
                            BarbershopUser newUser = new BarbershopUser { Email = model.Email, UserName = model.Email, };
                            IdentityResult result = await userManager.CreateAsync(newUser, model.Password);

                            if (result.Succeeded)
                            {
                                BarbershopUser createdUser = await userManager.FindByEmailAsync(model.Email);

                                Barbershop barbershop = new Barbershop(createdUser.Id, model.Email, model.Name, model.Address,
                    model.Telephone, model.Description, model.Instagram, model.Facebook,
                    model.Geoposition);

                                repository.AddBarbershop(barbershop);

                                Barbershop currentBarbershop = repository.Barbershops.Where(p => p.Equals(barbershop)).FirstOrDefault();
                                
                                if (model.barbershopImage != null)
                                {
                                    var extension = Path.GetExtension(model.barbershopImage.FileName);
                                    var fileName = Path.Combine(hostingEnvironment.WebRootPath, "images/Barbershops/", currentBarbershop.BarbershopId + extension);
                                    FileStream stream = new FileStream(fileName, FileMode.Create);
                                    model.barbershopImage.CopyTo(stream);
                                    stream.Close();
                                    currentBarbershop.PhotoLink = currentBarbershop.BarbershopId + extension;
                                    repository.AddBarbershop(currentBarbershop);
                                }

                                return RedirectToAction("LoginPage");
                            }
                            else
                            {
                                ModelState.AddModelError("", "This Emailed is used! Try another one");
                                return View();
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Passwords should match!");
                        return View();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return View(model);
        }
    }
}
