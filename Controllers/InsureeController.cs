using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarInsurance.Models;

namespace CarInsurance.Controllers
{
    public class InsureeController : Controller
    {
        private readonly InsuranceEntities db = new InsuranceEntities();

        // GET: Insuree
        public ActionResult Index()
        {
            return View(db.Insurees.ToList());
        }

        // GET: Insuree/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // GET: Insuree/Create
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            using (InsuranceEntities db = new InsuranceEntities())
            {
                var create = new Create();
                create.Id = insuree.Id;
                create.FirstName = insuree.FirstName;
                create.LastName = insuree.LastName;
                create.EmailAddress = insuree.EmailAddress;
                create.DateOfBirth = insuree.DateOfBirth;
                create.CarYear = insuree.CarYear;
                create.CarMake = insuree.CarMake;
                create.CarModel = insuree.CarModel;
                create.DUI = Convert.ToBoolean(insuree.DUI);
                create.SpeedingTickets = insuree.SpeedingTickets;
                create.CoverageType = Convert.ToBoolean(insuree.CoverageType);
                create.Quote = insuree.Quote;

                decimal baseRate = 50.00m;
                decimal sumRate;




                var today = DateTime.Today;
                var age = today.Year - create.DateOFBirth.Year;
                if (create.DateOFBirth.Date > today.AddYears(-age)) age--;

                //DECISION 1
                //"Is age 18 or younger?"
                if (age <= 18)
                {
                    sumRate = baseRate + 100.00m;

                }
                else
                {
                    sumRate = baseRate;

                }

                //DECISION 2
                //"Is age between 19 and 25?"
                if ((age - 19) * (age - 25) <= 0)
                {
                    sumRate += 50.00m;

                }


                //Decision 3
                //"Is persons Age Over 25?"
                if (age > 25)
                {
                    sumRate += 25.00m;

                }


                // DECISION 4 AND 5
                //"Is Car Year Outside of range 2000 to 2015?"

                if ((create.CarYear - 2000) * (create.CarYear - 2015) >= 0)
                {
                    sumRate += 25.00m;

                }


                // DECISION 6
                //"Is Make Porsche?"

                if (create.CarMake == "porsche")
                {
                    sumRate += 25.00m;

                }


                // DECISION 7
                //"Is Model 911 Carrera?"

                if (create.CarModel == "911 carrera")
                {
                    sumRate += 25.00m;
                }


                // DECISION 8
                //"How Many Speeding Tickets?"

                if (create.SpeedingTickets > 0)
                {
                    decimal ticketsFee = create.SpeedingTickets * 10.00m;
                    sumRate += ticketsFee;
                }

                // DECISION 9
                //"Have you had a DUI?"

                if (create.DUI == true)
                {
                    sumRate *= 1.25m;
                }

                // DECISION 10
                //"Full Coverage?"


                if (create.CoverageType == true)
                {
                    sumRate *= 1.50m;
                }

                create.Quote = sumRate;
                

            }

            return View();
        }

        // POST: Insuree/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                db.Insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(insuree);
        }

        // GET: Insuree/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                db.Entry(insuree).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuree);
        }

        // GET: Insuree/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Insuree insuree = db.Insurees.Find(id);
            db.Insurees.Remove(insuree);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
