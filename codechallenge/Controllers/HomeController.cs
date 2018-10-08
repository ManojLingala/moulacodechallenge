using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using codechallenge.Models;

namespace codechallenge.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        #region Customer Services

        public ActionResult getCustomers()
        {
            try
            {
                using (CustomerDBEntities db = new CustomerDBEntities())
                {
                    var customers = db.Customers.Where(w => w.isDeleted != true).ToList().Select(s => new
                    {
                        s.CustomerID,
                        s.FirstName,
                        s.LastName,
                        s.Email,
                        BirthDate = s.BirthDate.HasValue ? s.BirthDate.Value.ToShortDateString() : "",
                        FullName = s.FirstName + " " + s.LastName,
                        CustomerCode = (s.FirstName + "" + s.LastName).Replace(" ", "").ToLower() + (s.BirthDate.HasValue ? s.BirthDate.Value.ToString("yyyyMMdd") : "")
                    }).OrderBy(o => o.LastName).ToList();

                    return Json(new { customers }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception er)
            {
                return Json(new { status="error", msg = er.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<JsonResult> SaveCustomer(int? editCustomerID, Customer customer)
        {
            try
            {
                using (CustomerDBEntities db = new CustomerDBEntities())
                {
                    string userId = User.Identity.GetUserId();
                    Customer cust = new Customer();
                    //Check if this process for update or insert
                    if (editCustomerID.HasValue)
                    {
                        // if this for update, get current record
                        cust = await db.Customers.FindAsync(editCustomerID.Value);
                    }
                    else
                    {
                        // if this for insert new customer, defined created date and who user created
                        cust.DateCreated = DateTime.Now;
                        cust.CreatedBy = userId;
                    }

                    //General customer data for both process insert or update
                    cust.DateModified = DateTime.Now;
                    cust.ModifiedBy = userId;
                    cust.isDeleted = false;
                    cust.FirstName = customer.FirstName;
                    cust.LastName = customer.LastName;
                    cust.Email = customer.Email;
                    cust.BirthDate = customer.BirthDate;
                    //-------------------------------------------------------

                    if (editCustomerID.HasValue)  // For Update Record 
                        db.Entry(cust).State = System.Data.Entity.EntityState.Modified;
                    else
                        db.Customers.Add(cust);  // Insert New Record
                    
                    await db.SaveChangesAsync(); // 
                }
                return Json(new { status = "Done" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception er)
            {
                return Json(new { status = "error", msg = er.Message }, JsonRequestBehavior.AllowGet);
            }

        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<JsonResult> DeleteCustomer(int CustomerID)
        {
            try
            {
                using (CustomerDBEntities db = new CustomerDBEntities())
                {
                    var cust = await db.Customers.FindAsync(CustomerID);

                    cust.isDeleted = true;
                    cust.DateModified = DateTime.Now;
                    cust.ModifiedBy = User.Identity.GetUserId();

                    await db.SaveChangesAsync();
                }
                return Json(new { status = "Done" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception er)
            {
                return Json(new { status = "error", msg = er.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}