using Lab21.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab21.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            CoffeeShopDBEntities1 ORM = new CoffeeShopDBEntities1();

            ViewBag.ItemList = ORM.Items.ToList();
            ViewBag.Message = "GC Coffee Products";

            return View();
        } 

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
        public ActionResult Register()
        {

            return View();
        }
        public ActionResult AddNewUser(User newuser)
        {
            CoffeeShopDBEntities1 ORM = new CoffeeShopDBEntities1();

            //Validation
            if (ModelState.IsValid)
                {
              
                ORM.Users.Add(newuser);
                ORM.Entry(newuser).CurrentValues["SignUpDate"] = DateTime.Now;
                ORM.SaveChanges();
                ViewBag.Message = $"Thank you for registering {newuser.FirstName}";
                return View("confirm");
                
                }
            else
                {
                ViewBag.Address = Request.UserHostAddress;
                return View("Error");
                
                }
        }
     
        public ActionResult ShowItemDetails(string ItemName)
        {
            //1 ORM
            CoffeeShopDBEntities1 ORM = new CoffeeShopDBEntities1();

            //2 Locate an item
            Item Found = ORM.Items.Find(ItemName);
            //3 Show Item
            if (Found != null)
            {
                return View(Found);//return view as model
                //or
                //Viewbag.Found = Found;
                //return View;
            }
            else
            {
                ViewBag.ErrorMessage = "Item not Found";
                return View("Error");
            }
        }
        public ActionResult SearchByItemName(string itemname)
        {
            //1 create the ORM
            CoffeeShopDBEntities1 ORM = new CoffeeShopDBEntities1();

            ViewBag.ItemList = ORM.Items.Where(c => c.ItemName.Contains(itemname)).ToList();

            return View("SearchByItemName");
        }
    }
}