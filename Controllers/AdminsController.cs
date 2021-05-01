using CarInsurance.Models;
using CarInsurance.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CarInsurance.Controllers
{
    public class AdminsController : Controller
    {
        private readonly InsuranceEntities db = new InsuranceEntities();
        // GET: Admins
        // GET: Insuree/Details/5
        public ActionResult Index()
        {
            var insurees = (from c in db.Insurees where c.DUI != true select c).ToList();
            var adminVms = new List<AdminVM>();
            foreach (var insuree in insurees)
            {
                var adminVm = new AdminVM();
                adminVm.Id = insuree.Id;
                adminVm.FirstName = insuree.FirstName;
                adminVm.LastName = insuree.LastName;
                adminVm.EmailAddress = insuree.EmailAddress;
                adminVm.Quote = insuree.Quote;
                adminVms.Add(adminVm);
            }
            return View(adminVms);
        }
    }
}
