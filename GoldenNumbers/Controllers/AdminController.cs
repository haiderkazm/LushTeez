using GoldenNumbers.BL;
using GoldenNumbers.Helping_Classes;
using GoldenNumbers.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GoldenNumbers.Controllers
{
    public class AdminController : Controller
    {

        SessionDTO sessionDTO = new SessionDTO();
        // GET: Admin
        public ActionResult Index(string err = "")
        {



            if (sessionDTO.getName() != null)
            {
                return RedirectToAction("Dashboard", "Admin");
               
            }
            else
            {
                ViewBag.err = err;
                return View();
            }
        }

        public ActionResult PostLogin(string Email, string Password)
        {
            List<User> user = new UserBL().getUsersList().ToList();
            foreach (User us in user)
            {
                if (us.Email == Email && us.Password == Password)
                {
                    SessionDTO session = new SessionDTO();
                    session.Name = us.Name;
                    session.Id = us.Id;

                    Session["Session"] = session;

                    SessionDTO sdto = (SessionDTO)Session["Session"];
                    return RedirectToAction("DashBoard", "Admin");

                }
               
            }
                return RedirectToAction("Index", "Admin", new { err = "Incorrect Email or Password" });

        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Index", "Admin");
        }


        public ActionResult DashBoard(string message="")
        {
            if (sessionDTO.getName() == null)
            {
                
                return RedirectToAction("Index", "Admin");
               
            }
            else
            {
                string dt = DateTime.Now.ToString("dd/MM/yyyy");
                string[] st = dt.Split('/');
                //Current Month Stats
                List<Number> Numlst = new NumberBL().getNumberList().Where(x => x.CreatedAt.Value.Month == Convert.ToInt32(st[1])).ToList();
                ViewBag.MNumbersList = Numlst.Count;
                List<Customer> DNumlst = new CustomerBL().getCustomerList().Where(x => x.CreatedAt.Value.Month == Convert.ToInt32(st[1]) && x.DesiredNumber!=null ).ToList();
                ViewBag.MDNumbersList = DNumlst.Count;
                List<Customer> SNumlst = new CustomerBL().getCustomerList().Where(x => x.CreatedAt.Value.Month == Convert.ToInt32(st[1]) && x.SellingNumber != null).ToList();
                ViewBag.MSNumbersList = SNumlst.Count;
                List<Subscriber> subslst = new SubscriberBL().getSubscriberList().Where(x => x.CreatedAt.Value.Month == Convert.ToInt32(st[1])).ToList();
                ViewBag.SubscribersList = subslst.Count;

                //Total Stats

                List<Number> Tnumlst = new NumberBL().getNumberList();
                ViewBag.TNumbersList = Tnumlst.Count;
                List<Customer> TDNumlst = new CustomerBL().getCustomerList().Where(x => x.DesiredNumber != null).ToList();
                ViewBag.TDNumbersList = TDNumlst.Count;
                List<Customer> TSNumlst = new CustomerBL().getCustomerList().Where(x => x.SellingNumber != null).ToList();
                ViewBag.TSNumbersList = TSNumlst.Count;
                List<Subscriber> Tsubslst = new SubscriberBL().getSubscriberList();
                ViewBag.TSubscribersList = Tsubslst.Count;

                ViewBag.message = message;
                return View();
            }
           
        }
        public ActionResult AddNumber(string message = "")
        {
            if (sessionDTO.getName() == null)
            {

                return RedirectToAction("Index", "Admin");

            }
            else
            {
                ViewBag.message = message;
                return View();
            }
            

        }
        public ActionResult PostAddNumber(string Number,string Price, string SelectCompany, string SelectType, string SelectStatus)
        {

            List<Number> numbs = new NumberBL().getNumberList().ToList();
            foreach (Number nm in numbs)
            {
                if (nm.Number1 == Number)
                {
                    return RedirectToAction("AddNumber", "Admin", new { message = "Number Already Exists In Database" });

                }

            }

            Number num = new Number()
            {
                Number1 = Number,
                Price = Price,
                Company = SelectCompany,
                Type = SelectType,
                Status = SelectStatus,
                CreatedAt = DateTime.Now,
                IsActive = 1
            };

            if (new NumberBL().AddNumber(num) == false)
            {
                return RedirectToAction("AddNumber", "Admin", new { message = "All fields are Required" });
            }
            
            else
            {
               
                return RedirectToAction("AddNumber", "Admin", new { message = "Number's Information Added Succesfully" });

            }
           

        }
        #region Number
        public ActionResult ListNumbers(string message="")
        {
            if (sessionDTO.getName() == null)
            {

                return RedirectToAction("Index", "Admin");

            }
            else
            {
                List<Number> numbers = new NumberBL().getNumberList().OrderBy(x => x.Id).ToList();

                ViewBag.numbers = numbers;
                ViewBag.message = message;

                return View();
            }
            
        }

        public ActionResult DeleteNumber(int NumId)
        {
            Number num = new NumberBL().getNumberById(NumId);
            num.IsActive = 0;
            new NumberBL().UpdateNumber(num);
            return RedirectToAction("ListNumbers", "Admin");
        }
      
        public ActionResult EditNumber(int id , string message)
        {
            if (sessionDTO.getName() == null)
            {

                return RedirectToAction("Index", "Admin");

            }
            else
            {
                Number num = new NumberBL().getNumberById(id);
                ViewBag.num = num;
                ViewBag.message = message;
                return View();
            }
           
        }
        public ActionResult PostEditNumber(int id, string Number,string Price, string SelectCompany, string SelectType, string SelectStatus)
        {
            List<Number> numbs = new NumberBL().getNumberList().ToList();
            Number n = new NumberBL().getNumberById(id);
            if(n.Number1!=Number)
            {
                foreach (Number nm in numbs)
                {
                    if (nm.Number1 == Number)
                    {
                        return RedirectToAction("EditNumber", "Admin", new { id = id, message = "Number Already Exists in Database" });

                    }

                }
            }
          
                Number number = new NumberBL().getNumberById(id);

                Number num = new Number()
                {
                    Id = id,
                    Number1 = Number,
                    Price = Price,
                    Company = SelectCompany,
                    Type = SelectType,
                    Status = SelectStatus,
                    CreatedAt = DateTime.Now,
                    IsActive = 1
                };

                if (new NumberBL().UpdateNumber(num) == false)
                {
                    return RedirectToAction("EditNumber", "Admin", new { id = num.Id, message = "All fields are Required" });
                }
                else
                {

                    return RedirectToAction("ListNumbers", "Admin", new { message = "Number's Information Updated Succesfully" });

                }
            


            

        }
        #endregion

        public ActionResult DesiredNumbersList()
        {
            if (sessionDTO.getName() == null)
            {

                return RedirectToAction("Index", "Admin");

            }
            else
            {
                List<Customer> customers = new CustomerBL().getCustomerList().Where(x => x.IsActive == 1 && x.DesiredNumber != null).ToList();
                ViewBag.customers = customers;
                return View();
            }
           
        }
        public ActionResult SaleNumbersList()
        {
            if (sessionDTO.getName() == null)
            {

                return RedirectToAction("Index", "Admin");

            }
            else
            {
                List<Customer> customers = new CustomerBL().getCustomerList().Where(x => x.IsActive == 1 && x.SellingNumber != null).ToList();
                ViewBag.customers = customers;
                return View();
            }
            
        }
        public ActionResult ChangeNumberStatus(int id)
        {
            Customer customer = new CustomerBL().getCustomerById(id);
            if (customer.Status == "Contacted")
            {
                customer.Status = " Not Contacted";
            }
            else
            customer.Status = "Contacted";
            //Customer num = new Customer()
            //{
            //    Id=customer.Id,
            //    SellingNumber=customer.SellingNumber,
            //    Name=customer.Name,
            //    Phone=customer.Phone,
            //    Country=customer.Country,
            //    State=customer.State,
               
            //    CreatedAt=customer.CreatedAt,
               
            //   Status = "Contacted"
                
            //};
            new CustomerBL().UpdateCustomer(customer);

               return RedirectToAction("SaleNumbersList", "Admin");
        }

        public ActionResult ChangeDesiredNumberStatus(int id)
        {
            Customer customer = new CustomerBL().getCustomerById(id);
            if (customer.Status == "Contacted")
            {
                customer.Status = " Not Contacted";
            }
            else
                customer.Status = "Contacted";
            //Customer num = new Customer()
            //{
            //    Id=customer.Id,
            //    SellingNumber=customer.SellingNumber,
            //    Name=customer.Name,
            //    Phone=customer.Phone,
            //    Country=customer.Country,
            //    State=customer.State,

            //    CreatedAt=customer.CreatedAt,

            //   Status = "Contacted"

            //};
            new CustomerBL().UpdateCustomer(customer);

            return RedirectToAction("DesiredNumbersList", "Admin");
        }



        public ActionResult DeleteDesireNumber(int DnumId)
        {
            Customer cus = new CustomerBL().getCustomerById(DnumId);
            cus.IsActive = 0;
            new CustomerBL().UpdateCustomer(cus);
            return RedirectToAction("DesiredNumbersList", "Admin");
        }

        public ActionResult DeleteSaleNumber(int DnumId)
        {
            Customer cus = new CustomerBL().getCustomerById(DnumId);
            cus.IsActive = 0;
            new CustomerBL().UpdateCustomer(cus);
            return RedirectToAction("SaleNumbersList", "Admin");
        }

        public ActionResult SubscribersList()
        {
            if (sessionDTO.getName() == null)
            {

                return RedirectToAction("Index", "Admin");

            }
            else
            {
                List<Subscriber> subscribers = new SubscriberBL().getSubscriberList().Where(x => x.IsActive == 1).ToList();

                ViewBag.subscribers = subscribers;
                return View();
            }
           

        }

        public ActionResult DeleteSubscriber(int DnumId)
        {
            Subscriber sub = new SubscriberBL().getSubscriberById(DnumId);
            sub.IsActive = 0;
            new SubscriberBL().UpdateSubscriber(sub);
            return RedirectToAction("SubscribersList", "Admin");
        }


        public ActionResult EditProfile(int id,string message)
        {
            if (sessionDTO.getName() == null)
            {

                return RedirectToAction("Index", "Admin");

            }
            else
            {
                User us = new UserBL().getUserById(id);
                ViewBag.profile = us;
                ViewBag.message = message;
                return View();
            }

            
        }

        public ActionResult PostEditProfile(int id, string Name, string Email, string Phone, string Role, string Password, string ConfirmPassword)
        {
           
            if (Password != ConfirmPassword)
            {
                return RedirectToAction("EditProfile", "Admin", new { id = id, message = "Password and Confirm Password Doesn't Match" });
            }
            else
            {
                User us = new UserBL().getUserById(id);
                User user = new User()
                {
                    Id = id,
                    Name= Name,
                    Email=Email,
                    Phone= Phone,
                    Role=Role,
                    Password=Password,
                    CreatedAt = DateTime.Now,
                    IsActive = 1
                };
              
                if (new UserBL().UpdateUser(user) == false)
                {
                    return RedirectToAction("EditProfile", "Admin", new { id = user.Id, message = "Name , Email and Password fields are Required" });
                }
                else
                {
                    return RedirectToAction("EditProfile", "Admin", new { id=user.Id, message = "Profile Information Updated Succesfully" });

                }
            }
        }

    }
}