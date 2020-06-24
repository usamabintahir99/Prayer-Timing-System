using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PTS.Models;

namespace PTS.Controllers
{
    public class HomeController : Controller
    {
        PrayersTimingClassDataContext dc = new PrayersTimingClassDataContext();
        public ActionResult Index()
        {
            ViewBag.msg = TempData["msg"];
            return View(dc.PrayersTimings.Select(m=>m.Area).Distinct().ToList());
        }

        public ActionResult Category()
        {
            var areaName = Request["cat"];
            areaName.Replace("%20"," ");
            return View(dc.PrayersTimings.Where(a=>a.Area==areaName));
        }

        public ActionResult Edit(int id)
        {
            return View(dc.PrayersTimings.First(m=>m.Id==id));
        }

        public ActionResult EditDone(int id)
        {
            var v = dc.PrayersTimings.First(m => m.Id == id);
            v.Name= Request["MosqueName"];
            v.Area=Request["Area"];
            v.Address= Request["Address"];
            v.Fajar= Request["Fajar-Hours"] +":"+ Request["Fajar-Minutes"] +" "+ Request["Fajar-Meridiem"];
            v.Zuhar= Request["Zuhar-Hours"] + ":" + Request["Zuhar-Minutes"] + " " + Request["Zuhar-Meridiem"];
            v.Asar=Request["Asar-Hours"] + ":" + Request["Asar-Minutes"] + " " + Request["Asar-Meridiem"];
            v.Maghrib= Request["Magrib-Hours"] + ":" + Request["Magrib-Minutes"] + " " + Request["Magrib-Meridiem"];
            v.Isha=Request["Isha-Hours"] + ":" + Request["Isha-Minutes"] + " " + Request["Isha-Meridiem"];
            v.Jumma = Request["Jumma-Hours"] + ":" + Request["Jumma-Minutes"] + " " + Request["Jumma-Meridiem"];
            dc.SubmitChanges();
            TempData["msg"] = "Edited successfully.";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var v = dc.PrayersTimings.First(m => m.Id == id);
            dc.PrayersTimings.DeleteOnSubmit(v);
            dc.SubmitChanges();
            TempData["msg"] = "Deleted successfully.";
            return RedirectToAction("Index");
        }
        
        public ActionResult AddMosque()
        {
            ViewBag.Message = "Your add mosque page.";
            return View();
        }

        public ActionResult Add()
        {
            string Name = Request["MosqueName"];
            string Area = Request["Area"];
            string Address = Request["Address"];
            string Fajar = Request["Fajar-Hours"] +":"+ Request["Fajar-Minutes"] +" "+ Request["Fajar-Meridiem"];
            string Zuhar = Request["Zuhar-Hours"] + ":" + Request["Zuhar-Minutes"] + " " + Request["Zuhar-Meridiem"];
            string Asar = Request["Asar-Hours"] + ":" + Request["Asar-Minutes"] + " " + Request["Asar-Meridiem"];
            string Magrib = Request["Magrib-Hours"] + ":" + Request["Magrib-Minutes"] + " " + Request["Magrib-Meridiem"];
            string Isha = Request["Isha-Hours"] + ":" + Request["Isha-Minutes"] + " " + Request["Isha-Meridiem"];
            string Jumma = Request["Jumma-Hours"] + ":" + Request["Jumma-Minutes"] + " " + Request["Jumma-Meridiem"];
            PrayersTiming m = new PrayersTiming();
            m.Address = Address;
            m.Area = Area;
            m.Name = Name;
            m.Fajar = Fajar;
            m.Zuhar = Zuhar;
            m.Asar = Asar;
            m.Maghrib = Magrib;
            m.Isha = Isha;
            m.Jumma = Jumma;

            dc.PrayersTimings.InsertOnSubmit(m);
            dc.SubmitChanges();
            TempData["msg"] = "Mosque added successfully.";
        
            return RedirectToAction("Index");
        }
    }
}