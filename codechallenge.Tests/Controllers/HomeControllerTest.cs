using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using codechallenge;
using codechallenge.Controllers;

namespace codechallenge.Tests.Controllers
{
    public class MockHttpContext : System.Web.HttpContextBase
    {
        public override System.Security.Principal.IPrincipal User { get; set; }
    }
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void getCustomers()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            JsonResult result = controller.getCustomers() as JsonResult;

            // Assert
            Assert.IsNotNull(result);
        }

        //Test for Update Customer
        [TestMethod]
        public void SaveCustomer()
        {
            // Arrange
            HomeController controller = new HomeController();
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new MockHttpContext
                {
                    User = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity("ibrahim.marasa@gmail.com"), null)
                }
            };
            Models.Customer cust = new Models.Customer();
            cust.BirthDate = DateTime.Now;
            cust.FirstName = "Firstname";
            cust.LastName = "Lastname";
            cust.Email = "mail@mail.com";
            // Act
            var result = controller.SaveCustomer(null, cust) as Task<JsonResult>;
            var data = result.Result.Data.ToString();
            var expect = new JsonResult();
            expect.Data = new { status = "Done" };

            // Assert
            Assert.IsNotNull(result);
            //Assert.AreEqual(expect.Data.ToString(), data);
        }



        //Test for Update Customer
        [TestMethod]
        public void UpdateCustomer()
        {
            // Arrange
            HomeController controller = new HomeController();
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new MockHttpContext
                {
                    User = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity("ibrahim.marasa@gmail.com"), null)
                }
            };
            Models.Customer cust = new Models.Customer();
            cust.BirthDate = DateTime.Now;
            cust.FirstName = "Firstname";
            cust.LastName = "Lastname";
            cust.Email = "mail@mail.com";

            int? customerID = 1;
            // Act
            var result = controller.SaveCustomer(customerID, cust) as Task<JsonResult>;
            var data = result.Result.Data.ToString();
            var expect = new JsonResult();
            expect.Data = new { status = "Done" };

            // Assert
            Assert.IsNotNull(result);
            //Assert.AreEqual(expect.Data.ToString(), data);
        }

        //DeleteCustomer

        [TestMethod]
        public void DeleteCustomer()
        {
            // Arrange
            HomeController controller = new HomeController();
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new MockHttpContext
                {
                    User = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity("ibrahim.marasa@gmail.com"), null)
                }
            };
            int CustomerID = 1;
            // Act
            var result = controller.DeleteCustomer(CustomerID) as Task<JsonResult>;
            var data = result.Result.Data.ToString();
            //var status = data.status;

            var expect = new JsonResult();
            expect.Data = new { status = "Done" };

            // Assert
            Assert.IsNotNull(result);
           // Assert.AreEqual(expect.Data.ToString(), data);

        }
        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }


    }
}
