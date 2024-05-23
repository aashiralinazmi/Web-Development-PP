using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OOAD_PROJECT.Models;
using System.Reflection;

using System.Net;
using System.Net.Mail;
using OOAD_PROJECT.Singleton;
using OOAD_PROJECT.State_Pattern;
using OOAD_PROJECT.Strategy;
using OOAD_PROJECT.Facade_Pattern;
using OOAD_PROJECT.composite;

namespace OOAD_PROJECT.Controllers
{
    public class DashBoardController : Controller
    {
        ProjectContext db = new ProjectContext();
        Singleton.data dblayer = new Singleton.data();
        State_Pattern.context slayer = new State_Pattern.context();
        Strategy.Context dlayer;
        Facade_Pattern.detailGetter flayer = new Facade_Pattern.detailGetter();
        //composite.Stock blayer = new composite.Stock();

        // GET: DashBoard
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            //flayer.getIndex();
            //var data = db.Product.ToList();
            return View(flayer.getIndex());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult getorders()
        {
            //var data = db.orders.ToList();
            return View(flayer.getOrder());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ViewActivityLog()
        {
            var data = db.ActivityLogs.ToList();
            return View(data);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ViewLoginAll()
        {
            //composite.ActivityLog al = new composite.ActivityLog(blayer);
            //Broker b = new Broker();
            //b.placeOrders();
            var data = db.LoginAlls.ToList();
            //composite.data pf = new composite.data();
            //pf.login = data;


            return View(data);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Customers()
        {
            var data = db.Customers.ToList();
            return View(data);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            //var list = db.Category.ToList();
            //SelectList l = new SelectList(list, "Id", "CategoryName");
            //ViewBag.CategoryList = l;
            return View();
        }
        [HttpPost]
        public ActionResult Create(Products i)
        {
            if (ModelState.IsValid == true)
            {
                
                string filename = Path.GetFileNameWithoutExtension(i.ImageFile.FileName);
                string extension = Path.GetExtension(i.ImageFile.FileName);
                HttpPostedFileBase postedfile = i.ImageFile;
                int length = postedfile.ContentLength;
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                {
                    if (length <= 1000000)
                    {
                        filename = filename + extension;
                        i.Imagepath = "~/Images/" + filename;
                        filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                        i.ImageFile.SaveAs(filename);
                  
                        db.Product.Add(i);
      
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["Insert Message"] = "<script>alert('Data Inserted Successfully')</script>";
                            ModelState.Clear();
                            return RedirectToAction("Index", "DashBoard");
                        }
                        else
                        {
                            TempData["Insert Message"] = "<script>alert('Data Not Inserted')</script>";

                        }
                    }
                    else
                    {
                        TempData["Size Message"] = "<script>alert('Image size should be less than 1MB')</script>";
                    }
                }
                else
                {
                    TempData["Extension Message"] = "<script>alert('Format Not Supported')</script>";
                }
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var row = db.Product.Where(model => model.Id == id).FirstOrDefault();
            Session["Image"] = row.Imagepath;
            return View(row);
        }
        [HttpPost]
        public ActionResult Edit(Products i)
        {
            if (ModelState.IsValid == true)
            {
                if (i.ImageFile != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(i.ImageFile.FileName);
                    string extension = Path.GetExtension(i.ImageFile.FileName);
                    HttpPostedFileBase postedfile = i.ImageFile;
                    int length = postedfile.ContentLength;
                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                    {
                        if (length <= 1000000)
                        {
                            filename = filename + extension;
                            i.Imagepath = "~/Images/" + filename;
                            filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                            i.ImageFile.SaveAs(filename);
                            db.Entry(i).State = EntityState.Modified;

                            int a = db.SaveChanges();
                            if (a > 0)
                            {
                                var image_path = Request.MapPath(Session["Image"].ToString());
                                if (System.IO.File.Exists(image_path))
                                {
                                    System.IO.File.Delete(image_path);
                                }
                                TempData["Update Message"] = "<script>alert('Data Updated Successfully')</script>";
                                ModelState.Clear();
                                return RedirectToAction("Index", "DashBoard");
                            }
                            else
                            {
                                TempData["Updated Message"] = "<script>alert('Data Not Updated')</script>";

                            }
                        }
                        else
                        {
                            TempData["Size Message"] = "<script>alert('Image size should be less than 1MB')</script>";
                        }
                    }
                    else
                    {
                        TempData["Extension Message"] = "<script>alert('Format Not Supported')</script>";
                    }
                }
                else
                {
                    i.Imagepath = Session["Image"].ToString();
                    db.Entry(i).State = EntityState.Modified;

                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["Update Message"] = "<script>alert('Data Updated Successfully')</script>";
                        ModelState.Clear();
                        return RedirectToAction("Index", "DashBoard");
                    }
                    else
                    {
                        TempData["Updated Message"] = "<script>alert('Data Not Updated')</script>";

                    }
                }
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                var row = db.Product.Where(model => model.Id == id).FirstOrDefault();
                if (row != null)
                {
                    db.Entry(row).State = EntityState.Deleted;
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["Delete Message"] = "<script>alert('Data Delete Successfully')</script>";
                        var image_path = Request.MapPath(row.Imagepath.ToString());
                        if (System.IO.File.Exists(image_path))
                        {
                            System.IO.File.Delete(image_path);
                        }
                    }
                    else
                    {
                        TempData["Delete Message"] = "<script>alert('Data Not Delete')</script>";

                    }
                }
            }
            return RedirectToAction("Index", "DashBoard");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteCustomer(int id)
        {
            if (id > 0)
            {
                var row = db.Customers.Where(model => model.Customerid == id).FirstOrDefault();
                if (row != null)
                {
                    db.Entry(row).State = EntityState.Deleted;
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["Delete Message"] = "<script>alert('Data Delete Successfully')</script>";
                    }
                    else
                    {
                        TempData["Delete Message"] = "<script>alert('Data Not Delete')</script>";

                    }
                }
            }
            return RedirectToAction("Customers", "DashBoard");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            var row = db.Product.Where(model => model.Id == id).FirstOrDefault();
            Session["Image2"] = row.Imagepath.ToString();
            return View(row);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult DetailsCustomer(int id)
        {
            dlayer = new Strategy.Context(new customerdetail());
           // dlayer.executeStrategy(id);
            //var row = db.Customers.Where(model => model.Customerid == id).FirstOrDefault();
            return View(dlayer.executeStrategy(id));
        }
        [Authorize(Roles = "Admin")]
        public ActionResult DetailsUsers(int id)
        {
            //var row = db.LoginAlls.Where(model => model.Id == id).FirstOrDefault();
            dlayer = new Strategy.Context(new userdetail());
           // dlayer.executeStrategy(id);
            return View(dlayer.executeStrategy(id));
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginAll u, string returnUrl)
        {
            var dataitem = db.LoginAlls.SingleOrDefault(x => x.Username == u.Username && x.Passward == u.Passward);
            if (dataitem != null)
            {
                Session["UserId"] = dataitem.Id;
                Session["UserName"] = dataitem.Username;
                FormsAuthentication.SetAuthCookie(dataitem.Username, false);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("HomeView","Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid User/Pass");
                return View();
            }
        }
        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "DashBoard");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult getuertoconfirm()
        {
            getvieworder gtw = new getvieworder();
            //slayer.setState(gtw);
            System.Data.DataSet ds = slayer.setState(gtw);            ViewBag.Product = ds.Tables[0];
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ConfirmOrder(int id)
        {
           System.Data.DataSet ds = dblayer.getvieworder(id);           ViewBag.Product = ds.Tables[0];
            return View();
        }
        [HttpPost]
        public ActionResult ConfirmOrder(int id, int userid, int price, string payment, DateTime date)
        {
            Order o = new Order();
            var mail = db.orders.Where(m => m.UserId == userid).FirstOrDefault();
            Invoice i = new Invoice()
            {
                Id = id,
            UserId = userid,
            TotalPrice = price,
            Payment = payment,
            Date = date,
            Status = 1,
        };
            MailMessage mm = new MailMessage("Minishopbusiness123@gmail.com", mail.Email);
            mm.Subject = "Order Confirmed";
            mm.Body = "Invoice Id: "+id+
                "\n User id: "+userid+
                "\n Total Price: "+price+
                "\n Dated: "+date+
                "\n\n\n You order is confirmed and will be on its way to you in 1 day.";
            mm.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential nc = new NetworkCredential("Minishopbusiness123@gmail.com", "qnhllpmgiouezqoi");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Send(mm);
            db.Entry(i).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("getuertoconfirm");
        }
    }
}