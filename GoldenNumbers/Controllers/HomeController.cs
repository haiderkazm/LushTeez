using GoldenNumbers.BL;
using GoldenNumbers.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace GoldenNumbers.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {//Mobilink viewbags
            List<Number> JazzD = new NumberBL().getNumberList().Where(x => x.Company == "Mobilink" && x.Status == "Diamond").ToList();
            ViewBag.JazzD = JazzD;
            List<Number> JazzP = new NumberBL().getNumberList().Where(x => x.Company == "Mobilink" && x.Status == "Platinium").ToList();
            ViewBag.JazzP = JazzP;
            List<Number> JazzG = new NumberBL().getNumberList().Where(x => x.Company == "Mobilink" && x.Status == "Gold").ToList();
            ViewBag.JazzG = JazzG;

            // Telenor Viewbags
            List<Number> TelenorD = new NumberBL().getNumberList().Where(x => x.Company == "Telenor" && x.Status == "Diamond").ToList();
            ViewBag.TelenorD = TelenorD;
            List<Number> TelenorP = new NumberBL().getNumberList().Where(x => x.Company == "Telenor" && x.Status == "Platinium").ToList();
            ViewBag.TelenorP = TelenorP;
            List<Number> TelenorG = new NumberBL().getNumberList().Where(x => x.Company == "Telenor" && x.Status == "Diamond").ToList();
            ViewBag.TelenorG = TelenorG;

            //Ufone Viewbags
            List<Number> UfoneD = new NumberBL().getNumberList().Where(x => x.Company == "Ufone" && x.Status == "Diamond").ToList();
            ViewBag.UfoneD = UfoneD;
            List<Number> UfoneP = new NumberBL().getNumberList().Where(x => x.Company == "Ufone" && x.Status == "Platinium").ToList();
            ViewBag.UfoneP = UfoneP;
            List<Number> UfoneG = new NumberBL().getNumberList().Where(x => x.Company == "Ufone" && x.Status == "Gold").ToList();
            ViewBag.UfoneG = UfoneG;

            //warid viewbags
            List<Number> WaridD = new NumberBL().getNumberList().Where(x => x.Company == "Warid" && x.Status == "Diamond").ToList();
            ViewBag.WaridD = WaridD;
            List<Number> WaridP = new NumberBL().getNumberList().Where(x => x.Company == "Warid" && x.Status == "Platinium").ToList();
            ViewBag.WaridP = WaridP;
            List<Number> WaridG = new NumberBL().getNumberList().Where(x => x.Company == "Warid" && x.Status == "Gold").ToList();
            ViewBag.WaridG = WaridG;

            //Zong viewbag

            List<Number> ZongD = new NumberBL().getNumberList().Where(x => x.Company == "Zong" && x.Status == "Diamond").ToList();
            ViewBag.ZongD = ZongD;
            List<Number> ZongP = new NumberBL().getNumberList().Where(x => x.Company == "Zong" && x.Status == "Platinium").ToList();
            ViewBag.ZongP = WaridP;
            List<Number> ZongG = new NumberBL().getNumberList().Where(x => x.Company == "Zong" && x.Status == "Gold").ToList();
            ViewBag.ZongG = ZongG;


            return View();
        }

        public ActionResult About()
        {


            return View();
        }

        public ActionResult Contact(string message = "")
        {

            ViewBag.message = message;
            return View();
        }
        public ActionResult POstContact(string Name, string Email, string Subject, string Phone, string Message)
        {

            sendContactMail(Name, Email, Subject, Phone, Message);
            return RedirectToAction("Contact", "Home", new { message = "Thanks For Contacting Us" });

        }

        public bool sendContactMail(string Name, string Email, string Subject, string Phone, string Message)
        {
            MailMessage msg = new MailMessage();

            string text = "<link href='https://fonts.googleapis.com/css?family=Bree+Serif' rel='stylesheet'><style>  * {";
            text += "  font-family: 'Bree Serif', serif; }";
            text += " .list-group-item {       border: none;  }    .hor {      border-bottom: 5px solid black;   }";
            text += " .line {       margin-bottom: 20px; }";

            msg.From = new MailAddress("support@casearea.com");
            msg.To.Add("jazilsohail151@gmail.com");
            msg.Subject = Subject;
            msg.IsBodyHtml = true;
            string temp = "<html>" +
                "<head>" +
                "</head>" +
                "<body>" +
                "<nav class='navbar navbar-default'>" +
                "<div class='container-fluid'>" +
                "</div>" +
                " </nav>" +

                "<div class='text-left'>" +
                "<center>" +
                "<img alt='Company Logo' src = 'logo'>" +
                "<h1 class='text-center'>" +
                "Message From Golden NUmbers Exchange" +
                "</h1>" +
                "</center>" +
               "<h1>" +
                "Customer Details:" +
               "</h1>" +
               "<h3>Name:: " + Name + "</h3>" +
               "<br>" +

                 "<h3>Email:: " + Email + "<h3/>" +
                 "<h3>Contact Number:: " + Phone + "<h3/> " +
                 "<h3>Message:: " + Message + "<h3/> " +


               "<br>" +
                "</div>";



            msg.Body = temp;

            using (SmtpClient client = new SmtpClient())
            {
                client.EnableSsl = false;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("support@casearea.com", "delta@O27");
                client.Host = "webmail.casearea.com";
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.Send(msg);
            }
            return true;
        }
        public ActionResult TermCondition()
        {
            return View();
        }
        public ActionResult Blog()
        {

            return View();
        }
        public ActionResult DesiredNumber(string message = "")
        {


            ViewBag.message = message;
            return View();
        }
        public ActionResult PostDesiredNumber(string DesiredNumber, string Name, string Phone, string City, string State, string Country)
        {

            Customer cus = new Customer()
            {
                Name = Name,
                Phone = Phone,
                Country = Country,
                State = State,
                City = City,
                DesiredNumber = DesiredNumber,
                Status = "Not Contacted",
                CreatedAt = DateTime.Now,
                IsActive = 1
            };

            if (new CustomerBL().AddDesire(cus) == false)
            {
                return RedirectToAction("DesiredNumber", "Home", new { message = "All fields are Required" });
            }
            else
            {

                sendMail(DesiredNumber, Name, Phone, City, State, Country);
                return RedirectToAction("DesiredNumber", "Home", new { message = "Request Submitted Successfuly" });
            }


        }
        public bool sendMail(string DesiredNumber, string Name, string Phone, string City, string State, string Country)
        {
            MailMessage msg = new MailMessage();

            string text = "<link href='https://fonts.googleapis.com/css?family=Bree+Serif' rel='stylesheet'><style>  * {";
            text += "  font-family: 'Bree Serif', serif; }";
            text += " .list-group-item {       border: none;  }    .hor {      border-bottom: 5px solid black;   }";
            text += " .line {       margin-bottom: 20px; }";

            msg.From = new MailAddress("support@casearea.com");
            msg.To.Add("jazilsohail151@gmail.com");
            msg.Subject = "Request For Number";
            msg.IsBodyHtml = true;
            string temp = "<html>" +
                "<head>" +
                "</head>" +
                "<body>" +
                "<nav class='navbar navbar-default'>" +
                "<div class='container-fluid'>" +
                "</div>" +
                " </nav>" +

                "<div class='text-left'>" +
                "<center>" +
                "<img src = 'logo'>" +
                "<h1 class='text-center'>" +
                "Request For Desired Number!" +
                "</h1>" +
                "</center>" +
                "<h3 class='text-left'> " +
                "Customer want this NUmber: " + "<Span style='color:red'>" + DesiredNumber + "<Span/> " + "</h3>" +
                "<br>" +
               "<h1>" +
                "Customer Details:" +
               "</h1>" +
               "<h3>Name:: " + Name + "</h3>" +
               "<br>" +
                 "<h1>Address:: </ h1 > " + "<br> " +
                 "<h3>Country:: " + Country + "<h3/>" +
                 "<h3>State:: " + State + "<h3/> " +
                 "<h3>City:: " + City + "<h3/> " +

                 "<h3>Contact Number::" + Phone + "</h3>" +
               "<br>" +
                "</div>";



            msg.Body = temp;

            using (SmtpClient client = new SmtpClient())
            {
                client.EnableSsl = false;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("support@casearea.com", "delta@O27");
                client.Host = "webmail.casearea.com";
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.Send(msg);
            }
            return true;
        }



        public ActionResult SaleNumber(string message = "")
        {
            ViewBag.message = message;
            return View();
        }

        public ActionResult PostSaleNumber(string SellNumber, string Name, string Phone, string City, string State, string Country)
        {

            Customer cus = new Customer()
            {
                Name = Name,
                Phone = Phone,
                Country = Country,
                State = State,
                City = City,
                SellingNumber = SellNumber,
                Status = "Not Contacted",
                CreatedAt = DateTime.Now,
                IsActive = 1
            };

            if (new CustomerBL().AddSaleNumber(cus) == false)
            {
                return RedirectToAction("SaleNumber", "Home", new { message = "All fields are Required" });
            }
            else
            {
                SellNumberMail(SellNumber, Name, Phone, City, State, Country);
                return RedirectToAction("SaleNumber", "Home", new { message = "Request Submitted Successfuly" });
            }


        }


        public bool SellNumberMail(string SellNumber, string Name, string Phone, string City, string State, string Country)
        {
            MailMessage msg = new MailMessage();

            string text = "<link href='https://fonts.googleapis.com/css?family=Bree+Serif' rel='stylesheet'><style>  * {";
            text += "  font-family: 'Bree Serif', serif; }";
            text += " .list-group-item {       border: none;  }    .hor {      border-bottom: 5px solid black;   }";
            text += " .line {       margin-bottom: 20px; }";

            msg.From = new MailAddress("support@casearea.com");
            msg.To.Add("jazilsohail151@gmail.com");
            msg.Subject = "Message From Golden Numbers Exchange";
            msg.IsBodyHtml = true;
            string temp = "<html>" +
                "<head>" +
                "</head>" +
                "<body>" +
                "<nav class='navbar navbar-default'>" +
                "<div class='container-fluid'>" +
                "</div>" +
                " </nav>" +

                "<div class='text-left'>" +
                "<center>" +
                "<img src = 'logo'>" +
                "<h1 class='text-center'>" +
                "Seller's Message" +
                "</h1>" +
                "</center>" +
                "<h3 class='text-left'> " +
                "Customer want to Sell this NUmber: " + "<Span style='color:red'>" + SellNumber + "<Span/> " + "</h3>" +
                "<br>" +
               "<h1>" +
                "Customer Details:" +
               "</h1>" +
               "<h3>Name:: " + Name + "</h3>" +
               
                 "<h1>Address:: </ h1 > " + "<br> " +
                 "<h3>Country:: " + Country + "<h3/>" +
                 "<h3>State:: " + State + "<h3/> " +
                 "<h3>City:: " + City + "<h3/> " +

                 "<h3>Contact Number::" + Phone + "</h3>" +
               "<br>" +
                "</div>";



            msg.Body = temp;

            using (SmtpClient client = new SmtpClient())
            {
                client.EnableSsl = false;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("support@casearea.com", "delta@O27");
                client.Host = "webmail.casearea.com";
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.Send(msg);
            }
            return true;
        }

        public ActionResult AddSubscriber(string Email)
        {
            Subscriber sub = new Subscriber()
            {
                Email = Email,
                IsActive = 1,
                CreatedAt = DateTime.Now

            };
            if (new SubscriberBL().AddSubscriber(sub) == false)
            {

                return RedirectToAction("Index", "Home", new { messgae = "Field Required" });
            }
            else
            {

                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult BuyNumber()
        {
            #region Moblink
            List<Number> JazzR = new NumberBL().getNumberList().Where(x => x.Company == "Mobilink" && x.Type == "Regular").ToList();
            ViewBag.JazzR = JazzR;
            List<Number> JazzT = new NumberBL().getNumberList().Where(x => x.Company == "Mobilink" && x.Type == "Triple").ToList();
            ViewBag.JazzT = JazzT;
            List<Number> JazzTet = new NumberBL().getNumberList().Where(x => x.Company == "Mobilink" && x.Type == "Tetra").ToList();
            ViewBag.JazzTet = JazzTet;
            List<Number> JazzP = new NumberBL().getNumberList().Where(x => x.Company == "Mobilink" && x.Type == "Penta").ToList();
            ViewBag.JazzP = JazzP;
            List<Number> JazzH = new NumberBL().getNumberList().Where(x => x.Company == "Mobilink" && x.Type == "Hexa").ToList();
            ViewBag.JazzH = JazzH;
            List<Number> Jazz7 = new NumberBL().getNumberList().Where(x => x.Company == "Mobilink" && x.Type == "786").ToList();
            ViewBag.Jazz7 = Jazz7;
            List<Number> JazzTrP = new NumberBL().getNumberList().Where(x => x.Company == "Mobilink" && x.Type == "Triple Pair").ToList();
            ViewBag.JazzTrP = JazzTrP;
            List<Number> JazzUAN = new NumberBL().getNumberList().Where(x => x.Company == "Mobilink" && x.Type == "UAN").ToList();
            ViewBag.JazzUAN = JazzUAN;

            #endregion
            #region Telenor
            List<Number> TelenorR = new NumberBL().getNumberList().Where(x => x.Company == "Telenor" && x.Type == "Regular").ToList();
            ViewBag.TelenorR = TelenorR;
            List<Number> TelenorT = new NumberBL().getNumberList().Where(x => x.Company == "Telenor" && x.Type == "Triple").ToList();
            ViewBag.TelenorT = TelenorT;
            List<Number> TelenorTet = new NumberBL().getNumberList().Where(x => x.Company == "Telenor" && x.Type == "Tetra").ToList();
            ViewBag.TelenorTet = TelenorTet;
            List<Number> TelenorP = new NumberBL().getNumberList().Where(x => x.Company == "Telenor" && x.Type == "Penta").ToList();
            ViewBag.TelenorP = TelenorP;
            List<Number> TelenorH = new NumberBL().getNumberList().Where(x => x.Company == "Telenor" && x.Type == "Hexa").ToList();
            ViewBag.TelenorH = TelenorH;
            List<Number> Telenor7 = new NumberBL().getNumberList().Where(x => x.Company == "Telenor" && x.Type == "786").ToList();
            ViewBag.Telenor7 = Telenor7;
            List<Number> TelenorTrP = new NumberBL().getNumberList().Where(x => x.Company == "Telenor" && x.Type == "Triple Pair").ToList();
            ViewBag.TelenorTrP = TelenorTrP;
            List<Number> TelenorUAN = new NumberBL().getNumberList().Where(x => x.Company == "Telenor" && x.Type == "UAN").ToList();
            ViewBag.TelenorUAN = TelenorUAN;

            #endregion

            #region Ufone
            List<Number> UfoneR = new NumberBL().getNumberList().Where(x => x.Company == "Ufone" && x.Type == "Regular").ToList();
            ViewBag.UfoneR = UfoneR;
            List<Number> UfoneT = new NumberBL().getNumberList().Where(x => x.Company == "Ufone" && x.Type == "Triple").ToList();
            ViewBag.UfoneT = UfoneT;
            List<Number> UfoneTet = new NumberBL().getNumberList().Where(x => x.Company == "Ufone" && x.Type == "Tetra").ToList();
            ViewBag.UfoneTet = UfoneTet;
            List<Number> UfoneP = new NumberBL().getNumberList().Where(x => x.Company == "Ufone" && x.Type == "Penta").ToList();
            ViewBag.UfoneP = UfoneP;
            List<Number> UfoneH = new NumberBL().getNumberList().Where(x => x.Company == "Ufone" && x.Type == "Hexa").ToList();
            ViewBag.UfoneH = UfoneH;
            List<Number> Ufone7 = new NumberBL().getNumberList().Where(x => x.Company == "Ufone" && x.Type == "786").ToList();
            ViewBag.Ufone7 = Ufone7;
            List<Number> UfoneTrP = new NumberBL().getNumberList().Where(x => x.Company == "Ufone" && x.Type == "Triple Pair").ToList();
            ViewBag.UfoneTrP = UfoneTrP;
            List<Number> UfoneUAN = new NumberBL().getNumberList().Where(x => x.Company == "Ufone" && x.Type == "UAN").ToList();
            ViewBag.UfoneUAN = UfoneUAN;
            #endregion

            #region Warid

            List<Number> WaridR = new NumberBL().getNumberList().Where(x => x.Company == "Warid" && x.Type == "Regular").ToList();
            ViewBag.WaridR = WaridR;
            List<Number> WaridT = new NumberBL().getNumberList().Where(x => x.Company == "Warid" && x.Type == "Triple").ToList();
            ViewBag.WaridT = WaridT;
            List<Number> WaridTet = new NumberBL().getNumberList().Where(x => x.Company == "Warid" && x.Type == "Tetra").ToList();
            ViewBag.WaridTet = WaridTet;
            List<Number> WaridP = new NumberBL().getNumberList().Where(x => x.Company == "Warid" && x.Type == "Penta").ToList();
            ViewBag.WaridP = WaridP;
            List<Number> WaridH = new NumberBL().getNumberList().Where(x => x.Company == "Warid" && x.Type == "Hexa").ToList();
            ViewBag.WaridH = WaridH;
            List<Number> Warid7 = new NumberBL().getNumberList().Where(x => x.Company == "Warid" && x.Type == "786").ToList();
            ViewBag.Warid7 = Warid7;
            List<Number> WaridTrP = new NumberBL().getNumberList().Where(x => x.Company == "Warid" && x.Type == "Triple Pair").ToList();
            ViewBag.WaridTrP = WaridTrP;
            List<Number> WaridUAN = new NumberBL().getNumberList().Where(x => x.Company == "Warid" && x.Type == "UAN").ToList();
            ViewBag.WaridUAN = WaridUAN;
            #endregion

            #region Zong
            List<Number> ZongR = new NumberBL().getNumberList().Where(x => x.Company == "Zong" && x.Type == "Regular").ToList();
            ViewBag.ZongR = ZongR;
            List<Number> ZongT = new NumberBL().getNumberList().Where(x => x.Company == "Zong" && x.Type == "Triple").ToList();
            ViewBag.ZongT = ZongT;
            List<Number> ZongTet = new NumberBL().getNumberList().Where(x => x.Company == "Zong" && x.Type == "Tetra").ToList();
            ViewBag.ZongTet = ZongTet;
            List<Number> ZongP = new NumberBL().getNumberList().Where(x => x.Company == "Zong" && x.Type == "Penta").ToList();
            ViewBag.ZongP = ZongP;
            List<Number> ZongH = new NumberBL().getNumberList().Where(x => x.Company == "Zong" && x.Type == "Hexa").ToList();
            ViewBag.ZongH = ZongH;
            List<Number> Zong7 = new NumberBL().getNumberList().Where(x => x.Company == "Zong" && x.Type == "786").ToList();
            ViewBag.Zong7 = Zong7;
            List<Number> ZongTrP = new NumberBL().getNumberList().Where(x => x.Company == "Zong" && x.Type == "Triple Pair").ToList();
            ViewBag.ZongTrP = ZongTrP;
            List<Number> ZongUAN = new NumberBL().getNumberList().Where(x => x.Company == "Zong" && x.Type == "UAN").ToList();
            ViewBag.ZongUAN = ZongUAN;

            #endregion
            return View();


        }
    }
}