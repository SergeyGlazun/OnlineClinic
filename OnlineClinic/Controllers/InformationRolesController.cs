using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineClinic.Models;
using OnlineClinic.Models.AuthorizationAuthentication.Context;
using OnlineClinic.Models.AuthorizationAuthentication.Model;
using OnlineClinic.Models.AuthorizationAuthentication.ViewModels;
using OnlineClinic.Models.Db;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace OnlineClinic.Controllers
{
    public class InformationRolesController : Controller
    {
        
        ClinicContext db;
        AccountDbContext dbaccount;
        private readonly UserManager<User> userManager;
        RequestDoctor request;
        List<RequestDoctor> informationRequest;
        ExportStatictic exportStatictic;
        private readonly SignInManager<User> signInManager;
        IWebHostEnvironment _appEnvironment;
    

        public InformationRolesController(  ClinicContext db,
                                            AccountDbContext dbaccount, 
                                            UserManager<User> userManager,
                                            RequestDoctor request,
                                            SignInManager<User> signInManager,
                                            IWebHostEnvironment _appEnvironment
                                          )
        {
            this.db = db;
            this.dbaccount = dbaccount;
            this.userManager = userManager;
            this.request = request;
            informationRequest = new List<RequestDoctor>();
            exportStatictic = new ExportStatictic();
            this.signInManager = signInManager;
            this._appEnvironment = _appEnvironment;
        }

     /// <summary>
     /// получение списка записей пациента к доктору
     /// </summary>
     /// <returns></returns>     
        public IActionResult userInformation()
        {         

            string  Id= userManager.GetUserId(User);

            var DataReqest = from item in db.Requests
                              where Id == item.IdPatient
                             orderby item.Data ascending
                             select new
                              {
                                  Data = item.Data,
                                  NameDoctor = item.SurnameDoctor,
                                  Time = item.TimeReceipt,
                                  NuberCabinet = item.OfficeNumber
                              };
        
            foreach (var item in DataReqest)
            {
                informationRequest.Add(
                     new RequestDoctor
                     {
                         Data = item.Data,
                         SurnameDoctor = item.NameDoctor,
                         TimeReceipt = item.Time,
                         OfficeNumber = item.NuberCabinet
                     }
                    );
               
            }
            return View(informationRequest);
        }
        /// <summary>
        /// получение статистики регистрация врачей
        /// </summary>
        /// <returns></returns>
     
        public IActionResult adminInformation()
        {
            if (User.IsInRole("admin"))
            {

                exportStatictic.CountUsers = dbaccount.Users.Count();
                exportStatictic.CountDoctors = db.Doctors.Count();
                exportStatictic.CountRequest = db.Requests.Count();
            }
            exportStatictic.model = new RegisterModel();
            return View(exportStatictic);
        }
        /// <summary>
        /// форма для регистрации нового сотрудника
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult RegistrationNewDoctor()
        {
            return PartialView(exportStatictic);
        }
        [HttpPost]
        public async Task<IActionResult> RegistrationNewDoctor(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = model.UserName,
                    Lastname = model.Lastname,
                    AddressCity = model.Address,
                    PhoneNumber = model.PhoneNumber
                };

                db.Doctors.Add(
                       new Doctor
                      {
                          UserId = user.Id,
                          Surname = user.Lastname
                      }
                    );
              

                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await signInManager.SignInAsync(user, true);
                    await userManager.AddToRoleAsync(user, "doctor");
                    db.SaveChanges();
                    return RedirectToAction("adminInformation","InformationRoles");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        public IActionResult DoctorPage()
        {
           
            Doctor doctor = (from item in db.Doctors
                             where item.UserId == userManager.GetUserId(User)
                             select item).Single<Doctor>();

            var DataReqest = from item in db.Requests
                             where item.IdDoctor == doctor.Id
                             orderby item.Data ascending
                             select new
                             {
                                 Data = item.Data,
                                 NamePatient = item.SurnamePatient,
                                 Time = item.TimeReceipt,
                                 NuberCabinet = item.OfficeNumber
                             };

            foreach (var item in DataReqest)
            {
                informationRequest.Add(
                     new RequestDoctor
                     {
                         Data = item.Data,
                         SurnamePatient = item.NamePatient,
                         TimeReceipt = item.Time,
                         OfficeNumber = item.NuberCabinet
                     }
                    );

            }
            return View(informationRequest);           
        }

        public IActionResult RedactorDoctorPage()
        {           
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> RedactorDoctorPage(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/img/foto/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };
           
                Doctor doctor = (from item in db.Doctors
                                 where item.UserId == userManager.GetUserId(User)
                                 select item).Single<Doctor>();
              
                doctor.Foto = path;
              
                db.SaveChanges();
            }

            return PartialView();
        }
        [HttpGet]
        public IActionResult RedactorDoctorPageData()
        {          
            return View();
        }
        [HttpPost]
        public IActionResult RedactorDoctorPageData(Doctor doctor)
        {
            Doctor Thisdoctor = (from item in db.Doctors
                             where item.UserId == userManager.GetUserId(User)
                             select item).Single<Doctor>();

            Thisdoctor.Name = doctor.Name;
            Thisdoctor.Surname = doctor.Surname;
            Thisdoctor.Patronymic = doctor.Patronymic;
            Thisdoctor.Specialty = doctor.Specialty;
            Thisdoctor.OfficeNumber = doctor.OfficeNumber;

            db.SaveChanges();
            return PartialView();
        }
    }
}
