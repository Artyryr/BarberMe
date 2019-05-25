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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

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
                    model.Geoposition, "NotFound.png");

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

        [Authorize]
        [HttpGet]
        public async Task<ViewResult> BarbershopIndex()
        {
            BarbershopUser currentUser = await GetCurrentUserAsync();
            Barbershop barbershop = repository.Barbershops.Where(p => p.BarbershopUserId == currentUser.Id).FirstOrDefault();

            return View(barbershop);
        }

        [Authorize]
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

        [Authorize]
        [HttpGet]
        public ViewResult AddBarberPage(int id)
        {
            BarberRegistrationModel model = new BarberRegistrationModel();
            model.barbershopId = id;
            return View(model);
        }

        [Authorize]
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
                return RedirectToAction("BarbersPage", new { id = currentBarber.BarbershopId });
            }
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ViewResult EditBarber(int id)
        {
            Barber barber = repository.Barbers.Where(p => p.BarberId == id).FirstOrDefault();
            BarberRegistrationModel model = new BarberRegistrationModel();
            model.Barber = barber;
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditBarber(BarberRegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                repository.AddBarber(model.Barber);

                if (model.BarberImage != null)
                {
                    var extension = Path.GetExtension(model.BarberImage.FileName);
                    var fileName = Path.Combine(hostingEnvironment.WebRootPath, "images/Barbers/", model.Barber.BarberId + extension);
                    FileStream stream = new FileStream(fileName, FileMode.Create);
                    model.BarberImage.CopyTo(stream);
                    stream.Close();
                    model.Barber.PhotoLink = model.Barber.BarberId + extension;
                    repository.AddBarber(model.Barber);
                }
                return RedirectToAction("BarbersPage", new { id = model.Barber.BarbershopId });
            }
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult DeleteBarber(int id)
        {
            Barber barber = repository.Barbers.Where(p => p.BarberId == id).FirstOrDefault();
            repository.RemoveBarber(id);
            return RedirectToAction("BarbersPage", new { id = barber.BarbershopId });
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddSchedule(int id)
        {
            List<Barber> barbers = repository.Barbers.Where(p => p.BarbershopId == id).ToList();
            List<SelectListItem> variants = (from barber in repository.Barbers
                                             where barber.BarbershopId == id
                                             select new SelectListItem { Value = barber.BarberId.ToString(), Text = barber.LastName + " " + barber.FirstName }).ToList();

            Schedule schedule = new Schedule();
            ScheduleModel model = new ScheduleModel { Schedule = schedule, Names = variants, BarbershopId = id };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddSchedule(ScheduleModel model)
        {
            if (ModelState.IsValid)
            {
                List<Schedule> list = new List<Schedule>();
                TimeSpan difference = model.To.Subtract(model.From);

                int duration = difference.Hours * 60;
                int fragments = duration / 30;

                Schedule schedule = new Schedule();
                schedule = model.Schedule;
                schedule.Availability = true;
                TimeSpan time = new TimeSpan(model.From.Hour, model.From.Minute, 0);

                schedule.Date = schedule.Date + time;

                for (int i = 0; i < fragments; i++)
                {
                    Schedule sc = new Schedule { Availability = true, BarberId = schedule.BarberId, Date = schedule.Date.AddMinutes(i * 30), Time = schedule.Date.AddMinutes(i * 30) };
                    list.Add(sc);
                }

                int barbershopId = model.BarbershopId;
                repository.AddListSchedule(list);
                return RedirectToAction("SchedulePage", new { id = barbershopId });
            }
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult SchedulePage(int id)
        {
            List<Barber> barbers = repository.Barbers.Where(p => p.BarbershopId == id).ToList();
            Dictionary<int, List<List<Schedule>>> allSchedules = new Dictionary<int, List<List<Schedule>>> { };

            foreach (Barber barber in barbers)
            {
                List<List<Schedule>> barbersSchedule = new List<List<Schedule>>() { };

                for (int i = 0; i <= 6; i++)
                {
                    DateTime day = DateTime.Today.AddDays(i);

                    List<Schedule> schedule = (from sched in repository.Schedules
                                               where sched.BarberId == barber.BarberId
                                               && sched.Date.Date == day.Date
                                               orderby sched.Date
                                               select sched
                                               ).ToList();

                    barbersSchedule.Add(schedule);
                }
                allSchedules.Add(barber.BarberId, barbersSchedule);
            }

            SchedulePageModel model = new SchedulePageModel();
            model.Barbers = barbers;
            model.AllSchedules = allSchedules;

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult ServicesPage(int id)
        {
            List<Service> services = (from service in repository.Service
                                      where service.BarbershopId == id
                                      select service).ToList();
            Barbershop barbershop = (from barb in repository.Barbershops
                                     where barb.BarbershopId == id
                                     select barb).FirstOrDefault();

            ServicesPageModel model = new ServicesPageModel { Services = services, Barbershop = barbershop };

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddService(int id)
        {
            List<SelectListItem> serviceTypes = (from type in repository.ServiceTypes
                                                 select new SelectListItem { Value = type.ServiceTypeId.ToString(), Text = type.ServiceTypeName }).ToList();

            AddServiceModel model = new AddServiceModel { Service = new Service { BarbershopId = id }, ServiceTypes = serviceTypes };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddService(AddServiceModel model)
        {
            if (ModelState.IsValid)
            {
                repository.AddService(model.Service);
                return RedirectToAction("ServicesPage", new { id = model.Service.BarbershopId });
            }
            return View(model);
        }


        [Authorize]
        [HttpGet]
        public ViewResult EditService(int id)
        {
            Service service = repository.Service.Where(p => p.ServiceId == id).FirstOrDefault();
            List<SelectListItem> serviceTypes = (from type in repository.ServiceTypes
                                                 select new SelectListItem { Value = type.ServiceTypeId.ToString(), Text = type.ServiceTypeName }).ToList();
            AddServiceModel model = new AddServiceModel { Service = service, ServiceTypes = serviceTypes };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditService(AddServiceModel model)
        {
            if (ModelState.IsValid)
            {
                repository.AddService(model.Service);
                return RedirectToAction("ServicesPage", new { id = model.Service.BarbershopId });
            }
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult DeleteService(int id, int barbershopId)
        {
            repository.RemoveService(id);
            return RedirectToAction("ServicesPage", new { id = barbershopId });
        }

        [Authorize]
        [HttpGet]
        public ActionResult BarbershopInfo(int id)
        {
            Barbershop barbershop = repository.Barbershops.Where(p => p.BarbershopId == id).FirstOrDefault();
            return View(barbershop);
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditBarbershop(int id)
        {
            Barbershop barbershop = repository.Barbershops.Where(p => p.BarbershopId == id).FirstOrDefault();
            EditBarbershopModel model = new EditBarbershopModel { Barbershop = barbershop };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditBarbershop(EditBarbershopModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.barbershopImage != null)
                {
                    var extension = Path.GetExtension(model.barbershopImage.FileName);
                    var fileName = Path.Combine(hostingEnvironment.WebRootPath, "images/Barbershops/", model.Barbershop.BarbershopId + extension);
                    FileStream stream = new FileStream(fileName, FileMode.Create);
                    model.barbershopImage.CopyTo(stream);
                    stream.Close();
                    model.Barbershop.PhotoLink = model.Barbershop.BarbershopId + extension;
                    repository.AddBarbershop(model.Barbershop);
                }
                else
                {
                    repository.AddBarbershop(model.Barbershop);
                }
                return RedirectToAction("BarbershopInfo", new { id = model.Barbershop.BarbershopId });
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

            ServiceBookingModel model = new ServiceBookingModel { Barbershop = barbershop, BarbershopId = barbershop.BarbershopId };
            return View(model);
        }

        [HttpGet]
        public ViewResult BarberPage(int id)
        {

            Barber barber = repository.Barbers.Where(b => b.BarberId == id).FirstOrDefault();
            List<Review> reviews = (from review in repository.Reviews
                                    where review.BarberId == id
                                    orderby review.Date descending
                                    select review).ToList();

            Barbershop barbershop = repository.Barbershops.Where(b => b.BarbershopId == id).FirstOrDefault();

            BarberPageModel model = new BarberPageModel { Barber = barber, Reviews = reviews, Barbershop = barbershop };
            return View(model);
        }

        [HttpGet]
        public ViewResult ServiceBookingSelection(ServiceBookingModel model)
        {
            //List<Barber> barbers = repository.Barbers.Where(b => b.BarbershopId == model.BarbershopId).ToList();
            //List<Service> services = repository.Service.Where(s => s.BarbershopId == model.BarbershopId).ToList();

            if (model.BarberId != null && model.BarberId != 0)
            {
                Barber barber = repository.Barbers.Where(b => b.BarberId == model.BarberId).FirstOrDefault();
                model.Barber = barber;
            }
            if (model.ServiceId != null && model.ServiceId != 0)
            {
                Service service = repository.Service.Where(b => b.ServiceId == model.ServiceId).FirstOrDefault();
                model.Service = service;
            }
            if(model.ScheduleId != null && model.ScheduleId != 0)
            {
                Schedule schedule = repository.Schedules.Where(b => b.ScheduleId == model.ScheduleId).FirstOrDefault();
                model.Schedule = schedule;
            }

            return View(model);
        }

        [HttpGet]
        public ViewResult SelectBarber(ServiceBookingModel model)
        {
            List<Barber> barbers = repository.Barbers.Where(p => p.BarbershopId == model.BarbershopId).ToList();
            model.Barbers = barbers;
            return View(model);
        }

        [HttpGet]
        public ViewResult SelectService(ServiceBookingModel model)
        {
            List<Service> services = repository.Service.Where(p => p.BarbershopId == model.BarbershopId).ToList();
            model.Services = services;
            return View(model);
        }

        [HttpPost]
        public ActionResult BarberPage(Review review)
        {
            if (ModelState.IsValid)
            {
                repository.AddReview(review);
            }

            Barber barber = repository.Barbers.Where(p => p.BarberId == review.BarberId).FirstOrDefault();
            List<Review> reviews = (from rev in repository.Reviews
                                    where rev.BarberId == barber.BarberId
                                    orderby rev.Date descending
                                    select rev).ToList();
            Barbershop barbershop = repository.Barbershops.Where(b => b.BarbershopId == barber.BarbershopId).FirstOrDefault();

            BarberPageModel model = new BarberPageModel { Barber = barber, Reviews = reviews, Barbershop = barbershop };
            return View(model);
        }

        [HttpGet]
        public ViewResult SelectSchedule(ServiceBookingModel model)
        {
            Barber barber = repository.Barbers.Where(b => b.BarberId == model.BarberId).FirstOrDefault();
            Service service = repository.Service.Where(s => s.ServiceId == model.ServiceId).FirstOrDefault();

            int amountOfSections = service.ServiceDuration / 30;

            List<Schedule> schedules = (from schedule in repository.Schedules
                                        where schedule.BarberId == barber.BarberId
                                        && schedule.Availability == true
                                        orderby schedule.Date
                                        select schedule).ToList();

            List<List<Schedule>> relevantSchedules = new List<List<Schedule>>() { };

            if (amountOfSections == 2)
            {
                for (int i = 0; i < schedules.Count - 1; i++)
                {
                    if(schedules[i + 1].Date == schedules[i].Date.AddMinutes(30))
                    {
                        List<Schedule> foundSequence = new List<Schedule>() { };

                        foundSequence.Add(schedules[i]);
                        foundSequence.Add(schedules[i + 1]);

                        relevantSchedules.Add(foundSequence);
                    }
                }
            }
            else if (amountOfSections == 3) {
                for (int i = 0; i < schedules.Count - 2; i++)
                {
                    if (schedules[i + 1].Date == schedules[i].Date.AddMinutes(30) && schedules[i + 2].Date == schedules[i + 1].Date.AddMinutes(30))
                    {
                        List<Schedule> foundSequence = new List<Schedule>() { };

                        foundSequence.Add(schedules[i]);
                        foundSequence.Add(schedules[i + 1]);
                        foundSequence.Add(schedules[i + 2]);

                        relevantSchedules.Add(foundSequence);
                    }
                }
            }
            else if (amountOfSections == 4) {
                for (int i = 0; i < schedules.Count - 3; i++)
                {
                    if (schedules[i + 1].Date == schedules[i].Date.AddMinutes(30) && schedules[i + 2].Date == schedules[i + 1].Date.AddMinutes(30) 
                        && schedules[i + 3].Date == schedules[i + 2].Date.AddMinutes(30))
                    {
                        List<Schedule> foundSequence = new List<Schedule>() { };

                        foundSequence.Add(schedules[i]);
                        foundSequence.Add(schedules[i + 1]);
                        foundSequence.Add(schedules[i + 2]);
                        foundSequence.Add(schedules[i + 3]);

                        relevantSchedules.Add(foundSequence);
                    }
                }
            }
            else if (amountOfSections == 5)
            {
                for (int i = 0; i < schedules.Count - 4; i++)
                {
                    if (schedules[i + 1].Date == schedules[i].Date.AddMinutes(30) && schedules[i + 2].Date == schedules[i + 1].Date.AddMinutes(30)
                        && schedules[i + 3].Date == schedules[i + 2].Date.AddMinutes(30) && schedules[i + 4].Date == schedules[i + 3].Date.AddMinutes(30))
                    {
                        List<Schedule> foundSequence = new List<Schedule>() { };

                        foundSequence.Add(schedules[i]);
                        foundSequence.Add(schedules[i + 1]);
                        foundSequence.Add(schedules[i + 2]);
                        foundSequence.Add(schedules[i + 3]);
                        foundSequence.Add(schedules[i + 4]);

                        relevantSchedules.Add(foundSequence);
                    }
                }
            }
            else {
                foreach (var schedule in schedules)
                {
                    List<Schedule> foundSequence = new List<Schedule>() { };

                    foundSequence.Add(schedule);

                    relevantSchedules.Add(foundSequence);
                }
            }

            model.RelevantSchedules = relevantSchedules;
            
            return View(model);
        }

        [HttpGet]
        public ViewResult EnterCustomerInfo(ServiceBookingModel model)
        {
            return View(model);
        }

        [HttpPost]
        public ActionResult SummaryInfo(ServiceBookingModel model)
        {
            if (ModelState.IsValid)
            {
                Barber barber = repository.Barbers.Where(b => b.BarberId == model.BarberId).FirstOrDefault();
                Barbershop barbershop = repository.Barbershops.Where(b => b.BarbershopId == model.BarbershopId).FirstOrDefault();
                Service service = repository.Service.Where(b => b.ServiceId == model.ServiceId).FirstOrDefault();
                Schedule schedule = repository.Schedules.Where(s => s.ScheduleId == model.ScheduleId).FirstOrDefault();

                Order order = model.Order;
                order.Barber = barber;
                order.Barbershop = barbershop;
                order.Service = service;
                order.Schedule = schedule;
                order.Price = service.ServicePrice;

                if (model.Payment != null)
                {
                    repository.AddPayment(model.Payment);

                    Payment payment = repository.Payments.Where(p => p == model.Payment).FirstOrDefault();
                    order.Payment = payment;
                }

                model.Order = order;

                int amountOfSections = service.ServiceDuration / 30;
                List<Schedule> schedules = (from sch in repository.Schedules
                                            where sch.BarberId == barber.BarberId
                                            && sch.Availability == true
                                            orderby sch.Date
                                            select sch).ToList();

                List<Schedule> relevantSchedules = new List<Schedule>() { };
                int position = schedules.IndexOf(schedule);

                for(int i = 0; i < amountOfSections; i++)
                {
                    relevantSchedules.Add(schedules[position + i]);
                }

                foreach (var item in relevantSchedules)
                {
                    item.Availability = false;
                    repository.AddSchedule(item);
                }

                model.Order = order;

                repository.AddOrder(order);
                return View("SummaryInfo", model);
            }
            return View("EnterCustomerInfo", model);
        }
    }
}
