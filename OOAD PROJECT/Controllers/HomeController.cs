using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OOAD_PROJECT.Models;
using System.Data;
using System.Net;
using System.Net.Mail;
using OOAD_PROJECT.DataBaseAccessLayer;

namespace OOAD_PROJECT.Controllers
{


    public class HomeController : Controller
    {
        ProjectContext db = new ProjectContext();
        List<Cart> li = new List<Cart>();
        List<Invoice> li1 = new List<Invoice>();
        DataBaseAccessLayer.dataFactory dblayer = new DataBaseAccessLayer.dataFactory();


    
        // GET: Home
        public ActionResult HomeView()
        {
            if(TempData["cart"] != null)
            {
                int x = 0;

                List<Cart> li2 = TempData["cart"] as List<Cart>;
                foreach (var item in li2)
                {
                    x += item.Subtotal;
                }
                TempData["Total"] = x;
                TempData["Item_Count"] = li2.Count();
            }
            TempData.Keep();
            data products = dblayer.getData("products");
            DataSet ds = products.Data();
            ViewBag.Product = ds.Tables[0];
            return View();
        }
        public ActionResult AboutView()
        {
            return View();
        }
        public ActionResult ContactView()
        {
            return View();
        }
        public ActionResult OrderDetails(int? id)
        {
            var query = db.invoices.Where(m => m.UserId == id).ToList();
            return View(query);
        }
        public ActionResult MenView()
        {
            data products = dblayer.getData("men");
            DataSet ds = products.Data();
            ViewBag.Product = ds.Tables[0];
            return View();
        }
        public ActionResult WomenView()
        {
            data products = dblayer.getData("women");
            DataSet ds = products.Data();
            ViewBag.Product = ds.Tables[0];
            return View();
        }
        public ActionResult Detailssingleproductview(int id)
        {
               var row = db.Product.Where(model => model.Id == id).FirstOrDefault();
            Session["Image2"] = row.Imagepath.ToString();
            return View(row);
        }
        [HttpPost]
        public ActionResult Detailssingleproductview(int id,int qty)
        {
            Products p = db.Product.Where(model => model.Id == id).FirstOrDefault();
            Cart c = new Cart();
            c.ProductId = p.Id;
            c.ProductName = p.ProductName;
            c.SingleProductPrice = Convert.ToInt32(p.Price);
            c.Qunatity = Convert.ToInt32(qty);
            c.Subtotal = p.Price * qty;
            if(TempData["cart"] == null)
            {
                li.Add(c);
                TempData["cart"] = li;
            }
            else
            {
                List<Cart> li2 = TempData["cart"] as List<Cart>;
                int flag = 0;
                foreach (var item in li2)
                {
                    if(item.ProductId == c.ProductId)
                    {
                        item.Qunatity += c.Qunatity;
                        item.Subtotal += c.Subtotal;
                        flag = 1;
                    }
                }
                if(flag == 0)
                {
                    li2.Add(c);
                }
                TempData["cart"] = li2;
            }
            TempData.Keep();
            return RedirectToAction("HomeView");
        }

        public ActionResult remove(int? id)
        {
            if(TempData["cart"] == null)
            {
                TempData.Remove("Total");
                TempData.Remove("cart");
            }
            else
            {
                List<Cart> li2 = TempData["cart"] as List<Cart>;
                Cart c = li2.Where(x => x.ProductId == id).SingleOrDefault();
                li2.Remove(c);
                int s = 0;
                foreach(var item in li2)
                {
                    s += item.Subtotal;
                }
                TempData["Total"] = s;
            }
            return RedirectToAction("HomeView");
        }
        public ActionResult Checkout()
        {
            TempData.Keep();
            return View();
        }
        [HttpPost]
        public ActionResult Checkout(long ContactNo, string Address,string Email,string Note)
        {
            if(ModelState.IsValid)
            {
                Order o = new Order();
                List<Cart> li2 = TempData["cart"] as List<Cart>;
                Invoice i = new Invoice();
                i.UserId = Convert.ToInt32(Session["UserId"].ToString());
                i.Date = System.DateTime.Now;
                i.TotalPrice = (int)TempData["Total"];
                i.Payment = "Cash";
                db.invoices.Add(i);
                db.SaveChanges();
                //OrderBook
                foreach (var item in li2)
                {
                    o.ProductId = item.ProductId;
                    o.ContactNo = ContactNo;
                    o.Address = Address;
                    o.Email = Email;
                    o.Note = Note;
                    o.Date = System.DateTime.Now;
                    o.invoiceid = i.Id;
                    o.UserId = i.UserId;
                    o.Quantity = item.Qunatity;
                    o.SingleProductPrice = item.SingleProductPrice;
                    o.Subtotal = item.Subtotal;

                    MailMessage mm = new MailMessage("Minishopbusiness123@gmail.com", o.Email);
                    mm.Subject = "Order Placed";
                    mm.Body = "Product Id: "+o.ProductId+
                        "\n Contact No: "+o.ContactNo+
                        "\n Address"+o.Address+
                        "\n Email: "+o.Email+
                        "\n Note: "+o.Note+
                        "\n Date: "+o.Date+
                        "\n Invoice Id"+o.invoiceid+
                        "\n UserId: "+o.UserId+
                        "\n Quantity: "+o.Quantity+
                        "\n Single Product Price: "+o.SingleProductPrice+
                        "\n SubTotal: "+o.Subtotal+
                        "\n\n\n Your Order has been placed confirmation email will be send to you soon.";
                    mm.IsBodyHtml = false;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;

                    NetworkCredential nc = new NetworkCredential("Minishopbusiness123@gmail.com", "qnhllpmgiouezqoi");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = nc;
                    smtp.Send(mm);

                    db.orders.Add(o);
                    db.SaveChanges();
                   
                    
                }
                
                TempData.Remove("Total");
                TempData.Remove("cart");
                TempData["Delete Message"] = "<script>alert('Order PLaced Successfully')</script>";
                return RedirectToAction("HomeView");
            }
            TempData.Keep();
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: CUSTOMERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Customerid,Customername,UserName,CustomerE_Mail,CustomerPhoneNo,Passward,Address,City,Country")] Customer cUSTOMER)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(cUSTOMER);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    MailMessage mm = new MailMessage("Minishopbusiness123@gmail.com", cUSTOMER.CustomerE_Mail);
                    mm.Subject = "User Registered";
                    mm.Body = "Customer id: " + cUSTOMER.Customerid +
                        "\n Customer name: " + cUSTOMER.Customername +
                        "\n UserName: " + cUSTOMER.UserName +
                        "\n Customer E_Mail: " + cUSTOMER.CustomerE_Mail +
                        "\n Customer PhoneNo: " + cUSTOMER.CustomerPhoneNo +
                        "\n Passward: " + cUSTOMER.Passward +
                        "\n Address: " + cUSTOMER.Address +
                        "\n City: " + cUSTOMER.City +
                        "\n Country: " + cUSTOMER.Country +
                        "\n\n\n This is to tell you that you have been registered as MiniShop's Customer.";
                    mm.IsBodyHtml = false;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;

                    NetworkCredential nc = new NetworkCredential("Minishopbusiness123@gmail.com", "qnhllpmgiouezqoi");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = nc;
                    smtp.Send(mm);
                    TempData["Insert Message"] = "<script>alert('Successfully Signed Up')</script>";
                    //ModelState.Clear();
                    return RedirectToAction("HomeView", "Home");
                }
                else
                {
                    TempData["Insert Message"] = "<script>alert('Unsuccesfull SignUp Try Again!')</script>";

                }
            }

            return View(cUSTOMER);
        }
    }
}