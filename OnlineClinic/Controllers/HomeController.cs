using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineClinic.Models;
using OnlineClinic.Models.AuthorizationAuthentication.Context;
using OnlineClinic.Models.AuthorizationAuthentication.Model;
using OnlineClinic.Models.Db;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        ClinicContext db;
        ExportDoctorInformation export;
        RequestDoctor request;
        private readonly UserManager<User> userManager;
        AccountDbContext dbaccount;
        public HomeController(ILogger<HomeController> logger, ClinicContext clinic, ExportDoctorInformation export, RequestDoctor request, UserManager<User> userManager, AccountDbContext dbaccount)
        {
            _logger = logger;
            db = clinic;
            this.export = export;
            export.timeReceipt = new Dictionary<string, string>();
            this.request = request;
            this.userManager = userManager;
            this.dbaccount = dbaccount;
            if (export.timeReceipt.Count == 0)
            {
                export.timeReceipt.Add("8:00-8:30", "8:00");
                export.timeReceipt.Add("8:30-9:00", "8:30");
                export.timeReceipt.Add("9:00-9:30", "9:00");
                export.timeReceipt.Add("9:30-9:00", "9:30");
                export.timeReceipt.Add("10:00-10:30", "10:00");
                export.timeReceipt.Add("10:30-11:00", "10:30");
                export.timeReceipt.Add("11:00-11:30", "11:00");
                export.timeReceipt.Add("11:30-12:00", "11:30");
            }
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(db.Doctors);
        }
       /// <summary>
       /// Получение информации о докторе
       /// </summary>
       /// <param name="id"></param>
       /// <param name="day"></param>
       /// <returns></returns>
        [HttpGet]
        public IActionResult DoctorInformation(int id, int day)
        {
            export.IdDoctor = id;

            export.clinicContext = db;

            export.dayRegistration = day;


            var name = (from item in db.RecordTimes
                        where item.DoctorId == id && item.Day == day
                        select item.TimeReceipt).ToList();
            export.Time = name;

            return View(export);
        }
        /// <summary>
        /// вывод календаря доктор для записи
        /// </summary>
        /// <param name="id"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public IActionResult Calendar(int id, int day)
        {
            export.IdDoctor = id;

            export.clinicContext = db;

            export.dayRegistration = day;

           

            var name = (from item in db.RecordTimes
                        where item.DoctorId == id && item.Day == day
                        select item.TimeReceipt).ToList();
            export.Time = name;
            return PartialView(export);
        }
        /// <summary>
        /// Вывод страницы для записи
        /// </summary>
        /// <param name="timeRecord"></param>
        /// <param name="idDoctor"></param>
        /// <param name="dayRegistration"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult RecordPage(string timeRecord, int idDoctor, int dayRegistration)
        {

            request.TimeReceipt = timeRecord;

            request.Data = dayRegistration;
            request.IdDoctor = idDoctor;
            string Id= userManager.GetUserId(User);
            request.IdPatient = Id;

            var nam = from item in dbaccount.Users
                      where Id == item.Id
                      select item.Lastname;

            foreach (var item in nam)
            {
                request.SurnamePatient = item;
            }
         
            var nameDoctro = from item in db.Doctors
                             where item.Id == idDoctor
                             select new
                             {
                                 Name = item.Surname,
                                 kabinet = item.OfficeNumber
                             };

            foreach (var item in nameDoctro)
            {
                request.OfficeNumber = item.kabinet;
                request.SurnameDoctor = item.Name;
            }

            return View(request);
        }
        /// <summary>
        /// получение новой зписи и сохраниени в БД
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RecordPage(RequestDoctor request)
        {
            db.Requests.Add(
                new RequestDoctor
                {
                    Data = request.Data,
                    SurnamePatient = request.SurnamePatient,
                    SurnameDoctor = request.SurnameDoctor,
                    TimeReceipt = request.TimeReceipt,
                    OfficeNumber = request.OfficeNumber,
                    IdPatient= request.IdPatient,
                    IdDoctor = request.IdDoctor
                }
                );

            db.RecordTimes.Add(
                new RecordDayTime
                {
                    TimeReceipt = request.TimeReceipt,
                    Day = request.Data,
                    DoctorId = request.IdDoctor,
                    DayAppeal = DateTime.Now
                }
                );

            db.SaveChanges();

            return RedirectToAction("DoctorInformation" , new {id= request.IdDoctor, day = request.Data });
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
