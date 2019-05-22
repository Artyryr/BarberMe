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

        [HttpPost]
        public async Task<IActionResult> LoginPage(LoginModel account)
        {
            if (ModelState.IsValid)
            {
                BarbershopUser user =
                    await userManager.FindByEmailAsync(account.Email);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    if ((await signInManager.PasswordSignInAsync(user,
                            account.Password, false, false)).Succeeded)
                    {
                        //return RedirectToAction(account?.ReturnUrl ?? "BarbershopIndex");
                        return RedirectToAction("BarbershopIndex");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid name or password");
            return View(account);
        }
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
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

        [HttpGet]
        public async Task<ViewResult> BarbershopIndex()
        {
            BarbershopUser currentUser = await GetCurrentUserAsync();
            Barbershop barbershop = repository.Barbershops.Where(p => p.BarbershopUserId == currentUser.Id).FirstOrDefault();

            return View(barbershop);
        }

        [HttpGet]
        public async Task<ViewResult> BarbersPage(int id)
        {
            BarbershopUser currentUser = await GetCurrentUserAsync();
            Barbershop barbershop = repository.Barbershops.Where(p => p.BarbershopUserId == currentUser.Id).FirstOrDefault();
            List<Barber> barbers = repository.Barbers.Where(b => b.BarbershopId == id).ToList();

            BarbersPageModel model = new BarbersPageModel();
            model.Barbers = barbers;
            model.Barbershop = barbershop;

            return View(model);
        }

        [HttpGet]
        public ViewResult AddBarberPage(int id)
        {
            BarberRegistrationModel model = new BarberRegistrationModel();
            model.barbershopId = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult AddBarberPage(BarberRegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                Barber newBarber = new Barber();

                newBarber = model.Barber;
                newBarber.BarbershopId = model.barbershopId;

                repository.AddBarber(newBarber);

                Barber currentBarber = repository.Barbers.Where(p => p.Equals(newBarber)).FirstOrDefault();

                if (model.BarberImage != null)
                {
                    var extension = Path.GetExtension(model.BarberImage.FileName);
                    var fileName = Path.Combine(hostingEnvironment.WebRootPath, "images/Barbers/", currentBarber.BarberId + extension);
                    FileStream stream = new FileStream(fileName, FileMode.Create);
                    model.BarberImage.CopyTo(stream);
                    stream.Close();
                    currentBarber.PhotoLink = currentBarber.BarberId + extension;
                    repository.AddBarber(currentBarber);
                }
                return RedirectToAction("BarbersPage", currentBarber.BarbershopId);
            }
            return View(model);
        }
        //------------------------------  

        [HttpGet]
        public ViewResult CustomerIndex()
        {
            List<Barbershop> barbershops = repository.Barbershops.ToList();
            CustomerIndexModel model = new CustomerIndexModel { Barbershops = barbershops };
            return View(model);
        }

        [HttpGet]
        public ViewResult BarbershopPage(int id)
        {
            Barbershop barbershop = repository.Barbershops.Where(p => p.BarbershopId == id).FirstOrDefault();
            List<Service> services = repository.Service.Where(p => p.BarbershopId == barbershop.BarbershopId).ToList();
            List<Barber> barbers = repository.Barbers.Where(p => p.BarbershopId == barbershop.BarbershopId).ToList();

            barbershop.Services = services;
            barbershop.Barbers = barbers;
            return View(barbershop);
        }


    }
}
